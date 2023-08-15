using TodoList.Domain.Exceptions;
using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;
using TodoList.Domain.Tasks;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.UnitTest.TaskTests.CommonTaskTests
{
    public class AddTaskShould
    {
        [Fact]
        public void AddTask_UsingValidTask_ShouldAddTaskToChildTasks()
        {
            var mockTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockTaskBoardObject = mockTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskBoardObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockTaskBoardObject, 1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockChildTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            var mockChildTaskObject = mockChildTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockChildTaskObject.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(mockChildTaskObject, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.AddTask(mockChildTaskObject)).CallBase();
            var _ = mockTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(_, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _.AddTask(mockChildTaskObject);

            mockChildTask.Verify(m => m.MoveToParentTask(It.IsAny<BasicTask>()), Times.AtMost(2)); // first time when construct BasicTask
            Assert.Contains(_.ChildTasks, m => m.Id == mockChildTaskObject.Id);
            Assert.True(_.ChildTasks.Count == 1);
        }

        [Fact]
        public void AddTask_UsingNullTask_ShouldDoNothingToChildTasks()
        {
            BasicTask? nullTask = null;

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
#pragma warning disable CS8604 // Possible null reference argument.
            mockTask.Setup(m => m.AddTask(nullTask)).CallBase();
#pragma warning restore CS8604 // Possible null reference argument.
            var _ = mockTask.Object;

            var beforeAddCount = _.ChildTasks.Count;
#pragma warning disable CS8604 // Possible null reference argument.
            _.RemoveTask(nullTask);
#pragma warning restore CS8604 // Possible null reference argument.
            var afterAddCount = _.ChildTasks.Count;

            Assert.Equal(beforeAddCount, afterAddCount);
        }

        [Fact]
        public void AddTask_FromDifferentTaskBoard_ShouldThrowIncompatibleTaskBoardException()
        {
            var mockChildTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockChildTaskBoardObject = mockChildTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockChildTaskBoardObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockChildTaskBoardObject, 2);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockChildTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            var mockChildTaskObject = mockChildTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockChildTaskObject.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(mockChildTaskObject, mockChildTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockTaskBoardObject = mockTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskBoardObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockTaskBoardObject, 1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.AddTask(mockChildTaskObject)).CallBase();
            var _ = mockTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(_, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            Assert.Throws<IncompatibleTaskBoardException>(() => _.AddTask(mockChildTaskObject));
        }

        [Fact]
        public void AddTask_WithNullTaskBoard_ShouldThrowIncompatibleTaskBoardException()
        {
            var mockChildTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), null,
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            var mockChildTaskObject = mockChildTask.Object;

            var mockTaskBoard = new Mock<BasicTaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            var mockTaskBoardObject = mockTaskBoard.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockTaskBoardObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockTaskBoardObject, 1);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.AddTask(mockChildTaskObject)).CallBase();
            var _ = mockTask.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetProperty("OriginBoard").DeclaringType.GetProperty("OriginBoard").SetValue(_, mockTaskBoardObject);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            Assert.Throws<IncompatibleTaskBoardException>(() => _.AddTask(mockChildTaskObject));
        }
    }
}
