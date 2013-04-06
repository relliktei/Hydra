using HydraLib.Nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HYDRA
{
    public class DrawAbleConnector : Connector
    {

        private System.Windows.Forms.Panel graphPanel;

        public DrawAbleConnector(Guid Tail, Guid Head)
            : base(Tail, Head)
        { }

        public void Draw(Graphics panelGraphics, Dictionary<Guid, DrawableNode> allNodes)
        {
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.DodgerBlue, 3f);
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            panelGraphics.DrawLine(myPen, allNodes[TailNodeGuid].Location.X + 25, allNodes[TailNodeGuid].Location.Y + 25, allNodes[HeadNodeGuid].Location.X + 25, allNodes[HeadNodeGuid].Location.Y + 25);
            myPen.Dispose();
            panelGraphics.Dispose();
        }
    }
}
