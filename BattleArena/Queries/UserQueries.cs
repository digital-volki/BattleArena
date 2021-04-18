using BattleArena.Application;
using BattleArena.Application.Domain.Models;
using BattleArena.Application.Services.Interfaces;
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
    public class UserQueries
    {
        public PayloadBase<User> GetUser(
            [Service] IUserService userService,
            [ID(nameof(User))] Guid userId)
        {
            List<UserError> errors = new List<UserError>();

            User user = userService.GetUser(userId);

            if (user == null)
            {
                errors.Add(new UserError
                (
                    code: "500",
                    message: "Authorize users returns is null."
                ));
                return new PayloadBase<User>(errors);
            }

            return new PayloadBase<User>(user);
        }

        [UsePaging]
        public IQueryable<User> GetAllUsers(
            [Service] IUserService userService,
            [ID(nameof(User))] Guid userId)
        {
            User user = userService.GetUser(userId);

            if (user == null)
            {
                return new List<User>().AsQueryable();
            }

            return userService.GetUsers(user.League) ?? new List<User>().AsQueryable();
        }

        [UsePaging]
        public IQueryable<User> GetRating(
            [Service] IUserService userService,
            League league) => userService.GetUsers(league) ?? new List<User>().AsQueryable();





        public List<LeagueMetadata> GetLeagueMetadatas()
        {
            List<LeagueMetadata> metadatas = new List<LeagueMetadata>();

            foreach (League league in Enum.GetValues(typeof(League)))
            {
                metadatas.Add(new LeagueMetadata
                {
                    League = league,
                    ExperienceForUp = Consts.LeagueExperience[league],
                    WinMoney = Consts.Awards[league].Item1,
                    WinExperience = Consts.Awards[league].Item2
                });
            }

            return metadatas;
        }
    }
}
