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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HydraLib.Nodes;
using HydraLib.Nodes.NodeTypes;

namespace HYDRA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //WiP - Add all Node types to a combo box using reflection, save needing a new button for each type right now, or adding them by hand.
            // Not currently active.
            foreach (var x in FindSubClassesOf<Node>())
            {
                toolStripToolSelectorComboBox.Items.Add(new ComboBoxObject(x, x.Name));
            }
            toolStripToolSelectorComboBox.Items.Add(new ComboBoxObject(null, "No Tool"));
        }
 
        //Used to store logic nodes.
        private List<DrawableNode> _LogicNodes = new List<DrawableNode>();

        //Store all nodes using GUID as key and ONode as value.
        //Todo: should be only one list, requires some more refactoring.
        private Dictionary<Guid, Node> AllNodes = new Dictionary<Guid, Node>();
        private Dictionary<Guid, DrawableNode> AllDrawableNodes = new Dictionary<Guid, DrawableNode>();

        //Used as a stack for tool selection menu
        private List<ToolStripButton> lastSelectedTool = new List<ToolStripButton>();
       
        private Type _selectedNodeType;

        //Connector Button
        #region Connector_Button
        private void ConnectorToolButton_Click(object sender, EventArgs e)
        {
            switch (ConnectorToolButton.Checked)
            {
                case true:
                    //Tool remains checked until you select another tool.
                    break;
                case false:
                    if (lastSelectedTool.Count > 0)
                    {
                        lastSelectedTool[0].Checked = false;
                        lastSelectedTool.RemoveAt(0);
                    }
                    _selectedNodeType = null;
                    ConnectorToolButton.Checked = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
            }
        }
        #endregion

        //Execute Button
        #region Execute Button
        private void ExecuteToolButton_MouseClick(object sender, EventArgs e)
        {
            foreach (DrawableNode a in _LogicNodes)
            {
                a.Process(AllNodes);
                try { listVarWatch.FindItemWithText(a.GUID.ToString()).SubItems[1].Text = a.Value.ToString(); }
                catch { }
            }
        }
        #endregion

        //Draws || Interact with the graph panel.
        #region Graph_Panel
        Random r = new Random();
        private void graphPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //Make the center of the node appear on mouse position.
            var _nodePlacementPos = new Point(e.Location.X - 25, e.Location.Y - 25);

            //Connector Tool
            if (ConnectorToolButton.Checked)
            {
                return;
            }
            if (_selectedNodeType != null)
            {
                var node = new DrawableNode(getNode(), graphPanel, listVarWatch);
                node.Draw(_nodePlacementPos);
                ConsoleLogTextBox.Text += node.Log(); //Deploy log into the bottom textlog.
                addNode(node, true); // Adds the node to all our lists
                return;
            }
        }
        #endregion

        /// <summary>
        /// Helper function to add a node to appropriate lists
        /// </summary>
        /// <param name="node">The Drawable node to add</param>
        /// <param name="isLogic">True if this is a logic node</param>
        private void addNode(DrawableNode node, bool isLogic)
        {
            AllDrawableNodes.Add(node.GUID, node);
            AllNodes.Add(node.GUID, node.GetNode());
            if (isLogic)
                _LogicNodes.Add(node); //Add to a node List.

            node.onNodeClick += node_onNodeClick; // Grab the onclick event, and send to node_onNodeClick
        }

        /// <summary>
        /// Handles event for when a Node is clicked on in the graphPanel
        /// </summary>
        /// <param name="sender">The clicked on node</param>
        /// <param name="e">MouseEventArgs</param>
        void node_onNodeClick(DrawableNode sender, MouseEventArgs e)
        {
            //Return if not using Connector tool, and not left clicking.
            if (!ConnectorToolButton.Checked || e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            if (Connector.TailMouseOverGuid != Guid.Empty)
            {
                Connector.HeadOverGuid = sender.GUID;
                DrawAbleConnector con = new DrawAbleConnector(Connector.TailMouseOverGuid, Connector.HeadOverGuid);
                sender.Input.Add(con); //Add the connection to the destination node Input list.
                con.Draw(graphPanel.CreateGraphics(), AllDrawableNodes);//Draw the connector
                //Debug
                Console.WriteLine("Log: " + sender.Name + "|| Input count: " + sender.Input.Count + " || Output count: " + sender.Output.Count);
                return;
            }
            Connector.TailMouseOverGuid = sender.GUID;
            //MessageBox.Show("Stablished TAIL");
        }

        /// <summary>
        /// Helper function to get all super types of a class in the assembly
        /// </summary>
        /// <typeparam name="TBaseType"></typeparam>
        /// <returns></returns>
        public IEnumerable<Type> FindSubClassesOf<TBaseType>()
        {
            var baseType = typeof(TBaseType);
            var assembly = baseType.Assembly;

            return assembly.GetTypes().Where(t => t.IsSubclassOf(baseType));
        }

        /// <summary>
        /// Sets the current tool type to the currently selected node type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxObject a = (ComboBoxObject)toolStripToolSelectorComboBox.SelectedItem;
            _selectedNodeType = a._nodeType;
        }

        /// <summary>
        /// Creates a new node instance of the given type, calling the Nodes default constructor with a new GUID
        /// </summary>
        /// <returns></returns>
        public Node getNode()
        {
            return (Node)Activator.CreateInstance(_selectedNodeType, new object[] { Guid.NewGuid() });
        }
    }
}
