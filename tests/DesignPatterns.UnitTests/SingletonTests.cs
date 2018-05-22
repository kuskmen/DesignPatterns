namespace DesignPatterns.UnitTests
{
    using System;
    using System.Reflection;
    using NUnit.Framework;
    using DesignPatterns.Singleton;

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void Instance_ShouldReturnSameObjectAgain()
        {
            // Arrange
            // Act
            var firstInstance = HardcoreSingleton.Instance;
            var secondInstance = HardcoreSingleton.Instance;

            // Assert
            Assert.AreSame(firstInstance, secondInstance);
        }

        [Test]
        public void CreatingInstanceUsingReflection_ShouldThrowException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<Exception>(() =>
            {
                try
                {
                    typeof(HardcoreSingleton)
                        .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null)?
                        .Invoke(null);
                }
                catch (TargetInvocationException ex)
                {
                    if (ex.InnerException != null) throw ex.InnerException;
                }

            });
        }

        [Test]
        public void CloningSingleton_ShouldThrowException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<Exception>(() =>
            {
                try
                {
                    typeof(HardcoreSingleton)
                        .GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic)?
                        .Invoke(HardcoreSingleton.Instance, new object[0]);
                }
                catch (TargetInvocationException ex)
                {
                    if (ex.InnerException != null) throw ex.InnerException;
                }
            });
        }
    }
}
