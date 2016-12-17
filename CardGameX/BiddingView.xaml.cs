using CardGameX.Models;
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
    /// Interaction logic for BiddingView.xaml
    /// </summary>
    public partial class BiddingView : UserControl
    {
        public BiddingView()
        {
            InitializeComponent();
        }

        private void ItemsControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((VM)DataContext).Bid((sender as Button).Content.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((VM)DataContext).Bid((sender as Button).Content.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ((VM)DataContext).Bid((sender as Button).Content.ToString());
        }
    }
}
