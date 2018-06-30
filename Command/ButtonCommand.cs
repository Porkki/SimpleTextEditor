using SimpleTextEditor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTextEditor.Command
{
    class ButtonCommand : ICommand
    {
        private Action _Action;
        private Func<bool> _Function;
        private TextHandlerViewModel parent;

        public ButtonCommand(Action action, Func<bool> function)
        {
            _Action = action;
            _Function = function;
        }

        /// <summary>
        /// This is used when we need dynamic canexecute (greyed out) button.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="function"></param>
        /// <param name="parent">Pass "this" keyword from TextHandlerViewModel</param>
        public ButtonCommand(Action action, Func<bool> function, TextHandlerViewModel parent)
        {
            _Action = action;
            _Function = function;

            this.parent = parent;
            parent.PropertyChanged += delegate { CanExecuteChanged?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _Function();
        }

        public void Execute(object parameter)
        {
            _Action();
        }

    }
}
