using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RPG
{
    /// <summary>
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : Page
    {
        public Room()
        {
            InitializeComponent();

            List<RoomType> Rooms = new List<RoomType>
    {
        new RoomType("Treasure room", "Description1", "Room1.jpg"),
        new RoomType("Shrine room", "Description2", "altroom.jpg"),
    };

            Random random = new Random();
            RoomType selectedRoom = Rooms[random.Next(0, 2)];

            ChangeBackgroundImage(selectedRoom.Image);
            RoomTitleBox.Content = selectedRoom.Title;
            RoomDescriptionBox.Text = selectedRoom.Description;
        }

        private void ChangeBackgroundImage(string imagePath)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            Background = imageBrush;
        }

    }
}

