using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeatMap.Model.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void Matrix_CheckMaxNumberOfColumns_MaxNumberOfColumns()
        {
            var expected = 7;

            double[][] array = new double[4][]
            {
                new double[] { 1, 2, 3, 4, 5, 6, 7 },
                new double[] { 1, 2, 3, 4, 5 },
                new double[] { 1, 2, 3 },
                new double[] { 1, 2, 3, 4, 5, 6 }

            };

            Matrix matrix = new Matrix(array);
            var actual = matrix.MaxWidth;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Matrix_CheckMaxNumberOfRows_MaxNumberOfRows()
        {
            var expected = 5;

            double[][] array = new double[][]
            {
                new double[] { 1, 2, 3, 4 },
                new double[] { 1, 2, 3, 4, 5 },
                new double[] { 1, 2, 3 },
                new double[] { 1, 2, 3, 4, 5, 6 },
                new double[] { 1 }
            };

            Matrix matrix = new Matrix(array);
            var actual = matrix.MaxHeight;

            Assert.AreEqual(expected, actual);
        }
    }
}