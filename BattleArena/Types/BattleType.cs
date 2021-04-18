using BattleArena.Application.Domain.Models;
using BattleArena.Application.Services.Interfaces;
using HotChocolate.Types;
using System.Threading.Tasks;

namespace BattleArena.Types
{
    public class BattleType : ObjectType<Battle>
    {
        protected override void Configure(IObjectTypeDescriptor<Battle> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(u => u.BattleId)
                .ResolveNode((ctx, id) =>
                    Task<Battle>.Factory.StartNew(() =>
                    {
                        return ctx.Service<IBattleService>().Get(id);
                    }, ctx.RequestAborted));

        }
    }
}
