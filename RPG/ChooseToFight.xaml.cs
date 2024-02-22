using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RPG
{
    /// <summary>
    /// Interaction logic for ChooseToFight.xaml
    /// </summary>
    public partial class ChooseToFight : Window
    {
        public ChooseToFight()
        {
            InitializeComponent();
        }

        public void FillContent()
        {


            Grid grid = new()
            {
                Width = 50,
                Height = 50,
                Background = Brushes.Black
            };


        }

    }
}
