using HotChocolate;

namespace BattleArena.General
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception is RequestException)
            {
                var ex = error.Exception as RequestException;
                return error.WithCode(ex.Code).WithMessage(ex.Message).WithMessage(ex.StackTrace);
            }
            return error;
        }
    }
}
