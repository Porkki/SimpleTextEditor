using System;
using SimpleTextEditor.Command;
using SimpleTextEditor.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SimpleTextEditor.ViewModel
{
    class TextHandlerViewModel : INotifyPropertyChanged
    {
        //Creating an instance of the TextHandlerModel
        public TextHandlerModel HandlerModel { get; set; }

        //Creating an instance of the TabModel
        public TabModel TabsModel { get; set; }

        public WindowTitleModel TitleModel { get; set; }

        public StatusModel StatusModel { get; set; }

        //Commands (for buttons etc)
        public ButtonCommand NewCommand { get; private set; }
        public ButtonCommand OpenCommand { get; private set; }
        public ButtonCommand SaveAsCommand { get; private set; }
        public ButtonCommand SaveCommand { get; private set; }
        public ButtonCommand ExitCommand { get; private set; }

        public ButtonCommand NewTabCommand { get; private set; }
        public ButtonCommand RemoveTabCommand { get; private set; }

        public TextHandlerViewModel()
        {
            //Assigning handerlModel new instance of TextHandlerModel
            HandlerModel = new TextHandlerModel();
            TabsModel = new TabModel();
            TitleModel = new WindowTitleModel();
            StatusModel = new StatusModel();
            //We need to listen the modifications what happen in the model so we can tell the ui to update
            HandlerModel.PropertyChanged += HandlerModel_PropertyChanged;
            TabsModel.PropertyChanged += TabsModel_PropertyChanged;
            TitleModel.PropertyChanged += TitleModel_PropertyChanged;
            StatusModel.PropertyChanged += StatusModel_PropertyChanged;
            //Buttons
            NewCommand = new ButtonCommand(HandlerModel.NewExecute, HandlerModel.NewIsValid);
            OpenCommand = new ButtonCommand(HandlerModel.OpenExecute, HandlerModel.OpenIsValid);
            SaveAsCommand = new ButtonCommand(HandlerModel.SaveAsExecute, HandlerModel.SaveAsIsValid);
            SaveCommand = new ButtonCommand(HandlerModel.SaveExecute, HandlerModel.SaveIsValid, this);
            ExitCommand = new ButtonCommand(HandlerModel.ExitExecute, HandlerModel.ExitIsValid);

            NewTabCommand = new ButtonCommand(TabsModel.NewTabExecute, TabsModel.NewTabIsValid);
            RemoveTabCommand = new ButtonCommand(TabsModel.RemoveTabExecute, TabsModel.RemoveTabIsValid, this);
        }

        private void StatusModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Status":
                    Status = StatusModel.Message;
                    break;
            }
        }

        private void TitleModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "WindowTitle":
                    RaisePropertyChanged("WindowTitle");
                    break;
            }
        }

        private void TabsModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedTab":
                    TextContent = TabsModel.Content;
                    SelectedTab = TabsModel.SelectedTab;
                    break;
                case "FileName":
                    FileName = TabsModel.FileName;
                    break;
            }
        }

        /*If we receive information that something in models property is changed, 
         * we update it in viewmodel and therefore in the view too */
        private void HandlerModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "TextContent":
                    TextContent = HandlerModel.TextContent;
                    break;
                case "StatusMessage":
                    SetStatus(HandlerModel.StatusMessage);
                    break;
                case "FileName":
                    FileName = HandlerModel.FileName;
                    break;
            }
        }

        public string WindowTitle
        {
            get
            {
                return TitleModel.WindowTitle;
            }
        }

        public string FileName
        {
            get
            {
                return HandlerModel.FileName;
            }
            set
            {
                HandlerModel.FileName = value;
                TabsModel.FileName = value;
                TitleModel.FileName = value;
                RaisePropertyChanged("Header");
            }
        }

        //Getting and setting TextContent to handlerModel
        public string TextContent
        {
            get
            {
                return HandlerModel.TextContent;
            }
            set
            {
                HandlerModel.TextContent = value;
                TabsModel.Content = value;
                //If we change something ex. opening new file we raise propertychanged event to notify ui
                RaisePropertyChanged("TextContent");
            }
        }

        public void SetStatus(string msg)
        {
            StatusModel.Message = msg;
        }

        public string Status
        {
            get
            {
                return StatusModel.Message;
            }
            set
            {
                StatusModel.Message = value;
                RaisePropertyChanged("Status");
            }
        }

        public ObservableCollection<TabItems> TabItems
        {
            get
            {
                return TabsModel.TabItems;
            }
            set
            {
                TabsModel.TabItems = value;
                RaisePropertyChanged("TabItems");
            }
        }
        public int SelectedTab
        {
            get
            {
                return TabsModel.SelectedTab;
            }
            set
            {
                TabsModel.SelectedTab = value;
                RaisePropertyChanged("SelectedTab");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
