using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using CardGameX.CardGameXService;
using CardGameX.Models;
using CardGameX.CardGameXServiceCore;
using System.Collections.ObjectModel;

namespace CardGameX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /*public class ChatServiceCallback : IChatServiceCallback
    {
        public delegate void ClientNotifiedEventHandler(object sender, ClientNotifiedEventArgs e);
        public event ClientNotifiedEventHandler ClientNotified;

        public void HandleMessage(string value)
        {
            if (ClientNotified != null)
            {
                ClientNotified(this, new ClientNotifiedEventArgs(value));
            }
        }
    }

    public class CardGameServiceCallback : ICardGameServiceCallback
    {
        public delegate void ClientNotifiedEventHandler(object sender, ClientCallbackNotifiedEventArgs e);
        public event ClientNotifiedEventHandler ClientNotified;

        public void HandleMessage(string value)
        {
            if (ClientNotified != null)
            {
                ClientNotified(this, new ClientCallbackNotifiedEventArgs(value));
            }
        }

        public void HandleUri(string uri)
        {
            if (ClientNotified != null)
            {
                ClientNotified(this, new ClientCallbackNotifiedEventArgs(uri));
            }
        }

        public void Connected(int i)
        {
            if (ClientNotified != null)
            {
                ClientNotified(this, new ClientCallbackNotifiedEventArgs(i));
            }
        }
    }

    public class ClientNotifiedEventArgs : EventArgs
    {
        private readonly string message;

        public ClientNotifiedEventArgs(string message)
        {
            this.message = message;
        }

        public string Message { get { return message; } }
    }

    public class ClientCallbackNotifiedEventArgs : EventArgs
    {
        private readonly string uri;
        private readonly int i;

        public ClientCallbackNotifiedEventArgs(string uri)
        {
            this.uri = uri;
        }

        public ClientCallbackNotifiedEventArgs(int i)
        {
            this.i = i;
        }

        public string Uri { get { return uri; } }
        public int I { get { return i; } }
    }*/

    public partial class MainWindow : Window
    {
      /*  private Guid clientId;
        private string clientId2;
        private VM vm;
        private ChatServiceClient client;
        private InstanceContext instanceContext;
        private InstanceContext instanceContext2;
        private CardGameServiceClient client2;*/

        public MainWindow()
        {
            InitializeComponent();
            
        /*    ChatServiceCallback chatServiceCallback = new ChatServiceCallback();
            CardGameServiceCallback callback = new CardGameServiceCallback();

            chatServiceCallback.ClientNotified += ChatServiceCallback_ClientNotified;
            callback.ClientNotified += Callback_ClientNotified;

            instanceContext = new InstanceContext(chatServiceCallback);
            instanceContext2 = new InstanceContext(callback);

            client = new ChatServiceClient(instanceContext);
            client2 = new CardGameServiceClient(instanceContext2);*/

         //   ObservableCollection<BitmapImage> PlayerHand = new ObservableCollection<BitmapImage>();
          /*  try
            {
                clientId = client.Subscribe();
                clientId2 = client2.Subscribe();
            }
            catch
            {
            }*/

            /* foreach (string uri in client2.GetPlayerHand(clientId2))
             {
                 PlayerHand.Add(new BitmapImage(new Uri(@uri, UriKind.RelativeOrAbsolute)));
             }
             */
           
           // vm = new VM();
           // App.ViewModel.Inject(clientId2, client2);
          //  this.DataContext = vm;
            // list2.ItemsSource = vm.col;

        }

     //   private void Callback_ClientNotified1(object sender, ClientNotifiedEventArgs e)
     //   {
     //       throw new NotImplementedException();
     //   }

   //     private void Callback_ClientNotified(object sender, ClientCallbackNotifiedEventArgs e)
     //   {
            // player1card.Source = new BitmapImage(new Uri(e.Uri, UriKind.RelativeOrAbsolute));
      //      App.ViewModel.SwitchView = e.I;
     //       this.DataContext = App.ViewModel;
      //  }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }


        /*   private void EventSetter_OnHandler(object sender, RoutedEventArgs e)
           {
               var item = sender as ListViewItem;
              // var selected = list.SelectedIndex;

               if (item != null && item.IsSelected)
               {
                   string path = ((BitmapImage)item.Content).UriSource.AbsolutePath;
                   client2.GetUri(clientId2, path);

               //    player3card.Source = (BitmapImage)item.Content;
                 //  player2card.Source = (BitmapImage)item.Content;
                  // player1card.Source = (BitmapImage)item.Content;
                   //vm.col.RemoveAt(selected);
               }
              // list.ItemsSource = vm.col;

           }*/

        /*   private void textBox_KeyDown(object sender, KeyEventArgs e)
           {
               try
               {
                   if (e.Key == Key.Enter)
                   {
                     //  client.SendMessage(clientId, textBox.Text);
                    //   textBox.Text = string.Empty;
                   }
               }
               catch (Exception exception)
               {
                   MessageBox.Show(exception.Message);
               }
           }*/

        //    private void ChatServiceCallback_ClientNotified(object sender, ClientNotifiedEventArgs e)

        //    {
        // if (!string.IsNullOrEmpty(textBox.Text))
        //  {
        //     richTextBox.AppendText(textBox.Text += "\n");
        // }


        //richTextBox.AppendText(clientId.ToString() + ":" + " " + e.Message + "\r");



        //  }

        private void PositionPopup()
        {
            //BiddingPopUp.VerticalOffset = this.Top + (this.Height - BiddingPopUp.MaxHeight) / 2;
            //BiddingPopUp.HorizontalOffset = this.Left + (this.Width - BiddingPopUp.MaxWidth) / 2;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            PositionPopup();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PositionPopup();
            if (Pallet.WindowState == WindowState.Maximized)
            {
              //  BiddingPopUp.VerticalOffset = (SystemParameters.PrimaryScreenHeight - BiddingPopUp.MaxHeight) / 2;
                //BiddingPopUp.HorizontalOffset = (SystemParameters.PrimaryScreenWidth - BiddingPopUp.MaxWidth) / 2;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // BiddingPopUp.VerticalOffset = (SystemParameters.PrimaryScreenHeight - BiddingPopUp.MaxHeight) / 2;
           // BiddingPopUp.HorizontalOffset = (SystemParameters.PrimaryScreenWidth - BiddingPopUp.MaxWidth) / 2;
        }

        private void Pallet_StateChanged(object sender, EventArgs e)
        {
            //if (Pallet.WindowState == WindowState.Minimized)
            //{
               /* if (((VM)DataContext).PlayerHandEnabled != false)
                    BiddingPopUp.IsOpen = false;
            }
            else if (Pallet.WindowState == WindowState.Normal)
            {
                if (((VM)DataContext).PlayerHandEnabled != false)
                    BiddingPopUp.IsOpen = true;
            }
            else if (Pallet.WindowState == WindowState.Maximized)
            {
                if (((VM)DataContext).PlayerHandEnabled != false)
                    BiddingPopUp.IsOpen = true;
            }*/
        }
    }
}
