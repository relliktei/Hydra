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
using System.Diagnostics;


////////////////////////////////////////////////////////////////////////////
//                    NOT IMPLEMENTED                                     //
//                  AUTO GENERATED CLASS!                                 //
////////////////////////////////////////////////////////////////////////////
namespace HydraLib.Nodes.NodeTypes
{
    public class Formula : Node
    {
        public Formula(Guid id)
            : base(id)
        {
            this.Name = "Formula";
        }

        Random rand = new Random();
        public override float Evaluate()
        {
            Debug.WriteLine(string.Format("Evaluating node: {0}", this.Name));
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

