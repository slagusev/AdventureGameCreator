using Editor.Scripter.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.ScriptLineTypes
{
    class CommentWrapper : ScriptLineWrapper
    {
        public override System.Xml.Linq.XElement ToXML()
        {
            return Line.ToXML();
        }
        public CommentWrapper(Comment c)
        {
            Line = c;
        }
        public override bool? Execute()
        {
            return null;
        }
        public Comment Line { get; set; }
    }
}
