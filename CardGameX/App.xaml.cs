using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CardGameX
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Models.VM viewModel; //not sure how your viewmodel class is named
        public static Models.VM ViewModel   //and a property to access it from
        {
            get
            {
                if (viewModel == null)               //which creates the viewModel just before
                    viewModel = new Models.VM(); //it's first used
                return viewModel;
            }
        }
    }
}
