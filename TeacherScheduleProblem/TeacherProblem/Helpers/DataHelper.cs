using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherProblem.Helpers
{
    public static class DataHelper
    {
        public static List<List<int>> CopyMatrix(this List<List<int>> matrix)
        {
            var newMatrix = new List<List<int>>();
            for(int i = 0; i < matrix.Count; i++)
            {
                newMatrix.Add(new List<int>());
                for (int j = 0; j < matrix.Count; j++)
                {
                    newMatrix[i].Add(matrix[i][j]);
                }
            }

            return newMatrix;
        }
    }
}
