using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
