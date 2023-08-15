using TodoList.Domain.TaskBoards;

namespace TodoList.UnitTest.TaskBoardTests
{
    public class ConstructorShould
    {
        [Fact]
        public void Constructor_IsNewTrue_ShouldCallSetupDefaultBoard()
        {
            var mockTaskBoard = new Mock<TaskBoard>(It.IsAny<string>(), It.IsAny<string>(), true) { CallBase = true };

            var _ = mockTaskBoard.Object;

            mockTaskBoard.Verify(m => m.SetupDefaultBoard(), Times.Once);
        }

        [Fact]
        public void Constructor_IsNewFalse_ShouldNotCallSetupDefaultBoard()
        {
            var mockTaskBoard = new Mock<TaskBoard>(It.IsAny<string>(), It.IsAny<string>(), false) { CallBase = true };

            var _ = mockTaskBoard.Object;

            mockTaskBoard.Verify(m => m.SetupDefaultBoard(), Times.Never);
        }
    }
}
