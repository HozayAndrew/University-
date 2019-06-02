using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherProblem
{
    interface IAlgorithm
    {
        Result RunAlgorithm(Data data);
    }
}
