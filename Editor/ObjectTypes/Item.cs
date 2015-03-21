using Editor.Scripter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.ObjectTypes
{
    public class Item : INotifyPropertyChanged
    {
        
        
        public Item()
        {
            ScriptAbilityDefaults();
            WireCommands();
        }

        private void ScriptAbilityDefaults()
        {
            Description.CanDisplayText = false;
            Description.CanReturn = false;
            Description.CanStopGame = false;
            Description.CanAddText = true;
            Description.IsVariableScript = true;
            OnUse.CanReturn = false;
            OnUse.CanDisplayText = true;
            OnUse.IsVariableScript = true;
        }
        public void WireCommands()
        {
            SetItemClassCommand = new RelayCommand(SetItemClass);
        }
        public Item(Item i)
        {
            this.DefaultName = i.DefaultName;
            this.ItemID = i.ItemID;
            this.Description = i.Description;
            this.ItemClassParent = i.ItemClassParent;
            this.OnUse = i.OnUse;
            this.Removable = i.Removable;
            this.Usable = i.Usable;
            ScriptAbilityDefaults();
        }

        /// <summary>
        /// The <see cref="ItemName" /> property's name.
        /// </summary>
        public const string ItemNamePropertyName = "ItemName";

        private string _itemName = "";

        /// <summary>
        /// Sets and gets the ItemName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string ItemName
        {
            get
            {
                return _itemName;
            }

            set
            {
                if (_itemName == value)
                {
                    return;
                }

                _itemName = value;
                RaisePropertyChanged(ItemNamePropertyName);
            }
        }



        /// <summary>
        /// The <see cref="DefaultName" /> property's name.
        /// </summary>
        public const string DefaultNamePropertyName = "DefaultName";

        private string _defaultName = "";

        /// <summary>
        /// Sets and gets the DefaultName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string DefaultName
        {
            get
            {
                return _defaultName;
            }

            set
            {
                if (_defaultName == value)
                {
                    return;
                }

                _defaultName = value;
                RaisePropertyChanged(DefaultNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ItemID" /> property's name.
        /// </summary>
        public const string ItemIDPropertyName = "ItemID";

        private Guid _itemID = Guid.Empty;

        /// <summary>
        /// Sets and gets the ItemID property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid ItemID
        {
            get
            {
                return _itemID;
            }

            set
            {
                if (_itemID == value)
                {
                    return;
                }

                _itemID = value;
                RaisePropertyChanged(ItemIDPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="ItemClassParent" /> property's name.
        /// </summary>
        public const string ItemClassParentPropertyName = "ItemClassParent";

        private ItemClass _itemClassParent = null;

        /// <summary>
        /// Sets and gets the ItemClassParent property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemClass ItemClassParent
        {
            get
            {
                return _itemClassParent;
            }

            set
            {
                if (_itemClassParent == value)
                {
                    return;
                }
                if (_itemClassParent != null)
                {
                    if (_itemClassParent.Items.Contains(this))
                        _itemClassParent.Items.Remove(this);
                }

                _itemClassParent = value;
                if (value != null)
                {
                    value.Items.Add(this);
                }
                RaisePropertyChanged(ItemClassParentPropertyName);
                ItemProperties.Clear();
                PopulateProperties();
            }
        }

        

        /// <summary>
        /// The <see cref="ItemClassParentTemp" /> property's name.
        /// </summary>
        public const string ItemClassParentTempPropertyName = "ItemClassParentTemp";

        private ItemClass _itemClassParentTemp = null;

        /// <summary>
        /// Sets and gets the ItemClassParentTemp property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemClass ItemClassParentTemp
        {
            get
            {
                return _itemClassParentTemp;
            }

            set
            {
                if (_itemClassParentTemp == value)
                {
                    return;
                }

                _itemClassParentTemp = value;
                RaisePropertyChanged(ItemClassParentTempPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private Scripter.Script _description = new Scripter.Script();

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Scripter.Script Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                _description = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Usable" /> property's name.
        /// </summary>
        public const string UsablePropertyName = "Usable";

        private bool _usable = false;

        /// <summary>
        /// Sets and gets the Usable property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool Usable
        {
            get
            {
                return _usable;
            }

            set
            {
                if (_usable == value)
                {
                    return;
                }

                _usable = value;
                RaisePropertyChanged(UsablePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="OnUse" /> property's name.
        /// </summary>
        public const string OnUsePropertyName = "OnUse";

        private Script _onUse = new Script();

        /// <summary>
        /// Sets and gets the OnUse property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script OnUse
        {
            get
            {
                return _onUse;
            }

            set
            {
                if (_onUse == value)
                {
                    return;
                }

                _onUse = value;
                RaisePropertyChanged(OnUsePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Removable" /> property's name.
        /// </summary>
        public const string RemovablePropertyName = "Removable";

        private bool _removable = true;

        /// <summary>
        /// Set to false if this item cannot be removed from the player's inventory by the player.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool Removable
        {
            get
            {
                return _removable;
            }

            set
            {
                if (_removable == value)
                {
                    return;
                }

                _removable = value;
                RaisePropertyChanged(RemovablePropertyName);
            }
        }





        public void PopulateProperties()
        {
            ItemClass parentClass = this.ItemClassParent;
            while (parentClass != null)
            {
                foreach (var property in parentClass.ItemProperties)
                {
                    ItemProperties.Add(new ItemProperty() { Name = parentClass.Name + ":" + property.Name,
                        BaseVariable = property,
                        Value = property.GetDefaultValue()});
                }

                parentClass = parentClass.ParentClass;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public RelayCommand SetItemClassCommand { get; set; }

        public void SetItemClass()
        {
            if (this.ItemClassParentTemp != null)
            {
                if (this.ItemClassParentTemp != this.ItemClassParent)
                {
                    SetItemClass(this.ItemClassParentTemp);
                }
                else
                {
                    System.Windows.MessageBox.Show("The item is already of the selected class.");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select an item class.");
            }
        }
        public void SetItemClass(ItemClass p)
        {
            ItemClassParent = p;
        }
        /// <summary>
        /// The <see cref="ItemProperties" /> property's name.
        /// </summary>
        public const string ItemPropertiesPropertyName = "ItemProperties";

        private ObservableCollection<ItemProperty> _itemProperties = new ObservableCollection<ItemProperty>();

        /// <summary>
        /// Sets and gets the ItemProperties property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ItemProperty> ItemProperties
        {
            get
            {
                return _itemProperties;
            }

            set
            {
                if (_itemProperties == value)
                {
                    return;
                }

                _itemProperties = value;
                RaisePropertyChanged(ItemPropertiesPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="SelectedProperty" /> property's name.
        /// </summary>
        public const string SelectedPropertyPropertyName = "SelectedProperty";

        private ItemProperty _selectedProperty = null;

        /// <summary>
        /// Sets and gets the SelectedProperty property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemProperty SelectedProperty
        {
            get
            {
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
            }
        }

        public XElement ToXml()
        {
            var props = ItemProperties.Where(a => !a.UseDefaultValue);
            return new XElement("Item",
                new XElement("Name", this.ItemName),
                new XElement("Id", this.ItemID),
                new XElement("DefaultName", this.DefaultName),
                new XElement("Description", this.Description.ToXML()),
                new XElement("ItemClass", this.ItemClassParent.Name),
                new XElement("Removable", this.Removable),
                new XElement("Properties", from a in this.ItemProperties select a.ToXml()),
                new XElement("OnUse", this.OnUse.ToXML()));
                                            
        }
        public static Item FromXml(XElement xml)
        {
            Item i = new Item();
            if (xml.Element("Name") != null)
            {
                i.ItemName = xml.Element("Name").Value;
            }
            if (xml.Element("Id") != null)  
            {
                i.ItemID = Guid.Parse(xml.Element("Id").Value);
            }
            if (xml.Element("DefaultName") != null)
            {
                i.DefaultName = xml.Element("DefaultName").Value;
            }
            if (xml.Element("Description") != null)
            {
                i.Description = Script.FromXML(xml.Element("Description").Element("Script"), i.Description);
            }
            if (xml.Element("OnUse") != null)
            {
                i.OnUse = Script.FromXML(xml.Element("OnUse").Element("Script"), i.OnUse);
            }
            if (xml.Element("Removable") != null)
            {
                i.Removable = Convert.ToBoolean(xml.Element("Removable").Value);
            }
            if (xml.Element("ItemClass") != null)
            {
                i.ItemClassParent = MainViewModel.MainViewModelStatic.ItemClasses.Where(a => a.Name == xml.Element("ItemClass").Value).FirstOrDefault();
            }
            //i.PopulateProperties();
            if (xml.Element("Properties") != null)
            {
                foreach (var a in xml.Element("Properties").Elements())
                {
                    ItemProperty ip = ItemProperty.FromXml(a);
                    var match = i.ItemProperties.Where(b => b.BaseVariable == ip.BaseVariable).FirstOrDefault();
                    if (match != null)
                    {
                        match.UseDefaultValue = ip.UseDefaultValue;
                        match.Value = ip.Value;
                    }
                        
                }
            }
            return i;

        }
    }

}
