using Editor.ObjectTypes;
using Editor.Scripter;
using Player.ScriptLineTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Player.ObjectTypesWrappers
{
    class ScriptWrapper
    {
        public ScriptWrapper(Script s)
        {
            ScriptBase = s;

            VariableResult = null;
            TextResult = new List<object>();
        }
        Script ScriptBase = null;
        public ItemInstance ItemBase = null;
        public List<object> TextResult { get; set; }
        public VarRef VariableResult { get; set; }
        public bool IsRootScript = false;
        public bool StopExecution = false;
        public Dictionary<Guid, VariableWrapper> localVars = new Dictionary<Guid,VariableWrapper>();
        public bool isSubscript = false;
        public Dictionary<string, VariableWrapper> localVarsByName = new Dictionary<string, VariableWrapper>();
        public void DupeVars(ScriptWrapper s)
        {
            ScriptWrapper parent = s;
            while (parent != null && parent.localVars.Count() == 0)
            {
                parent = parent.parent;
            }
            if (parent != null)
            {
                foreach (var a in parent.localVars)
                {
                    VariableWrapper vw = new VariableWrapper(a.Value.VariableBase);
                    vw.CurrentCommonEventValue = a.Value.CurrentCommonEventValue;
                    vw.CurrentDateTimeValue = a.Value.CurrentDateTimeValue;
                    vw.CurrentItemValue = a.Value.CurrentItemValue;
                    vw.CurrentNumberValue = a.Value.CurrentNumberValue;
                    vw.CurrentStringValue = a.Value.CurrentStringValue;
                    localVars.Add(a.Key, vw);
                    localVarsByName.Add(a.Value.VariableBase.Name, vw);
                }
            }
        }
        public bool? Execute()
        {
            TextResult.Clear();
            if (parent == null)
            {
                foreach (var a in MainViewModel.GetMainViewModelStatic().CurrentGame.VarById.Where(a => a.Value.VariableBase.Name.StartsWith("_")))
                {
                    var wrapper = new VariableWrapper(a.Value.VariableBase);
                    if (!localVars.ContainsKey(a.Key))
                    {
                        localVars.Add(a.Key, wrapper);
                        localVarsByName.Add(a.Value.VariableBase.Name, a.Value);
                    }
                }
            }
            foreach (var line in ScriptBase.ScriptLines.Where(a => a.GetType() != typeof(CommentWrapper)))
            {
                ScriptLineWrapper currentLine = ScriptLineWrapper.GetScriptLineWrapper(line, this);
                if (currentLine != null)
                {
                    var result = currentLine.Execute();
                    //Stop executing the script once a return statement has been triggered.
                    if (result != null)
                        return result;
                }
                if (StopExecution)
                {
                    if (this.parent != null && !this.IsRootScript )
                    {
                        this.parent.StopExecution = true;
                    }
                    break;
                }
                //currentLine.Dispose();
            }
            return null;
        }

        public ScriptWrapper parent = null;
        public ScriptWrapper GetTopParent()
        {
            var lastParent = this;
            while (lastParent.parent != null)
            {
                lastParent = lastParent.parent;
            }
            return lastParent;
        }
        public ScriptWrapper GetRoot()
        {
            if (this.IsRootScript)
                return this;
            else if (this.parent == null)
                return this;
            else return this.parent.GetRoot();
        }
        public VariableWrapper GetVarById(Guid id)
        {
            if (localVars.ContainsKey(id) && !isSubscript)
            {
                return localVars[id];
            }
            else if (parent != null)
            {
                return parent.GetVarById(id);
            }
            else
            {
                if (MainViewModel.GetMainViewModelStatic().CurrentGame.VarById.ContainsKey(id))
                    return MainViewModel.GetMainViewModelStatic().CurrentGame.VarById[id];
                else return null;
            }
        }
        private void RebuildVarsByName()
        {
            localVarsByName = new Dictionary<string, VariableWrapper>();
            foreach (var a in localVars)
            {
                localVarsByName.Add(a.Value.VariableBase.Name, a.Value);
            }
        }
        public VariableWrapper GetVarByName(string name)
        {
            RebuildVarsByName();
            if (localVarsByName.ContainsKey(name))
            {
                return localVarsByName[name];
            }
            else if (parent != null)
            {
                return parent.GetVarByName(name);
            }
            else
            {
                if (MainViewModel.GetMainViewModelStatic().CurrentGame.VarByName.ContainsKey(name))
                    return MainViewModel.GetMainViewModelStatic().CurrentGame.VarByName[name];
                else return null;
            }
        }
        public XElement ToXML()
        {
            return this.ScriptBase.ToXML();
        }
    }
}
