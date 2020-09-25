using DocumentFormat.OpenXml.InkML;
using HeatMap.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HeatMap.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private WriteableBitmap writeableBitmap;

        public WriteableBitmap _writeableBitmap
        {
            get 
            {
                writeableBitmap = new Model.HeatMap().wb;
                return writeableBitmap;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
