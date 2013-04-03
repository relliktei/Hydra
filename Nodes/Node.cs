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

namespace HYDRA.Nodes
{
    public class Node
    {
        //GUID - Global Unique Identifer used to map our nodes.
        private Guid _guid;
        public Guid GUID { get { return _guid; } set { _guid = value; } }

        //Input & Output connectors
        public List<Connector> Input = new List<Connector>();
        public List<Connector> Output = new List<Connector>();

        //Node Dimensions
        protected Int32 Height = 39;
        protected Int32 Width = 33;

        //Name of the node
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        //Value visual label
        public Label ValueLabel;

        //Node Location (Set onDraw event)
        private Point _location;
        public Point Location { get { return _location; } set { _location = value; } }

        //Draw Form Control
        private Control _panel;
        public Control Panel { get { return _panel; } set { _panel = value; } }

        //Value
        private float _value;
        public float Value { get { return _value; } set { _value = value; } }

        //ToolTip handler
        protected ToolTip _toolTip = new ToolTip();

        //Constructor
        public Node(Guid id, Control panel)
        {
            this.GUID = id;
            this.Panel = panel;
        }

        //Draw node
        public virtual void Draw(Point Location)
        {
            //Override logic on child nodes.
        }

        public void Draw(Point Location, Image image)
        {
            if (Location.X != 0 || Location.Y != 0)
                this.Location = Location;

            var nodePBox = new PictureBox();
            nodePBox.Image = image;
            nodePBox.Width = this.Width;
            nodePBox.Height = this.Height;
            nodePBox.BackColor = Color.Gray;
            nodePBox.Location = this.Location;
            //Node Events
            nodePBox.MouseClick += new MouseEventHandler(nodeMouseClick);
            nodePBox.MouseHover += nodeMouseHover;

            //Label that will display the result/value of the Node.
            var _valueLabel = new Label(); //Value Label
            _valueLabel.Width = this.Width;
            _valueLabel.Height = 13;
            _valueLabel.BackColor = Color.Gray;
            _valueLabel.Font = new Font(_valueLabel.Font, FontStyle.Bold);
            _valueLabel.Text = this.Value.ToString();
            _valueLabel.Location = new Point(this.Location.X, this.Location.Y + 33);
            _valueLabel.ForeColor = System.Drawing.Color.Gold;

            //Add Node And Label
            Panel.Controls.Add(nodePBox);
            Panel.Controls.Add(_valueLabel);
            //Bring the label to the front so its properly displayed.
            Panel.Controls[((_valueLabel) as Label).TabIndex].BringToFront();

            ValueLabel = _valueLabel;

            nodePBox.Show();
            _valueLabel.Show();
        }

        //Provides on Hover tooltip information about the node.
        private void nodeMouseHover(object sender, EventArgs e)
        {
            //_toolTip.InitialDelay = 0;
            //_toolTip.Show(this.Name + "\nInput count: " + Input.Count + " \nOutput count: " + Output.Count + "\nValue: " + Value, ValueLabel, 1700);
        }

        //Updates the GUID with the Node in which the mouse its placed. Used to connect nodes.
        private void nodeMouseClick(object sender, MouseEventArgs e)
        {
            if (Connector.TailMouseOverGuid != Guid.Empty)
            {
                Connector.HeadOverGuid = GUID;

                if (e.Button == MouseButtons.Right)
                {
                    Output.Add(new Connector(Connector.TailMouseOverGuid, Connector.HeadOverGuid, (Panel.CreateGraphics()) as Graphics));
                    return;
                }
                Input.Add(new Connector(Connector.TailMouseOverGuid, Connector.HeadOverGuid, (Panel.CreateGraphics()) as Graphics)); //Add the connection to the destination node Input list.

                //Debug
                Console.WriteLine("Log: " + this.Name + "|| Input count: " + Input.Count + " || Output count: " + Output.Count);
                return;
            }
            Connector.TailMouseOverGuid = GUID;
            //MessageBox.Show("Stablished TAIL");
        }

        //Logging
        public virtual String Log()
        {
            //Override logic on child nodes.
            return "";
        }

        //Node Logic
        public virtual float Process()
        {
            //Override logic on child nodes.
            return 0f;
        }
    }
}
