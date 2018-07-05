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
        private string _FileName;
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
                    TabItems[SelectedTab].Header = FileName;
                    RaisePropertyChanged("FileName");
                }
            }
        }

        private int _SelectedTab;
        public int SelectedTab
        {
            get
            {
                return _SelectedTab;
            }
            set
            {
                if (value != _SelectedTab && value != -1) //SelectedTab goes -1 if no tab is selected ex. when tab is removed
                {
                    _SelectedTab = value;
                    //Set the Content to the correct tab content
                    Content = TabItems[SelectedTab].Content;
                    RaisePropertyChanged("SelectedTab");
                } else if (value == -1) //We default the view to the first tab in the TabItems
                {
                    _SelectedTab = 0;
                    Content = TabItems[SelectedTab].Content;
                    RaisePropertyChanged("SelectedTab");
                }
            }
        }

        private string _Content;
        public string Content
        {
            get
            {
                return TabItems[SelectedTab].Content;
            }
            set
            {
                if (value != _Content)
                {
                    _Content = value;
                    TabItems[SelectedTab].Content = value;
                    RaisePropertyChanged("Content");
                }
            }
        }

        /// <summary>
        /// Creating new ObservableCollection of TabItems where we store information about tabs
        /// </summary>
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
            //Intializing TabItems
            TabItems = new ObservableCollection<TabItems>();
            //Adding default tab
            TabItems.Add(new TabItems("Default", ""));
        }

        public bool NewTabIsValid()
        {
            return true;
        }
        private int x = 0;
        public void NewTabExecute()
        {
            string tabname = String.Format("New File {0}", x.ToString());
            TabItems.Add(new TabItems(tabname, ""));
            SelectedTab = TabItems.Count - 1; //Move view to new tab
            FileName = tabname;
            x++;
        }

        public bool RemoveTabIsValid()
        {
            if (TabItems.Count <= 1)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public void RemoveTabExecute()
        {
            int TempIndex = SelectedTab;
            TabItems.RemoveAt(SelectedTab);
            if (TempIndex == 1)
            {
                SelectedTab = 0;
            } else
            {
                SelectedTab = TempIndex - 1;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
