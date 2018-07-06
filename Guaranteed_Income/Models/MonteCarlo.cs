using Guaranteed_Income.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guaranteed_Income.Services
{
    public class MonteCarlo
    {
        private static Mutex mutex = new Mutex();
        public List<List<double>> trialsList = new List<List<double>> { };
        public double currentValue { get; set; }
        public double expectedReturn { get; set; }
        public double standardDeviation { get; set; }
        public double time { get; set; } //how many days,months, or years
        public static int trials = 10000; //how many trials


        public MonteCarlo(double currentValue, double expectedReturn, double standardDeviation, double time)
        {

            this.currentValue = currentValue;
            this.expectedReturn = expectedReturn;
            this.standardDeviation = standardDeviation;
            this.time = time;

            //Console.WriteLine("Starting monte carlo");

            var iops = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }; //if this server were to run other things this could be turned down
            Parallel.For(0, trials, iops, element =>
            {
                RunSimulation();
            });
            //Console.WriteLine("Monte carlo done");
        }

        private void RunSimulation()
        {

            double change;
            double trialValue = currentValue;
            List<double> trial = new List<double> { }; //record the current trial

            for (int j = 0; j < time; j++)
            {
                change = trialValue * ((expectedReturn) + (standardDeviation * (SafeRandom.NextDouble(-3.0, 3.0))));
                trialValue += change;
                trial.Add(trialValue);
            }
            trial = trial.Select(x => Math.Round(x, 2)).ToList();

            mutex.WaitOne();
            trialsList.Add(trial); //make sure only one thread accesses the list at any given time to record its trial
            mutex.ReleaseMutex();
        }
    }
}
