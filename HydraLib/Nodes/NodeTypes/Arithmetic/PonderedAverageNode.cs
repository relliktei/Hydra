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
    public class PonderedAverageNode : Node
    {
        public PonderedAverageNode(Guid id)
            : base(id)
        {
            this.Name = "PonderedAverage";
        }

        public override float Process(Dictionary<Guid, Node> allNodes)
        {
            //((Input0 * Weight0) + (Input1 * Weight1) + .….) / (Weight0 + Weight1 + .....).
            //We concatenate the different input values in here:
            float Result = 0;
            float _weightSum = 0;

            if (Input.Count >= 2)
            {
                for (int i = 0; i < Input.Count; i+=2)
                {
                    var _floatValue = allNodes[Input[i].TailNodeGuid].Value * allNodes[Input[i+1].TailNodeGuid].Value; //Input * Weight
                    _weightSum += allNodes[Input[i + 1].TailNodeGuid].Value; //Concatenate the Weights sum
                    Result += _floatValue; //This will save the sum of all Input * Weight operation which at the end will be divided by the Weight sum.
                }

                //Get the pondered average
                Result /= _weightSum;

                Console.WriteLine("Log: " + this.Name + "|| Processed an operation with " + Input.Count + " input elements the result was " + Result);

                this.Value = Result;
                // Return the result, so DrawableNode which called this Process(), can update its display label
                return Result;
            }
            return 0f;
        }
    }
}
