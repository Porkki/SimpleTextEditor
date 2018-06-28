using Microsoft.Win32;
using SimpleTextEditor.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTextEditor.ViewModel
{
    class TextHandlerViewModel : INotifyPropertyChanged
    {
        public TextHandlerModel handlerModel { get; set; }

        //Commands (for buttons etc)
        public ICommand OpenCommand { get; private set; }

        public TextHandlerViewModel()
        {
            handlerModel = new TextHandlerModel();
            OpenCommand = new FileOpenCommand(this);
        }

        public string TextContent
        {
            get
            {
                return handlerModel.TextContent;
            }
            set
            {
                handlerModel.TextContent = value;
                RaisePropertyChanged("TextContent");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }

    class FileOpenCommand : ICommand
    {
        TextHandlerViewModel parent;

        public FileOpenCommand(TextHandlerViewModel parent)
        {
            this.parent = parent;
            parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == true)
            {
                parent.TextContent = File.ReadAllText(openFile.FileName);
            }
        }

    }
}
