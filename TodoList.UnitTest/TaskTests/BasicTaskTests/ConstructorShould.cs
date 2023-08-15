using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;
using TodoList.Domain.Tasks;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.UnitTest.TaskTests.BasicTaskTests
{
    public class ConstructorShould
    {
        [Fact]
        public void Constructor_WhenCalled_ShouldCallEditMethods()
        {
            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(), 
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());

            var _ = mockTask.Object;

            mockTask.Verify(m => m.MoveToTaskBoard(It.IsAny<TaskBoard>()), Times.Once);
            mockTask.Verify(m => m.MoveToParentTask(It.IsAny<BasicTask>()), Times.Once);
            mockTask.Verify(m => m.EditTaskStatus(It.IsAny<TaskStatus>()), Times.Once);
            mockTask.Verify(m => m.EditTaskPriority(It.IsAny<TaskPriority>()), Times.Once);
        }
    }
}
