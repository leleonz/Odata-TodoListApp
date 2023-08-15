using System.Reflection;
using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;
using TodoList.Domain.Tasks;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.UnitTest.TaskTests.CommonTaskTests
{
    public class DeleteShould
    {
        [Fact]
        public void Delete_WhenCalled_ShouldSetIsDeletedToTrue()
        {
            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.Delete()).CallBase();
            var _ = mockTask.Object;

            _.Delete();

            Assert.True(_.IsDeleted);
        }

        [Fact]
        public void Delete_WhenCalled_ShouldCallDeleteForChildTasks()
        {
            var mockChild1 = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());

            var mockChild2 = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());

            var mockChildTasks = new List<BasicTask>
            {
                mockChild1.Object,
                mockChild2.Object
            };

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.Delete()).CallBase();
            var _ = mockTask.Object;

            var _childTasksField = _.GetType().GetField("_childTasks", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _childTasksField.SetValue(_, mockChildTasks);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _.Delete();

            mockChild1.Verify(m => m.Delete(), Times.Once);
            mockChild2.Verify(m => m.Delete(), Times.Once);
        }
    }
}
