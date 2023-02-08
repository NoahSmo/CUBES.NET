using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.View;
using WpfApp.ViewModel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected void ApplicationStart(object sender,StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged+=(s, ev)=>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainWindow();
                    mainView.Show();
                    mainView.WindowState = System.Windows.WindowState.Maximized;
                    loginView.Close();
                }
            };
        }

    }
}
