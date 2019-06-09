using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherProblem.Helpers;

namespace TeacherProblem
{
    public class Experiments
    {
        internal List<ExperimentResult> GetExperimentResults(IAlgorithm algorithm, List<Data> data)
        {
            var results = new List<ExperimentResult>();
            foreach(var group in data.GroupBy(i => i.Count))
            {
                var averageTime = 0.0;
                var averageSum = 0;
                foreach (var item in group)
                {
                    var result = algorithm.RunAlgorithm(item);
                    averageTime += result.AlgorithmTime;
                    averageSum += result.SumTime;
                }

                results.Add(new ExperimentResult { Size = group.Key, Time = averageTime / group.Count(), FunctionResult = averageSum / group.Count()});
            }

            return results;
        }
    }
}
