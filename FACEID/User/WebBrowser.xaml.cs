using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace FACEID.User
{
    public partial class WebBrowser : PhoneApplicationPage
    {
        public string url { get; set; }
        public WebBrowser()
        {
            InitializeComponent();

            webBrowser1.Loaded += webBrowser1_Loaded;
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string parameterValue = NavigationContext.QueryString["parameter"];
            grdProgress.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;

            

            if (parameterValue == "register")
            {
                url = "https://face.id/register/";

                webBrowser1.Source = new Uri(url, UriKind.Absolute);

            
            }
            else if (parameterValue == "forgot")
            {
                url = "https://face.id/wp-login.php";

                webBrowser1.Source = new Uri(url, UriKind.Absolute);
        
        
        }
            
            
            }

        private void webBrowser1_Loaded(object sender, RoutedEventArgs e)
        {
            grdProgress.Visibility = Visibility.Collapsed;
            ProgressBar.IsIndeterminate = false;
        }



        
    }
}