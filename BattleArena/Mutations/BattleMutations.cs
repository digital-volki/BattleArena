using BattleArena.Application.Domain.Models;
using BattleArena.Application.Services.Interfaces;
using BattleArena.General;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;

namespace BattleArena.Mutations
{
    [ExtendObjectType("Mutation")]
    public class BattleMutations
    {
        public PayloadBase<Battle> CallBattle(
            [Service] IBattleService battleService,
            [ID(nameof(User))] Guid userId,
            [ID(nameof(User))] Guid opponentId)
        {
            List<UserError> errors = new List<UserError>();

            Battle battle = battleService.Create(userId, opponentId);

            if (battle == null)
            {
                errors.Add(new UserError
                (
                    code: "500",
                    message: "Failed to create."
                ));
                return new PayloadBase<Battle>(errors);
            }

            return new PayloadBase<Battle>(battle);
        }

        public PayloadBase<Battle> StartBattle(
            [Service] IBattleService battleService,
            [ID(nameof(Battle))] Guid battleId,
            [ID(nameof(User))] Guid userId)
        {
            List<UserError> errors = new List<UserError>();

            Battle battle = battleService.Start(battleId, userId);

            if (battle == null)
            {
                errors.Add(new UserError
                (
                    code: "500",
                    message: "Failed to change status."
                ));
                return new PayloadBase<Battle>(errors);
            }

            return new PayloadBase<Battle>(battle);
        }

        public PayloadBase<Battle> CheckAnswers(
            [Service] IBattleService battleService,
            [ID(nameof(Battle))] Guid battleId,
            [ID(nameof(User))] Guid userId,
            Dictionary<Guid, string> questionsAnswers)
        {
            List<UserError> errors = new List<UserError>();

            Battle battle = battleService.CheckAnswers(battleId, userId, questionsAnswers);

            if (battle == null)
            {
                errors.Add(new UserError
                (
                    code: "500",
                    message: "Failed to check answers."
                ));
                return new PayloadBase<Battle>(errors);
            }

            return new PayloadBase<Battle>(battle);
        }
    }
}
