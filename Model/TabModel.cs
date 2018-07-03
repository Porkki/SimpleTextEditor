using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextEditor.Model
{
    class TabModel : INotifyPropertyChanged
    {

        private int _SelectedTab;
        public int SelectedTab
        {
            get
            {
                return _SelectedTab;
            }
            set
            {
                if (value != _SelectedTab)
                {
                    _SelectedTab = value;
                    RaisePropertyChanged("SelectedTab");
                }
            }
        }

        private ObservableCollection<TabItems> _TabItems;
        public ObservableCollection<TabItems> TabItems
        {
            get
            {
                return _TabItems;
            }
            set
            {
                _TabItems = value;
                RaisePropertyChanged("TabItem");
            }
        }

        public TabModel()
        {
            TabItems = new ObservableCollection<TabItems>();
        }

        public bool NewTabIsValid()
        {
            return true;
        }
        private int x = 0;
        public void NewTabExecute()
        {
            TabItems.Add(new TabItems(x.ToString(), "ASD"));
            x++;
        }

        public bool RemoveTabIsValid()
        {
            return true;
        }

        public void RemoveTabExecute()
        {
            TabItems.RemoveAt(SelectedTab);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
