using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TeacherProblem.Helpers;

namespace TeacherProblem
{
    public class AntColonyOptimizationAlgorithms : IAlgorithm
    {
        private const float Alpha = 1f;
        private const float Beta = 1f;
        private const float P = 0.7f;

        private const float StartTeta = 0.1f;

        private Random _random = new Random();
        private int MinPathWeight;

        private int Count;

        private List<List<float>> TetaMatrix;

        private TempResult Record;

        private struct TempResult
        {
            public TempResult(List<int> path, int sum)
            {
                Path = path;
                Sum = sum;
            }
            public List<int> Path { get; private set; }
            public int Sum { get; private set; }
        }

        private struct PartPath
        {
            public PartPath(int startIndex, int finishIndex, float fullPathLength)
            {
                StartIndex = startIndex;
                FinishIndex = finishIndex;
                FullPathLength = fullPathLength;
            }
            public int StartIndex { get; }
            public int FinishIndex { get; }
            public float FullPathLength { get; }
        }

        public Result RunAlgorithm(Data data)
        {
            var currentDate = DateTime.Now;

            Count = data.Count;

            TetaMatrix = new List<List<float>>();
            for (int i = 0; i < data.Count; i++)
            {
                TetaMatrix.Add(new List<float>());
                for (int j = 0; j < data.Count; j++)
                {
                    TetaMatrix[i].Add(StartTeta);
                }
            }

            MinPathWeight = GetMinPathWeight(data.Matrix.CopyMatrix());

            Record = new TempResult(new List<int>(), 10000000);

            var numberOfIterationToExit = 5;
            if (data.Count > 20) numberOfIterationToExit = 10;

            var countBadResults = 0;
            while (countBadResults < numberOfIterationToExit)
            {
                var newValue = OneStep(data);
                if (Record.Sum > newValue.Sum)
                {
                    Record = newValue;
                    countBadResults = 0;
                }
                else
                {
                    countBadResults++;
                }
            }

            var newDate = DateTime.Now;

            return new Result
            {

                AlgorithmTime = (newDate - currentDate).TotalMilliseconds,
                StudentSequence = Record.Path,
                SumTime = Record.Sum + data.Time.Sum()
            };
        }

        private TempResult OneStep(Data data)
        {
            // create ants
            List<TempResult> results = new List<TempResult>();

            for (int i = 0; i < data.Count; i++)
            {
                var result = GetResult(i, data.Matrix.CopyMatrix());
                results.Add(result);
            }

            SetNewWeights(results);

            return results.OrderBy(i => i.Sum).First();
        }

        private void SetNewWeights(List<TempResult> results)
        {
            //save all tuples like i j path
            var pathes = new List<PartPath>();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Path.Count == Count)
                {
                    var fullLength = (float)MinPathWeight / results[i].Sum;
                    for (int j = 1; j < Count; j++)
                    {
                        pathes.Add(new PartPath(results[i].Path[j - 1], results[i].Path[j], fullLength));
                    }
                }
            }

            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    TetaMatrix[i][j] *= (1 - P);
                    var allExistPathes = pathes.Where(k => k.StartIndex == i && k.FinishIndex == j);
                    if(allExistPathes.Any())
                    {
                        var delta = allExistPathes.Sum(s => s.FullPathLength);
                        TetaMatrix[i][j] += delta;
                    }
                }
            }
        }

        private TempResult GetResult(int startEdge, List<List<int>> wights)
        {
            var path = new List<int>
            {
                startEdge
            };

            var sumWeights = 0;

            var currentEdge = startEdge;

            while (true)
            {
                var edge = FindStep(currentEdge, wights);
                if (edge == -1)
                {
                    break;
                }
                else
                {
                    path.Add(edge);
                    sumWeights += wights[currentEdge][edge];
                    SetUsedEdge(currentEdge, wights);
                    currentEdge = edge;
                }
            }

            return new TempResult(path, sumWeights);
        }

        private int FindStep(int edge, List<List<int>> wights)
        {
            var probabilty = new List<float>();

            for (int i = 0; i < wights.Count; i++)
            {
                if (wights[edge][i] < 100000)
                {
                    var value = (float)(Math.Pow(TetaMatrix[edge][i], Alpha) * Math.Pow(1f / wights[edge][i], Beta));
                    probabilty.Add(value);
                }
                else
                {
                    probabilty.Add(0f);
                }
            }

            var intervals = new List<float>();
            var tempInterval = 0f;
            var probabiltySum = probabilty.Sum();

            for (int i = 0; i < wights.Count; i++)
            {
                probabilty[i] /= probabiltySum;
                tempInterval += probabilty[i];
                intervals.Add(tempInterval);
            }

            var randomValue = _random.NextDouble();
            int index = -1;
            for (int i = 0; i < intervals.Count; i++)
            {
                if (randomValue < intervals[i])
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private void SetUsedEdge(int edge, List<List<int>> matrix)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                matrix[i][edge] = 1000000;
                matrix[edge][i] = 1000000;
            }
        }

        private int GetMinPathWeight(List<List<int>> matrix)
        {
            var sum = 0;
            for (int i = 0; i < matrix.Count; i++)
            {
                var value = matrix[i].Min();
                for (int j = 0; j < matrix.Count; j++)
                {
                    matrix[i][j] -= value;
                }

                sum += value;
            }

            for (int i = 0; i < matrix.Count; i++)
            {
                var newTempList = new List<int>();

                for (int j = 0; j < matrix.Count; j++)
                {
                    newTempList.Add(matrix[j][i]);
                }

                var value = newTempList.Min();

                for (int j = 0; j < matrix.Count; j++)
                {
                    matrix[j][i] -= value;
                }

                sum += value;
            }

            return sum;
        }
    }
}
