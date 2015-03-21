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
    public class ItemClass : INotifyPropertyChanged
    {

        /// <summary>
        /// The <see cref="ParentClass" /> property's name.
        /// </summary>
        public const string ParentClassPropertyName = "ParentClass";

        private ItemClass _parentClass = null;

        /// <summary>
        /// Sets and gets the ParentClass property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ItemClass ParentClass
        {
            get
            {
                return _parentClass;
            }

            set
            {
                if (_parentClass == value)
                {
                    return;
                }
                if (_parentClass != null)
                {
                    if (_parentClass.ChildClasses.Contains(this))
                        _parentClass.ChildClasses.Remove(this);
                }
                
                _parentClass = value;
                if (value != null)
                {
                    value.ChildClasses.Add(this);
                }
                RaisePropertyChanged(ParentClassPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ChildClasses" /> property's name.
        /// </summary>
        public const string ChildClassesPropertyName = "ChildClasses";

        private ObservableCollection<ItemClass> _childClasses = new ObservableCollection<ItemClass>();

        /// <summary>
        /// Sets and gets the ChildClasses property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ItemClass> ChildClasses
        {
            get
            {
                return _childClasses;
            }

            set
            {
                if (_childClasses == value)
                {
                    return;
                }

                _childClasses = value;
                RaisePropertyChanged(ChildClassesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ItemProperties" /> property's name.
        /// </summary>
        public const string ItemPropertiesPropertyName = "ItemProperties";

        private ObservableCollection<Variable> _itemProperties = new ObservableCollection<Variable>();

        /// <summary>
        /// Sets and gets the ItemProperties property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Variable> ItemProperties
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

        private Variable _selectedProperty = null;

        /// <summary>
        /// Sets and gets the SelectedProperty property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Variable SelectedProperty
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
                SelectedPropertyNotNull = SelectedProperty != null;
                RaisePropertyChanged(SelectedPropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedPropertyNotNull" /> property's name.
        /// </summary>
        public const string SelectedPropertyNotNullPropertyName = "SelectedPropertyNotNull";

        private bool _selectedPropertyNotNull = false;

        /// <summary>
        /// Sets and gets the SelectedPropertyNotNull property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool SelectedPropertyNotNull
        {
            get
            {
                return _selectedPropertyNotNull;
            }

            set
            {
                if (_selectedPropertyNotNull == value)
                {
                    return;
                }

                _selectedPropertyNotNull = value;
                RaisePropertyChanged(SelectedPropertyNotNullPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _name = "";

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="Items" /> property's name.
        /// </summary>
        public const string ItemsPropertyName = "Items";

        private ObservableCollection<Item> _items = new ObservableCollection<Item>();

        /// <summary>
        /// Sets and gets the Items property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Item> Items
        {
            get
            {
                return _items;
            }

            set
            {
                if (_items == value)
                {
                    return;
                }

                _items = value;
                RaisePropertyChanged(ItemsPropertyName);
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
        public static ItemClass GetBaseItemClass()
        {
            return new ItemClass
            {
                Name = "Item"
            };
        }

        public void Invalidate()
        {
            Items = new ObservableCollection<Item>(from a in MainViewModel.MainViewModelStatic.Items where a.ItemClassParent == this select a);
        }

        public XElement ToXML()
        {
            return new XElement("ItemClass", new XElement("Name", this.Name),
                                             new XElement("Parent", this.ParentClass != null ? this.ParentClass.Name : ""),
                                             new XElement("Properties", from a in this.ItemProperties select a.ToXml()));
        }
        public static ItemClass FromXML(XElement xml)
        {
            ItemClass i = new ItemClass();
            if (xml.Element("Name") != null)
            {
                i.Name = xml.Element("Name").Value;
            }
            if (xml.Element("Parent") != null)
            {
                i.ParentClass = (from a in MainViewModel.MainViewModelStatic.ItemClasses where a.Name == xml.Element("Parent").Value select a).FirstOrDefault();
            }
            if (xml.Element("Properties") != null)
            {
                i.ItemProperties = new ObservableCollection<Variable>(from a in xml.Element("Properties").Elements() select Variable.FromXML(a));
            }
            return i;
        }


        public List<Item> GetAllChildItems()
        {
            List<Item> childItems = new List<Item>();
            foreach (var a in this.Items)
            {
                childItems.Add(a);
            }
            foreach (var a in this.ChildClasses)
            {
                foreach (var b in a.GetAllChildItems())
                {
                    childItems.Add(b);
                }
            }
            return childItems;
        }

    }
}
