using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    public class GenericGroup<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// The <see cref="Groups" /> property's name.
        /// </summary>
        public const string GroupsPropertyName = "Groups";

        private ObservableCollection<Tuple<string, ObservableCollection<T>>> _groups = new ObservableCollection<Tuple<string, ObservableCollection<T>>>();

        /// <summary>
        /// Sets and gets the Groups property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<Tuple<string, ObservableCollection<T>>> Groups
        {
            get
            {
                return _groups;
            }

            set
            {
                if (_groups == value)
                {
                    return;
                }

                _groups = value;
                RaisePropertyChanged(GroupsPropertyName);
            }
        }
        ObservableCollection<T> boundCollection;
        Func<T, string> Parser;
        Func<T, string> NameGrabber;
        public GenericGroup(ObservableCollection<T> collection, Func<T, string> keyFunc, Func<T, string> objName)
        {

            boundCollection = collection;
            boundCollection.CollectionChanged += boundCollection_CollectionChanged;
            Parser = keyFunc;
            NameGrabber = objName;
            foreach (var a in collection)
            {
                AddToList(Parser(a),a);
            }
        }

        private T ConvertToT(object a)
        {
            return (T)(Convert.ChangeType(a, typeof(T)));
        }
        void boundCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (boundCollection != sender)
            {
                return;
            }
            else
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (var a in e.NewItems)
                        {
                            var b = ConvertToT(a);
                            AddToList(Parser(b), b);
                        }
                        
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (var a in e.OldItems)
                        {
                            var b = ConvertToT(a);
                            RemoveObject(b);
                        }
                        break;
                }
            }
        }
        public void RefreshAll()
        {
            Groups.Clear();
            if (boundCollection != null)
            {
                foreach (var a in boundCollection)
                    AddToList(Parser(a), a);
            }
        }
        private void AddToList(string key, T obj)
        {
            var group = Groups.Where(a => a.Item1 == key).FirstOrDefault();
            int i;
            if (group == null)
            {
                group = new Tuple<string,ObservableCollection<T>>(key, new ObservableCollection<T>());
                for (i = 0; i < Groups.Count() && Groups[i].Item1.CompareTo(group.Item1) < 0; i++) ;
                Groups.Insert(i, group);
            }

            for (i = 0; i < group.Item2.Count() && NameGrabber(group.Item2[i]).CompareTo(NameGrabber(obj)) < 0; i++) ;
            group.Item2.Insert(i, obj);
         }
        public void RefreshInList(T obj)
        {
            if (RemoveObject(obj) || boundCollection.Contains(obj))
                AddToList(Parser(obj), obj);
        }
        private bool RemoveObject(T obj)
        {
            var group = (from a in Groups where a.Item2.Contains(obj) select a).FirstOrDefault();
            if (group != null)
            {
                group.Item2.Remove(obj);
                return true;
            }
            else return false;
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
