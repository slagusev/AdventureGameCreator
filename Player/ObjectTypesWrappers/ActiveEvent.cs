using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ObjectTypesWrappers
{
    class ActiveEvent
    {
        public ScriptWrapper Condition { get; set; }
        public ScriptWrapper Result { get; set; }

        public bool CheckCondition()
        {
            return Condition.Execute() == true;
        }
        public bool? Execute()
        {
            if (CheckCondition())
            {
                var res = Result.Execute();
                MainViewModel.GetMainViewModelStatic().CurrentGame.ActiveEvents.Remove(this);
                return res;
            }
            else return null;
        }
    }
}
