using HotChocolate.Execution;

namespace BattleArena.General
{
    public class RequestException : QueryException
    {
        public string Code { get; }
        public RequestException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}
