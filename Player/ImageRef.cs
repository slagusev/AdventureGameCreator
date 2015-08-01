using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    class ImageRef
    {
        public ImageRef() { }
        public ImageRef(ImageRef imgRef, int width, int height)
        {
            Width = width;
            Height = height;
            Path = imgRef.Path;
        }
        public string Path { get; set; }
        public int? Width;
        public int? Height;
    }
}
