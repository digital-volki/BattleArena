using System.Collections.Generic;

namespace BattleArena.General
{
    public abstract class Payload
    {
#nullable enable
        protected Payload(IReadOnlyList<UserError>? errors = null)
        {
            Errors = errors;
        }

        public IReadOnlyList<UserError>? Errors { get; }
#nullable enable
    }
}
