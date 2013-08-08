using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraLib.Nodes
{
    public static class NodeMap
    {
        public static Dictionary<Guid, Node> Nodes = new Dictionary<Guid, Node>();

        public static float GetNodeValue(Guid guid)
        {
            return Nodes[guid].Value;
        }
    }
}
