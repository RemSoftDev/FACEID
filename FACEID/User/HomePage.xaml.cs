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
using System.IO.IsolatedStorage;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Phone.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace FACEID.User
{
    public partial class HomePage : PhoneApplicationPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void FaceIdTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FaceIdTextBox.BorderBrush = new SolidColorBrush(Colors.White);
        }

        private void SignOutButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.settings.Clear();
            if (App.settings.Contains("username"))
            {
                App.settings.Remove("username");
            }
            if (App.settings.Contains("MemberId"))
            {
                App.settings.Remove("MemberId");
            }
            NavigationService.Navigate(new Uri("/User/Login.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CapturedVideoImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!string.IsNullOrEmpty(FaceIdTextBox.Text))
            {
                if (isNetworkConnected())
                {
                    grdProgress.Visibility = Visibility.Visible;
                    ProgressBar.IsIndeterminate = true;
                    WebService(FaceIdTextBox.Text);
                }
                else
                {
                    MessageBox.Show("Please Check Internet Connection");
                }
            }
            else
            {
                MessageBox.Show("Please Enter FACE.ID");
            }
        }

        public static sbyte[] ConvertToSbyteArray(byte[] tempByte)
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

        public static string byteToHex(sbyte[] hash)
        {
            string res = string.Empty;
            foreach (sbyte b in hash)
            {
                res += b.ToString("x").PadLeft(2, '0');
            }
            return res;
        }


        public async void WebService(string strFaceId)
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();

                using (HttpClient client = new HttpClient(httpClientHandler))
                using (System.Net.Http.MultipartFormDataContent multipart = new MultipartFormDataContent())
                {
                    //client.BaseAddress = new Uri("http://face.id/");

                    // "http://face.id/faceid1/index.php/webservice/memberdetails"

                    string url = "https://face.id/api.php";



                    string a = "{\"member_id\":\"" + strFaceId + "\"}" + "c1IaqR8Dp7L6sLVbq5b2VlsouTN0ezNAYYnmf0WGb6zbxT7P";

                    byte[] bytes = Encoding.UTF8.GetBytes(a);
                    string token = string.Empty;

                    string ress = string.Empty;
                    using (SHA1Managed sha1 = new SHA1Managed())
                    {
                        sbyte[] hash = ConvertToSbyteArray(sha1.ComputeHash(bytes));
                        //ress = Convert.ToBase64String(hash);
                        token = byteToHex(hash);
                    }




                    var formContent = new FormUrlEncodedContent(new[]

                    {

                        new KeyValuePair<string, string>("request", "getUserId"),
                        new KeyValuePair<string, string>("data", "{\"member_id\":\""+ strFaceId + "\"}"),
                        new KeyValuePair<string, string>("token", token),
                        new KeyValuePair<string, string>("public_key", "faceid")

                    }
                    );

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.PostAsync(url, formContent);


                    // Debug.WriteLine("[FaceID] First API call data (1): " + response);

                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            var data = response.Content.ReadAsStringAsync();
                            // Debug.WriteLine("[FaceID] API[1] data: " + data);

                            JObject objData1 = JObject.Parse(data.Result.ToString());
                            JObject objData = (JObject)objData1["user_meta"];
                            //var objData = JsonConvert.DeserializeObject<IdResponse>(data.Result.ToString());

                            // Debug.WriteLine("[FaceID] First API call data (2): " + objData);

                            // Face model responses:
                            /*
                            1: go straight to scanning
                            2: Expired
                            3: exists but is not a member of the business
                            4: level 1 ID, not age verified
                            5: Not yet started
                            6: Not yet active
                            */

                            var ass = objData["account_status"][0].ToString();
                            int face_response = 0;
                            if (objData["account_status"][0].ToString() == "active")
                            {
                                face_response = 1;
                            }



                            Debug.WriteLine("[FaceID] Initial data response code:" + face_response);
                            if (objData["account_status"][0].ToString() == "active")
                            {
                                if (!App.settings.Contains("name"))
                                {
                                    App.settings.Add("name", objData["first_name"][0].ToString() + " " + objData["last_name"][0].ToString());
                                }
                                else
                                {
                                    App.settings["name"] = objData["first_name"][0].ToString() + " " + objData["last_name"][0].ToString();
                                }
                                if (!App.settings.Contains("dob"))
                                {
                                    App.settings.Add("dob", objData["dob"][0].ToString());
                                }
                                else
                                {
                                    App.settings["dob"] = objData["dob"][0].ToString();
                                }
                                if (!App.settings.Contains("member_id"))
                                {
                                    App.settings.Add("member_id", strFaceId);
                                }
                                else
                                {
                                    App.settings["member_id"] = strFaceId;
                                }
                                if (!App.settings.Contains("identity_id"))
                                {
                                    App.settings.Add("identity_id", objData["member_id"][0].ToString());
                                    Debug.WriteLine("Identity id   " + objData["member_id"][0].ToString());
                                }
                                else
                                {
                                    App.settings["identity_id"] = objData["member_id"][0].ToString();
                                    Debug.WriteLine("Identity id   " + objData["member_id"][0].ToString());
                                }
                                // Now send to the FaceID API to see if the logged in user is a business and that the punter is a member
                                var faceIDHttpClientHandler = new HttpClientHandler();
                                using (HttpClient faceIDClient = new HttpClient(faceIDHttpClientHandler))
                                using (System.Net.Http.MultipartFormDataContent faceIDMultipart = new MultipartFormDataContent())
                                {

                                    string faceIDAPIUrl = "https://face.id/api.php";

                                    string API_version = "?ver=1.0";

                                    string FACEID_API = "https://face.id/faceidapi.php";

                                    // Debug.WriteLine("[FaceID] Attempting to connect to API: " + faceIDAPIUrl);

                                    var faceIDAPIValuePairs = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("business_id", "193"), new KeyValuePair<string, string>("member_id", strFaceId) });
                                    HttpResponseMessage faceIDAPIresponse = new HttpResponseMessage();
                                    // Debug.WriteLine("[FaceID] API Response (1): " + faceIDAPIresponse);
                                    faceIDAPIresponse = await faceIDClient.PostAsync(faceIDAPIUrl, faceIDAPIValuePairs);
                                    // Debug.WriteLine("[FaceID] API Response (2): " + faceIDAPIresponse);

                                    // If 'business' comes back as "1" they're a business with globals set to 'no'
                                    // Otherwise they're a business who doesn't care about memberships or they're not a business
                                    if (faceIDAPIresponse.IsSuccessStatusCode)
                                    {
                                        Debug.WriteLine("[FaceID] Response is success");
                                        try
                                        {
                                            // Debug.WriteLine("[FaceID] Trying");
                                            var faceIDData = faceIDAPIresponse.Content.ReadAsStringAsync();
                                            // Debug.WriteLine("[FaceID] data: " + faceIDData);
                                            var faceIDObjData = JsonConvert.DeserializeObject<faceIDMemberData>(faceIDData.Result.ToString());
                                            Debug.WriteLine("[FaceID] API Data: " + faceIDObjData);
                                            // Initialise all of the possible data chunks that can come back with the API
                                            if (!App.settings.Contains("user_id"))
                                            {
                                                App.settings.Add("user_id", "");
                                            }
                                            else
                                            {
                                                App.settings["user_id"] = "";
                                            }
                                            if (!App.settings.Contains("user_level"))
                                            {
                                                App.settings.Add("user_level", "");
                                            }
                                            else
                                            {
                                                App.settings["user_level"] = "";
                                            }
                                            if (!App.settings.Contains("parent_account_id"))
                                            {
                                                App.settings.Add("parent_account_id", "");
                                            }
                                            else
                                            {
                                                App.settings["parent_account_id"] = "";
                                            }
                                            if (!App.settings.Contains("start_date"))
                                            {
                                                App.settings.Add("start_date", "");
                                            }
                                            else
                                            {
                                                App.settings["start_date"] = "";
                                            }
                                            if (!App.settings.Contains("expiry_date"))
                                            {
                                                App.settings.Add("expiry_date", "");
                                            }
                                            else
                                            {
                                                App.settings["expiry_date"] = "";
                                            }
                                            if (!App.settings.Contains("user_address1"))
                                            {
                                                App.settings.Add("user_address1", "");
                                            }
                                            else
                                            {
                                                App.settings["user_address1"] = "";
                                            }
                                            if (!App.settings.Contains("user_address2"))
                                            {
                                                App.settings.Add("user_address2", "");
                                            }
                                            else
                                            {
                                                App.settings["user_address2"] = "";
                                            }
                                            if (!App.settings.Contains("user_town"))
                                            {
                                                App.settings.Add("user_town", "");
                                            }
                                            else
                                            {
                                                App.settings["user_town"] = "";
                                            }
                                            if (!App.settings.Contains("user_county"))
                                            {
                                                App.settings.Add("user_county", "");
                                            }
                                            else
                                            {
                                                App.settings["user_county"] = "";
                                            }
                                            if (!App.settings.Contains("user_postcode"))
                                            {
                                                App.settings.Add("user_postcode", "");
                                            }
                                            else
                                            {
                                                App.settings["user_postcode"] = "";
                                            }
                                            if (!App.settings.Contains("user_data"))
                                            {
                                                App.settings.Add("user_data", "no");
                                            }
                                            else
                                            {
                                                App.settings["user_data"] = "no";
                                            }

                                            if (faceIDObjData.business.Equals("1"))
                                            {
                                                Debug.WriteLine("[FaceID] Business response: [1]");
                                                Debug.WriteLine("[FaceID] Member data: " + faceIDObjData.member);
                                                try
                                                {
                                                    if (faceIDObjData.member.Equals(null))
                                                    {
                                                        Debug.WriteLine("[FaceID] Null member data");
                                                        face_response = 3;
                                                    }
                                                    else
                                                    {
                                                        Debug.WriteLine("[FaceID] checking dates");
                                                        // Compare the start and expiry dates with today
                                                        DateTime startdate = DateTime.ParseExact(faceIDObjData.member.start_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                                        DateTime expirydate = DateTime.ParseExact(faceIDObjData.member.expiry_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                                                        DateTime dateToday = DateTime.Now;
                                                        // int dateCheckB = DateTime.Compare(startdate, dateToday);
                                                        // int dateCheckE = DateTime.Compare(expirydate, dateToday);
                                                        int dateCheckB = startdate.CompareTo(dateToday);
                                                        int dateCheckE = expirydate.CompareTo(dateToday);
                                                        Debug.WriteLine("[FaceID] Startdate: " + startdate + " | Expiry: " + expirydate + " | Today: " + dateToday);
                                                        Debug.WriteLine("[FaceID] Start date check: " + dateCheckB.ToString());
                                                        Debug.WriteLine("[FaceID] Expiry date check: " + dateCheckE.ToString());
                                                        if (dateCheckE < 0)
                                                        {
                                                            // Expired
                                                            face_response = 2;
                                                        }
                                                        else if (dateCheckE == 0)
                                                        {
                                                            // Expires today
                                                            face_response = 1;
                                                        }
                                                        else if (dateCheckE > 0)
                                                        {
                                                            // Expires in the future
                                                            face_response = 1;
                                                        }
                                                        if (dateCheckB > 0)
                                                        {
                                                            // Starts in the future
                                                            face_response = 5;
                                                        }
                                                        // Update the data  as neccesary
                                                        App.settings["user_id"] = faceIDObjData.member.user_id;
                                                        App.settings["parent_account_id"] = faceIDObjData.member.parent_account_id;
                                                        App.settings["start_date"] = faceIDObjData.member.start_date;
                                                        App.settings["expiry_date"] = faceIDObjData.member.expiry_date;
                                                        App.settings["user_level"] = faceIDObjData.member.user_level;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Debug.WriteLine("[FaceID] Error in the data checking: " + ex);
                                                }
                                            }
                                            else
                                            {
                                                Debug.WriteLine("[FaceID] Business response: " + faceIDObjData.business);
                                                if (faceIDObjData.member.Equals(null))
                                                {
                                                    face_response = 6;
                                                }
                                                else
                                                {
                                                    // Update the data  as neccesary
                                                    App.settings["user_id"] = faceIDObjData.member.user_id;
                                                    App.settings["user_level"] = faceIDObjData.member.user_level;
                                                    App.settings["user_address1"] = faceIDObjData.member.user_address1;
                                                    App.settings["user_address2"] = faceIDObjData.member.user_address2;
                                                    App.settings["user_town"] = faceIDObjData.member.user_town;
                                                    App.settings["user_county"] = faceIDObjData.member.user_county;
                                                    App.settings["user_postcode"] = faceIDObjData.member.user_postcode;
                                                    App.settings["user_data"] = faceIDObjData.member.user_data;

                                                    switch (faceIDObjData.member.user_level)
                                                    {
                                                        case "2":
                                                            // Age verification details supplied
                                                            face_response = 1;
                                                            break;
                                                        default:
                                                            // Age not verified
                                                            face_response = 4;
                                                            break;
                                                    }
                                                }
                                            }
                                            Debug.WriteLine("[FaceID] User ID: " + faceIDObjData.member.user_id.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            // Error Handling here
                                            Debug.WriteLine("[FaceID] Something's gone horribly wrong: " + ex);
                                        }
                                    }
                                    if (face_response == 1)
                                    {
                                        Debug.WriteLine("[FaceID] Passed data verification, going to scan");
                                        NavigationService.Navigate(new Uri("/User/Authenticate.xaml", UriKind.RelativeOrAbsolute));
                                    }
                                    else
                                    {
                                        Debug.WriteLine("[FaceID] Can't scan this one");
                                        String theMessage = "";
                                        switch (face_response)
                                        {
                                            case 2:
                                                theMessage = "This user account's membership has expired.";
                                                break;
                                            case 3:
                                                theMessage = "This user cannot be verified with this business account.";
                                                break;
                                            case 4:
                                                theMessage = "This is a Level 1 FACE.ID and cannot be used for proof of age or proof of ID.";
                                                break;
                                            case 5:
                                                theMessage = "This user account's membership has not started yet.";
                                                break;
                                            case 6:
                                                theMessage = "This member ID is not active.";
                                                break;
                                        }
                                        Debug.WriteLine("[FaceID] Data response: " + theMessage);
                                        this.Dispatcher.BeginInvoke(delegate ()
                                        {
                                            grdProgress.Visibility = Visibility.Collapsed;
                                            ProgressBar.IsIndeterminate = false;
                                            MessageBox.Show(theMessage);
                                        });

                                    }
                                }
                            }
                            else
                            {
                                this.Dispatcher.BeginInvoke(delegate ()
                                {
                                    grdProgress.Visibility = Visibility.Collapsed;
                                    ProgressBar.IsIndeterminate = false;
                                    MessageBox.Show("FACEID Not Found");
                                });
                            }

                        }
                        catch (Exception)
                        {
                            this.Dispatcher.BeginInvoke(delegate ()
                            {
                                grdProgress.Visibility = Visibility.Collapsed;
                                ProgressBar.IsIndeterminate = false;
                                MessageBox.Show("Please Try Again");
                            });
                        }

                    }
                    else
                    {
                        this.Dispatcher.BeginInvoke(delegate ()
                        {
                            grdProgress.Visibility = Visibility.Collapsed;
                            ProgressBar.IsIndeterminate = false;
                            MessageBox.Show("FACEID Not Found");
                        });
                    }

                }
            }
            catch (Exception)
            {
                this.Dispatcher.BeginInvoke(delegate ()
                {
                    grdProgress.Visibility = Visibility.Collapsed;
                    ProgressBar.IsIndeterminate = false;
                    MessageBox.Show("Please Try Again");
                });
            }


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (App.settings.Contains("username"))
            {
                EmailTextBlock.Text = (string)App.settings["username"];
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            var result = MessageBox.Show("Do you want to exit?", "Attention!", MessageBoxButton.OKCancel);

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

        public bool isNetworkConnected()
        {
            return DeviceNetworkInformation.IsNetworkAvailable;
        }


        private void FaceIdTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                System.Windows.Input.GestureEventArgs a = null;
                CapturedVideoImage_Tap(sender, a);
            }
        }
    }
}