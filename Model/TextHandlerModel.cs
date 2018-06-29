using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;

namespace SimpleTextEditor.Model
{
    class TextHandlerModel : INotifyPropertyChanged
    {
        //Stores what text we have currently in file and/or in texteditor
        private string _TextContent;

        private string _FilePath;

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

        #region CommandStuff
        public bool OpenIsValid()
        {
            return true;
        }

        public void OpenExecute()
        {
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
            }
        }

        public bool SaveAsIsValid()
        {
            return true;
        }
        public void SaveAsExecute()
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, this.TextContent);
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
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
