<<<<<<< HEAD:HydraLib/Nodes/NodeTypes/Arithmetic/Maximum.cs
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

using HydraLib.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

////////////////////////////////////////////////////////////////////////////
//                    NOT IMPLEMENTED                                     //
//                  AUTO GENERATED CLASS!                                 //
////////////////////////////////////////////////////////////////////////////
namespace HYDRA.Nodes.NodeTypes
{
    public class Maximum : Node
    {
        public Maximum(Guid id)
            : base(id)
        {
            this.Name = "Maximum";
        }

        Random rand = new Random();
        public override float Evaluate()
        {
            Debug.WriteLine(string.Format("Evaluating child : {0}", this.Name));
            this.Value = rand.Next(1, 10);
            return 1f;
        }

        public override float Process(Dictionary<Guid, Node> allNodes)
        {
            //Implement
            return 1f;
        }
    }
}
=======
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

using HydraLib.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
////////////////////////////////////////////////////////////////////////////
//                    NOT IMPLEMENTED                                     //
//                  AUTO GENERATED CLASS!                                 //
////////////////////////////////////////////////////////////////////////////
namespace HYDRA.Nodes.NodeTypes
{
    public class MaximumNode : Node
    {
        public MaximumNode(Guid id)
            : base(id)
        {
            this.Name = "Maximum";
        }

        public override float Process(Dictionary<Guid, Node> allNodes)
        {
            //Implement
            return 1f;
        }
    }
}
>>>>>>> parent of c4e91c8... Refactored node names.:HydraLib/Nodes/NodeTypes/Arithmetic/MaximumNode.cs
