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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HYDRA.Nodes
{
    public class Node
    {
        //Displays node value
        public Label GUITEXT;

        //GUID - Global Unique Identifer used to map our nodes.
        private UInt32 _guid;
        public UInt32 GUID { get { return _guid; } set { _guid = value; } }

        //Input & Output connectors
        public List<Connector> Input = new List<Connector>();
        public List<Connector> Output = new List<Connector>();

        //Name of the node
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        //Node Location (Set onDraw event)
        private Point _location;
        public Point Location { get { return _location; } set { _location = value; } }

        //Draw Form Control
        private Control _panel;
        public Control Panel { get { return _panel; } set { _panel = value; } }

        //Result
        protected Int32 Result;

        //Value
        private float _value;
        public float Value { get { return _value; } set { _value = value; } }

        //Constructor
        public Node(UInt32 id, Control panel)
        {
            this.GUID = id;
            this.Panel = panel;
        }

        //Draw node
        public virtual void Draw(Point Location)
        {
            //Override logic on child nodes.
        }

        //Logging
        public virtual String Log()
        {
            //Override logic on child nodes.
            return "";
        }

        //Node Logic
        public virtual float Process()
        {
            //Override logic on child nodes.
            return 0f;
        }
    }
}
