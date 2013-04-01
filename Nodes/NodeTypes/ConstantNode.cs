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
        public ConstantNode(Guid id, Control panel, float Value)
            : base(id, panel)
        {
            this.Value = Value;
            this.Name = "Constant";
        }

        public override void Draw(Point Location)
        {
            if(Location.X != 0 || Location.Y != 0)
                this.Location = Location;
            //Create a PictureBox object which will hold the Node visuals.
            var nodePBox = new PictureBox();
            //Event used to GET the GUID from the clicked node, used to make the proper connections.
            nodePBox.MouseClick += new MouseEventHandler(nodePBox_MouseClick);
            //General PBox properties.
            nodePBox.Image = HYDRA.Properties.Resources.Constant;
            nodePBox.Width = this.Width;
            nodePBox.Height = this.Height;
            nodePBox.Location = this.Location;
            //Add the new Pbox Object to the Parent form Panel control.
            Panel.Controls.Add(nodePBox);

            //Label that will display the value of the Node.
            var _valueLabel = new Label(); //Value Label
            _valueLabel.Text = this.Value + "";
            _valueLabel.Location = new Point(this.Location.X, this.Location.Y + 50);
            Panel.Controls.Add(_valueLabel);
            ValueLabel = _valueLabel;
            _valueLabel.Show();
            nodePBox.Show();
            base.Draw(this.Location);
        }

        public override string Log()
        {
            return "<<<New Action>>>" + Environment.NewLine + "Created " + this.Name + " node." + Environment.NewLine + "Value: " + this.Value.ToString() + Environment.NewLine + "Position: " + this.Location + Environment.NewLine + "Guid: " + this.GUID + Environment.NewLine + Environment.NewLine ;
        }

        //Updates the GUID with the Node in which the mouse its placed. Used to connect nodes.
        public void nodePBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Connector.TailMouseOverGuid != Guid.Empty)
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
            
        }
    }
}
