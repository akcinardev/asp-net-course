namespace CRUDTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange  // Declaration of vars
            MyMath mm = new MyMath();
            int input1 = 10;
            int input2 = 20;
            int expected = 30;

            // Act      // Call the method
            int actual = mm.Add(input1, input2);

            // Assert   // Compare expected and output value
            Assert.Equal(expected, actual);
        }
    }
}