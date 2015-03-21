using Editor.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.ItemManagement
{
    public class SetItemProperty : ScriptLine
    {
        /// <summary>
        /// The <see cref="SelectedItemClass" /> property's name.
        /// </summary>
        public const string SelectedItemClassPropertyName = "SelectedItemClass";

        private ItemClass _selectedItemClass = null;

        /// <summary>
        /// Sets and gets the SelectedItemClass property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemClass SelectedItemClass
        {
            get
            {
                if (_selectedItemClass == null)
                {
                    var itemClass = MainViewModel.MainViewModelStatic.ItemClasses.Where(a => a.Name == SelectedItemClassName).FirstOrDefault();
                    if (itemClass != null)
                    {
                        SelectedItemClass = itemClass;
                    }
                }
                return _selectedItemClass;
            }

            set
            {
                if (_selectedItemClass == value)
                {
                    return;
                }
                
                _selectedItemClass = value;
                RaisePropertyChanged(SelectedItemClassPropertyName);
                if (value != null) SelectedItemClassName = SelectedItemClass.Name;
                if (SourceVarRef != null && SourceVarRef.LinkedVariable != null && _selectedItemClass != null)
                {
                    RecalculateAvailableProperties();
                }
            }
        }

        private void RecalculateAvailableProperties()
        {
            var oldSelected = SelectedProperty;
            var allProps = SelectedItemClass.ItemProperties;
            if (SourceVarRef.LinkedVariable.IsDateTime)
            {
                AvailableProperties = new ObservableCollection<Variable>(allProps.Where(a => a.IsDateTime).ToList());
            }
            else if (SourceVarRef.LinkedVariable.IsNumber)
            {
                AvailableProperties = new ObservableCollection<Variable>(allProps.Where(a => a.IsNumber).ToList());
            }
            else if (SourceVarRef.LinkedVariable.IsString)
            {
                AvailableProperties = new ObservableCollection<Variable>(allProps.Where(a => a.IsString).ToList());
            }
            else if (SourceVarRef.LinkedVariable.IsItem)
            {
                AvailableProperties = new ObservableCollection<Variable>(allProps.Where(a => a.IsItem).ToList());
            }
        }
        /// <summary>
        /// The <see cref="AvailableProperties" /> property's name.
        /// </summary>
        public const string AvailablePropertiesPropertyName = "AvailableProperties";

        private ObservableCollection<Variable> _availableProperties = new ObservableCollection<Variable>();

        /// <summary>
        /// Sets and gets the AvailableProperties property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Variable> AvailableProperties
        {
            get
            {
                return _availableProperties;
            }

            set
            {
                if (_availableProperties == value)
                {
                    return;
                }

                _availableProperties = value;
                RaisePropertyChanged(AvailablePropertiesPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedProperty" /> property's name.
        /// </summary>
        public const string SelectedPropertyPropertyName = "SelectedProperty";

        private Variable _selectedProperty = null;

        /// <summary>
        /// Sets and gets the SelectedProperty property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Variable SelectedProperty
        {
            get
            {
                if (SelectedItemClass != null)
                {
                    var prop = SelectedItemClass.ItemProperties.Where(a => a.Name == SelectedPropertyName).FirstOrDefault();
                    if (prop != null)
                    {
                        SelectedProperty = prop;
                    }
                    
                }
                return _selectedProperty;
            }

            set
            {
                if (_selectedProperty == value)
                {
                    return;
                }

                _selectedProperty = value;
                RaisePropertyChanged(SelectedPropertyPropertyName);
                if (value != null) SelectedPropertyName = SelectedProperty.Name;
            }
        }

        /// <summary>
        /// The <see cref="SelectedItemClassName" /> property's name.
        /// </summary>
        public const string SelectedItemClassNamePropertyName = "SelectedItemClassName";

        private string _selectedItemClassName = "";

        /// <summary>
        /// Sets and gets the SelectedItemClassName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string SelectedItemClassName
        {
            get
            {
                return _selectedItemClassName;
            }

            set
            {
                if (_selectedItemClassName == value)
                {
                    return;
                }

                _selectedItemClassName = value;
                RaisePropertyChanged(SelectedItemClassNamePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedPropertyName" /> property's name.
        /// </summary>
        public const string SelectedPropertyNamePropertyName = "SelectedPropertyName";

        private string _selectedPropertyName = "";

        /// <summary>
        /// Sets and gets the SelectedPropertyName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string SelectedPropertyName
        {
            get
            {
                return _selectedPropertyName;
            }

            set
            {
                if (_selectedPropertyName == value)
                {
                    return;
                }

                _selectedPropertyName = value;
                RaisePropertyChanged(SelectedPropertyNamePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="VarRef" /> property's name.
        /// </summary>
        public const string VarRefPropertyName = "VarRef";

        private VarRef _varRef = new VarRef();

        /// <summary>
        /// Sets and gets the VarRef property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef VarRef
        {
            get
            {
                return _varRef;
            }

            set
            {
                if (_varRef == value)
                {
                    return;
                }

                _varRef = value;
                RaisePropertyChanged(VarRefPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SourceVarRef" /> property's name.
        /// </summary>
        public const string SourceVarRefPropertyName = "SourceVarRef";

        private VarRef _sourceVarRef = new VarRef();

        /// <summary>
        /// Sets and gets the SourceVarRef property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public VarRef SourceVarRef
        {
            get
            {
                return _sourceVarRef;
            }

            set
            {
                if (_sourceVarRef == value)
                {
                    return;
                }

                _sourceVarRef = value;
                if (SelectedItemClass != null && SourceVarRef != null && SourceVarRef.LinkedVariable != null )
                {
                    RecalculateAvailableProperties();
                }
                RaisePropertyChanged(SourceVarRefPropertyName);
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("SetItemProperty",
                new XElement("Source", SourceVarRef.LinkedVarId),
                new XElement("Destination", VarRef.LinkedVarId),
                new XElement("ItemClass", SelectedItemClassName),
                new XElement("Property", SelectedPropertyName));
        }
        public override string Plaintext
        {
            get
            {
                return "Set the " + SelectedItemClassName + ":" + SelectedPropertyName + " property of "
                    + (VarRef != null && VarRef.LinkedVariable != null ? VarRef.LinkedVariable.Name : "UNKNOWN") + " to "
                    + (SourceVarRef != null && SourceVarRef.LinkedVariable != null ? SourceVarRef.LinkedVariable.Name : "UNKNOWN");
            }
        }
        public static SetItemProperty FromXML(XElement xml)
        {
            SetItemProperty sip = new SetItemProperty();
            if (xml.Element("Source") != null)
            {
                sip.SourceVarRef = new VarRef(Guid.Parse(xml.Element("Source").Value));
            }
            if (xml.Element("Destination") != null)
            {
                sip.VarRef = new VarRef(Guid.Parse(xml.Element("Destination").Value));
            }
            if (xml.Element("ItemClass") != null)
            {
                sip.SelectedItemClassName = xml.Element("ItemClass").Value;
            }
            if (xml.Element("Property") != null)
            {
                sip.SelectedPropertyName = xml.Element("Property").Value;
            }
            return sip;
        }

    }
}
