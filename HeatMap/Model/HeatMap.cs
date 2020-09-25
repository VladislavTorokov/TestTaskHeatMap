using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Vml;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeatMap.Model
{
    public class HeatMap
    {
        public WriteableBitmap wb { get; }

        Int32Rect rect;

        byte[] pixels;

        public HeatMap()
        {
            var path = System.IO.Path.GetFullPath("DataExample.csv");
            CsvReader csvReader = new CsvReader(path);
            var data = csvReader.GetData();

            Matrix matrix = new Matrix(data);

            var overallRatio = GetOverallRatio(matrix);

            wb = new WriteableBitmap(matrix.Width, matrix.Height, 1, 1*overallRatio, PixelFormats.Bgra32, null);
            rect = new Int32Rect(0, 0, matrix.Width, matrix.Height);
            pixels = new byte[matrix.Width * matrix.Height * wb.Format.BitsPerPixel / 8];
            SetColor(matrix.elements);
        }

        public void SetColor(double[][] matrix)
        {
            var maxValue = 255;
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int column = 0; column < matrix[row].Length; column++)
                {
                    var heatColor = CreateHeatColor(matrix[row][column], maxValue);
                    int pixelOffset = (column + row * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = heatColor[0];
                    pixels[pixelOffset + 1] = heatColor[1];
                    pixels[pixelOffset + 2] = heatColor[2];
                    pixels[pixelOffset + 3] = heatColor[3];
                }
                int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;

                var array = pixels.ToArray();
                wb.WritePixels(rect, array, stride, 0);
            }
        }

        private double GetOverallRatio(Matrix matrix)
        {
            var imageWidth = 800;
            var imageHeight = 420;
            double imageAspectRatio = imageWidth / (imageHeight * 1.0);
            double matrixAspectRatio = matrix.Width / (matrix.Height * 1.0);
            return imageAspectRatio / matrixAspectRatio;
        }


        //C этим разобраться
        private byte[] CreateHeatColor(double value, double max)
        {
            if (max == 0) max = 255;
            var pct = value / max;

            byte blue;
            byte green;
            byte red;
            byte alpha = 255;

            if (pct < 0.34)
            {
                blue = (byte)(255 - (127 * Math.Min(3 * pct, 1)));
                green = 0;
                red = 0;
            }
            else if (pct < 0.67)
            {
                blue = 64;
                green = (byte)(255 * Math.Min(3 * (pct - 0.333333), 1));
                red = 255;
            }
            else
            {
                blue = 0;
                green = (byte)(255 * Math.Min(3 * (1 - pct), 1));
                red = 255;
            }

            var color = new byte[] { blue, green, red, alpha };
            return color;
        }
    }
}
