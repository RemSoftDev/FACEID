using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Net.NetworkInformation;

namespace FACEID.User
{
    public partial class test : PhoneApplicationPage
    {
        public test()
        {
            InitializeComponent();
        }
        private void UserNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UserNameTextBox.BorderBrush = new SolidColorBrush(Colors.Transparent);

            int i = Convert.ToInt16(UserNameTextBox.Tag);

            if (i == 1)
            {
                UserNameTextBox.Text = string.Empty;
            }
            UserNameTextBox.Tag = 0;

        }

        private void PasswodTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswodTextBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
            int i = Convert.ToInt16(PasswodTextBox.Tag);
            if (i == 1)
            {
                PasswodTextBox.Password = string.Empty;

            }
            PasswodTextBox.Tag = 0;
        }


        private void PasswodTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                //      SignIn_Click(sender, e);
                System.Windows.Input.GestureEventArgs a = new System.Windows.Input.GestureEventArgs();
                SignInButton_Tap(sender, a);
            }
        }


        private void SignInButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            int i = Convert.ToInt16(UserNameTextBox.Tag);

            //           NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));
            if (i == 0)
            {
                if (!string.IsNullOrEmpty(UserNameTextBox.Text) && !string.IsNullOrEmpty(PasswodTextBox.Password.ToString()))
                {
                    if (isNetworkConnected())
                    {
                        this.Dispatcher.BeginInvoke(delegate()
                        {
                            grdProgress.Visibility = Visibility.Visible;
                            ProgressBar.IsIndeterminate = true;
                        });
                        LoginWebService(UserNameTextBox.Text.ToString().Trim(), PasswodTextBox.Password.ToString().Trim());
                    }
                    else
                    { MessageBox.Show("Please Check Internet Connection"); }

                }
                else
                {
                    MessageBox.Show("Please Enter Details");
                }
            }
            else
            {

                MessageBox.Show("Please Enter Details");
            }

        }

        private void RegistrationTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (isNetworkConnected())
            {
                NavigationService.Navigate(new Uri("/User/WebBrowser.xaml?parameter=register", UriKind.RelativeOrAbsolute));
                //NavigationService.Navigate(new Uri("/Page1.xaml?parameter=test", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Please Check Internet Connection");

            }
        }

        private void UserNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UserNameTextBox.Tag = 0;
        }


        public async void LoginWebService(string strUserName, string strPassword)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://face.id/");
            var url = "api_faceidlogin";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            // HttpResponseMessage res = await client.PostAsync(string.Format(url,"dinesh.pathak@credencys.com","Dinesh123"));


            //  MultipartContent mul = new MultipartContent();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email",strUserName), 
                new KeyValuePair<string, string>("password",strPassword) 
            });


            HttpResponseMessage res = await client.PostAsync(url, formContent);
            this.Dispatcher.BeginInvoke(delegate()
            {
                grdProgress.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            });

            if (res.IsSuccessStatusCode)
            {

                var data = res.Content.ReadAsStringAsync();
                LoginResponse objResponse = JsonConvert.DeserializeObject<LoginResponse>(data.Result.ToString());



                if (objResponse != null)
                {
                    if (objResponse.status == 1)
                    {



                        if (!App.settings.Contains("username"))
                        {
                            App.settings.Add("username", strUserName);
                        }

                        if (objResponse.member_id != null)
                        {

                            if (objResponse.is_model_created == 1 && objResponse.has_faceid == 1)
                            {

                                // HOMEPAGE
                                //App.settings.Add("IdentityId",objResponse.identityID);
                                NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));

                            }


                            else if (objResponse.is_model_created == 0 && objResponse.has_faceid == 1)
                            {

                                //create model

                                if (!App.LoginDetail.Contains("ID"))
                                {
                                    App.LoginDetail.Add("ID", objResponse.data.ID);



                                    NavigationService.Navigate(new Uri("/User/ModelCreate.xaml", UriKind.RelativeOrAbsolute));
                                }
                            }
                            else if (objResponse.is_model_created == 0 && objResponse.has_faceid == 0)
                            {

                                //home
                                //App.settings.Add("IdentityId", objResponse.identityID);
                                NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));
                            }

                        }
                        else
                        {
                            this.Dispatcher.BeginInvoke(delegate()
                            {

                                MessageBox.Show("Enter Proper User Details");
                            });
                        }


                    }
                    else
                    {
                        //status not 1
                        this.Dispatcher.BeginInvoke(delegate()
                        {

                            MessageBox.Show("Enter Proper User Details");
                        });
                    }
                }
                //when objresponse !=null
                else
                {
                    this.Dispatcher.BeginInvoke(delegate()
                    {

                        MessageBox.Show("Enter Proper User Details");
                    });
                }



            }

                    //when response is not 200
            else
            {
                this.Dispatcher.BeginInvoke(delegate()
                {

                    MessageBox.Show("Enter Proper User Details");
                });
            }




        }

        public bool isNetworkConnected()
        {
            return DeviceNetworkInformation.IsNetworkAvailable;
        }

        private void ForgotPaswordLink_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            if (isNetworkConnected())
            {
                NavigationService.Navigate(new Uri("/User/WebBrowser.xaml?parameter=forgot", UriKind.RelativeOrAbsolute));
            }
            else
            {
                MessageBox.Show("Please Check Internet Connection");

            }
        }

        private void UserNameTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {

                PasswodTextBox.Focus();
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {

            e.Cancel = true;

            var result = MessageBox.Show("Do you want to exit?", "Attention!",
                                       MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {

                // IsolatedStorageSettings.ApplicationSettings.Save();
                Application.Current.Terminate();
                // base.OnBackKeyPress(e);
                //  return;
            }
            else
            {
                e.Cancel = true;
            }
        }
    
    
    }
}