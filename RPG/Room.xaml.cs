using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RPG
{
    /// <summary>
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : Page
    {
        Random r = new Random();
        RoomType NextFirstRoom, NextSecondRoom, NextThirdRoom;
        int Counter = 0;
        public Room()
        {
            InitializeComponent();
            MainGrid.Visibility = Visibility.Visible;
        }

        private void ChangeBackgroundImage(string imagePath)
        {
            RoomBackground.ImageSource = new BitmapImage(new Uri($"{imagePath}", UriKind.Absolute));
        }


        private void SceneChange(RoomType current)
        {
                NextFirstRoom = RoomType.RandomRooms[r.Next(RoomType.RandomRooms.Count())];
                NextSecondRoom = RoomType.RandomRooms[r.Next(RoomType.RandomRooms.Count())];
                NextThirdRoom = RoomType.RandomRooms[r.Next(RoomType.RandomRooms.Count())];
                FirstButton.Visibility = Visibility.Visible;
                SecondButton.Visibility = Visibility.Visible;
                ThirdButton.Visibility = Visibility.Visible;
                FirstButton.Content = NextFirstRoom.Title + "" + NextFirstRoom.Fight;
                SecondButton.Content = NextSecondRoom.Title + "" + NextSecondRoom.Fight;
                ThirdButton.Content = NextThirdRoom.Title + "" + NextThirdRoom.Fight;
                Counter++;
                if ((Counter + 1) % 3 == 0)
                {
                    FirstButton.Content = RoomType.StoryRooms[(Counter + 1) / 3].Title;
                    NextFirstRoom = RoomType.StoryRooms[(Counter + 1) / 3];
                    SecondButton.Visibility = Visibility.Collapsed;
                    ThirdButton.Visibility = Visibility.Collapsed;
                }
            if (Counter == 6)
            {
                current = RoomType.StoryRooms[RoomType.StoryRooms.Count-2];
                FirstButton.Content = "Proceed";
                SecondButton.Visibility = Visibility.Collapsed;
                ThirdButton.Visibility = Visibility.Collapsed;
            }
            if (Counter == 7)
            {
                current = RoomType.StoryRooms[RoomType.StoryRooms.Count - 1];
                FirstButton.Content = "Exit";
                SecondButton.Visibility = Visibility.Collapsed;
                ThirdButton.Visibility = Visibility.Collapsed;
            }
            ChangeBackgroundImage(current.Image);
            RoomTitleBox.Content = current.Title;
            RoomDescriptionBox.Text = current.Description;
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Collapsed;
            SceneChange(RoomType.StoryRooms[0]);

        }
        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            if (NextFirstRoom.Fight != "")
            {
                Main.ShowFight(Counter);
            }
            if (Counter!=7)
            {
                SceneChange(NextFirstRoom);
            }
            else
            {
                MainGrid.Visibility = Visibility.Collapsed;
            }
        }
        private void SecondButton_Click(object sender, RoutedEventArgs e)
        {
            if (NextSecondRoom.Fight != "")
            {
                Main.ShowFight(Counter);
            }
            SceneChange(NextSecondRoom);
        }
        private void ThirdButton_Click(object sender, RoutedEventArgs e)
        {
            if (NextThirdRoom.Fight != "")
            {
                Main.ShowFight(Counter);
            }
            SceneChange(NextThirdRoom);
        }
    }
}

