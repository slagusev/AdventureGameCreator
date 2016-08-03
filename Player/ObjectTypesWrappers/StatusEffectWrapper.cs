using Editor.ObjectTypes;
using Editor.Scripter.StatusEffects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Player.ObjectTypesWrappers
{
    class StatusEffectWrapper : INotifyPropertyChanged
    {

        public GenericRef<StatusEffect> LinkedEffect = GenericRef<StatusEffect>.GetStatusEffectRef();
        List<VariableWrapper> TempVariables = new List<VariableWrapper>();
        public List<ScriptStatusEffectArgumentValue> stringArguments = new List<ScriptStatusEffectArgumentValue>();
        public List<ScriptStatusEffectArgumentValue> numberArguments = new List<ScriptStatusEffectArgumentValue>();
        public StatusEffectWrapper(GenericRef<StatusEffect> se, Game g)
        {
            LinkedEffect = se;
            TempVariables = new List<VariableWrapper>();
            foreach (var tempVar in g.VarByName.Where(a => a.Value.VariableBase.Name.StartsWith("_")))
            {
                TempVariables.Add(new VariableWrapper(tempVar.Value.VariableBase));
                
            }
            
        }
        public XElement ToXML()
        {
            return new XElement("ActiveStatusEffect", new XElement("LinkedStatusEffect", LinkedEffect.Ref),
                                new XElement("TempVariables", TempVariables.Select(a => a.ToXML())),
                                new XElement("Arguments", stringArguments.Union(numberArguments).Select(a => new XElement("Argument", new XElement("Id", a.Id), new XElement("Type", a.IsNumber ? "number" : "string"), new XElement("Value", a.IsNumber ? a.NumberValue.ToString() : a.StringValue)))));
        }

        

        public static StatusEffectWrapper FromXML(XElement xml, Game g)
        {
            var seRef = GenericRef<StatusEffect>.GetStatusEffectRef();
            seRef.Ref = Guid.Parse(xml.Element("LinkedStatusEffect").Value);
            StatusEffectWrapper wrapper = new StatusEffectWrapper(seRef, g);

            foreach (var tempVariableXml in xml.Element("TempVariables").Elements())
            {
                var tempVariable = VariableWrapper.FromXML(tempVariableXml, g, g.VarById[Guid.Parse(tempVariableXml.Element("Id").Value)].VariableBase);
                foreach (var a in wrapper.TempVariables)
                {
                    if (a.VariableBase.Id == tempVariable.VariableBase.Id)
                    {
                        a.CurrentCommonEventValue = tempVariable.CurrentCommonEventValue;
                        a.CurrentDateTimeValue = tempVariable.CurrentDateTimeValue;
                        a.CurrentItemValue = tempVariable.CurrentItemValue;
                        a.CurrentNumberValue = tempVariable.CurrentNumberValue;
                        a.CurrentStringValue = tempVariable.CurrentStringValue;
                    }
                }
            }
            foreach (var a in xml.Element("Arguments").Elements("Argument"))
            {
                Guid id = Guid.Parse(a.Element("Id").Value);

                if (a.Element("Type").Value == "number")
                {
                    wrapper.numberArguments.Add(new ScriptStatusEffectArgumentValue { IsNumber = true, Id = id, NumberValue = Convert.ToInt32(a.Element("Value").Value) });
                }
                else
                {
                    wrapper.stringArguments.Add(new ScriptStatusEffectArgumentValue { IsString = true, Id = id, StringValue = a.Element("Value").Value });
                }
            }

            return wrapper;
        }

        public void RunInit()
        {
            ScriptWrapper sw = new ScriptWrapper(LinkedEffect.Value.OnInitialize);
            foreach (var local in TempVariables)
            {
                sw.localVars.Add(local.VariableBase.Id, local);
                sw.localVarsByName.Add(local.VariableBase.Name, local);
            }
            sw.CurrentStatusEffect = this;
            sw.Execute(true);
        }

        public void RunOnMove()
        {
            ScriptWrapper sw = new ScriptWrapper(LinkedEffect.Value.OnMove);
            foreach (var local in TempVariables)
            {
                sw.localVars.Add(local.VariableBase.Id, local);
                sw.localVarsByName.Add(local.VariableBase.Name, local);
            }
            sw.CurrentStatusEffect = this;
            sw.Execute(true);
        }

        public bool RunCheckIfResolved()
        {
            ScriptWrapper sw = new ScriptWrapper(LinkedEffect.Value.CheckIfCleared);
            foreach (var local in TempVariables)
            {
                sw.localVars.Add(local.VariableBase.Id, local);
                sw.localVarsByName.Add(local.VariableBase.Name, local);
            }
            sw.CurrentStatusEffect = this;
            bool? result = sw.Execute(true);
            return (result == true);
        }

        public void RunStack()
        {
            ScriptWrapper sw = new ScriptWrapper(LinkedEffect.Value.OnStack);
            foreach (var local in TempVariables)
            {
                sw.localVars.Add(local.VariableBase.Id, local);
                sw.localVarsByName.Add(local.VariableBase.Name, local);
            }
            sw.CurrentStatusEffect = this;
            sw.Execute(true);
        }
        public void RunFinalize()
        {
            ScriptWrapper sw = new ScriptWrapper(LinkedEffect.Value.OnFinish);
            foreach (var local in TempVariables)
            {
                sw.localVars.Add(local.VariableBase.Id, local);
                sw.localVarsByName.Add(local.VariableBase.Name, local);
            }
            sw.CurrentStatusEffect = this;
            sw.Execute(true);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
