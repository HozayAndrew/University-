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
    public class TimeResultFunction
    {
        public IList<DataPoint> Points { get; }

        public TimeResultFunction(List<ExperimentResult> results)
        {
            Points = new List<DataPoint>();
            foreach(var result in results)
            {
                Points.Add(new DataPoint(result.Size, result.Time));
            }
        }
    }
}
