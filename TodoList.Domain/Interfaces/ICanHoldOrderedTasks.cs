using TodoList.Domain.Tasks.ForOrders;

namespace TodoList.Domain.Interfaces
{
    public interface ICanHoldOrderedTasks : ICanHoldTasks
    {
        IReadOnlyList<OrderedTaskId> OrderedTaskIdList { get; }

        void ReorderTask(string reorderTaskId, string? previousTaskId, string? nextTaskId);
    }
}
