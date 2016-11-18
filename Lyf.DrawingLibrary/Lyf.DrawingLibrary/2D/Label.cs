using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyf.DrawingLibrary._2D
{
    /// <summary>
    /// Class that handles the data associated with text title and its associated font
    /// properties
    /// </summary>
    /// 
    [Serializable]
    public class Label
    {
        /// <summary>
        /// The "String" text to be displayed
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
