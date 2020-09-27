using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeatMap.Model.Tests
{
    [TestClass()]
    public class CsvReaderTests
    {
        [TestMethod()]
        public void CsvReader_3x5MatrixEqualityCheck_3x5MatrixEqual()
        {
            double[][] expected = new double[3][]
                {
                new double[] { 12.56,   13.55,  14.54,  15.53,  16.52},
                new double[] { 97.1,    99.1,   103.1,  107.1,  115.1},
                new double[] { 205.123, 234.653,19.54,  70.43,  102.32 }
            };

            var path = System.IO.Path.GetFullPath(@"Data\Matrix3x5.csv");
            CsvReader csvReader = new CsvReader(path);
            var actual = csvReader.GetData();

            for (int i = 0; i < expected.Length; i++)
            {
                CollectionAssert.AreEqual(expected[i], actual[i], "Не все элементы матриц равны");
            }
        }
    }
}