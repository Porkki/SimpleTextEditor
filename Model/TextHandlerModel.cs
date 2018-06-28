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
        private string _TextContent;

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
