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
        private List<List<double>> trialsList = new List<List<double>> { };
        private double currentValue;
        private double expectedReturn;
        private double standardDeviation;
        private double time; //how many days,months, or years
        private int trials = 10000; //how many trials
        IRandom x = SafeRandom.Generator;
        
        
        public MonteCarlo(double currentValue, double expectedReturn, double standardDeviation, double time)
        {
            this.currentValue = currentValue;
            this.expectedReturn = expectedReturn;
            this.standardDeviation = standardDeviation;
            this.time = time;

            //Console.WriteLine("Starting monte carlo");

            var iops = new ParallelOptions() { MaxDegreeOfParallelism = System.Environment.ProcessorCount };
            Parallel.For(0, trials,iops, x => {
                RunSimulation();
            });
            //Console.WriteLine("Monte carlo done");
        }

        public void RunSimulation()
        {
            double change;
            double trialValue = currentValue;
            List<double> trial;
            trial = new List<double> { }; //record the current trial

            for (int j = 0; j < time; j++)
            {
                change = trialValue * ((expectedReturn) + (standardDeviation * (x.NextDouble(-3.0, 3.0))));
                trialValue += change;
                trialValue = Math.Max(trialValue, 0);
                trial.Add(trialValue);
            }

            mutex.WaitOne();
            trialsList.Add(trial); //make sure only one thread accesses the list at any given time to record its trial
            mutex.ReleaseMutex();
        }
    }
}
