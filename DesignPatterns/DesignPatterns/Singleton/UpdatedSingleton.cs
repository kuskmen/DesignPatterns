using System;
using System.Threading;

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
     *
     *  Issues:
     *            - Instances can still be created by: cloning, reflection, sub-classing singleton class.
     *
     **/

    public class UpdatedSingleton
    {
        /// <summary>
        ///     Static variable that holds the instance of the class.
        /// </summary>
        private static UpdatedSingleton _instance;

        /// <summary>
        ///     Static dummy object used for lock semaphore.
        /// </summary>
        private static readonly object InstanceLock = new object();

        /// <summary>
        ///     Private constructor to ensure that instance can be made only within the class.
        /// </summary>
        private UpdatedSingleton() { }

        /// <summary>
        ///     Static method that provides sort of lazy instantiating of our class instance creation
        ///     and providing multithreaded safe inizialization.
        ///
        ///     Notice that this is slow because only one thread can access <see cref="Instance" /> at a time.
        ///     This is fixed in <see cref="Instance2"/>
        /// </summary>
        /// <returns> Instance of <see cref="UpdatedSingleton"/></returns>
        public static UpdatedSingleton Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    return _instance ?? new UpdatedSingleton();
                }
            }
        }

        /// <summary>
        ///     Updated version of <see cref="Instance"/> method.
        /// </summary>
        /// <returns> Instance of <see cref="UpdatedSingleton"/></returns>
        public static UpdatedSingleton Instance2
        {
            get
            {
                // This is called Double-check locking:
                if (_instance == null)
                {
                    // Lets say by this point of the code two threads passed null check.
                    // Next code will ensure only one of them will continue and the other one will
                    // simply do nothing within the semaphore.
                    lock (InstanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new UpdatedSingleton();
                        }
                    }
                }
                return _instance;
            }
        }

        /**
         *
         *  Some things that were interesting while I was
         *  learning singleton design pattern implementation.
         *
         *  If you are developing device oriented architecture and against CLI specifications
         *  as well as hardware you might consider taking different implementation of Instance property
         *
         **/

        public static UpdatedSingleton Instance3
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceLock)
                    {
                        // What may happen here is that when * _instance * is accessed concurrently,
                        // this is because * _instance * might be accessed un-initialized because of a race condition.
                        // In order for us to fix this we need to add memory barrier.
                        _instance = new UpdatedSingleton();
                        Thread.MemoryBarrier();

                        // however full fence memory barrier such as * Thread.MemoryBarrier() * is expensive performancewise
                        // this will be somehow optimized by using lazy load instantiation with * Interlocked API * in the next Instance
                        // property
                    }
                }
                return _instance;
            }
        }

        public static UpdatedSingleton Instance4
        {
            get
            {
                if (_instance == null)
                {
                    UpdatedSingleton newInstance = new UpdatedSingleton();

                    // Interlocked API is atomic and implemented using memory barriers thus
                    // achieving best performance while locks are slower given the fact that they
                    // require kernel mode transition

                    if (Interlocked.CompareExchange(ref _instance, newInstance, null) != null &&
                        newInstance is IDisposable)
                    {
                        ((IDisposable)newInstance).Dispose();
                    }
                }
                return _instance;
            }
        }
    }

    /**
     *
     *  Same thing can be achieved far more simpler but at the cost of
     *  laziness and slight performence drawback in some cases.
     *
     **/

    public class UpdatedSingletonv2
    {
        private static readonly UpdatedSingletonv2 _instance = new UpdatedSingletonv2();

        /// <summary>
        ///     Explicit static constructor in order C# compiler not to mark class as beforefieldinit.
        /// </summary>
        static UpdatedSingletonv2() { }

        private UpdatedSingletonv2() { }

        /// <summary>
        ///     Static field holding the instance of the class.
        /// </summary>
        /// <remarks>
        ///     Because this class has static constructor, it is specified as having a relaxed semantic
        ///     for its type-initializer method(this is called beforefieldinit). What this means is
        ///     that this method will be invoked immediately prior to executing that static constructor.
        ///
        ///     Note how if we have multiple(or lets say a lot) static field initializers this
        ///     may lead to performance drawback.
        /// </remarks>
        public static UpdatedSingletonv2 Instance { get; } = _instance;

    }

    /**
      *
      *  We can easily workaround this potential performance drawback and
      *  improve laziness with nested type.
      *
      **/

    public class UpdatedSingletonv3
    {
        private UpdatedSingletonv3() { }

        /// <remarks>
        ///     Without static class constructor this type will be marked as beforefieldinit
        ///     meaning that this property will be initialized at, or sometime before, its invocation
        ///     thus granting lazy instantiation.
        /// </remarks>
        public static UpdatedSingletonv3 Instance { get; } = InstanceHolder.instance;

        /// <summary>
        ///     Nested type that holds the outer singleton instance.
        /// </summary>
        /// <remarks>
        ///     Old pattern of marking type not to be beforefieldinit is used here in order to
        ///     eliminate performance drawbacks because this nested type will hold only one
        ///     static field initializer because of its purpose.
        /// </remarks>
        private static class InstanceHolder
        {
            static InstanceHolder() { }

            internal static readonly UpdatedSingletonv3 instance = new UpdatedSingletonv3();
        }
    }
}
