using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class StopGameWrapper : ScriptLineWrapper
    {
        public override bool? Execute()
        {
            MainViewModel.GetMainViewModelStatic().IsGameOver = true;
            return null;
        }
    }
}
