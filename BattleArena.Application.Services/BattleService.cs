using BattleArena.Application.Domain.Interfaces;
using BattleArena.Application.Domain.Mapping;
using BattleArena.Application.Domain.Models;
using BattleArena.Application.Services.Interfaces;
using BattleArena.Core.PostgreSQL.Models;
using BattleArena.Core.PostgreSQL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleArena.Application.Services
{
    public class BattleService : IBattleService
    {
        private readonly IBattleDomain _battleDomain;
        private readonly ITaskDomain _taskDomain;
        private readonly IUserDomain _userDomain;
        private readonly IUserService _userService;

        public BattleService(
            IBattleDomain battleDomain,
            ITaskDomain taskDomain,
            IUserDomain userDomain,
            IUserService userService)
        {
            _battleDomain = battleDomain;
            _taskDomain = taskDomain;
            _userDomain = userDomain;
            _userService = userService;
        }

        public Battle CheckAnswers(Guid battleId, Guid userId, Dictionary<Guid, string> questionsAnswers)
        {
            DbBattle dbBattle = _battleDomain.Get(battleId);

            if (dbBattle == null)
            {
                return null;
            }

            if (dbBattle.Caller != userId && dbBattle.Receiver != userId)
            {
                return null;
            }

            List<DbResult> newResults = new List<DbResult>();

            foreach (var question in dbBattle.Task.Questions)
            {
                newResults.Add(
                    new DbResult
                    {
                        Id = Guid.NewGuid(),
                        Battle = dbBattle,
                        UserId = userId,
                        QuestionId = question.Id,
                        Result = questionsAnswers[question.Id] == question.Answer
                    });
            }

            dbBattle.Results = _taskDomain.AddResult(newResults);

            Affiliation affiliation = dbBattle.Caller == userId ? Affiliation.FromMe : Affiliation.ToMe;

            switch (affiliation)
            {
                case Affiliation.FromMe:
                    dbBattle.Status = BattleStatus.ReceivingAwaiting;
                    break;
                case Affiliation.ToMe:
                    dbBattle.EndDate = DateTime.Now;
                    dbBattle.Status = BattleStatus.Completed;
                    AccrueAwards(dbBattle);
                    break;
            }

            return Mapping.MapBattle(_battleDomain.Update(dbBattle));
        }

        public Battle Create(Guid userId, Guid opponentId)
        {
            DbTask dbTask = _taskDomain.GetRandom();
            if (dbTask == null)
            {
                return null;
            }

            DbBattle dbBattle = new DbBattle
            {
                Id = Guid.NewGuid(),
                Caller = userId,
                Receiver = opponentId,
                Task = dbTask,
                Status = BattleStatus.Created
            };

            return Mapping.MapBattle(_battleDomain.Create(dbBattle));
        }

        public Battle Get(Guid battleId)
        {
            DbBattle dbBattle = _battleDomain.Get(battleId);
            Battle battle = Mapping.MapBattle(dbBattle);
            battle.Caller = _userService.GetUser(dbBattle.Caller);
            battle.Receiver = _userService.GetUser(dbBattle.Receiver);

            return battle;
        }

        public Battle Start(Guid battleId, Guid userId)
        {
            DbBattle dbBattle = _battleDomain.Get(battleId);

            if (dbBattle == null)
            {
                return null;
            }

            if (dbBattle.Caller != userId && dbBattle.Receiver != userId)
            {
                return null;
            }

            Affiliation affiliation = dbBattle.Caller == userId ? Affiliation.FromMe : Affiliation.ToMe;

            switch (affiliation)
            {
                case Affiliation.FromMe:
                    dbBattle.Status = BattleStatus.CallerProcessing;
                    break;
                case Affiliation.ToMe:
                    dbBattle.Status = BattleStatus.ReceiverProcessing;
                    break;
            }

            return Mapping.MapBattle(_battleDomain.Update(dbBattle));
        }

        private void AccrueAwards(DbBattle dbBattle)
        {
            IOrderedEnumerable<IGrouping<Guid, DbResult>> res = dbBattle.Results.GroupBy(r => r.UserId).OrderByDescending(g => g.Count());
            DbUser winner = _userDomain.Get(res.First().Key);
            DbUser loser = _userDomain.Get(res.Last().Key);

            Tuple<int, int> winnerAwards = Consts.Awards[League.Transistor.Convert(winner.Experience)];

            winner.Balance += winnerAwards.Item1;
            winner.Experience += winnerAwards.Item2;

            Tuple<int, int> loserAwards = Consts.Awards[League.Transistor.Convert(loser.Experience)];

            loser.Balance += (int)(loserAwards.Item1 * Consts.LoserPercent);
            loser.Experience += (int)(loserAwards.Item2 * Consts.LoserPercent);

            _userDomain.Update(winner);
            _userDomain.Update(loser);
        }
    }
}
