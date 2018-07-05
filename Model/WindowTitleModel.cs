using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextEditor.Model
{
    class WindowTitleModel : INotifyPropertyChanged
    {
        private string _WindowTitlePrefix = "Simple Text Editor";
        public string WindowTitle
        {
            get
            {
                if (_FileName == String.Empty || _FileName == null)
                {
                    return _WindowTitlePrefix;
                } else
                {
                    return string.Format("{0} - {1}", _WindowTitlePrefix, _FileName);
                }

            }
        }
        
        private string _FileName;
        public string FileName
        {
            set
            {
                if (value != _FileName)
                {
                    _FileName = value;
                    //If file name changes then the Windows title needs to be changed too so we notify the viewmodel about it
                    RaisePropertyChanged("WindowTitle");
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
