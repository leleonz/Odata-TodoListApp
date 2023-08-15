using TodoList.Domain.TaskBoards;
using TodoList.Domain.TaskBoards.Attributes;

namespace TodoList.UnitTest.TaskBoardTests
{
    public class SetupDefaultBoardShould
    {
        [Fact]
        public void SetupDefaultBoard_WhenCalled_ShouldAddDefaultAvailableStatuses()
        {
            var defaultStatusesCount = 3;
            var mockTaskBoard = new Mock<TaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()) { CallBase = true };

            var mockTaskBoardObject = mockTaskBoard.Object;
            mockTaskBoardObject.SetupDefaultBoard();

            Assert.Equal(defaultStatusesCount, mockTaskBoardObject.AvailableStatuses.Count);
            Assert.Collection(mockTaskBoardObject.AvailableStatuses.OrderBy(s => s.Name),
                s => Assert.Equal(DefaultAttributes.Completed, s.Name),
                s => Assert.Equal(DefaultAttributes.InProgress, s.Name),
                s => Assert.Equal(DefaultAttributes.NotStarted, s.Name));
        }

        [Fact]
        public void SetupDefaultBoard_WhenCalled_ShouldAddDefaultAvailablePriorities()
        {
            var defaultPrioritiesCount = 4;
            var mockTaskBoard = new Mock<TaskBoard>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()) { CallBase = true };

            var mockTaskBoardObject = mockTaskBoard.Object;
            mockTaskBoardObject.SetupDefaultBoard();

            Assert.Equal(defaultPrioritiesCount, mockTaskBoardObject.AvailablePriorities.Count);
            Assert.Collection(mockTaskBoardObject.AvailablePriorities.OrderBy(p => p.Name),
                p => Assert.Equal(DefaultAttributes.High, p.Name),
                p => Assert.Equal(DefaultAttributes.Low, p.Name),
                p => Assert.Equal(DefaultAttributes.Medium, p.Name),
                p => Assert.Equal(DefaultAttributes.Urgent, p.Name));
        }

        
    }
}
