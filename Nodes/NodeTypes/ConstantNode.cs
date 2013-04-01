// Copyright (C) 2013 Iker Ruiz Arnauda
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HYDRA.Nodes.NodeTypes
{
    public class ConstantNode : Node
    {
        public ConstantNode(UInt32 id, Control panel, float Value)
            : base(id, panel)
        {
            this.Value = Value;
            this.Name = "Constant";
        }

        public override void Draw(Point Location)
        {
            if(Location.X != 0 || Location.Y != 0)
                this.Location = Location;

            var nodePBox = new PictureBox();
            nodePBox.Image = HYDRA.Properties.Resources.Constant;
            nodePBox.Width = HYDRA.Properties.Resources.AdditionNode.Width;
            nodePBox.Height = HYDRA.Properties.Resources.AdditionNode.Height;
            nodePBox.Location = this.Location;
            nodePBox.MouseClick += new MouseEventHandler(nodePBox_MouseClick);
            Panel.Controls.Add(nodePBox);
            var val = new Label();
            val.Text = this.Value + "";
            val.Location = new Point(this.Location.X, this.Location.Y + 50);
            Panel.Controls.Add(val);
            GUITEXT = val;
            val.Show();
            nodePBox.Show();
            base.Draw(this.Location);
        }

        public override string Log()
        {
            return "<<<New Action>>>" + Environment.NewLine + "Created " + this.Name + " node." + Environment.NewLine + "Value: " + this.Value.ToString() + Environment.NewLine + "Position: " + this.Location + Environment.NewLine + "Guid: " + this.GUID + Environment.NewLine ;
        }

        //Updates the GUID with the Node in which the mouse its placed. Used to connect nodes.
        public void nodePBox_MouseClick(object sender, MouseEventArgs e)
        {
            /*if (Form1.bUsingConnector == false)
            {
                Console.WriteLine(Input.Count + "   " + Output.Count);
            }*/
            if (Connector.TailMouseOverGuid > 0)
            {
                Connector.HeadOverGuid = GUID;
                if (e.Button == MouseButtons.Right)
                {
                    Output.Add(new Connector(Connector.TailMouseOverGuid, Connector.HeadOverGuid));
                }
                Input.Add(new Connector(Connector.TailMouseOverGuid, Connector.HeadOverGuid));
            }
            else
            {
                Connector.TailMouseOverGuid = GUID;
                //MessageBox.Show("Stablished TAIL");
            }
            Console.WriteLine(Input.Count + "   " + Output.Count);
        }
    }
}
