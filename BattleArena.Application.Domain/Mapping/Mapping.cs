using BattleArena.Application.Domain.Models;
using BattleArena.Core.PostgreSQL.Models;
using System;
using System.Linq;

namespace BattleArena.Application.Domain.Mapping
{
    public static class Mapping
    {
        public static User MapUser(DbUser dbUser)
        {
            if (dbUser == null)
            {
                return null;
            }

            Guid.TryParse(dbUser.Id, out Guid resultId);
            return new User
            {
                Id = resultId,
                Username = dbUser.UserName,
                Experience = dbUser.Experience,
                Balance = dbUser.Balance,
                Energe = dbUser.Energe,
                Battles = dbUser.Battles.Select(dbBattle => MapBattle(dbBattle)).ToList()
            };
        }

        public static Battle MapBattle(DbBattle dbBattle)
        {
            if (dbBattle == null)
            {
                return null;
            }

            return new Battle
            {
                BattleId = dbBattle.Id,
                Results = dbBattle.Results.Select(res => MapResult(res)).ToList(),
                Task = MapTask(dbBattle.Task),
                Status = dbBattle.Status,
                EndDate = dbBattle.EndDate
            };
        }

        public static Task MapTask(DbTask dbTask)
        {
            if (dbTask == null)
            {
                return null;
            }

            return new Task
            {
                TaskId = dbTask.Id,
                Questions = dbTask.Questions.Select(dbQuestion => MapQuestion(dbQuestion)).ToList()
            };
        }

        public static Question MapQuestion(DbQuestion dbQuestion)
        {
            if (dbQuestion == null)
            {
                return null;
            }

            return new Question
            {
                Id = dbQuestion.Id,
                Description = dbQuestion.Description,
                Answer = dbQuestion.Answer
            };
        }

        public static Result MapResult(DbResult dbResult)
        {
            if (dbResult == null)
            {
                return null;
            }

            return new Result
            {
                Id = dbResult.Id,
                BattleId = dbResult.Battle.Id,
                UserId = dbResult.UserId,
                QuestionId = dbResult.QuestionId,
                Res = dbResult.Result
            };
        }
    }
}
