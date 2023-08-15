namespace TodoList.Domain.Tasks.ForOrders
{
    public class OrderedTaskId
    {
        public int SequenceNumber { get; private set; }
        public string TaskId { get; private set; }

        public OrderedTaskId(int sequenceNumber, string task)
        {
            SequenceNumber = sequenceNumber;
            TaskId = task;
        }
    }
}
