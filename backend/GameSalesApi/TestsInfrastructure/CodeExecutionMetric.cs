using System;
using System.Diagnostics;

namespace TestsInfrastructure
{
    /// <summary>
    /// Check execution time of target method
    /// </summary>
    public class CodeExecutionMetric
    {
        private readonly Stopwatch timer = new Stopwatch();
        private readonly string benchmarkName;

        /// <summary>
        /// Check in a using way with out to console
        /// </summary>
        /// <param name="benchmarkName">Test name</param>
        public CodeExecutionMetric(string benchmarkName)
        {
            this.benchmarkName = benchmarkName;
            timer.Start();
        }

        public void Dispose()
        {
            timer.Stop();
            Console.WriteLine($"{benchmarkName} {timer.Elapsed}");
        }

        /// <summary>
        /// Check execution time of target <paramref name="toTime"/>
        /// </summary>
        /// <param name="toTime">Target action</param>
        /// <returns>Elapsed time</returns>
        public static TimeSpan ActionChecker(Action toTime)
        {
            var timer = Stopwatch.StartNew();
            toTime();
            timer.Stop();

            return timer.Elapsed;
        }

        /// <summary>
        /// Check execution time of target <paramref name="toTime"/>
        /// </summary>
        /// <param name="toTime">Target action</param>
        /// <param name="expectedTimeInMiliseconds">Expected time to <paramref name="toTime"/> execute in miliseconds</param>
        /// <returns>Elapsed time</returns>
        public static void ActionChecker(Action toTime, int expectedTimeInMiliseconds)
        {
            var timer = Stopwatch.StartNew();
            toTime();
            timer.Stop();

            if (timer.ElapsedMilliseconds > expectedTimeInMiliseconds)
                throw new ArgumentException($"Expected time was {expectedTimeInMiliseconds}, actual - {timer.ElapsedMilliseconds}");
        }
    }
}
