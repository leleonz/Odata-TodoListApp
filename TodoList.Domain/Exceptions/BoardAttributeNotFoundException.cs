namespace TodoList.Domain.Exceptions
{
    public class BoardAttributeNotFoundException : Exception
    {
        public BoardAttributeNotFoundException() : base()
        {

        }

        public BoardAttributeNotFoundException(string message) : base(message)
        {

        }

        public BoardAttributeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
