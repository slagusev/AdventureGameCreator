using Editor.Scripter.TextFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class StartConversationWrapper : ScriptLineWrapper
    {
        StartConversation line = null;

        public StartConversationWrapper(StartConversation sc)
        {
            line = sc;
        }

        public override bool? Execute()
        {
            if (line.ConversationID.LinkedConversation != null)
            {
                var vm = MainViewModel.GetMainViewModelStatic();
                vm.CurrentConversation = new ObjectTypesWrappers.ConversationWrapper(line.ConversationID.LinkedConversation);
                vm.SetConversationMode();
                vm.CurrentConversation.GoToStage(vm.CurrentConversation.CurrentStage);
                return null;
            }
            return false;
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return line.ToXML();
        }
    }
}
