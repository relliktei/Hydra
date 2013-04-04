using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYDRA.Nodes
{
    class NodeMenu : ContextMenuStrip
    {
        private Node node;

        public NodeMenu(Node node) : base()
        {
            this.node = node;
            this.Items.Add("Add Watch",null,onWatchClick);
        }

        private void onWatchClick(object sender, EventArgs e)
        {
            string[] value_guid = {node.Value.ToString(), node.Guid.ToString() };
            node.VarWatch.Items.Add(node.Name).SubItems.AddRange(value_guid);
        }
    }
}
