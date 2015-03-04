using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ReturnFalseWrapper : ScriptLineWrapper
    {

        public override bool? Execute()
        {
            return false;
        }
    }
}
