using HYDRA.Properties;
using HydraLib.Nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace HYDRA
{
    public class DrawableNode
    {
        private Node _node;

      
        //Node Dimensions
        protected Int32 Height = 39;
        protected Int32 Width = 33;
        //Value visual label
        public Label ValueLabel;

        //Node Location (Set onDraw event)
        private Point _location;
        public Point Location { get { return _location; } set { _location = value; } }

        //Draw Form Control
        private Control _panel;
        public Control Panel { get { return _panel; } set { _panel = value; } }
        //ToolTip handler
        protected ToolTip _toolTip = new ToolTip();

        public DrawableNode(Node node,Control panel)
        {
            _panel = panel;
            _node = node;
        }

        public string Log()
        {
            return Environment.NewLine + "<<<New Action>>>" + Environment.NewLine + "Created " + this._node.Name + " node." + Environment.NewLine + "Position: " + this.Location + Environment.NewLine + "Guid: " + this._node.GUID + Environment.NewLine;
        }   
        public void Draw(Point Location)
        {
            if (Location != null && Location.X != 0 || Location.Y != 0)
                this.Location = Location;

            var nodePBox = new PictureBox();
            nodePBox.Image = getNodeImage(_node.Name); ;
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
            _valueLabel.Text = this._node.Value.ToString();
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

        private Image getNodeImage(string p)
        {
            ResourceManager rm = Resources.ResourceManager;
            Bitmap myImage = (Bitmap)rm.GetObject(p);
            return myImage;
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
            if (e.Button == MouseButtons.Right)
            {
                //Modify Value (FOR TESTING PURPOSES)
                Random r = new Random();
                this._node.Value = r.Next(20, 100);
                ValueLabel.Text = this._node.Value + "";
                return;
            }

            else if (Connector.TailMouseOverGuid != Guid.Empty)
            {
                Connector.HeadOverGuid = this._node.GUID;

                this._node.Input.Add(new DrawAbleConnector(Connector.TailMouseOverGuid, Connector.HeadOverGuid)); //Add the connection to the destination node Input list.

                //Debug
                Console.WriteLine("Log: " + this._node.Name + "|| Input count: " + this._node.Input.Count + " || Output count: " + this._node.Output.Count);
                return;
            }
            Connector.TailMouseOverGuid = this._node.GUID;
            //MessageBox.Show("Stablished TAIL");
        }



        public Guid GUID { get { return this._node.GUID; } }

        internal void Process(Dictionary<Guid, Node> AllNodes)
        {
            this.ValueLabel.Text = this._node.Process(AllNodes).ToString();
        }

        public Node GetNode() { return _node; }
    }
}
