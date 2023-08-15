namespace TodoList.Domain.Exceptions
{
    public static class ExceptionMessage
    {
        public const string TaskStatusNotFound = "Selected status is not available in current task board. Please add in the task board and try again.";
        public const string TaskPriorityNotFound = "Selected priority is not available in current task board. Please add in the task board and try again.";
        public const string ParentTaskNotFound = "Selected parent task not found/ not belong to current task board.";
        public const string TaskBoardCannotBeNull = "A task must belongs to a task board.";
        public const string TaskCannotHaveChildTasks = "Selected parent task cannot hold any child/ sub-tasks.";
        public const string CannotAddChildTaskFromOtherBoard = "Adding task from other task board as child/ sub-task is not allowed. Please move task to same board before trying again.";
    }
}
