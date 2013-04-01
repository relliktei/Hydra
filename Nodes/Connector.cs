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

namespace HYDRA.Nodes
{
    public class Connector
    {
        //Tail MouseOver GUID
        private static Guid _TailmouseOverGuid;
        public static Guid TailMouseOverGuid { get { return _TailmouseOverGuid; } set { _TailmouseOverGuid = value; } }

        //Head MouseOver GUID
        private static Guid _HeadmouseOverGuid;
        public static Guid HeadOverGuid { get { return _HeadmouseOverGuid; } set { _HeadmouseOverGuid = value; } }

        //Tail - This is the source node of the link.
        private Guid _tailNodeGuid;
        public Guid TailNodeGuid { get { return _tailNodeGuid; } set { _tailNodeGuid = value; } }

        //Head - This is the destination node of the link.
        private Guid _headNodeGuid;
        public Guid HeadNodeGuid { get { return _headNodeGuid; } set { _headNodeGuid = value; } }

        public Connector(Guid Tail, Guid Head)
        {
            this._tailNodeGuid = Tail;
            this._headNodeGuid = Head;
            ClearConnectors();
        }

        private void ClearConnectors()
        {
            TailMouseOverGuid = Guid.Empty;
            HeadOverGuid = Guid.Empty;
        }

    }
}
