using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherProblem.Helpers;
using Model;

namespace TeacherProblem
{
    public class GreedyAlgorithm : IAlgorithm
    {
        public Result RunAlgorithm(Data data)
        {
            var currentDate = DateTime.Now;
            Result result = null;

            for (int i = 0; i < data.Count; i++)
            {
                var tempResult = FindPass(data, i);
                if(result == null || result.SumTime > tempResult.SumTime)
                {
                    result = tempResult;
                }
            }

            var newDate = DateTime.Now;
            result.AlgorithmTime = (newDate - currentDate).TotalMilliseconds;

            return result;
        }

        private Result FindPass(Data data, int startEdge)
        {
            var copyMatrix = data.Matrix.CopyMatrix();

            var path = new List<int>();
            path.Add(startEdge);

            var currentEdge = startEdge;

            var sumTime = GetSumTime(data.Time);

            while (true)
            {
                var edge = FindMinStep(copyMatrix, currentEdge);
                if (edge == -1)
                {
                    break;
                }
                else
                {
                    path.Add(edge);
                    sumTime += copyMatrix[currentEdge][edge];
                    SetUsedEdge(currentEdge, copyMatrix);
                    currentEdge = edge;
                }
            }

            var result = new Result
            {
                StudentSequence = path,
                SumTime = sumTime
            };

            return result;
        }

        private int FindMinStep(List<List<int>> matrix, int edge)
        {
            var minValue = 1000000;
            var newEdge = -1;

            for (int j = 0; j < matrix.Count; j++)
            {
                if (minValue > matrix[edge][j])
                {
                    minValue = matrix[edge][j];
                    newEdge = j;
                }
            }

            return newEdge;
        }

        private void SetUsedEdge(int edge, List<List<int>> matrix)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                matrix[i][edge] = 1000000;
                matrix[edge][i] = 1000000;
            }
        }

        private int GetSumTime(List<int> times)
        {
            var time = 0;
            foreach (var i in times)
            {
                time += i;
            }

            return time;
        }
    }
}
