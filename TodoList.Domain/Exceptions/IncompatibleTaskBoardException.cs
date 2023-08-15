namespace TodoList.Domain.Exceptions
{
    public class IncompatibleTaskBoardException : Exception
    {
        public IncompatibleTaskBoardException() : base()
        {

        }

        public IncompatibleTaskBoardException(string message) : base(message)
        {

        }

        public IncompatibleTaskBoardException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
