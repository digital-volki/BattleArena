using BattleArena.Application.Domain.Models;
using BattleArena.Application.Services.Interfaces;
using HotChocolate.Types;
using System.Threading.Tasks;

namespace BattleArena.Types
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(u => u.Id)
                .ResolveNode((ctx, id) =>
                    Task<User>.Factory.StartNew(() =>
                    {
                        return ctx.Service<IUserService>().GetUser(id);
                    }, ctx.RequestAborted));

        }
    }
}
