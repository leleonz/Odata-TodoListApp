using TodoList.Domain.Exceptions;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Tasks;
using TodoList.Domain.Tasks.ForOrders;

namespace TodoList.Domain.TaskBoards
{
    public class BasicTaskBoard : TaskBoard, ICanHoldOrderedTasks
    {
        private List<OrderedTaskIdMap> _orderedTaskIdMaps = new();
        private List<OrderedTaskId> _orderedTaskIdList = new();
        public override IReadOnlyList<OrderedTaskId> OrderedTaskIdList => _orderedTaskIdList.Any() ? _orderedTaskIdList.AsReadOnly() : ConstructOrderedTasksIdList();

        protected BasicTaskBoard() : base() { }

        public BasicTaskBoard(string? name, string? description, bool isNew = false) : base(name, description, isNew)
        {
            
        }

        public override void AddTask(BasicTask task)
        {
            if (task != null)
            {
                if (task.ParentTask != null)
                {
                    var parentTask = _childTasks.FirstOrDefault(childTask => childTask.Id == task.ParentTask.Id);
                    if (parentTask == null)
                    {
                        throw new ParentTaskNotFoundException(ExceptionMessage.ParentTaskNotFound);
                    }
                    else
                    {
                        if (parentTask is ICanHoldTasks tasks)
                        {
                            tasks.AddTask(task);
                        }
                        else
                        {
                            throw new NotTaskHolderException(ExceptionMessage.TaskCannotHaveChildTasks);
                        }
                    }
                }
                _childTasks.Add(task);
            }
        }

        public override void RemoveTask(BasicTask task)
        {
            _childTasks.Remove(task);
        }

        public override void RemoveTask(string taskId)
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

        public override void ReorderTask(string currentTaskId, string? previousTaskId, string? nextTaskId)
        {
            // find if current task is mapped to another previous task
            var currentTaskAsNextTaskMap = _orderedTaskIdMaps.FirstOrDefault(pair => pair.NextTaskId == currentTaskId);
            if (string.IsNullOrWhiteSpace(previousTaskId))
            {
                if (currentTaskAsNextTaskMap != null)
                {
                    currentTaskAsNextTaskMap.NextTaskId = null;
                }
            }
            else if (!string.IsNullOrWhiteSpace(previousTaskId) && _childTasks.Any(task => task.Id == previousTaskId))
            {
                if (currentTaskAsNextTaskMap != null)
                {
                    currentTaskAsNextTaskMap.NextTaskId = null;
                }

                // find if previous task is mapped to a different task
                var previousTaskMap = _orderedTaskIdMaps.FirstOrDefault(pair => pair.CurrentTaskId == previousTaskId);
                if (previousTaskMap != null)
                {
                    previousTaskMap.NextTaskId = currentTaskId;
                }
            }

            // find if current task is mapped to another next task
            var currentTaskMap = _orderedTaskIdMaps.FirstOrDefault(pair => pair.CurrentTaskId == currentTaskId);
            if (string.IsNullOrWhiteSpace(nextTaskId))
            {
                if (currentTaskMap != null)
                {
                    currentTaskMap.NextTaskId = null;
                }
            }
            else if (!string.IsNullOrWhiteSpace(nextTaskId) && _childTasks.Any(task => task.Id == nextTaskId))
            {
                // find if next task is mapped to another previous task
                var nextTaskMap = _orderedTaskIdMaps.FirstOrDefault(pair => pair.NextTaskId == nextTaskId);
                if (nextTaskMap != null)
                {
                    nextTaskMap.NextTaskId = null;
                }
                
                if (currentTaskMap != null)
                {
                    currentTaskMap.NextTaskId = nextTaskId;
                }
                else
                {
                    _orderedTaskIdMaps.Add(new OrderedTaskIdMap(currentTaskId, nextTaskId));
                }
            }

            ConstructOrderedTasksIdList();
        }

        private IReadOnlyList<OrderedTaskId> ConstructOrderedTasksIdList()
        {            
            var mappedTasks = new List<OrderedTaskMap>();
            foreach (var map in _orderedTaskIdMaps.Where(map => !string.IsNullOrWhiteSpace(map.NextTaskId)))
            {
                var mapCurrentTask = _childTasks.FirstOrDefault(task => task.Id == map.CurrentTaskId);
                var mapNextTask = _childTasks.FirstOrDefault(task => task.Id == map.NextTaskId);

                if (mapCurrentTask != null && mapNextTask != null)
                {
                    mappedTasks.Add(new OrderedTaskMap(mapCurrentTask, mapNextTask));
                }
            }
            var orderMappedTasks = mappedTasks.OrderBy(map => map.CurrentTask.CreatedAt);

            var linkedTaskIds = new LinkedList<string>(_childTasks.Where(task => !orderMappedTasks.Any(map => map.NextTask.Id == task.Id)).OrderBy(task => task.CreatedAt).Select(task => task.Id));
            foreach (var orderMappedTask in orderMappedTasks)
            {
                var test = linkedTaskIds.Find(orderMappedTask.CurrentTask.Id);
                if (test != null)
                {
                    linkedTaskIds.AddAfter(test, orderMappedTask.NextTask.Id);
                }
            }

            var currentNode = linkedTaskIds.First;
            var currentSequenceNumber = 1;
            while (currentNode != null)
            {
                _orderedTaskIdList.Add(new OrderedTaskId(currentSequenceNumber, currentNode.Value));
                currentSequenceNumber++;
                currentNode = currentNode.Next;
            }

            return _orderedTaskIdList;
        }
    }
}
