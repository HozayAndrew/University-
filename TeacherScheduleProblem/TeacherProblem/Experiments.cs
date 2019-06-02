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
        internal List<ExperimentResult> GetExperimentResults(IAlgorithm algorithm, ExperimentsData data)
        {
            var results = new List<ExperimentResult>();

            var currentValue = data.StartCount;

            while (currentValue < data.FinishCount)
            {
                var tempData = DataHelper.GetData(currentValue);
                var result = algorithm.RunAlgorithm(tempData);

                results.Add(new ExperimentResult { Size = currentValue, Time = result.AlgorithmTime });

                currentValue += data.Step;
            }

            return results;
        }
    }
}
