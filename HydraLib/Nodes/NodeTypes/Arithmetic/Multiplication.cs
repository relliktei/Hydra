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


namespace HydraLib.Nodes.NodeTypes
{
    public class Multiplication : Node
    {
        public Multiplication(Guid id)
            : base(id)
        {
            this.Name = "Multiplication";
        }



        public override float Process(Dictionary<Guid, Node> allNodes)
        {
            float Result = 0;

            if (Input.Count == 2)
            {
                for (int i = 0; i < Input.Count; i++)
                {
                    //MessageBox.Show(Form1.AllNodes[Input[i].TailNodeGuid].Value.ToString());
                    var _floatValue = allNodes[Input[i].TailNodeGuid].Value;
                    if (Result == 0)
                        Result = _floatValue;
                    else
                        Result *= _floatValue;
                }
                Console.WriteLine("Log: " + this.Name + "|| Processed an operation with " + Input.Count + " input elements the result was " + Result);

                this.Value = Result;
                return Result;
               // this.ValueLabel.Text = Result + "";
            }
            return 1f;
        }

      


    }
}

