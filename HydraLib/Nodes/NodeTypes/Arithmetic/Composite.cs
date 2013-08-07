using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace HydraLib.Nodes.NodeTypes
{
    public class Composite : Node
    {
        public List<Node> childs = new List<Node>();

        public Composite(Guid id) : base(id) { this.Name = "Composite"; }

        public override float Evaluate()
        {
            Debug.WriteLine(string.Format("Evaluating CompositeNode"));
            foreach (Node node in childs.Where(x => x.Input.Count > 0))
            {
                node.Evaluate();
            }

            return 0f; //Must return error code?
        }

        public void AddNode(Node node)
        {
            Debug.WriteLine(string.Format("Adding node: {0} to composite", node.GetType()));
            childs.Add(node);
        }

        public void RemoveNode(Node node)
        {
            Debug.WriteLine(string.Format("Removing node: {0} from composite", node.GetType()));
            childs.Remove(node);
        }
    }
}
