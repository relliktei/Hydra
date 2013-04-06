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
                toolsComboBox.Items.Add(new ComboBoxObject(x, x.Name));
            }
            toolsComboBox.Items.Add(new ComboBoxObject(null, "No Tool"));
        }

        //Used to store logic nodes.
        private List<DrawableNode> _LogicNodes = new List<DrawableNode>();

        //Store all nodes using GUID as key and ONode as value.
        //Todo: should be only one list, requires some more refactoring.
        private Dictionary<Guid, Node> AllNodes = new Dictionary<Guid, Node>();
        private Dictionary<Guid, DrawableNode> AllDrawableNodes = new Dictionary<Guid, DrawableNode>();

        //Used as a stack for tool selection menu
        private List<ToolStripButton> lastSelectedTool = new List<ToolStripButton>();

        //Unsure what the plan was with above stack
        private Type _selectedNodeType;

        //Addition Button
        #region Addition_Button
        private void AdditionToolButton_Click(object sender, EventArgs e)
        {
            switch (AdditionToolButton.Checked)
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

                    AdditionToolButton.Checked = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
            }
        }

        //Addition button image change on mouse enter.
        private void AdditionToolButton_MouseEnter(object sender, EventArgs e)
        {
            ((sender) as ToolStripButton).Image = HYDRA.Properties.Resources.Addition;
        }

        //Addition button image change on mouse leave.
        private void AdditionToolButton_MouseLeave(object sender, EventArgs e)
        {
            ((sender) as ToolStripButton).Image = HYDRA.Properties.Resources.Addition;
        }
        #endregion

        //Constant Button
        #region Constant_Button
        private void ConstantToolButton_Click(object sender, EventArgs e)
        {
            switch (ConstantToolButton.Checked)
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

                    ConstantToolButton.Checked = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
            }
        }

        //Constant button image change on mouse enter.
        private void ConstantToolButton_MouseEnter(object sender, EventArgs e)
        {
            ((sender) as ToolStripButton).Image = HYDRA.Properties.Resources.Constant;
        }

        //Constant button image change on mouse enter.
        private void ConstantToolButton_MouseLeave(object sender, EventArgs e)
        {
            ((sender) as ToolStripButton).Image = HYDRA.Properties.Resources.Constant;
        }
        #endregion

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
                    ConnectorToolButton.Checked = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
            }
        }
        #endregion

        //Multiplication Button
        #region Multiplication Button
        private void MultiplicationToolButton_Click(object sender, EventArgs e)
        {
            switch (MultiplicationToolButton.Checked)
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

                    MultiplicationToolButton.Checked = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
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
                var node = new DrawableNode(getNode(), graphPanel);
                node.Draw(_nodePlacementPos);
                logTextBox.Text += node.Log(); //Deploy log into the bottom textlog.
                addNode(node, true); // Adds the node to all our lists
                return;
            }

            /*//Addition Node
            if (AdditionToolButton.Checked)
            {
                var _newAdditionNode = new DrawableNode(new AdditionNode(Guid.NewGuid()),graphPanel); //Creates a new drawable node and passes it an additionnode + the graphpanel
                _newAdditionNode.Draw(_nodePlacementPos); //Draw it on mouse location.
                logTextBox.Text += _newAdditionNode.Log(); //Deploy log into the bottom textlog.
                addNode(_newAdditionNode, true); // Adds the node to all our lists
                return;
            }

            //Multiplication Node
            if (MultiplicationToolButton.Checked)
            {
                var _newMultiplicationNode = new DrawableNode(new MultiplicationNode(Guid.NewGuid()), graphPanel); //New Node.
                _newMultiplicationNode.Draw(_nodePlacementPos); //Draw it on mouse location.
                logTextBox.Text += _newMultiplicationNode.Log(); //Deploy log into the bottom textlog.
                addNode(_newMultiplicationNode, true);
                return;
            }
            
            //Substraction Node
            if (SubstractionToolButton.Checked)
            {
                var _newSubstractionNode = new DrawableNode(new SubstractionNode(Guid.NewGuid()), graphPanel); //New Node.
                _newSubstractionNode.Draw(_nodePlacementPos); //Draw it on mouse location.
                logTextBox.Text += _newSubstractionNode.Log(); //Deploy log into the bottom textlog.
                addNode(_newSubstractionNode, true);
                return;
            }

            //Division Node
            if (DivisionToolButton.Checked)
            {
                var _newDivisionNode = new DrawableNode(new DivisionNode(Guid.NewGuid()), graphPanel); //New Node.
                _newDivisionNode.Draw(_nodePlacementPos); //Draw it on mouse location.
                logTextBox.Text += _newDivisionNode.Log(); //Deploy log into the bottom textlog.
                addNode(_newDivisionNode, true);
                return;
            }

            //Constant Node
            if (ConstantToolButton.Checked)
            {
                var _newConstantNode = new DrawableNode(new ConstantNode(Guid.NewGuid(), r.Next(20, 100)), graphPanel); //New Node.
                _newConstantNode.Draw(_nodePlacementPos);
                logTextBox.Text += _newConstantNode.Log();
                addNode(_newConstantNode,false);
                return;
            }*/
        }
        #endregion

        //Substraction Button
        #region Substraction Button
        private void SubstractionToolButton_Click(object sender, EventArgs e)
        {
            switch (SubstractionToolButton.Checked)
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

                    SubstractionToolButton.Checked = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
            }
        }

        private void SubstractionToolButton_MouseEnter(object sender, EventArgs e)
        {
            ((sender) as ToolStripButton).Image = HYDRA.Properties.Resources.Substraction;
        }

        private void SubstractionToolButton_MouseLeave(object sender, EventArgs e)
        {
            ((sender) as ToolStripButton).Image = HYDRA.Properties.Resources.Substraction;
        }
        #endregion

        //Execute Button
        #region Execute Button
        private void ExecuteToolButton_MouseClick(object sender, EventArgs e)
        {
            foreach (DrawableNode a in _LogicNodes)
            {
                a.Process(AllNodes);
            }
        }
        #endregion

        //Division Button
        #region Division Button
        private void DivisionToolButton_Click(object sender, EventArgs e)
        {
            switch (DivisionToolButton.Checked)
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

                    DivisionToolButton.Checked = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
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
            ComboBoxObject a = (ComboBoxObject)toolsComboBox.SelectedItem;
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
