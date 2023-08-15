using TodoList.Domain.Exceptions;
using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.Domain.Tasks
{
    public class BasicTask
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus? TaskStatus { get; private set; }
        public TaskPriority? TaskPriority { get; private set; }
        public List<string> Labels { get; private set; } = new List<string>();
        public bool IsDeleted { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public BasicTask? ParentTask { get; private set; }
        public TaskBoard OriginBoard { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected BasicTask() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BasicTask(string name, string description, BasicTaskBoard originBoard, TaskStatus? status = null, TaskPriority? priority = null, BasicTask? parentTask = null)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            // move to domain service to use different id generator
            Id = Guid.NewGuid().ToString();

            Name = name;
            Description = description;
            IsDeleted = false;
            CreatedAt = DateTime.UtcNow;

            MoveToTaskBoard(originBoard);
            MoveToParentTask(parentTask);
            EditTaskStatus(status);
            EditTaskPriority(priority);
        }

        /// <summary>
        /// Soft delete current task.
        /// </summary>
        public virtual void Delete()
        {
            IsDeleted = true;
        }

        public virtual void EditTaskStatus(long? statusId)
        {
            if (!statusId.HasValue)
            {
                TaskStatus = null;
            }
            else
            {
                var newStatus = OriginBoard.AvailableStatuses.FirstOrDefault(status => status.Id == statusId);
                if (newStatus != null)
                {
                    TaskStatus = newStatus;
                }
                else
                {
                    throw new BoardAttributeNotFoundException(ExceptionMessage.TaskStatusNotFound);
                }
            }
        }

        public virtual void EditTaskStatus(TaskStatus? status)
        {
            EditTaskStatus(status != null ? status.Id : null);
        }

        public virtual void EditTaskPriority(long? priorityId)
        {
            if (!priorityId.HasValue)
            {
                TaskPriority = null;
            }
            else
            {
                var newPriority = OriginBoard.AvailablePriorities.FirstOrDefault(priority => priority.Id == priorityId);
                if (newPriority != null)
                {
                    TaskPriority = newPriority;
                }
                else
                {
                    throw new BoardAttributeNotFoundException(ExceptionMessage.TaskPriorityNotFound);
                }
            }
        }

        public virtual void EditTaskPriority(TaskPriority? priority)
        {
            EditTaskPriority(priority != null ? priority.Id : null);
        }

        public virtual void MoveToParentTask(string? taskId)
        {
            if (!string.IsNullOrWhiteSpace(taskId))
            {
                ParentTask = null;
            }
            else
            {
                var newParentTask = OriginBoard.ChildTasks.FirstOrDefault(childTask => childTask.Id == taskId);
                if (newParentTask != null)
                {
                    ParentTask = newParentTask;
                }
                else
                {
                    throw new ParentTaskNotFoundException(ExceptionMessage.ParentTaskNotFound);
                }
            }
        }

        public virtual void MoveToParentTask(BasicTask? task)
        {
            MoveToParentTask(task != null ? task.Id : null);
        }

        /// <summary>
        /// Move current <see cref="BasicTask"/> to another <see cref="BasicTaskBoard"/>. This will remove/ clear all task board related fields, e.g. TaskStatus, TaskPriority, ParentTask.
        /// </summary>
        /// <param name="taskBoard">Destination task board</param>
        /// <exception cref="TaskBoardNullException"></exception>
        public virtual void MoveToTaskBoard(TaskBoard taskBoard)
        {
            OriginBoard = taskBoard ?? throw new TaskBoardNullException(ExceptionMessage.TaskBoardCannotBeNull);

            // Clear all board related fields
            TaskStatus = null;
            TaskPriority = null;
            ParentTask = null;
        }
    }
}
