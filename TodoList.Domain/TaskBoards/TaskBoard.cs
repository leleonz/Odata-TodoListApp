using TodoList.Domain.Interfaces;
using TodoList.Domain.TaskBoards.Attributes;
using TodoList.Domain.Tasks;
using TodoList.Domain.Tasks.ForOrders;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.Domain.TaskBoards
{
    public abstract class TaskBoard : ICanHoldOrderedTasks
    {
        public long Id { get; private set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<TaskStatus> AvailableStatuses { get; private set; } = new List<TaskStatus>();
        public List<TaskPriority> AvailablePriorities { get; private set; } = new List<TaskPriority>();
        public IReadOnlyList<string> AvailableLabels => _childTasks.SelectMany(p => p.Labels).Distinct().ToList().AsReadOnly();

        protected List<BasicTask> _childTasks = new List<BasicTask>();
        public IReadOnlyList<BasicTask> ChildTasks => _childTasks.AsReadOnly();

        public abstract IReadOnlyList<OrderedTaskId> OrderedTaskIdList { get; }

        protected TaskBoard() { }

        public TaskBoard(string? name, string? description, bool isNew = false)
        {
            Name = name;
            Description = description;

            if (isNew)
            {
                SetupDefaultBoard();
            }
        }

        public virtual void SetupDefaultBoard()
        {
            AvailableStatuses.Clear();
            AvailableStatuses.Add(new TaskStatus(DefaultAttributes.NotStarted));
            AvailableStatuses.Add(new TaskStatus(DefaultAttributes.InProgress));
            AvailableStatuses.Add(new TaskStatus(DefaultAttributes.Completed));

            AvailablePriorities.Clear();
            AvailablePriorities.Add(new TaskPriority(DefaultAttributes.Low));
            AvailablePriorities.Add(new TaskPriority(DefaultAttributes.Medium));
            AvailablePriorities.Add(new TaskPriority(DefaultAttributes.High));
            AvailablePriorities.Add(new TaskPriority(DefaultAttributes.Urgent));
        }

        public abstract void ReorderTask(string reorderTaskId, string? previousTaskId, string? nextTaskId);

        public abstract void AddTask(BasicTask task);

        public abstract void RemoveTask(BasicTask task);

        public abstract void RemoveTask(string taskId);
    }
}
