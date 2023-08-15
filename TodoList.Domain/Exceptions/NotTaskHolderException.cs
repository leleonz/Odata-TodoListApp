namespace TodoList.Domain.Exceptions
{
    public class NotTaskHolderException : Exception
    {
        public NotTaskHolderException() : base()
        {

        }

        public NotTaskHolderException(string message) : base(message)
        {

        }

        public NotTaskHolderException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
