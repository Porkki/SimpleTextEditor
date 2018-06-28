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
        //Creating an instance of the TextHandlerModel
        public TextHandlerModel handlerModel { get; set; }

        //Commands (for buttons etc)
        public ICommand OpenCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public TextHandlerViewModel()
        {
            //Assigning handerlModel new instance of TextHandlerModel
            handlerModel = new TextHandlerModel();
            OpenCommand = new FileOpenCommand(this);
            SaveAsCommand = new FileSaveAsCommand(this);
            SaveCommand = new FileSaveCommand(this);
        }

        //Getting and setting TextContent to handlerModel
        public string TextContent
        {
            get
            {
                return handlerModel.TextContent;
            }
            set
            {
                handlerModel.TextContent = value;
                //If we change something ex. opening new file we raise propertychanged event to notify ui
                RaisePropertyChanged("TextContent");
            }
        }

        public string FilePath
        {
            get
            {
                return handlerModel.FilePath;
            }
            set
            {
                handlerModel.FilePath = value;
                RaisePropertyChanged("FilePath");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }

    class FileSaveCommand : ICommand
    {
        //We make new ViewModel object called parent
        TextHandlerViewModel parent;

        public FileSaveCommand(TextHandlerViewModel parent)
        {
            //Assigns FileOpenCommand parent to TextHandlerViewModel so we can access the data
            this.parent = parent;
            //This is black magic
            parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            //We grey out the button if user has not opened any files
            if (parent.FilePath == string.Empty || parent.FilePath == null)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            File.WriteAllText(parent.FilePath, parent.TextContent);
        }

    }

    class FileSaveAsCommand : ICommand
    {
        //We make new ViewModel object called parent
        TextHandlerViewModel parent;

        public FileSaveAsCommand(TextHandlerViewModel parent)
        {
            //Assigns FileOpenCommand parent to TextHandlerViewModel so we can access the data
            this.parent = parent;
            //This is black magic
            parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //Creates new SaveFileDialog called saveFile
            SaveFileDialog saveFile = new SaveFileDialog();

            //Opens the file dialog and saves the file to user specified location
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, parent.TextContent);
            }
        }

    }

    class FileOpenCommand : ICommand
    {
        //We make new ViewModel object called parent
        TextHandlerViewModel parent;
        
        public FileOpenCommand(TextHandlerViewModel parent)
        {
            //Assigns FileOpenCommand parent to TextHandlerViewModel so we can access the data
            this.parent = parent;
            //This is black magic
            parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //Creates new OpenFileDialog called openFile
            OpenFileDialog openFile = new OpenFileDialog();

            /*Opens the file dialog and sets the ViewModels 
             * textcontent to whatever file user decides to open */
            if (openFile.ShowDialog() == true)
            {
                parent.TextContent = File.ReadAllText(openFile.FileName);

                //We save the path so we can enable the save button
                parent.FilePath = openFile.FileName;
            }
        }

    }
}
