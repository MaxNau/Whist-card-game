using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CardGameX.Models
{
    public class Player
    {
        public string Name;
        public ObservableCollection<BitmapImage> PlayerHand { get; set; }

        public Player()
        {
            PlayerHand = new ObservableCollection<BitmapImage>();
            /*PlayerHand.Add(new BitmapImage(new Uri(@"pack://application:,,,/Content/CardFaces/DiamondAce.png", UriKind.RelativeOrAbsolute)));
            PlayerHand.Add(new BitmapImage(new Uri(@"pack://application:,,,/Content/CardFaces/DiamondKing.png", UriKind.RelativeOrAbsolute)));
            PlayerHand.Add(new BitmapImage(new Uri(@"/Content/CardFaces/DiamondJack.png", UriKind.RelativeOrAbsolute)));
            PlayerHand.Add(new BitmapImage(new Uri(@"/Content/CardFaces/DiamondTen.png", UriKind.RelativeOrAbsolute)));
            PlayerHand.Add(new BitmapImage(new Uri(@"/Content/CardFaces/DiamondNine.png", UriKind.RelativeOrAbsolute)));
            PlayerHand.Add(new BitmapImage(new Uri(@"/Content/CardFaces/DiamondEight.png", UriKind.RelativeOrAbsolute)));
            PlayerHand.Add(new BitmapImage(new Uri(@"/Content/CardFaces/DiamondSeven.png", UriKind.RelativeOrAbsolute)));
            PlayerHand.Add(new BitmapImage(new Uri(@"/Content/CardFaces/DiamondAce.png", UriKind.RelativeOrAbsolute)));
            PlayerHand.Add(new BitmapImage(new Uri(@"/Content/CardFaces/DiamondAce.png", UriKind.RelativeOrAbsolute)));
            PlayerHand.Add(new BitmapImage(new Uri(@"/Content/CardFaces/DiamondAce.png", UriKind.RelativeOrAbsolute)));*/
        }
    }
}
