namespace Singleton
{
    /**
     * 
     *  Following classes implement Standart Singleton pattern.
     *  
     *  Benefits: 
     *            - This implementation incapsulates creation of the instance within the class 
     *              and holding that instance throughout the application lifespan.
     * 
     *  Issues: 
     *            - This current implementation has multithreading issues.
     *            - Instances can still be created by: cloning, reflection, sub-classing singleton class.
     * 
     **/

    public class StandartSingleton
    {
        /// <summary>
        ///     Static variable that holds the instance of the class.
        /// </summary>
        private static StandartSingleton _instance;

        /// <summary>
        ///     Private constructor to ensure that instance can be made only within the class.
        /// </summary>
        private StandartSingleton() { }

        /// <summary>
        ///     Static method that provides sort of lazy instantiating of our class instance creation.
        /// </summary>
        /// <returns> Instance of <see cref="StandartSingleton"/></returns>
        public static StandartSingleton GetInstance()
        {
            return _instance ?? (_instance = new StandartSingleton());
        }
    }

    /**
     * 
     *  Same thing can be achieved with static constructor and
     *  readonly field holding the instance.
     * 
     **/

    public class StandartSingletonv2
    {
        /// <summary>
        ///     Immutable class variable holding the instance of the class.
        /// </summary>
        public static readonly StandartSingletonv2 Instance;

        /// <summary>
        ///     Private constructor to ensure that instance can be made only within the class.
        /// </summary>
        private StandartSingletonv2() { }

        /// <summary>
        ///     Static constructor to ensure one time instantiation per application start.
        /// </summary>
        static StandartSingletonv2()
        {
            Instance = new StandartSingletonv2();
        }
    }
}
