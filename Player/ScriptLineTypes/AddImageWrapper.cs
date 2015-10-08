using Editor.Scripter.TextFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class AddImageWrapper : ScriptLineWrapper
    {
        AddImage line;
        public AddImageWrapper(AddImage ai)
        {
            line = ai;

        }
        public override bool? Execute()
        {
            ImageRef i = new ImageRef() { Path = Editor.MainViewModel.AbsolutePath(MainViewModel.GetMainViewModelStatic().Location, line.Path) };
            this.parent.TextResult.Add(i);
            return null;
        }
    }
}
