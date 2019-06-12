using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherProblemApp
{
    public class Generator
    {
        private Random _random = new Random();

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
                        matrix[i].Add(_random.Next(10, 50));
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
                matrix.Add(_random.Next(10, 50));
            }

            return matrix;
        }
    }
}
