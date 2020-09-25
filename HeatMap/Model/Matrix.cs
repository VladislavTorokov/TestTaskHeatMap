using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatMap.Model
{
    public class Matrix
    {
        /// <summary>
        /// Количество столбцов
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Количество строк 
        /// </summary>
        public int Height { get; set; }

        private bool isSquare = true;

        public double MaxValue { get; set; }

        public double[][] elements { get; set; }

        public Matrix(double[][] data)
        {
            Height = data.Length;

            for (int i = 0; i < data.Length; i++)
            {
                if (i > 0)
                {
                    if (data[i - 1].Length != data[i].Length)
                    {
                        isSquare = false;
                        throw new Exception("матрица не прямоугольная");
                    }
                }
            }
            if (isSquare)
            {
                Width = data[0].Length;
                elements = data;
            }
            MaxValue = GetMax();
        }

        private double GetMax()
        {
            var max = elements.Select(x => x.Max()).Max();
            return max;
        }
    }
}
