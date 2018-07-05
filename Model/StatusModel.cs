using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextEditor.Model
{
    class StatusModel : INotifyPropertyChanged
    {
        private string _Message;
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                if (value != _Message)
                {
                    _Message = value;
                    RaisePropertyChanged("Status");
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
