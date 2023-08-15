using TodoList.Domain.TaskBoards;

namespace TodoList.UnitTest.TaskBoardTests.BasicTaskBoardTests
{
    public class AddTaskShould
    {
        [Fact]
        public void AddTask_UsingValidTask_ShouldAddTaskToChildTasks()
        {
            var mock = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var test = mock.Object;
            //var test = new BasicTaskBoard("test", null, true);
            var prop = typeof(BasicTaskBoard).GetProperty("Id").DeclaringType.GetProperty("Id");
            prop.SetValue(test, 2);
        }

        [Fact]
        public void AddTask_UsingNullTask_ShouldDoNothingToChildTasks()
        {

        }

        [Fact]
        public void AddTask_ToIsICanHoldTasks_ShouldAddTaskToChildTaskInParent()
        {

        }

        [Fact]
        public void AddTask_ToIsNotICanHoldTasks_ShouldThrowNotTaskHolderException()
        {

        }

        [Fact]
        public void AddTask_ToParentTaskNotInCurrentTaskBoard_ShouldThrowParentTaskNotFoundException()
        {

        }
    }
}
