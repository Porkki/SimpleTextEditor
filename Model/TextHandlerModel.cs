﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;

namespace SimpleTextEditor.Model
{
    class TextHandlerModel : INotifyPropertyChanged
    {
        //Stores what text we have currently in file and/or in texteditor
        private string _TextContent;
        private string _FileName;
        //Stores opened or saved as.. filepath for enabling Save button
        private string FilePath;

        //Variables to see if text is modified after file open
        private string _LoadedText;
        private bool _TextChanged = false;

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

                    if (_LoadedText != _TextContent)
                    {
                        _TextChanged = true;
                    }
                }
            }
        }

        private string _StatusMessage;
        public string StatusMessage
        {
            get
            {
                return _StatusMessage;
            }
            set
            {
                if (value != _StatusMessage)
                {
                    _StatusMessage = value;
                    RaisePropertyChanged("StatusMessage");
                }
            }
        }

        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                if (value != _FileName)
                {
                    _FileName = value;
                    RaisePropertyChanged("FileName");
                }

            }
        }

        #region CommandStuff
        public bool NewIsValid()
        {
            return true;
        }
        public void NewExecute()
        {
            StatusMessage = "Creating new file...";
            FilePath = "";
            TextContent = "";
            FileName = "New File";
            StatusMessage = "New file created!";
        }

        public bool OpenIsValid()
        {
            return true;
        }

        public void OpenExecute()
        {
            StatusMessage = "Opening file...";
            //Creates new OpenFileDialog called openFile
            OpenFileDialog openFile = new OpenFileDialog();

            /*Opens the file dialog and sets the ViewModels 
             * textcontent to whatever file user decides to open */
            if (openFile.ShowDialog() == true)
            {
                _TextChanged = false;
                _LoadedText = File.ReadAllText(openFile.FileName);
                TextContent = File.ReadAllText(openFile.FileName);
                FileName = openFile.SafeFileName;
                RaisePropertyChanged("TextContent");
                //We save the path so we can enable the save button
                FilePath = openFile.FileName;
                StatusMessage = String.Format("File opened: {0}", this.FilePath);
            }
        }

        public bool SaveAsIsValid()
        {
            return true;
        }
        public void SaveAsExecute()
        {
            StatusMessage = "Saving file...";
            SaveFileDialog saveFile = new SaveFileDialog();

            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, this.TextContent);
                FilePath = saveFile.FileName;
                FileName = saveFile.SafeFileName;
                StatusMessage = String.Format("File saved to: {0}", saveFile.FileName);
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
            StatusMessage = String.Format("File saved to: {0}", this.FilePath);
        }

        public bool ExitIsValid()
        {
            return true;
        }
        public void ExitExecute()
        {
            if (this._TextChanged)
            {
                MessageBoxResult ExitConfirm = MessageBox.Show("Are you sure you want to exit without saving?", "Confirmation",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ExitConfirm == MessageBoxResult.Yes)
                {
                    Application.Current.MainWindow.Close();
                }
            } else
            {
                Application.Current.MainWindow.Close();
            }
            
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
