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

        public string Fight { get; set; }

        public RoomType(string _title, string _description, string _image, string _fight)
        {
            Title = _title;
            Description = _description;
            Image = _image;
            Fight = _fight;
        }

        public static List<RoomType> StoryRooms = new List<RoomType>
            {
                //new RoomType("???", "You wake up with an aching head and no idea where you are. You look around and see that you are in what seems to be a prison cell. To your surprise, the door seems to be slightly open...", "prisoncell.jpg", ""),
                new RoomType("Armory", "You went through the door and you find yourself in an armory. It seems to have been looted, however you find a sword under all the rubble.", "pack://application:,,,/RPG;component\\Armory.jpg", ""),
                new RoomType("Gilded Hallways", "You find yourself in a gilded hallway adored by a number of breathtaking paintings and a tremendous amount of gold trim.\n\nYou must be in some kind of Royal Castle.", "pack://application:,,,/RPG;component\\GildedHallway.jpg", ""),
                new RoomType("Before the Great Doors", "You see a set of massive, gilded doors before you.\n\nYou sense a strong foe inside.", "pack://application:,,,/RPG;component\\GreatDoors.jpg", " (Fight)"),
                new RoomType("Victory", "You defeated the evil king!", "pack://application:,,,/RPG;component\\Victory.jpg", "")
            };

        public static List<RoomType> RandomRooms = new List<RoomType>
            {
                new RoomType("Treasure room", "You look around and you see riches. \n\nYou decide to take some.", "pack://application:,,,/RPG;component\\ChestRoom.jpg", " (Fight)"),
                new RoomType("Empty hallway", "It really is quite empty, you can see nothing worthy of note.", "pack://application:,,,/RPG;component\\Hallway.jpg", ""),
                new RoomType("Blacksmith room", "You look around and see a blacksmith's workshop. You decide to use the tools you found to sharpen your weapon. \n\nIt is now stronger.", "pack://application:,,,/RPG;component\\Blacksmith.jpg", " (Fight)"),
                new RoomType("Shrine room", "As you enter the room, you are overtaken by a feeling of some higher power. You see an altar with a basin of water. You are overtaken by the urge to drink from it.\n\nYou suddenly feel much better.", "pack://application:,,,/RPG;component\\GreatHall.jpg", " (Fight)"),
            };
    }
}
