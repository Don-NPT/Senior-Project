using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ExamplePlayModeTest
    {
        [Test]
        public void ExampleTest()
        {
            // Arrange
            int a = 2;
            int b = 3;

            // Act
            int result = a + b;

            // Assert
            Assert.AreEqual(5, result);
        }
    }
}
