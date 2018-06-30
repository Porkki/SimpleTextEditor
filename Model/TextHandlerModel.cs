using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace SimpleTextEditor.Model
{
    class TextHandlerModel : INotifyPropertyChanged
    {
        //Stores what text we have currently in file and/or in texteditor
        private string _TextContent;
        private string _FilePath;
        private string _Status;

        public string TextContent
        {
            get
            {
                return _TextContent;
            }
            set
            {
                if (value != _TextContent)
                {
                    _TextContent = value;
                    RaisePropertyChanged("TextContent");
                }
            }
        }

        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                if (value != _FilePath)
                {
                    _FilePath = value;
                    RaisePropertyChanged("FilePath");
                }
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if (value != _Status)
                {
                    _Status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }

        //At this point littlebit unnessecary...
        private void SetStatus(string message)
        {
            Status = message;
        }

        #region CommandStuff
        public bool NewIsValid()
        {
            return true;
        }
        public void NewExecute()
        {
            SetStatus("Creating new file...");
            FilePath = "";
            TextContent = "";
            SetStatus("New file created!");
        }

        public bool OpenIsValid()
        {
            return true;
        }

        public void OpenExecute()
        {
            SetStatus("Opening file...");
            //Creates new OpenFileDialog called openFile
            OpenFileDialog openFile = new OpenFileDialog();

            /*Opens the file dialog and sets the ViewModels 
             * textcontent to whatever file user decides to open */
            if (openFile.ShowDialog() == true)
            {
                this.TextContent = File.ReadAllText(openFile.FileName);
                RaisePropertyChanged("TextContent");
                //We save the path so we can enable the save button
                this.FilePath = openFile.FileName;
                SetStatus(String.Format("File opened: {0}", this.FilePath));
            }
        }

        public bool SaveAsIsValid()
        {
            return true;
        }
        public void SaveAsExecute()
        {
            SetStatus("Saving file...");
            SaveFileDialog saveFile = new SaveFileDialog();

            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, this.TextContent);
                this.FilePath = saveFile.FileName;
                SetStatus(String.Format("File saved to: {0}", saveFile.FileName));
            }
        }

        public bool SaveIsValid()
        {
            //We grey out the button if user has not opened any files
            if (this.FilePath == string.Empty || this.FilePath == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void SaveExecute()
        {
            File.WriteAllText(this.FilePath, this.TextContent);
            SetStatus(String.Format("File saved to: {0}", this.FilePath));
        }

        public bool ExitIsValid()
        {
            return true;
        }
        public void ExitExecute()
        {
            Application.Current.MainWindow.Close();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
