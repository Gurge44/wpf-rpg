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
                new RoomType("Treasure room", "You wake up with an aching head and no idea where you are. You look around and see that you are in what seems to be a prison cell.", "Room1.jpg"),
                new RoomType("Shrine room", "The door before you is unlike all others you have come across before. You sense a strong foe behind it.", "altroom.jpg"),
            };

            Random random = new Random();
            RoomType selectedRoom = Rooms[0];

            ChangeBackgroundImage(selectedRoom.Image);
            RoomTitleBox.Content = selectedRoom.Title;
            RoomDescriptionBox.Text = selectedRoom.Description;
            MainWindow.RoomCounter++;
        }

        private void ChangeBackgroundImage(string imagePath)
        {
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            Background = imageBrush;
        }

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            RoomFrame.NavigationService.Navigate(new Room());
        }
        private void SecondButton_Click(object sender, RoutedEventArgs e)
        {
            FirstButton_Click( sender, e);
        }
    }
}

