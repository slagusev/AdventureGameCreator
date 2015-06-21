using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.ObjectTypes
{
    public class GenericRef<T> : INotifyPropertyChanged
    {
        public GenericRef(Func<Guid, T> existsQuery, Func<T,Guid> idQuery)
        {
            ExistsQuery = existsQuery;
            this.idQuery = idQuery;
        }

        //public GenericRef(Func<Guid, VarArray> func1, Func<VarArray, Guid> func2)
        //{
        //    // TODO: Complete member initialization
        //    this.func1 = func1;
        //    this.func2 = func2;
        //}

        /// <summary>
        /// The <see cref="Value" /> property's name.
        /// </summary>
        public const string ValuePropertyName = "Value";

        private T _property = default(T);

        /// <summary>
        /// Sets and gets the Value property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public T Value
        {
            get
            {
                var result = ExistsQuery(Ref);
                {
                    _property = result;
                    
                }
                return _property;
            }

            set
            {

                
                _property = value;
                var id = idQuery(value);
                RaisePropertyChanged(ValuePropertyName);
                if (id != Ref)
                    Ref = id;
            }
        }

        /// <summary>
        /// The <see cref="Ref" /> property's name.
        /// </summary>
        public const string RefPropertyName = "Ref";

        private Guid _ref = Guid.Empty;

        /// <summary>
        /// Sets and gets the Ref property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid Ref
        {
            get
            {
                return _ref;
            }

            set
            {
                if (_ref == value)
                {
                    return;
                }

                _ref = value;
                RaisePropertyChanged(RefPropertyName);
                RaisePropertyChanged(ValuePropertyName);
            }
        }


        Func<Guid, T> ExistsQuery;
        Func<T,Guid> idQuery;

        public event PropertyChangedEventHandler PropertyChanged;
        private Func<Guid, VarArray> func1;
        private Func<VarArray, Guid> func2;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public static GenericRef<Room> GetRoomRef()
        {
            return new GenericRef<Room>(id =>
            {
                foreach (var a in MainViewModel.MainViewModelStatic.Zones)
                {
                    foreach (var b in a.Rooms)
                    {
                        if (b.RoomID == id)
                            return b;
                    }
                }
                return null;
            }, room =>
            {
                if (room != null)
                    return room.RoomID;
                else return Guid.Empty;
            });
        }
        public static GenericRef<VarArray> GetArrayRef()
        {
            return new GenericRef<VarArray>(id =>
            {
                foreach (var a in MainViewModel.MainViewModelStatic.Arrays)
                {
                    
                    if (a.Id == id)
                        return a;
                    
                }
                return null;
            }, arr =>
            {
                if (arr != null)
                    return arr.Id;
                else return Guid.Empty;
            });
        }
    }
}
