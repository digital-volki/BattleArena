using System.Collections.Generic;

namespace BattleArena.General
{
    public class PayloadBase<T> : Payload
    {
        public PayloadBase(T value)
        {
            Value = value;
        }

        public PayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public T Value { get; }
    }
}
