<<<<<<< HEAD:HydraLib/Nodes/NodeTypes/Arithmetic/Hysteresis.cs
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

namespace HYDRA.Nodes.NodeTypes
{
    public class Hysteresis : Node
    {
        private float _vOn;
        public float vOn { get { return _vOn; } set { _vOn = value; } }
        private float _vOff;
        public float vOff { get { return _vOff; } set { _vOff = value; } }

        public Hysteresis(Guid id)
            : base(id)
        {
            this.isBool = true;
            this.vOn = 5;
            this.vOff = 4;
            this.Name = "Hysteresis";
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
            //We concatenate the different input values in here:
            float Result = 0;

            if (Input.Count == 1)
            {
                var _floatValue = allNodes[Input[0].TailNodeGuid].Value;
                if (_floatValue > vOn)
                {
                    Result = 1;
                }
                else if (_floatValue < vOff)
                {
                    Result = 0;
                }
                else
                {
                    Result = 0;
                }

                Console.WriteLine("Log: " + this.Name + "|| Processed an operation with " + Input.Count + " input elements the result was " + Convert.ToBoolean(Result));

                this.Value = Result;
                // Return the result, so DrawableNode which called this Process(), can update its display label
                return Result;
            }
            return 0f;
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

namespace HYDRA.Nodes.NodeTypes
{
    public class HysteresisNode : Node
    {
        private float _vOn;
        public float vOn { get { return _vOn; } set { _vOn = value; } }
        private float _vOff;
        public float vOff { get { return _vOff; } set { _vOff = value; } }

        public HysteresisNode(Guid id)
            : base(id)
        {
            this.isBool = true;
            this.vOn = 5;
            this.vOff = 4;
            this.Name = "Hysteresis";
        }

        public override float Process(Dictionary<Guid, Node> allNodes)
        {
            //We concatenate the different input values in here:
            float Result = 0;

            if (Input.Count == 1)
            {

                var _floatValue = allNodes[Input[0].TailNodeGuid].Value;
                if (_floatValue > vOn)
                {
                    Result = 1;
                }
                else if (_floatValue < vOff)
                {
                    Result = 0;
                }
                else
                {
                    Result = 0;
                }

                Console.WriteLine("Log: " + this.Name + "|| Processed an operation with " + Input.Count + " input elements the result was " + Convert.ToBoolean(Result));

                this.Value = Result;
                // Return the result, so DrawableNode which called this Process(), can update its display label
                return Result;
            }
            return 0f;
        }
    }
>>>>>>> parent of c4e91c8... Refactored node names.:HydraLib/Nodes/NodeTypes/Arithmetic/HysteresisNode.cs
}