namespace DataStructures.UnitTests
{
    using System;
    using DataStructures.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public class ArrayExtensionsTests
    {
        [Test]
        public void Resize_WithNullAsSourceArray_ShouldThrowArgumentNullException()
        {
            // Arrange
            int[] a = null;
            
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => ArrayExtensions.Resize(ref a, 3, default));
        }

        [TestCase(-3), TestCase(1)]
        public void Resize_WithNegativeOrLessThanOriginalSize_ShouldThrowArgumentException(int l)
        {
            // Arrange
            var a = new [] { 0, 1, 2, 3 };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => ArrayExtensions.Resize(ref a, l, default));
        }

        [Test]
        public void Resize_WithValidNewSizeAndDefaultValue_ShouldResizeArrayAccordingly()
        {
            // Arrange
            var a = new [] { 0, 1, 2 };
            // Act
            ArrayExtensions.Resize(ref a, 5, int.MaxValue);

            // Assert
            Assert.AreEqual(5, a.Length);
            Assert.AreEqual(int.MaxValue, a[a.Length - 1]);

        }

        [Test]
        public void RemoveAt_WithIndexZero_ShouldArrayWithZeroElements()
        {
            // Arrange
            var a = new [] { 5 };
            
            // Act
            var actualArray = a.RemoveAt(0);
            
            // Assert
            Assert.AreEqual(0, actualArray.Length);
        }

    }
}
