﻿using System;
using SimpleTextEditor.Command;
using SimpleTextEditor.Model;
using System.ComponentModel;

namespace SimpleTextEditor.ViewModel
{
    class TextHandlerViewModel : INotifyPropertyChanged
    {
        //Creating an instance of the TextHandlerModel
        public TextHandlerModel handlerModel { get; set; }

        //Commands (for buttons etc)
        public ButtonCommand NewCommand { get; private set; }
        public ButtonCommand OpenCommand { get; private set; }
        public ButtonCommand SaveAsCommand { get; private set; }
        public ButtonCommand SaveCommand { get; private set; }

        public TextHandlerViewModel()
        {
            //Assigning handerlModel new instance of TextHandlerModel
            handlerModel = new TextHandlerModel();
            //We need to listen the modifications what happen in the model so we can tell the ui to update
            handlerModel.PropertyChanged += HandlerModel_PropertyChanged;
            //Buttons
            NewCommand = new ButtonCommand(handlerModel.NewExecute, handlerModel.NewIsValid);
            OpenCommand = new ButtonCommand(handlerModel.OpenExecute, handlerModel.OpenIsValid);
            SaveAsCommand = new ButtonCommand(handlerModel.SaveAsExecute, handlerModel.SaveAsIsValid);
            SaveCommand = new ButtonCommand(handlerModel.SaveExecute, handlerModel.SaveIsValid, this);
        }
        
        /*If we receive information that something in models property is changed, 
         * we update it in viewmodel and therefore in the view too */
        private void HandlerModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "TextContent":
                    TextContent = handlerModel.TextContent;
                    break;
                case "Status":
                    Status = handlerModel.Status;
                    break;
            }
        }

        //Getting and setting TextContent to handlerModel
        public string TextContent
        {
            get
            {
                return handlerModel.TextContent;
            }
            set
            {
                handlerModel.TextContent = value;
                //If we change something ex. opening new file we raise propertychanged event to notify ui
                RaisePropertyChanged("TextContent");
            }
        }

        public string Status
        {
            get
            {
                return handlerModel.Status;
            }
            set
            {
                handlerModel.Status = value;
                RaisePropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
