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
using Newtonsoft;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using System.IO.IsolatedStorage;
using Microsoft.Phone.Net.NetworkInformation;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;


namespace FACEID.User
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();


            Resolutions res = ResolutionHelper.CurrentResolution;

        }

        private void UserNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UserNameTextBox.BorderBrush = new SolidColorBrush(Colors.White);

            int i = Convert.ToInt16(UserNameTextBox.Tag);

            if (i == 1)
            {
                UserNameTextBox.Text = string.Empty;
            }
            UserNameTextBox.Tag = 0;

        }

        private void PasswodTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswodTextBox.BorderBrush = new SolidColorBrush(Colors.White);
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
                        this.Dispatcher.BeginInvoke(delegate ()
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


        public static string LOGIN_URL = "https://face.id/api.php";

        private static sbyte[] ConvertToSbyteArray(byte[] tempByte)
        {
            sbyte[] res = new sbyte[tempByte.Length];


            for (int i = 0; i < res.Length; i++)
            {
                if (tempByte[i] >= 128)
                {
                    res[i] = Convert.ToSByte(tempByte[i] - 256);
                }
                else
                {
                    res[i] = Convert.ToSByte(tempByte[i]);
                }
            }

            return res;
        }

        private static string byteToHex(sbyte[] hash)
        {
            string res = string.Empty;
            foreach (sbyte b in hash)
            {
                res += b.ToString("x").PadLeft(2, '0');
            }
            return res;
        }

        public async void LoginWebService(string strUserEmail, string strPassword)
        {
            HttpClient client = new HttpClient();


            //client.BaseAddress = new Uri(LOGIN_URL);
            //var url = "api_faceidlogin";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            // HttpResponseMessage res = await client.PostAsync(string.Format(url,"dinesh.pathak@credencys.com","Dinesh123"));
            //  MultipartContent mul = new MultipartContent();

            JObject jObj = new JObject();

            jObj.Add("email", strUserEmail);
            jObj.Add("password", strPassword);

            string a = "{\"email\":\"" + strUserEmail + "\",\"password\":\"" + strPassword + "\"}" + "c1IaqR8Dp7L6sLVbq5b2VlsouTN0ezNAYYnmf0WGb6zbxT7P";

            byte[] bytes = Encoding.UTF8.GetBytes(a);
            string token = string.Empty;

            string ress = string.Empty;
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                sbyte[] hash = ConvertToSbyteArray(sha1.ComputeHash(bytes));
                //ress = Convert.ToBase64String(hash);
                token = byteToHex(hash);
            }

            //myString = Encoding.UTF8.GetString(bytes);

            //String token = encryptPassword(a);


            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("request", "authenticateUser"),
                new KeyValuePair<string, string>("data","{\"email\":\"" + strUserEmail + "\",\"password\":\"" + strPassword + "\"}"),
                new KeyValuePair<string, string>("token",token),
                new KeyValuePair<string, string>("public_key","faceid")

            });

            // client.Timeout = new TimeSpan(15000); ;
            HttpResponseMessage res = await client.PostAsync(LOGIN_URL, formContent);


            this.Dispatcher.BeginInvoke(delegate ()
            {
                grdProgress.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
            });

            if (res.IsSuccessStatusCode)
            {

                var data = res.Content.ReadAsStringAsync();

                JObject jobject = JObject.Parse(data.Result.ToString());
                var jobjectList = jobject.Children();

                JObject jsonObjectUserMeta = (JObject)jobject.GetValue("user_meta");
                if (!App.settings.Contains("username"))
                {
                    App.settings.Add("username", strUserEmail);
                }

                if (jobject["user_id"] != null)
                {

                    // HOMEPAGE
                    //App.settings.Add("IdentityId",objResponse.identityID);
                    NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));
                    
                    if (jsonObjectUserMeta["has_faceid"].Equals("Yes"))
                    {
                        //create model

                        if (!App.LoginDetail.Contains("ID"))
                        {
                            App.LoginDetail.Add("ID", jobject["user_id"]);

                            NavigationService.Navigate(new Uri("/User/ModelCreate.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                    else
                    {
                        //home
                        //App.settings.Add("IdentityId", objResponse.identityID);
                        NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));
                    }

                }
                else
                {
                    this.Dispatcher.BeginInvoke(delegate ()
                    {

                        MessageBox.Show("Enter Proper User Details");
                    });
                }


                if (!jsonObjectUserMeta.ToString().Equals(" "))
                {
                    JArray jsonNickname = (JArray)jsonObjectUserMeta.GetValue("nickname");

                    //App.settings.Add("username", jsonNickname.First);
                    NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));

                }
                #region olld code 

                //LoginResponse objResponse = JsonConvert.DeserializeObject<LoginResponse>(data.Result.ToString());

                //if (objResponse != null)
                //{
                //    if (objResponse.status == 0)
                //    {

                //        if (!App.settings.Contains("username"))
                //        {
                //            App.settings.Add("username", strUserEmail);
                //        }

                //        if (objResponse.member_id != null)
                //        {

                //            if (objResponse.is_model_created == 1 && objResponse.has_faceid == 1)
                //            {

                //                // HOMEPAGE
                //                //App.settings.Add("IdentityId",objResponse.identityID);
                //                NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));

                //            }


                //            else if (objResponse.is_model_created == 0 && objResponse.has_faceid == 1)
                //            {

                //                //create model

                //                if (!App.LoginDetail.Contains("ID"))
                //                {
                //                    App.LoginDetail.Add("ID", objResponse.data.ID);



                //                    NavigationService.Navigate(new Uri("/User/ModelCreate.xaml", UriKind.RelativeOrAbsolute));
                //                }
                //            }
                //            else if (objResponse.is_model_created == 0 && objResponse.has_faceid == 0)
                //            {

                //                //home
                //                //App.settings.Add("IdentityId", objResponse.identityID);
                //                NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));
                //            }

                //        }
                //        else
                //        {
                //            this.Dispatcher.BeginInvoke(delegate ()
                //            {

                //                MessageBox.Show("Enter Proper User Details");
                //            });
                //        }


                //    }
                //    else
                //    {
                //        //status not 1
                //        this.Dispatcher.BeginInvoke(delegate ()
                //        {

                //            MessageBox.Show("Enter Proper User Details");
                //        });
                //    }
                //}
                ////when objresponse !=null
                //else
                //{
                //    this.Dispatcher.BeginInvoke(delegate ()
                //    {

                //        MessageBox.Show("Enter Proper User Details");
                //    });
                //}
                #endregion
            }

            //when response is not 200
            else
            {
                this.Dispatcher.BeginInvoke(delegate ()
                {
                    MessageBox.Show("Enter Proper User Details");
                });
            }
        }

        //private static string encryptPassword(string password)
        //{
        //    string sha1 = "";
        //    try
        //    {
        //        MessageDigest crypt = MessageDigest.getInstance("SHA-1");
        //        crypt.reset();
        //        crypt.update(password.getBytes("UTF-8"));
        //        sha1 = byteToHex(crypt.digest());
        //    }
        //    catch (NoSuchAlgorithmException e)
        //    {
        //        e.printStackTrace();
        //    }
        //    catch (UnsupportedEncodingException e)
        //    {
        //        e.printStackTrace();
        //    }
        //    return sha1;
        //}

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