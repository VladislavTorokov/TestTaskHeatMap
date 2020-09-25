using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatMap.Model
{
    public class CsvReader
    {
        List<double[]> data;
        public CsvReader(string filePath)
        {
            data = new List<double[]>();

            using (TextFieldParser tfp = new TextFieldParser(filePath))
            {
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(",");

                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

                while (!tfp.EndOfData)
                {
                    var doubleArray = tfp.ReadFields().Select(x => double.Parse(x, formatter)).ToArray();
                    data.Add(doubleArray);
                }
            }
        }

        public double[][] GetData()
        {
            return data.ToArray();
        }
    }
}
