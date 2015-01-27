using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.Conditions
{
    class Conditional : Brancher, INotifyPropertyChanged
    {

        //Condition property
        public Conditional()
        {
            this.Contents = new ObservableCollection<Script>();
            this.Contents.Add(new Script());
            //this.Contents[0].ScriptLines.Add(new Scripter.Misc.Blank());
            this.Contents.Add(new Script());
            //this.Contents[1].ScriptLines.Add(new Scripter.Misc.Blank());
            ThenStatement = this.Contents[0];
            ElseStatement = this.Contents[1];
        }

        /// <summary>
        /// The <see cref="ThenStatement" /> property's name.
        /// </summary>
        public const string ThenStatementPropertyName = "ThenStatement";

        private Script _thenStatement = null;

        /// <summary>
        /// Sets and gets the ThenStatement property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script ThenStatement
        {
            get
            {
                return _thenStatement;
            }

            set
            {
                if (_thenStatement == value)
                {
                    return;
                }

                _thenStatement = value;
                RaisePropertyChanged(ThenStatementPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ElseStatement" /> property's name.
        /// </summary>
        public const string ElseStatementPropertyName = "ElseStatement";

        private Script _elseStatement = null;

        /// <summary>
        /// Sets and gets the ElseStatement property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public Script ElseStatement
        {
            get
            {
                return _elseStatement;
            }

            set
            {
                if (_elseStatement == value)
                {
                    return;
                }

                _elseStatement = value;
                RaisePropertyChanged(ElseStatementPropertyName);
            }
        }

        public override string Plaintext
        {
            get
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("if");
                sb.AppendLine("{");
                string[] ifLines = string.Join("\n", from a in this.Contents[0].ScriptLines where a.GetType() != typeof(Editor.Scripter.Misc.Blank) select a.Plaintext).Split('\n');
                foreach (var line in ifLines)
                {
                    var tempLine = line.Replace("\r", "");
                    if (!string.IsNullOrWhiteSpace(tempLine))
                    {
                        sb.AppendLine("  " + tempLine);
                    }
                }
                sb.Append("}");
                if (this.Contents[1].ScriptLines.Count > 1)
                {
                    sb.AppendLine();
                    sb.AppendLine("Else");
                    sb.AppendLine("{");
                    var elseLines = (string.Join("\n", from a in this.Contents[1].ScriptLines where a.GetType() != typeof(Editor.Scripter.Misc.Blank) select a.Plaintext)).Split('\n');
                    foreach (var line in elseLines)
                    {
                        var tempLine = line.Replace("\r", "");
                        if (!string.IsNullOrWhiteSpace(tempLine))
                        {
                            sb.AppendLine("  " + tempLine);
                        }
                    }
                    sb.Append("}");
                }
                return sb.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override XElement ToXML()
        {
            return new XElement("If",new XElement("Condition" /*TODO: HANDLE CONDITION*/),
                                     new XElement("Then", ThenStatement.ToXML()),
                                     new XElement("Else", ElseStatement.ToXML()));    
        }

        public static Conditional FromXML(XElement xml)
        {
            Conditional c = new Conditional();
            //TODO: HANDLE CONDITION


            //Handle Then
            c.ThenStatement = Script.FromXML(xml.Element("Then").Element("Script"));
            c.Contents[0] = c.ThenStatement;

            //Handle Else
            c.ElseStatement = Script.FromXML(xml.Element("Else").Element("Script"));
            c.Contents[1] = c.ElseStatement;

            return c;
            

        }
    }
}
