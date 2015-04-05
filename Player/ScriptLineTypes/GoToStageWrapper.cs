using Editor.Scripter.ConversationFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class GoToStageWrapper : ScriptLineWrapper
    {
        GoToStage line;
        public GoToStageWrapper(GoToStage gts)
        {
            line = gts;
        }

        public override bool? Execute()
        {
            MainViewModel.GetMainViewModelStatic().CurrentConversation.GoToStage(line.Stage);
            return true;
        }
    }
}
