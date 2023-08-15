using TodoList.Domain.Tasks;

namespace TodoList.Domain.Interfaces
{
    public interface ICanHoldTasks
    {
        IReadOnlyList<BasicTask> ChildTasks { get; }

        void AddTask(BasicTask task);
        void RemoveTask(BasicTask task);
        void RemoveTask(string taskId);
    }
}
