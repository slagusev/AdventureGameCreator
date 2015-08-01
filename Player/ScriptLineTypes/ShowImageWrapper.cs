using Editor.Scripter.TextFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class ShowImageWrapper : ScriptLineWrapper
    {
        ShowImage line;
        public ShowImageWrapper(ShowImage si)
        {
            line = si;
        }

        public override bool? Execute()
        {
            MainViewModel.WriteImage(new ImageRef() { Path = Editor.MainViewModel.AbsolutePath(MainViewModel.GetMainViewModelStatic().Location, line.Path) }, this.parent, false);
            return null;
        }
    }
}
