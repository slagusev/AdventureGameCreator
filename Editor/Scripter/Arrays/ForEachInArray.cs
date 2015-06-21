using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.Scripter.Arrays
{
    class ForEachInArray : ScriptLine
    {

        public ForEachInArray()
        {

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

        public override System.Xml.Linq.XElement ToXML()
        {
            throw new NotImplementedException();
        }
    }
}
