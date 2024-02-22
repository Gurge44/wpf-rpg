using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Automation;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Fight : Window
    {
        public Fight()
        {
            InitializeComponent();
            

            Closing += OnClose;
        }

        public void OnClose(object? sender, CancelEventArgs e)
        {
            var x = new MainWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            x.Show();
            try
            {
                this.Close();
            }
            catch { }
        }

        private void Choose()
        {
            CurrentCharacterListPage.Instance ??= new();
            CurrentMainWindow.Instance?.Frame.NavigationService.Navigate(new ChooseToFight());
            Fight_label.Visibility = Visibility.Hidden;
        }

        private void Create_Team_Grid(int RoomNumber)
        {
            switch (RoomNumber)
            {
                case 1:
                    Fight_1();
                    break;
            }
        }

        private void Fight_1()
        {

        }

    }
}