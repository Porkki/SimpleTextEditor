using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextEditor.Model
{
    class TabItems
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

        public string Header { get; set; }
        public string Content { get; set; }
    }
}
