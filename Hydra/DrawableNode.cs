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
        //Event handlers to pass click events to main form
        public delegate void NodeClickedHandler(DrawableNode sender, MouseEventArgs e);
        public event NodeClickedHandler onNodeClick;

        //The real node
        private Node _node;

        //Global Unique Identifer from the real node.
        public Guid GUID { get { return this._node.GUID; } set { _node.GUID = value; } }

        //Node VarWatch ListView
        private ListView _varwatch;
        public ListView VarWatch { get { return _varwatch; } set { _varwatch = value; } }

        //Helpers to access node data
        public Node GetNode() { return _node; }

        public string Name { get { return _node.Name; } set { _node.Name = value; } }
        public float Value { get { return _node.Value; } set { _node.Value = value; ValueLabel.Text = value + ""; } }
        public List<Connector> Input { get { return _node.Input; } set { _node.Input = value; } }
        public List<Connector> Output { get { return _node.Output; } set { _node.Output = value; } }

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

        //Pbox that holds the node "inside"
        PictureBox _nodePBox;

        /// <summary>
        /// Pass it a type like AdditionNode, the panel to draw in and a ListView that will display nodes values.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="Panel"></param>
        /// <param name="ListView"></param>
        public DrawableNode(Node node, Control panel, ListView varwatch)
        {
            _panel = panel;
            _node = node;
            _varwatch = varwatch;
            //Start to move more logic out of draw so it can be recalled to update the visible node without adding more events etc - Lotus
            _nodePBox = new PictureBox();
            _nodePBox.MouseClick += new MouseEventHandler(nodeMouseClick);
            _nodePBox.MouseHover += nodeMouseHover;
            _nodePBox.ContextMenu = buildContextMenu();
        }

        /// <summary>
        /// Todo: probably should call _node.Log() here as well
        /// Todo: Implement proper Logger class in order to attach a logger to each node. Keeping record of node operations and error handling.
        /// </summary>
        /// <returns></returns>
        public string Log()
        {
            return Environment.NewLine + "<<<New Action>>>" + Environment.NewLine + "Created " + this._node.Name + " node." + Environment.NewLine + "Position: " + this.Location + Environment.NewLine + "Guid: " + this._node.GUID + Environment.NewLine;
        }

        public void Draw(Point Location)
        {
            if (Location != null && Location.X != 0 || Location.Y != 0)
                this.Location = Location;

            _nodePBox.Image = getNodeImage(_node.Name); ;
            _nodePBox.Width = this.Width;
            _nodePBox.Height = this.Height;
            _nodePBox.BackColor = Color.Gray;
            _nodePBox.Location = this.Location;
            //Node Events

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
            Panel.Controls.Add(_nodePBox);
            Panel.Controls.Add(_valueLabel);
            //Bring the label to the front so its properly displayed.
            Panel.Controls[((_valueLabel) as Label).TabIndex].BringToFront();

            ValueLabel = _valueLabel;

            _nodePBox.Show();
            _valueLabel.Show();
        }

        //Context menu for node
        private ContextMenu buildContextMenu()
        {
            ContextMenu m = new ContextMenu();
            MenuItem changeval = new MenuItem("Set Value", new EventHandler(setValueClick));
            MenuItem watch = new MenuItem("Add Watch", new EventHandler(onWatchClick));
            m.MenuItems.Add(watch);
            m.MenuItems.Add(changeval);
            return m;
        }

        //Add node to the watchVariableList
        private void onWatchClick(object sender, EventArgs e)
        {
            string[] value_guid = { this.Value.ToString(), this.GUID.ToString() };
            this.VarWatch.Items.Add(this.Name).SubItems.AddRange(value_guid);
        }

        //Set Value
        private void setValueClick(object sender, EventArgs e)
        {
            InputBox b = new InputBox();
            if (b.ShowDialog() == DialogResult.OK)
                this.Value = b.GetInputValue();
        }

        /// <summary>
        /// Uses reflection to get the nodes image, we use the name as its the same as the image name luckily -Lotus
        /// </summary>
        /// <param name="p">name of the node ie Addition</param>
        /// <returns>the image to use</returns>
        private Image getNodeImage(string p)
        {
            ResourceManager rm = Resources.ResourceManager;
            Bitmap myImage = (Bitmap)rm.GetObject(p);
            return myImage;
        }

        //Provides on Hover tooltip information about the node. KINDA ANNOYING Lotus ;) - Iker
        private void nodeMouseHover(object sender, EventArgs e)
        {
            //_toolTip.InitialDelay = 0;
            //_toolTip.Show(this.Name + "\nInput count: " + Input.Count + " \nOutput count: " + Output.Count + "\nValue: " + Value, ValueLabel, 1700);
        }

        //Todo: Implement proper variable values watcher in a ListView.
        private void nodeMouseClick(object sender, MouseEventArgs e)
        {
            //Fire the event form1 is subscribed to, send it the mouseevent, plus this drawable node.
            if (onNodeClick != null)
                onNodeClick(this, e);

            return;
        }

        /// <summary>
        /// We pass Process() along to _node, and set the label to the result.
        /// </summary>
        /// <param name="AllNodes"></param>
        internal void Process(Dictionary<Guid, Node> AllNodes)
        {
            this.ValueLabel.Text = this._node.Process(AllNodes).ToString();
        }
    }
}
