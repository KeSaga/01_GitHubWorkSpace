using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyf.DrawingLibrary._2D
{
    public class PaneBase
    {
        protected bool _isPenWidthScaled;

        public float ScaledPenWidth(float penWidth, float scaleFactor)
        {

            if (_isPenWidthScaled) return (float)(penWidth * scaleFactor);
            else return penWidth;

        }
        
    }
}
