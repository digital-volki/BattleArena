using BattleArena.Application.Domain.Models;
using BattleArena.Application.Services.Interfaces;
using BattleArena.Core.PostgreSQL.Models.Enums;
using BattleArena.General;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleArena.Queries
{
    [ExtendObjectType("Query")]
    public class BattleQueries
    {
        [UsePaging]
        public IQueryable<Battle> GetBattles(
            [Service] IUserService userService,
            [ID(nameof(User))] Guid userId,
            BattleStatus? battleStatus,
            Affiliation? affiliation)
        {
            User user = userService.GetUser(userId);
            if (user == null)
            {
                return new List<Battle>().AsQueryable();
            }

            return (battleStatus switch
                {
                    BattleStatus.Completed => user.Battles.Where(b => b.Status == BattleStatus.Completed).ToList(),
                    BattleStatus.ReceivingAwaiting => user.Battles.Where
                    (
                        b => b.Status == BattleStatus.ReceivingAwaiting && (
                            b.Caller.Id == userId ? Affiliation.FromMe : Affiliation.ToMe) == affiliation).ToList(),
                    _ => user.Battles
                }).AsQueryable();
        }
    }
}
