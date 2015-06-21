using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Arrays
{
    public class ForEachInArray : ScriptLine
    {

        public ForEachInArray(Script baseScript)
        {
            this.ExecutingScript = new Script(baseScript);
        }

        /// <summary>
        /// The <see cref="LinkedArray" /> property's name.
        /// </summary>
        public const string LinkedArrayPropertyName = "LinkedArray";

        private GenericRef<VarArray> _linkedArray = GenericRef<VarArray>.GetArrayRef();

        /// <summary>
        /// Sets and gets the LinkedArray property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public GenericRef<VarArray> LinkedArray
        {
            get
            {
                return _linkedArray;
            }

            set
            {
                if (_linkedArray == value)
                {
                    return;
                }

                _linkedArray = value;
                RaisePropertyChanged(LinkedArrayPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LinkedVar" /> property's name.
        /// </summary>
        public const string LinkedVarPropertyName = "LinkedVar";

        private VarRef _linkedVar = new VarRef();

        /// <summary>
        /// Sets and gets the LinkedVar property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef LinkedVar
        {
            get
            {
                return _linkedVar;
            }

            set
            {
                if (_linkedVar == value)
                {
                    return;
                }

                _linkedVar = value;
                RaisePropertyChanged(LinkedVarPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ExecutingScript" /> property's name.
        /// </summary>
        public const string ExecutingScriptPropertyName = "ExecutingScript";

        private Script _executingScript = new Script();

        /// <summary>
        /// Sets and gets the ExecutingScript property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script ExecutingScript
        {
            get
            {
                return _executingScript;
            }

            set
            {
                if (_executingScript == value)
                {
                    return;
                }

                _executingScript = value;
                RaisePropertyChanged(ExecutingScriptPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ClearArray" /> property's name.
        /// </summary>
        public const string ClearArrayPropertyName = "ClearArray";

        private bool _clearArray = false;

        /// <summary>
        /// Sets and gets the ClearArray property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool ClearArray
        {
            get
            {
                return _clearArray;
            }

            set
            {
                if (_clearArray == value)
                {
                    return;
                }

                _clearArray = value;
                RaisePropertyChanged(ClearArrayPropertyName);
            }
        }

        public override System.Xml.Linq.XElement ToXML()
        {
            return new System.Xml.Linq.XElement("ForEachInArray", new XElement("LinkedArray", LinkedArray != null ? LinkedArray.Ref : Guid.Empty),
                                                                  new XElement("LinkedVar", LinkedVar!= null ? LinkedVar.LinkedVarId : Guid.Empty),
                                                                  new XElement("ClearArray", ClearArray),
                                                                  new XElement("ExecutingScript", ExecutingScript.ToXML()));
        }

        public static ForEachInArray FromXML(XElement xml, Script baseScript)
        {
            var res = new ForEachInArray(baseScript)
            {
                LinkedArray = GenericRef<VarArray>.GetArrayRef(),
                LinkedVar = new VarRef(Guid.Parse(xml.Element("LinkedVar").Value)),
                ExecutingScript = Script.FromXML(xml.Element("ExecutingScript").Element("Script"), baseScript),
                ClearArray = Boolean.Parse(xml.Element("ClearArray").Value)

            };
            res.LinkedArray.Ref = Guid.Parse(xml.Element("LinkedArray").Value);
            return res;
        }

        public override string Plaintext
        {
            get
            {
                var scriptString = string.Join("\n",ExecutingScript.ScriptLines.Where(a => a.GetType() != typeof(Scripter.Misc.Blank)).Select(a => "  "+a.Plaintext));
                return "For Each object in " + (LinkedArray != null && LinkedArray.Value != null ? LinkedArray.Value.Name : "UNKNOWN ARRAY")
                    + ", store the value in " + (LinkedVar != null && LinkedVar.LinkedVariable != null ? LinkedVar.LinkedVariable.Name : "UNKNOWN VARIABLE") + ", and perform the following actions:\n{\n" + scriptString + "\n}";
            }
        }
    }
}
