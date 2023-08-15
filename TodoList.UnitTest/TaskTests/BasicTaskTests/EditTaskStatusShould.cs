using TodoList.Domain.Exceptions;
using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;
using TodoList.Domain.Tasks;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.UnitTest.TaskTests.BasicTaskTests
{
    public class EditTaskStatusShould
    {
        [Fact]
        public void EditTaskStatus_UsingTaskStatusIdFromOriginBoard_ShouldChangeTaskStatus()
        {
            var mockTaskStatus = new Mock<TaskStatus>(It.IsAny<string>(), It.IsAny<string>());
            var mockTaskStatusObject = mockTaskStatus.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskStatusObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockTaskStatusObject, 1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockTaskBoardObject = mockTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskBoardObject.GetType().GetProperty("AvailableStatuses").DeclaringType.GetProperty("AvailableStatuses").SetValue(mockTaskBoardObject, new List<TaskStatus> { mockTaskStatusObject });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskStatus(mockTaskStatusObject.Id)).CallBase();
            var _ = mockTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(_, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _.EditTaskStatus(mockTaskStatusObject.Id);

            Assert.True(_.TaskStatus != null && _.TaskStatus.Id == mockTaskStatusObject.Id);
        }

        [Fact]
        public void EditTaskStatus_UsingNull_ShouldChangeTaskStatusToNull()
        {
            long? nullId = null;

            var mockTaskStatus = new Mock<TaskStatus>(It.IsAny<string>(), It.IsAny<string>());
            var mockTaskStatusObject = mockTaskStatus.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskStatusObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockTaskStatusObject, 1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockTaskBoardObject = mockTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskBoardObject.GetType().GetProperty("AvailableStatuses").DeclaringType.GetProperty("AvailableStatuses").SetValue(mockTaskBoardObject, new List<TaskStatus> { mockTaskStatusObject });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskStatus(nullId)).CallBase();
            var _ = mockTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(_, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _.EditTaskStatus(nullId);

            Assert.True(_.TaskStatus == null);
        }

        [Fact]
        public void EditTaskStatus_UsingTaskPStatusIdNotFromOriginBoard_ShouldThrowBoardAttributeNotFoundException()
        {
            var mockOtherTaskStatus = new Mock<TaskStatus>(It.IsAny<string>(), It.IsAny<string>());
            var mockOtherTaskStatusObject = mockOtherTaskStatus.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockOtherTaskStatusObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockOtherTaskStatusObject, 2);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTaskStatus = new Mock<TaskStatus>(It.IsAny<string>(), It.IsAny<string>());
            var mockTaskStatusObject = mockTaskStatus.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskStatusObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockTaskStatusObject, 1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockTaskBoardObject = mockTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskBoardObject.GetType().GetProperty("AvailableStatuses").DeclaringType.GetProperty("AvailableStatuses").SetValue(mockTaskBoardObject, new List<TaskStatus> { mockTaskStatusObject });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskStatus(mockOtherTaskStatusObject.Id)).CallBase();
            var _ = mockTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(_, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            Assert.Throws<BoardAttributeNotFoundException>(() => _.EditTaskStatus(mockOtherTaskStatusObject.Id));
        }

        [Fact]
        public void EditTaskStatus_WithTaskStatus_ShouldCallEditTaskStatusById()
        {
            var mockTaskStatus = new Mock<TaskStatus>(It.IsAny<string>(), It.IsAny<string>()) { CallBase = true };
            var mockTaskStatusObject = mockTaskStatus.Object;

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskStatus(null as TaskStatus)).CallBase();
            mockTask.Setup(m => m.EditTaskStatus(mockTaskStatusObject)).CallBase();
            var _ = mockTask.Object;

            _.EditTaskStatus(mockTaskStatusObject);

            mockTask.Verify(m => m.EditTaskStatus(It.IsAny<long?>()), Times.AtMost(2)); // first time when construct BasicTask
        }

        [Fact]
        public void EditTaskStatus_WithNullTaskPriority_ShouldCallEditTaskStatusById()
        {
            TaskStatus? nullPriority = null;

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskStatus(nullPriority)).CallBase();
            var _ = mockTask.Object;

            _.EditTaskStatus(nullPriority);

            mockTask.Verify(m => m.EditTaskStatus(It.IsAny<long?>()), Times.AtMost(2)); // first time when construct BasicTask
        }
    }
}
