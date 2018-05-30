namespace DesignPatterns.UnitTests
{
    using System.Text;
    using DesignPatterns.Object_Pool_Pattern;
    using NUnit.Framework;

    [TestFixture]
    public class ObjectPoolTests
    {
        private readonly IObjectPool<StringBuilder> sut = new ObjectPool<StringBuilder>(() => new StringBuilder());

        [Test]
        public void Allocate_WithEmptyPool_ShouldCreateNewInstanceOfPooledObjectAndReturnItForUse()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsInstanceOf<StringBuilder>(sut.Allocate());
        }

        [Test]
        public void Allocate_WhenCalledBeforeItemIsFreed_ShouldCreateNewInstanceOfPooledObject()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreNotSame(sut.Allocate(), sut.Allocate());
        }

        [Test]
        public void Free_WhenCalledAfterItemIsAdded_ShouldReturnAlreadyCreatedInstanceToPoolSoLaterCanBeReused()
        {
            // Arrange
            var firstInstace = sut.Allocate();

            // Act
            sut.Free(firstInstace);

            // Assert
            Assert.AreSame(firstInstace, sut.Allocate());
        }

    }
}
