namespace TodoList.Domain.Tasks.ForOrders
{
    public class OrderedTaskMap
    {
        public BasicTask CurrentTask { get; private set; }
        public BasicTask NextTask { get; private set; }

        public OrderedTaskMap(BasicTask currentTask, BasicTask nextTask)
        {
            CurrentTask = currentTask;
            NextTask = nextTask;
        }
    }
}
