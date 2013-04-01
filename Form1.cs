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
        private List<Node> _nodes = new List<Node>();

        public static Dictionary<uint, Node> AllNodes = new Dictionary<uint, Node>();

        private List<ToolStripButton> lastSelectedTool = new List<ToolStripButton>();
        //Addition node button
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            bUsingConnector = false;
            switch (toolStripButton1.Checked)
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

                    toolStripButton1.Checked = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
            }        
        }

        //Constant node button
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            bUsingConnector = false;
            switch (toolStripButton2.Checked)
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

                    toolStripButton2.Checked = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
            }        
        }

        public static bool bUsingConnector = false;
        //Connector
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            switch (toolStripButton4.Checked)
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

                    toolStripButton4.Checked = true;
                    bUsingConnector = true;
                    lastSelectedTool.Add((sender) as ToolStripButton);
                    break;
            }           
        }

        Random r = new Random();
        //Draws || Interact with the graph panel.
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            //Connector Tool
            if (toolStripButton4.Checked)
            {             
                return;
            }

            //Addition Node
            if (toolStripButton1.Checked)
            {
                var _newAdditionNode = new AdditionNode(NodeGuid.GUID, this.panel1);
                //NodeGuidMap.nodeGuidMap.Add(_newAdditionNode.GUID, _newAdditionNode);
                _newAdditionNode.Draw(e.Location);
                textBox1.Text += _newAdditionNode.Log();
                _nodes.Add(_newAdditionNode);
                AllNodes.Add(_newAdditionNode.GUID, _newAdditionNode);
                return;
            }

            //Constant Node
            if (toolStripButton2.Checked)
            {
                var _newConstantNode = new ConstantNode(NodeGuid.GUID, this.panel1, r.Next(100));
                
                _newConstantNode.Draw(e.Location);
                textBox1.Text += _newConstantNode.Log();
               // _nodes.Add(_newConstantNode);
                AllNodes.Add(_newConstantNode.GUID, _newConstantNode);
                return;
            }
        }

        //Execute
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            foreach(Node a in _nodes)
            {
                a.Process();
            }           
        }
    }
}
