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
        //Used to store logic nodes.
        private List<DrawableNode> _LogicNodes = new List<DrawableNode>();

        //Store all nodes using GUID as key and ONode as value.
        //Todo: should be only one list, requires some more refactoring.
        private Dictionary<Guid, Node> AllNodes = new Dictionary<Guid, Node>();
        private Dictionary<Guid, DrawableNode> AllDrawableNodes = new Dictionary<Guid, DrawableNode>();

        //Used as a stack for tool selection menu
        private List<ToolStripButton> lastSelectedTool = new List<ToolStripButton>();

        //Stores the TYPE of the node that its being drawed/selected
        private Type _selectedNodeType;

        //Context menu for drawPanel, this is used to add nodes into the drawPanel.
        private ContextMenu drawPanelCtxMenu;

        //List used to link the ContextMenu selected item with the proper Node Type the user wants to use.
        private List<ComboBoxObject> usuableNodeList = new List<ComboBoxObject>();

        public Form1()
        {
            InitializeComponent();
            CreateDrawPanelCtxMenu();
        }

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
        private void drawPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //Make the center of the node appear on mouse position.
            var _placementPos = new Point(e.Location.X - 25, e.Location.Y - 25);

            //Context Menu
            if (e.Button == MouseButtons.Right)
                drawPanelCtxMenu.Show(this,_placementPos,LeftRightAlignment.Right);
        
            //Connector Tool
            if (ConnectorToolButton.Checked)
            {
                return;
            }

            //Node Drawing
            if (_selectedNodeType != null)
            {
                var node = new DrawableNode(getNode(), drawPanel, listVarWatch);
                node.Draw(_placementPos);
                ConsoleLogTextBox.Text += node.Log(); //Deploy log into the bottom textlog.
                addNode(node, true); // Adds the node to all our lists
                return;
            }
        }
        #endregion

        /// <summary>
        /// Helper function to create the drawPanel Context Menu
        /// Also fills the usuableNodeList which we will use to link the CtxMenu selection with the proper type of Object the user wants to draw.
        /// </summary>
        private void CreateDrawPanelCtxMenu()
        {
            drawPanelCtxMenu = new ContextMenu();
            foreach (var nodeSubClass in FindSubClassesOf<Node>())
            {
                usuableNodeList.Add(new ComboBoxObject(nodeSubClass, nodeSubClass.Name));
                drawPanelCtxMenu.MenuItems.Add(new MenuItem(nodeSubClass.Name, DrawPanelCtxMenu_SelectedtItemChanged));
            }
        }

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
                con.Draw(drawPanel.CreateGraphics(), AllDrawableNodes);//Draw the connector
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
        /// Sets the current type to the currently selected node type from the drawPanelContexMenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawPanelCtxMenu_SelectedtItemChanged(object sender, EventArgs e)
        {
            //We find the index of the selected node over the ContextMenu in our usuableNodeList.
            var index = usuableNodeList.FindIndex(_nodeInList => _nodeInList._name.Equals((sender as MenuItem).Text, StringComparison.Ordinal));
            //Define the Object using our list.
            ComboBoxObject a = (ComboBoxObject)usuableNodeList[index];
            //Set the selected NodeType from the selected Object.
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
