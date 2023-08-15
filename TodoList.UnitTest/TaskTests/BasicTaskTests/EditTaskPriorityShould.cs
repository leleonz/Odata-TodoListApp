using TodoList.Domain.Exceptions;
using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;
using TodoList.Domain.Tasks;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.UnitTest.TaskTests.BasicTaskTests
{
    public class EditTaskPriorityShould
    {
        [Fact]
        public void EditTaskPriority_UsingTaskPriorityIdFromOriginBoard_ShouldChangeTaskPriority()
        {
            var mockTaskPriority = new Mock<TaskPriority>(It.IsAny<string>(), It.IsAny<string>());
            var mockTaskPriorityObject = mockTaskPriority.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskPriorityObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockTaskPriorityObject, 1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockTaskBoardObject = mockTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskBoardObject.GetType().GetProperty("AvailablePriorities").DeclaringType.GetProperty("AvailablePriorities").SetValue(mockTaskBoardObject, new List<TaskPriority> { mockTaskPriorityObject });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskPriority(mockTaskPriorityObject.Id)).CallBase();
            var _ = mockTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(_, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _.EditTaskPriority(mockTaskPriorityObject.Id);

            Assert.True(_.TaskPriority != null && _.TaskPriority.Id == mockTaskPriorityObject.Id);
        }

        [Fact]
        public void EditTaskPriority_UsingNull_ShouldChangeTaskPriorityToNull()
        {
            long? nullId = null;

            var mockTaskPriority = new Mock<TaskPriority>(It.IsAny<string>(), It.IsAny<string>());
            var mockTaskPriorityObject = mockTaskPriority.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskPriorityObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockTaskPriorityObject, 1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockTaskBoardObject = mockTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskBoardObject.GetType().GetProperty("AvailablePriorities").DeclaringType.GetProperty("AvailablePriorities").SetValue(mockTaskBoardObject, new List<TaskPriority> { mockTaskPriorityObject });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskPriority(nullId)).CallBase();
            var _ = mockTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(_, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _.EditTaskPriority(nullId);

            Assert.True(_.TaskPriority == null);
        }

        [Fact]
        public void EditTaskPriority_UsingTaskPriorityIdNotFromOriginBoard_ShouldThrowBoardAttributeNotFoundException()
        {
            var mockOtherTaskPriority = new Mock<TaskPriority>(It.IsAny<string>(), It.IsAny<string>());
            var mockOtherTaskPriorityObject = mockOtherTaskPriority.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockOtherTaskPriorityObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockOtherTaskPriorityObject, 2);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTaskPriority = new Mock<TaskPriority>(It.IsAny<string>(), It.IsAny<string>());
            var mockTaskPriorityObject = mockTaskPriority.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskPriorityObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockTaskPriorityObject, 1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockTaskBoardObject = mockTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskBoardObject.GetType().GetProperty("AvailablePriorities").DeclaringType.GetProperty("AvailablePriorities").SetValue(mockTaskBoardObject, new List<TaskPriority> { mockTaskPriorityObject });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskPriority(mockOtherTaskPriorityObject.Id)).CallBase();
            var _ = mockTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(_, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            Assert.Throws<BoardAttributeNotFoundException>(() => _.EditTaskPriority(mockOtherTaskPriorityObject.Id));
        }

        [Fact]
        public void EditTaskPriority_WithTaskPriority_ShouldCallEditTaskPriorityById()
        {
            var mockTaskPriority = new Mock<TaskPriority>(It.IsAny<string>(), It.IsAny<string>()) { CallBase = true };
            var mockTaskPriorityObject = mockTaskPriority.Object;

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskPriority(null as TaskPriority)).CallBase();
            mockTask.Setup(m => m.EditTaskPriority(mockTaskPriorityObject)).CallBase();
            var _ = mockTask.Object;

            _.EditTaskPriority(mockTaskPriorityObject);

            mockTask.Verify(m => m.EditTaskPriority(It.IsAny<long?>()), Times.AtMost(2)); // first time when construct BasicTask
        }

        [Fact]
        public void EditTaskPriority_WithNullTaskPriority_ShouldCallEditTaskPriorityById()
        {
            TaskPriority? nullPriority = null;

            var mockTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.EditTaskPriority(nullPriority)).CallBase();
            var _ = mockTask.Object;

            _.EditTaskPriority(nullPriority);

            mockTask.Verify(m => m.EditTaskPriority(It.IsAny<long?>()), Times.AtMost(2)); // first time when construct BasicTask
        }
    }
}
