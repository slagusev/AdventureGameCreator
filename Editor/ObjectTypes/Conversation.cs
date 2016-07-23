using Editor.NewForms;
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
    public class Conversation : INotifyPropertyChanged
    {
        public Conversation()
        {
            WireCommands();
        }
        public void WireCommands()
        {
            this.NewStageCommand = new RelayCommand(NewStage);
            this.RemoveStageCommand = new RelayCommand(RemoveStage);
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
        /// The <see cref="SelectedStage" /> property's name.
        /// </summary>
        public const string SelectedStagePropertyName = "SelectedStage";

        private ConversationStage _selectedStage = null;

        /// <summary>
        /// Sets and gets the SelectedStage property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ConversationStage SelectedStage
        {
            get
            {
                return _selectedStage;
            }

            set
            {
                if (_selectedStage == value)
                {
                    return;
                }

                _selectedStage = value;
                RaisePropertyChanged(SelectedStagePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";

        private Guid _id = Guid.NewGuid();

        /// <summary>
        /// Sets and gets the Id property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Guid Id
        {
            get
            {
                return _id;
            }

            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;
                RaisePropertyChanged(IdPropertyName);
            }
        }
        
        /// <summary>
        /// The <see cref="Stages" /> property's name.
        /// </summary>
        public const string StagesPropertyName = "Stages";

        private ObservableCollection<ConversationStage> _stages = new ObservableCollection<ConversationStage>();

        /// <summary>
        /// Sets and gets the Stages property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ObservableCollection<ConversationStage> Stages
        {
            get
            {
                return _stages;
            }

            set
            {
                if (_stages == value)
                {
                    return;
                }

                _stages = value;
                RaisePropertyChanged(StagesPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="GroupName" /> property's name.
        /// </summary>
        public const string GroupNamePropertyName = "GroupName";

        private string _groupName = "Default";

        /// <summary>
        /// Sets and gets the GroupName property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string GroupName
        {
            get
            {
                return _groupName;
            }

            set
            {
                if (_groupName == value)
                {
                    return;
                }

                _groupName = value;
                RaisePropertyChanged(GroupNamePropertyName);
                if (MainViewModel.MainViewModelStatic != null)
                {
                    MainViewModel.MainViewModelStatic.ConversationGroups.RefreshInList(this);
                }
            }
        }


        /// <summary>
        /// The <see cref="StartingStage" /> property's name.
        /// </summary>
        public const string StartingStagePropertyName = "StartingStage";

        private int _startingStage = 0;

        /// <summary>
        /// Sets and gets the StartingStage property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int StartingStage
        {
            get
            {
                return _startingStage;
            }

            set
            {
                if (_startingStage == value)
                {
                    return;
                }

                _startingStage = value;
                RaisePropertyChanged(StartingStagePropertyName);
            }
        }

        public void NewStage()
        {
            var stage = new NewConversationStage();
            stage.associatedConvo = this;
            if (Stages.Count() > 0)
                stage.txtId.Text = (this.Stages.Select(a => a.StageId).Max() + 10).ToString();
            else
                stage.txtId.Text = "10";
            stage.ShowDialog();
            if (stage.DialogResult == true)
            {
                var res = new ConversationStage();
                res.StageId = Convert.ToInt32(stage.txtId.Text);
                res.StageName = stage.txtFriendlyName.Text;
                Stages.Add(res);
                Stages = new ObservableCollection<ConversationStage>(Stages.OrderBy(a => a.StageId));
                SelectedStage = res;
            }
        }
        public void RemoveStage()
        {
            if (SelectedStage != null)
            {
                Stages.Remove(SelectedStage);
                SelectedStage = null;
            }
            
        }
        public RelayCommand NewStageCommand { get; set; }
        public RelayCommand RemoveStageCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public XElement ToXML()
        {
            return new XElement("Conversation",
                new XElement("Name", this.Name),
                new XElement("Id", this.Id),
                new XElement("StartStage", this.StartingStage),
                new XElement("Stages", this.Stages.Select(a => a.ToXML())),
                new XElement("GroupName", this.GroupName));
        }
        public static Conversation FromXML(XElement xml)
        {
            Conversation c = new Conversation();
            if (xml.Element("Name") != null)
            {
                c.Name = xml.Element("Name").Value;
            }
            if (xml.Element("GroupName") != null)
            {
                c.GroupName = xml.Element("GroupName").Value;
            }
            if (xml.Element("Id") != null)
            {
                c.Id = Guid.Parse(xml.Element("Id").Value);
            }
            if (xml.Element("StartStage") != null)
            {
                c.StartingStage = Convert.ToInt32(xml.Element("StartStage").Value);
            }
            if (xml.Element("Stages") != null)
            {
                c.Stages = new ObservableCollection<ConversationStage>(xml.Element("Stages").Elements().Select(a => ConversationStage.FromXML(a)));
            }
            return c;
        }
    }
}
