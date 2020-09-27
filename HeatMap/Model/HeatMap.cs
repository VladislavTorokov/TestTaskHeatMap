using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeatMap.Model
{
    public class HeatMap
    {
        public WriteableBitmap wb { get; }

        private Int32Rect rect;

        public HeatMap(string path)
        {
            #region GetMatrix
            CsvReader csvReader = new CsvReader(path);               //Принимаем и считываем
            var data = csvReader.GetData();                          //Получаем данные

            Matrix matrix = new Matrix(data);
            #endregion

            var overallRatio = GetOverallRatio(matrix);//Получаем коэффициент соотношения сторон matrix 
                                                       //и window для вписывания HeatMap в image
            //Инициализация WriteableBitmap и его составляющих
            wb = new WriteableBitmap(matrix.MaxWidth, matrix.MaxHeight, 1, 1 * overallRatio, PixelFormats.Bgra32, null);
            rect = new Int32Rect(0, 0, matrix.MaxWidth, matrix.MaxHeight);

            var pixels = new byte[matrix.MaxWidth * matrix.MaxHeight * wb.Format.BitsPerPixel / 8];
            SetColor(matrix.Elements, pixels);
        }

        //Заполняет массив pixels цветом
        private void SetColor(double[][] matrix, byte[] pixels)
        {
            ColorHeatMap colorHeat = new ColorHeatMap();
            var maxValue = 255;
            byte alpha = 255;
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int column = 0; column < matrix[row].Length; column++)
                {
                    var color = colorHeat.GetColorForValue(matrix[row][column], maxValue);
                    int pixelOffset = (column + row * wb.PixelWidth) * wb.Format.BitsPerPixel / 8;
                    pixels[pixelOffset] = color.B;
                    pixels[pixelOffset + 1] = color.G;
                    pixels[pixelOffset + 2] = color.R;
                    pixels[pixelOffset + 3] = alpha;
                }
                int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;

                var arrayPixels = pixels.ToArray();
                wb.WritePixels(rect, arrayPixels, stride, 0);
            }
        }

        private double GetOverallRatio(Matrix matrix)
        {
            var imageWidth = 800;  //Дефолтные значения window
            var imageHeight = 400; //image.Height = window.Height - 50

            double imageAspectRatio = imageWidth / (imageHeight * 1.0);
            double matrixAspectRatio = matrix.MaxWidth / (matrix.MaxHeight * 1.0);

            return imageAspectRatio / matrixAspectRatio;
        }
    }
}
