using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraLib.Nodes
{
    public interface INode
    {
        float Process(Dictionary<Guid, Node> AllNodes);
        string Log();
    }
}
