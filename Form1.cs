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
using HYDRA.Nodes;
using HYDRA.Nodes.NodeTypes;

namespace HYDRA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Used to store logic nodes.
        private List<Node> _LogicNodes = new List<Node>();

        //Store all nodes using GUID as key and ONode as value.
        public static Dictionary<Guid, Node> AllNodes = new Dictionary<Guid, Node>();

        //Used as a stack for tool selection menu
        private List<ToolStripButton> lastSelectedTool = new List<ToolStripButton>();

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

        //Execute Button
        #region Execute Button
        private void ExecuteToolButton_MouseClick(object sender, EventArgs e)
        {
            foreach (Node a in _LogicNodes)
            {
                a.Process();
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

            //Addition Node
            if (AdditionToolButton.Checked)
            {
                var _newAdditionNode = new AdditionNode(Guid.NewGuid(), this.graphPanel); //New Node.
                _newAdditionNode.Draw(_nodePlacementPos); //Draw it on mouse location.
                logTextBox.Text += _newAdditionNode.Log(); //Deploy log into the bottom textlog.
                _LogicNodes.Add(_newAdditionNode); //Add to a node List.
                AllNodes.Add(_newAdditionNode.GUID, _newAdditionNode); //Add to dictionary.
                return;
            }
            
            //Substraction Node
            if (SubstractionToolButton.Checked)
            {
                var _newSubstractionNode = new SubstractionNode(Guid.NewGuid(), this.graphPanel); //New Node.
                _newSubstractionNode.Draw(_nodePlacementPos); //Draw it on mouse location.
                logTextBox.Text += _newSubstractionNode.Log(); //Deploy log into the bottom textlog.
                _LogicNodes.Add(_newSubstractionNode); //Add to a node List.
                AllNodes.Add(_newSubstractionNode.GUID, _newSubstractionNode); //Add to dictionary.
                return;
            }

            //Constant Node
            if (ConstantToolButton.Checked)
            {
                var _newConstantNode = new ConstantNode(Guid.NewGuid(), this.graphPanel, r.Next(1,100));
                _newConstantNode.Draw(_nodePlacementPos);
                logTextBox.Text += _newConstantNode.Log();
                AllNodes.Add(_newConstantNode.GUID, _newConstantNode);
                return;
            }
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
    }
}
