namespace TodoList.Domain.Exceptions
{
    public  class TaskBoardNullException : Exception
    {
        public TaskBoardNullException() : base()
        {

        }

        public TaskBoardNullException(string message) : base(message)
        {

        }

        public TaskBoardNullException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
