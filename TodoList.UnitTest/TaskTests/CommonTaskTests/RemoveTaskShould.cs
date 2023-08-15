using System.Reflection;
using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;
using TodoList.Domain.Tasks;
using TaskStatus = TodoList.Domain.TaskBoards.Attributes.TaskStatus;

namespace TodoList.UnitTest.TaskTests.CommonTaskTests
{
    public class RemoveTaskShould
    {
        [Fact]
        public void RemoveTask_UsingValidTask_ShouldRemoveTaskFromChildTasks()
        {
            const string mockChildTaskId = "task-1";
            var mockChild = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            var mockChildTaskObject = mockChild.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockChildTaskObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockChildTaskObject, mockChildTaskId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockChildTasks = new List<BasicTask>
            {
                mockChildTaskObject
            };

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.RemoveTask(mockChildTaskObject)).CallBase();
            var _ = mockTask.Object;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _.GetType().GetField("_childTasks", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField).SetValue(_, mockChildTasks);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _.RemoveTask(mockChildTaskObject);

            mockChild.Verify(m => m.MoveToParentTask(null as BasicTask), Times.AtMost(2)); // first time when construct BasicTask
            Assert.DoesNotContain(_.ChildTasks, m => m.Id == mockChildTaskId);
        }

        [Fact]
        public void RemoveTask_UsingInvalidTask_ShouldDoNothingToChildTasks()
        {
            var mockInvalidTask = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            var mockInvalidTaskObject = mockInvalidTask.Object;

            var mockChild = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            var mockChildTaskObject = mockChild.Object;

            var mockChildTasks = new List<BasicTask>
            {
                mockChildTaskObject
            };

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.RemoveTask(mockInvalidTaskObject)).CallBase();
            var _ = mockTask.Object;

            var _childTasksField = _.GetType().GetField("_childTasks", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _childTasksField.SetValue(_, mockChildTasks);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var beforeRemoveCount = _.ChildTasks.Count;
            _.RemoveTask(mockInvalidTaskObject);
            var afterRemoveCount = _.ChildTasks.Count;

            mockInvalidTask.Verify(m => m.MoveToParentTask(null as BasicTask), Times.AtMostOnce);  // when construct BasicTask
            mockChild.Verify(m => m.MoveToParentTask(null as BasicTask), Times.AtMostOnce); // when construct BasicTask
            Assert.Equal(beforeRemoveCount, afterRemoveCount);
        }

        [Fact]
        public void RemoveTask_UsingNullTask_ShouldDoNothingToChildTasks()
        {
            BasicTask? nullTask = null;
            var mockChild = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            var mockChildTaskObject = mockChild.Object;

            var mockChildTasks = new List<BasicTask>
            {
                mockChildTaskObject
            };

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
#pragma warning disable CS8604 // Possible null reference argument.
            mockTask.Setup(m => m.RemoveTask(nullTask)).CallBase();
#pragma warning restore CS8604 // Possible null reference argument.
            var _ = mockTask.Object;

            var _childTasksField = _.GetType().GetField("_childTasks", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _childTasksField.SetValue(_, mockChildTasks);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var beforeRemoveCount = _.ChildTasks.Count;
#pragma warning disable CS8604 // Possible null reference argument.
            _.RemoveTask(nullTask);
#pragma warning restore CS8604 // Possible null reference argument.
            var afterRemoveCount = _.ChildTasks.Count;

            Assert.Equal(beforeRemoveCount, afterRemoveCount);
        }

        [Fact]
        public void RemoveTask_UsingValidId_ShouldCallRemoveTaskByBasicTaskObject()
        {
            const string mockChildTaskId = "task-1";
            var mockChild = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            var mockChildTaskObject = mockChild.Object;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mockChildTaskObject.GetType().GetProperty("Id").DeclaringType.GetProperty("Id").SetValue(mockChildTaskObject, mockChildTaskId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var mockChildTasks = new List<BasicTask>
            {
                mockChildTaskObject
            };

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
            mockTask.Setup(m => m.RemoveTask(mockChildTaskId)).CallBase();
            var _ = mockTask.Object;

            var _childTasksField = _.GetType().GetField("_childTasks", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _childTasksField.SetValue(_, mockChildTasks);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            _.RemoveTask(mockChildTaskId);

            mockTask.Verify(m => m.RemoveTask(It.IsAny<BasicTask>()), Times.Once);
        }

        [Theory]
        [InlineData("random-id")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void RemoveTask_UsingInvalidId_ShouldNotCallRemoveTaskByBasicTaskObject(string? taskId)
        {
            var mockChild = new Mock<BasicTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());

            var mockChildTasks = new List<BasicTask>
            {
                mockChild.Object
            };

            var mockTask = new Mock<CommonTask>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TaskBoard>(),
                It.IsAny<DateTime>(), It.IsAny<TaskStatus>(), It.IsAny<TaskPriority>(), It.IsAny<BasicTask>());
#pragma warning disable CS8604 // Possible null reference argument.
            mockTask.Setup(m => m.RemoveTask(taskId)).CallBase();
#pragma warning restore CS8604 // Possible null reference argument.
            var _ = mockTask.Object;

            var _childTasksField = _.GetType().GetField("_childTasks", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _childTasksField.SetValue(_, mockChildTasks);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

#pragma warning disable CS8604 // Possible null reference argument.
            _.RemoveTask(taskId);
#pragma warning restore CS8604 // Possible null reference argument.

            mockTask.Verify(m => m.RemoveTask(It.IsAny<BasicTask>()), Times.Never);
        }
    }
}
