using HydraLib.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYDRA
{
    public class ComboBoxObject
    {
        public Type _nodeType;
        String _text;

        public ComboBoxObject(Type node, string label)
        {
            //Take the type of the node, and create an instance of it.
            this._nodeType = node;
            this._text = label;
        }

        
        public override string ToString()
        {
            return _text;
        }
    }
}
