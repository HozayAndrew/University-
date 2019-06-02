using Model;
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

        public static Data GetData(int capacity)
        {
            var data = new Data
            {
                Count = capacity,
                Matrix = GenerateMatrix(capacity),
                Time = GenerateTimeMatrix(capacity)
            };
            return data;
        }

        private static Random _random = new Random();

        private static List<List<int>> GenerateMatrix(int capacity)
        {
            var matrix = new List<List<int>>();
            for (int i = 0; i < capacity; i++)
            {
                matrix.Add(new List<int>());
                for (int j = 0; j < capacity; j++)
                {
                    if (i != j)
                    {
                        matrix[i].Add(_random.Next(0, 10));
                    }
                    else
                    {
                        matrix[i].Add(1000000);
                    }
                }
            }

            return matrix;
        }

        private static List<int> GenerateTimeMatrix(int capacity)
        {
            var matrix = new List<int>();
            for (int i = 0; i < capacity; i++)
            {
                matrix.Add(_random.Next(0, 10));
            }

            return matrix;
        }
    }
}
