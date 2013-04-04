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


////////////////////////////////////////////////////////////////////////////
//                    NOT IMPLEMENTED                                     //
//                  AUTO GENERATED CLASS!                                 //
////////////////////////////////////////////////////////////////////////////
namespace HYDRA.Nodes.NodeTypes
{
    public class ScaleNode : Node
    {
        public ScaleNode(Guid id, Control panel)
            : base(id, panel)
        {
            this.Name = "Scale";
        }

        public override void Draw(Point Location)
        {
            base.Draw(Location, HYDRA.Properties.Resources.Scale);
        }

        public override float Process()
        {
            //Implement
            return 1f;
        }

        public override string Log()
        {
            return Environment.NewLine + "<<<New Action>>>" + Environment.NewLine + "Created " + this.Name + " node." + Environment.NewLine + "Position: " + this.Location + Environment.NewLine + "Guid: " + this.GUID + Environment.NewLine;
        }


    }
}

