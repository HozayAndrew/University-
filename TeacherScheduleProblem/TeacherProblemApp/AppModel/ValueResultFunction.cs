using Model;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherProblemApp.AppModel
{
    public class ValueResultFunction
    {
        public IList<DataPoint> Points { get; }

        public ValueResultFunction(List<ExperimentResult> results)
        {
            Points = new List<DataPoint>();
            foreach (var result in results)
            {
                Points.Add(new DataPoint(result.Size, result.FunctionResult));
            }
        }
    }
}
