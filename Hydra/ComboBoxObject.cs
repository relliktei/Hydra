using HydraLib.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYDRA
{
    /// <summary>
    /// Simple holder object, to make adding nodes to a toolbox simple and easy.
    /// </summary>
    public class ComboBoxObject
    {
        /// <summary>
        /// Refrence to the Type of node.
        /// </summary>
        public Type _nodeType;

        /// <summary>
        /// Nodes name, displayed in combobox thanks to ToString() override
        /// </summary>
        private String _text;

        /// <summary>
        /// Takes a node type like ConstantNode or AdditionNode and a label
        /// </summary>
        /// <param name="node">Node type</param>
        /// <param name="label">Label text</param>
        public ComboBoxObject(Type node, string label)
        {
            this._nodeType = node;
            this._text = label;
        }

        public override string ToString()
        {
            return _text;
        }
    }
}
