using System.Linq;

namespace HeatMap.Model
{
    public class Matrix
    {
        /// <summary>
        /// Максимальное количество столбцов в матрице
        /// </summary>
        public int MaxWidth { get; set; }
        /// <summary>
        /// Максимальное количество строк в матрице
        /// </summary>
        public int MaxHeight { get; set; }

        public double[][] Elements { get; set; }

        public Matrix(double[][] data)
        {
            MaxHeight = data.Length;

            //Выбираем самую максимальную длину строки
            MaxWidth = data.Select(x => x.Length).ToArray().Max();

            Elements = data;
        }
    }
}
