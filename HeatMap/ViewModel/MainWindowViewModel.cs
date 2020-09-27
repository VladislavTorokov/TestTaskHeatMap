using HeatMap.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HeatMap.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            dialogService = new DefaultDialogService();
        }

        #region OpenFile
        IDialogService dialogService;

        // Команда открытия файла
        private ICommand openCommand;
        public ICommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new DelegateCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              Path = dialogService.FilePath;
                              dialogService.ShowMessage("Файл открыт");
                              OnPropertyChanged("Path");
                          }
                          else
                          {
                              dialogService.ShowMessage("Данный тип файла не поддерживается");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }
        #endregion

        #region GetHeatMap
        string path;
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("_writeableBitmap");
            }
        }

        private WriteableBitmap writeableBitmap;
        public WriteableBitmap _writeableBitmap
        {
            get
            {
                if (Path != null)
                    return writeableBitmap = new Model.HeatMap(Path).wb;
                return null;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
