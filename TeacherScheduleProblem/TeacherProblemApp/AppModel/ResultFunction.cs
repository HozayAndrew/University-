using Model;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherProblemApp.AppModel
{
    public class ResultFunction
    {
        public IList<DataPoint> Points { get; private set; }

        public ResultFunction(List<ExperimentResult> results)
        {
            Points = new List<DataPoint>();
            foreach(var result in results)
            {
                Points.Add(new DataPoint(result.Size, result.Time));
            }
        }
    }
}
