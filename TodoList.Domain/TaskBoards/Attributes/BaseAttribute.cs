namespace TodoList.Domain.TaskBoards.Attributes
{
    public abstract class BaseAttribute
    {
        public long Id { get; private set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public BaseAttribute(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }
    }
}
