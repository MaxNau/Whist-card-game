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

namespace CardGameX
{
    /// <summary>
    /// Interaction logic for GamePelletView.xaml
    /// </summary>
    public partial class GamePelletView : UserControl
    {
        public GamePelletView()
        {
            InitializeComponent();
           // DataContext = App.ViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          // int i = list4.Items.Count;
        }

        private void South_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null)
            {
                var cardImage = item.Content as BitmapImage;
                ((Models.VM)DataContext).ThrowCard(cardImage);
            }
        }
    }
}
