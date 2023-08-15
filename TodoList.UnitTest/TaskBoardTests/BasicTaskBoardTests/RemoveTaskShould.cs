namespace TodoList.UnitTest.TaskBoardTests.BasicTaskBoardTests
{
    public class RemoveTaskShould
    {
        [Fact]
        public void RemoveTask_UsingValidId_ShouldRemoveTaskFromChildTasks()
        {

        }

        [Theory]
        [InlineData("random-id")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void RemoveTask_UsingInvalidId_ShouldDoNothingToChildTasks(string? taskId)
        {

        }

        [Fact]
        public void RemoveTask_UsingValidTask_ShouldRemoveTaskFromChildTasks()
        {

        }

        [Fact]
        public void RemoveTask_UsingInvalidTask_ShouldDoNothingToChildTasks()
        {

        }

        [Fact]
        public void RemoveTask_UsingNullTask_ShouldDoNothingToChildTasks()
        {

        }
    }
}
