using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherProblem.Model;
using TeacherProblem.Helpers;

namespace TeacherProblem
{
    public class GreedyAlgorithm
    {
        public List<Result> RunAlgorithm(Data data)
        {
            var results = new List<Result>();

            for (int i = 0; i < data.Count; i++)
            {
                results.Add(FindPass(data, i));
            }

            return results;
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
