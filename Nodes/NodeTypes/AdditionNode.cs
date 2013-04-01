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
    public class AdditionNode : Node
    {
        public AdditionNode(UInt32 id, Control panel)
            : base(id, panel)
        {
            this.Name = "Addition";
        }

        public override void Draw(Point Location)
        {
            if (Location.X != 0 || Location.Y != 0)
                this.Location = Location;

            var nodePBox = new PictureBox();
            nodePBox.Image = HYDRA.Properties.Resources.AdditionNode;
            nodePBox.Width = HYDRA.Properties.Resources.AdditionNode.Width;
            nodePBox.Height = HYDRA.Properties.Resources.AdditionNode.Height;
            nodePBox.Location = this.Location;
            nodePBox.MouseClick +=new MouseEventHandler(nodePBox_MouseClick);
            Panel.Controls.Add(nodePBox);
            nodePBox.Show();
            base.Draw(this.Location);
        }

        public override float Process()
        {
            if (Input.Count >= 2)
            {
                float a = Form1.AllNodes[Input[0].TailNodeGuid].Value;
                float b = Form1.AllNodes[Input[1].TailNodeGuid].Value;
                Console.WriteLine("Processed Addition node with " + Input.Count + " input elements the result was " + (a+b));
                if (Output.Count > 0)
                {
                    Form1.AllNodes[Output[0].TailNodeGuid].Value = a+b;
                    Form1.AllNodes[Output[0].TailNodeGuid].GUITEXT.Text = (a + b) + "";
                }
                return a + b;
            }
            //set the values of any nodes in the Output list.
            Console.WriteLine("Processed Addition node with " + Input.Count + " input elements");
            return 1f;
        }
        public override string Log()
        {
            return "<<<New Action>>>" + Environment.NewLine + "Created " + this.Name + " node." + Environment.NewLine + "Position: " + this.Location + Environment.NewLine + "Guid: " + this.GUID + Environment.NewLine;
        }

        //Updates the GUID with the Node in which the mouse its placed. Used to connect nodes.
        public void nodePBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Connector.TailMouseOverGuid > 0)
            {

                Connector.HeadOverGuid = GUID;
                if (e.Button == MouseButtons.Right)
                {
                    Output.Add(new Connector(Connector.TailMouseOverGuid, Connector.HeadOverGuid));
                    return;
                }
                Input.Add(new Connector(Connector.TailMouseOverGuid, Connector.HeadOverGuid));
            }
            else
            {
                Connector.TailMouseOverGuid = GUID;
                //MessageBox.Show("Stablished TAIL");
            }
        }
    }
}
