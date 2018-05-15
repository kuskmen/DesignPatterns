using System;

namespace DesignPatterns.Singleton
{
    /**
    *
    *  Following classes implement Standart Singleton pattern.
    *
    *  Benefits:
    *            - This implementation incapsulates creation of the instance within the class
    *              and holding that instance throughout the application lifespan.
    *            - This fixes the multithreading issues that standart singleton pattern introduce.
    *            - Instances can't be created by: cloning, reflection, sub-classing singleton class.
    *  Issues:
    *            ???
    **/

    ///<summary>
    ///     This class extends <see cref="UpdatedSingletonv3"/> implementation.
    /// </summary>
    public sealed class HardcoreSingleton
    {
        /// <remarks>
        ///     Adding simple check in the private constructor prevents class
        ///     from being instantiated thru reflection.
        /// </remarks>
        private HardcoreSingleton()
        {
            if (Instance != null)
            {
                throw new Exception("Please do not create new " +
                                    $"instances of singleton class {this.GetType().Name} via reflection");
            }
        }

        public static HardcoreSingleton Instance { get; } = InstanceHolder.instance;

        private static class InstanceHolder
        {
            static InstanceHolder() { }

            // ReSharper disable once InconsistentNaming
            internal static readonly HardcoreSingleton instance = new HardcoreSingleton();
        }

        /// <summary>
        ///     Overrides <see cref="object.MemberwiseClone()"/> method in order
        ///     to prevent <see cref="HardcoreSingleton"/> cloning.
        /// </summary>
        /// <returns>
        ///     Warns user not to clone singleton type.
        /// </returns>
        private new object MemberwiseClone()
        {
            throw new Exception($"Please do not clone {this.GetType().Name}.");
        }
    }
}
