using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HydraLib.Nodes.NodeTypes
{
    class TestDeviceNode : Node
    {
        public TestDeviceNode(Guid id)
            : base(id)
        {
            this.Name = "TestDevice";
        }

        Random r = new Random();
        public override float Process(Dictionary<Guid, Node> allNodes)
        {
            var _randomNumber = r.Next(0, 10);
            this.Value = _randomNumber;
            return _randomNumber;
        }           
    }
}
