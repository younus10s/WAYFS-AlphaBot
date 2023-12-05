public class ExampleTests
{
    [Fact]
    public void ExampleUnitTest_ReturnsTrue()
    {
        // Arrange
        var robot = "Gunnar";

        // Act
        var actual = robot.Contains("G");

        // Assert
        var expected = true;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 2, 4)]
    public void ExampleUnitTest_AddingNumbers(int a, int b, int expected)
    {
        // Act
        var actual = a + b;

        // Assert
        Assert.Equal(expected, actual);
    }
}
