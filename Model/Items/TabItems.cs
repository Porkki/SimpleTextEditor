using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextEditor.Model
{
    class TabItems : INotifyPropertyChanged
    {
        /// <summary>
        /// Default constructor for TabItems
        /// </summary>
        /// <param name="header">Tab Name/Title</param>
        /// <param name="content"></param>
        public TabItems(string header, string content)
        {
            Header = header;
            Content = content;
        }
        private string _Header;
        public string Header
        {
            get
            {
                return _Header;
            }
            set
            {
                if (value != _Header)
                {
                    _Header = value;
                    RaisePropertyChanged("Header");
                }
            }
        }
        private string _Content;
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                if (value != _Content)
                {
                    _Content = value;
                    RaisePropertyChanged("Content");
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
