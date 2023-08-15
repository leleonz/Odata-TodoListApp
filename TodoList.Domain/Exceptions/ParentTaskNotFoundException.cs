namespace TodoList.Domain.Exceptions
{
    public class ParentTaskNotFoundException : Exception
    {
        public ParentTaskNotFoundException() : base()
        {

        }

        public ParentTaskNotFoundException(string message) : base(message)
        {

        }

        public ParentTaskNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
