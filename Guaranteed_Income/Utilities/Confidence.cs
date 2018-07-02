using Guaranteed_Income.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guaranteed_Income.Utilities
{
    public class Confidence
    {
        List<List<double>> data;

        public Confidence(List<List<double>> data)
        {
            MonteCompare comparer = new MonteCompare();
            StoParallelMergeSort<List<double>> mergeSort = new StoParallelMergeSort<List<double>>(comparer);
            mergeSort.Sort(data,false);
            this.data = data;
        }

        public List<double> FindInterval(int confidence)
        {
            return data[confidence * (MonteCarlo.trials / 100)];
        }
    }
}
