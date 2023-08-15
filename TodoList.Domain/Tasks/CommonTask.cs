using TodoList.Domain.Exceptions;
using TodoList.Domain.Interfaces;
using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.Domain.Tasks
{
    public class CommonTask : BasicTask, ICanHoldTasks
    {
        public DateTime? DueDate { get; set; }

        protected List<BasicTask> _childTasks = new();
        public IReadOnlyList<BasicTask> ChildTasks => _childTasks.AsReadOnly();

        protected CommonTask() : base() { }

        public CommonTask(string name, string description, BasicTaskBoard originBoard, DateTime? dueDate = null, TaskStatus? status = null, TaskPriority? priority = null, BasicTask? parentTask = null) : base(name, description, originBoard, status, priority, parentTask)
        {
            DueDate = dueDate;
        }

        public virtual void AddTask(BasicTask task)
        {
            if (task != null)
            {
                if (task.OriginBoard == null || (task.OriginBoard != null && task.OriginBoard.Id != OriginBoard.Id))
                {
                    throw new IncompatibleTaskBoardException(ExceptionMessage.CannotAddChildTaskFromOtherBoard);
                }
                task.MoveToParentTask(this);
                _childTasks.Add(task);
            }
        }

        public virtual void RemoveTask(BasicTask task)
        {
            if (task != null && _childTasks.Any(t => t.Id == task.Id))
            {
                task.MoveToParentTask(null as BasicTask);
                _childTasks.Remove(task);
            }
            
        }

        public virtual void RemoveTask(string taskId)
        {
            if (!string.IsNullOrWhiteSpace(taskId))
            {
                var selectedTask = _childTasks.FirstOrDefault(childTask => childTask.Id == taskId);
                if (selectedTask != null)
                {
                    RemoveTask(selectedTask);
                }
            }
        }

        /// <summary>
        /// (Soft)Delete current task and all the ChildTasks.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            foreach(var task in _childTasks)
            {
                task.Delete();
            }
        }
    }
}
