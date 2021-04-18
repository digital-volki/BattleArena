using BattleArena.Application.Domain.Models;
using BattleArena.Application.Services.Interfaces;
using BattleArena.General;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;

namespace BattleArena.Mutations
{
    [ExtendObjectType("Mutation")]
    public class UserMutations
    {
        public PayloadBase<User> GenerateUser(
            [Service] IUserService userService)
        {
            List<UserError> errors = new List<UserError>();

            User user = userService.GenerateUser();

            if (user == null)
            {
                errors.Add(new UserError
                (
                    code: "500",
                    message: "Failed to create."
                ));
                return new PayloadBase<User>(errors);
            }

            return new PayloadBase<User>(user);
        }
    }
}
