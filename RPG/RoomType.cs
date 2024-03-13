using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class RoomType
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public RoomType(string _title, string _description, string _image) 
        { 
            Title = _title;
            Description = _description;
            Image = _image;
        }
    }
}
