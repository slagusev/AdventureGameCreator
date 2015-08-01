﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Editor.Scripter.TextFunctions
{
    public class ShowImage : ScriptLine
    {
        /// <summary>
        /// The <see cref="Path" /> property's name.
        /// </summary>
        public const string PathPropertyName = "Path";

        private string _path = "";

        /// <summary>
        /// Sets and gets the Path property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                if (_path == value)
                {
                    return;
                }

                _path = value;
                RaisePropertyChanged(PathPropertyName);
            }
        }
        public override System.Xml.Linq.XElement ToXML()
        {
            return new XElement("ShowImage", Path);
        }
        public static ShowImage FromXML(XElement xml)
        {
            return new ShowImage() { Path = xml.Value };
        }
        public override string Plaintext
        {
            get
            {
                return "Display image: " + Path;
            }
        }
    }
}
