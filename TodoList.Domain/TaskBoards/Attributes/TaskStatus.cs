namespace TodoList.Domain.TaskBoards.Attributes
{
    public class TaskStatus : BaseAttribute
    {
        public TaskStatus(string name, string? description = null) : base(name, description)
        {
        }
    }
}
