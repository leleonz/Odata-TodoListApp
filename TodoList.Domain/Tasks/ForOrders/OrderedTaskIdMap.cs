namespace TodoList.Domain.Tasks.ForOrders
{
    public class OrderedTaskIdMap
    {
        public string CurrentTaskId { get; private set; }
        public string? NextTaskId { get; set; }

        public OrderedTaskIdMap(string currentTaskId, string? nextTaskId)
        {
            CurrentTaskId = currentTaskId;
            NextTaskId = nextTaskId;
        }
    }
}
