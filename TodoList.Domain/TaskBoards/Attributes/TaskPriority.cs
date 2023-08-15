namespace TodoList.Domain.TaskBoards.Attributes
{
    public class TaskPriority : BaseAttribute
    {
        public TaskPriority(string name, string? description = null) : base(name, description)
        {
        }
    }
}
