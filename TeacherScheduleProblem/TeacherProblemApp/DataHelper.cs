using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherProblem.Model;

namespace TeacherProblemApp
{
    public class DataHelper
    {
        private Random _random = new Random();

        private List<List<int>> GenerateMatrix(int capacity)
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

        private List<int> GenerateTimeMatrix(int capacity)
        {
            var matrix = new List<int>();
            for (int i = 0; i < capacity; i++)
            {
                matrix.Add(_random.Next(0, 10));
            }

            return matrix;
        }

        public Data GetData(int capacity)
        {
            var data = new Data
            {
                Count = capacity,
                Matrix = GenerateMatrix(capacity),
                Time = GenerateTimeMatrix(capacity)
            };
            return data;
        }
    }
}
