using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;
using TodoList.Domain.Tasks;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.UnitTest.TaskTests.BasicTaskTests
{
    public class DeleteShould
    {
        [Fact]
        public void Delete_WhenCalled_ShouldSetIsDeletedToTrue()
        {
            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.Delete()).CallBase();
            var _ = mockTask.Object;

            _.Delete();

            Assert.True(_.IsDeleted);
        }
    }
}
