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
    public class SubstractionNode : Node
    {
        public SubstractionNode(Guid id, Control panel)
            : base(id, panel)
        {
            this.Name = "Substraction";
        }

        public override void Draw(Point Location)
        {
            if (Location.X != 0 || Location.Y != 0)
                this.Location = Location;

            var nodePBox = new PictureBox();
            nodePBox.Image = HYDRA.Properties.Resources.Substraction;
            nodePBox.Width = this.Width;
            nodePBox.Height = this.Height;
            nodePBox.BackColor = Color.Gray;
            nodePBox.Location = this.Location;
            nodePBox.MouseClick += new MouseEventHandler(nodePBox_MouseClick);

            //Label that will display the result/value of the Node.
            var _valueLabel = new Label(); //Value Label
            _valueLabel.Width = this.Width;
            _valueLabel.Height = 13;
            _valueLabel.BackColor = Color.Gray;
            _valueLabel.Font = new Font(_valueLabel.Font, FontStyle.Bold);
            _valueLabel.Text = this.Value + "";
            _valueLabel.Location = new Point(this.Location.X, this.Location.Y + 48);
            _valueLabel.ForeColor = System.Drawing.Color.Gold;

            //Add Node And Label
            Panel.Controls.Add(nodePBox);
            Panel.Controls.Add(_valueLabel);
            Panel.Controls[((_valueLabel) as Label).TabIndex].BringToFront();

            ValueLabel = _valueLabel;

            nodePBox.Show();
            _valueLabel.Show();
            base.Draw(this.Location);
        }

        public override float Process()
        {
            //We concatenate the different input values in here:
            float Sum = 0;

            if (Input.Count >= 2)
            {
                for (int i = 0; i < Input.Count; i++)
                {
                    //MessageBox.Show(Form1.AllNodes[Input[i].TailNodeGuid].Value.ToString());
                    var _floatValue = Form1.AllNodes[Input[i].TailNodeGuid].Value;
                    Sum -= _floatValue;
                }
                Console.WriteLine("Log: " + this.Name + "|| Processed an addition with " + Input.Count + " input elements the result was " + Sum);

                this.Value = Sum;
                this.ValueLabel.Text = Sum + "";
            }
            return 1f;
        }

        public override string Log()
        {
            return Environment.NewLine + "<<<New Action>>>" + Environment.NewLine + "Created " + this.Name + " node." + Environment.NewLine + "Position: " + this.Location + Environment.NewLine + "Guid: " + this.GUID + Environment.NewLine;
        }

        //Updates the GUID with the Node in which the mouse its placed. Used to connect nodes.
        public void nodePBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Connector.TailMouseOverGuid != Guid.Empty)
            {
                Connector.HeadOverGuid = GUID;
                if (e.Button == MouseButtons.Right)
                {
                    Output.Add(new Connector(Connector.TailMouseOverGuid, Connector.HeadOverGuid, (Panel.CreateGraphics()) as Graphics));
                    var _last = Output.Last();
                    return;
                }
                Input.Add(new Connector(Connector.TailMouseOverGuid, Connector.HeadOverGuid, (Panel.CreateGraphics()) as Graphics)); //Add the connection to the node Input list.
                //Input.Last().Draw((Panel.CreateGraphics()) as Graphics, new Point(0, 0), new Point(415, 190)); //Draw the Connector Line

                Console.WriteLine("Log: " + this.Name + "|| Input count: " + Input.Count + " || Output count: " + Output.Count);
            }
            else
            {
                Connector.TailMouseOverGuid = GUID;
                //MessageBox.Show("Stablished TAIL");
            }
        }
    }
}

