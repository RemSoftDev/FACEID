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
using System.Windows.Media.Imaging;
using Microsoft.Devices;
using System.Threading;
using System.IO;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media.Animation;
using System.Net.Http;
using Microsoft.Phone.Net.NetworkInformation;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FACEID.User
{
    public partial class ModelCreate : PhoneApplicationPage
    {

        #region Variable Declaration
        string page1 = "Welcome to FACE.ID. In order for the software to get to know the individual features of your face (Biometrics) you need to take 8 short videos and they are best taken at home and during the day. Each video needs to be taken under different lighting conditions to ensure a perfect match.";
        string page2 = "You'll be given suggestions at the start of each take on where best to position yourself around your home. Please ensure you complete the whole process in one take, you will need about 3 minutes.";

        string page3 = "When taking the videos, make sure you hold the phone close enough so you fill the whole screen with your face, ignore the icons at the top of the screen that will cover your forehead slightly… a larger shot of your face the better! Avoid direct sunlight onto your face whilst keeping a natural facial pose.";
        string page4 = "The App will talk you through the process - its a doddle.";
    //    string page5 = "The App will talk you through the process - its a doddle.";
        int Skiptag = 0;
        PhotoCamera cam = new PhotoCamera();
        private static ManualResetEvent pauseFramesEvent = new ManualResetEvent(true);
        private WriteableBitmap wb;
       
       
        MultiResImageChooserUri mul = new MultiResImageChooserUri();
        IsolatedStorageFile isStore = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();
        // ImageBrush BlackRoundImage = new ImageBrush();
        // ImageBrush OrangeRoundImage = new ImageBrush();
        BitmapImage BlackRoundImage = new BitmapImage();
        BitmapImage OrangeRoundImage = new BitmapImage();
        #endregion

        #region Events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


            // Check to see if the camera is available on the phone.
            try
            {


                if (PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing) == true)
                {
                    // Initialize the default camera.


                    cam = new Microsoft.Devices.PhotoCamera(CameraType.FrontFacing);

                    //Set the VideoBrush source to the camera

                    viewfinderBrush.RelativeTransform = new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 90, ScaleX = -1 };
                }
                else
                {
                    cam = new Microsoft.Devices.PhotoCamera(CameraType.FrontFacing);
                    viewfinderBrush.RelativeTransform = new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 90 };
                }
                viewfinderBrush.SetSource(cam);
            }
            catch (Exception)
            {

                this.Dispatcher.BeginInvoke(delegate()
                {

                    MessageBox.Show("Camera Required For This App");
                });

            }

            if (App.settings.Contains("ListOfMemberIds"))
            {

                BlackRoundImage = new BitmapImage(mul.RoundImageBlack);

                OrangeRoundImage = new BitmapImage(mul.RoundImage);

                int countmemberIds = ((List<String>)App.settings["ListOfMemberIds"]).Count;

                switch (countmemberIds)
                {
                    case 1:

                        grdCongratulation.Visibility = Visibility.Collapsed;
                        grd1.Visibility = Visibility.Visible;
                        grd2.Visibility = Visibility.Visible;
                        grd3.Visibility = Visibility.Visible;

                        Line1.Visibility = Visibility.Visible;
                        Line2.Visibility = Visibility.Visible;
                        Line3.Visibility = Visibility.Visible;
                        Line4.Visibility = Visibility.Visible;



                        RoundImage.Source = BlackRoundImage;
                        RoundImage1.Source = OrangeRoundImage;
                        RoundImage2.Source = BlackRoundImage;


                        NumberTextBlock1.Text = "1";
                        NumberTextBlock2.Text = "2";
                        NumberTextBlock3.Text = "3";

                        GuidTextBlock.Visibility = Visibility.Visible;
                        GuidTextBlock.Text = "Step into the middle of the same room, well away from the window.";

                        StartScaningButton.Visibility = Visibility.Visible;
                        FocasImage.Visibility = Visibility.Visible;
                        grdSwapCamera.Visibility = Visibility.Visible;
                        break;

                    case 2:
                        grd1.Visibility = Visibility.Visible;
                        grd2.Visibility = Visibility.Visible;
                        grd3.Visibility = Visibility.Visible;

                        Line1.Visibility = Visibility.Visible;
                        Line2.Visibility = Visibility.Visible;
                        Line3.Visibility = Visibility.Visible;
                        Line4.Visibility = Visibility.Visible;

                        RoundImage.Source = BlackRoundImage;
                        RoundImage1.Source = OrangeRoundImage;
                        RoundImage2.Source = BlackRoundImage;

                        NumberTextBlock1.Text = "2";
                        NumberTextBlock2.Text = "3";
                        NumberTextBlock3.Text = "4";

                        GuidTextBlock.Visibility = Visibility.Visible;
                        GuidTextBlock.Text = "Take a video in the bathroom but, please face a window.";

                        StartScaningButton.Visibility = Visibility.Visible;
                        FocasImage.Visibility = Visibility.Visible;
                        grdSwapCamera.Visibility = Visibility.Visible;
                        break;


                    case 3:
                        grd1.Visibility = Visibility.Visible;
                        grd2.Visibility = Visibility.Visible;
                        grd3.Visibility = Visibility.Visible;

                        Line1.Visibility = Visibility.Visible;
                        Line2.Visibility = Visibility.Visible;
                        Line3.Visibility = Visibility.Visible;
                        Line4.Visibility = Visibility.Visible;

                        RoundImage.Source = BlackRoundImage;
                        RoundImage1.Source = OrangeRoundImage;
                        RoundImage2.Source = BlackRoundImage;


                        NumberTextBlock1.Text = "3";
                        NumberTextBlock2.Text = "4";
                        NumberTextBlock3.Text = "5";

                        GuidTextBlock.Visibility = Visibility.Visible;
                        GuidTextBlock.Text = "Now in the hallway (without a window ideally) with the lights ON.";
                        StartScaningButton.Visibility = Visibility.Visible;
                        FocasImage.Visibility = Visibility.Visible;
                        grdSwapCamera.Visibility = Visibility.Visible;
                        break;

                    case 4:
                        grd1.Visibility = Visibility.Visible;
                        grd2.Visibility = Visibility.Visible;
                        grd3.Visibility = Visibility.Visible;

                        Line1.Visibility = Visibility.Visible;
                        Line2.Visibility = Visibility.Visible;
                        Line3.Visibility = Visibility.Visible;
                        Line4.Visibility = Visibility.Visible;

                        RoundImage.Source = BlackRoundImage;
                        RoundImage1.Source = OrangeRoundImage;
                        RoundImage2.Source = BlackRoundImage;


                        NumberTextBlock1.Text = "4";
                        NumberTextBlock2.Text = "5";
                        NumberTextBlock3.Text = "6";
                        GuidTextBlock.Visibility = Visibility.Visible;
                        GuidTextBlock.Text = "Now in any room, with the curtains CLOSED and lights OFF.";
                        StartScaningButton.Visibility = Visibility.Visible;
                        FocasImage.Visibility = Visibility.Visible;
                        grdSwapCamera.Visibility = Visibility.Visible;
                        break;

                    case 5:
                        grd1.Visibility = Visibility.Visible;
                        grd2.Visibility = Visibility.Visible;
                        grd3.Visibility = Visibility.Visible;

                        Line1.Visibility = Visibility.Visible;
                        Line2.Visibility = Visibility.Visible;
                        Line3.Visibility = Visibility.Visible;
                        Line4.Visibility = Visibility.Visible;

                        RoundImage.Source = BlackRoundImage;
                        RoundImage1.Source = OrangeRoundImage;
                        RoundImage2.Source = BlackRoundImage;


                        NumberTextBlock1.Text = "5";
                        NumberTextBlock2.Text = "6";
                        NumberTextBlock3.Text = "7";
                        GuidTextBlock.Visibility = Visibility.Visible;
                        GuidTextBlock.Text = "Now in the same room as before, with the curtains CLOSED but with the lights ON.";
                        StartScaningButton.Visibility = Visibility.Visible;
                        FocasImage.Visibility = Visibility.Visible;
                        grdSwapCamera.Visibility = Visibility.Visible;
                        break;

                    case 6:
                        grd1.Visibility = Visibility.Visible;
                        grd2.Visibility = Visibility.Visible;
                        grd3.Visibility = Visibility.Visible;

                        Line1.Visibility = Visibility.Visible;
                        Line2.Visibility = Visibility.Visible;
                        Line3.Visibility = Visibility.Visible;
                        Line4.Visibility = Visibility.Visible;

                        RoundImage.Source = BlackRoundImage;
                        RoundImage1.Source = OrangeRoundImage;
                        RoundImage2.Source = BlackRoundImage;

                        NumberTextBlock1.Text = "6";
                        NumberTextBlock2.Text = "7";
                        NumberTextBlock3.Text = "8";
                        GuidTextBlock.Visibility = Visibility.Visible;
                        GuidTextBlock.Text = "Now in a bright room with the lights ON and the curtains OPEN.";
                        StartScaningButton.Visibility = Visibility.Visible;
                        FocasImage.Visibility = Visibility.Visible;
                        grdSwapCamera.Visibility = Visibility.Visible;
                        break;

                    case 7:
                        grd1.Visibility = Visibility.Visible;
                        grd2.Visibility = Visibility.Visible;
                        grd3.Visibility = Visibility.Collapsed;

                        Line1.Visibility = Visibility.Visible;
                        Line2.Visibility = Visibility.Visible;


                        RoundImage.Source = BlackRoundImage;
                        RoundImage1.Source = OrangeRoundImage;




                        NumberTextBlock1.Text = "7";
                        NumberTextBlock2.Text = "8";
                        GuidTextBlock.Visibility = Visibility.Visible;
                        GuidTextBlock.Text = "Take a video in any other room of your choice.";
                        StartScaningButton.Visibility = Visibility.Visible;
                        FocasImage.Visibility = Visibility.Visible;
                        grdSwapCamera.Visibility = Visibility.Visible;
                        break;

                    case 8:


                        DoneImage.Visibility = Visibility.Visible;
                        AcceptedImage.Visibility = Visibility.Visible;

                        MainImage.Visibility = Visibility.Collapsed;
                        MainImageSlideIn.Stop();

                        grdProgress.Visibility = Visibility.Collapsed;
                        ProgressBar.IsIndeterminate = false;


                        grd1.Visibility = Visibility.Visible;
                        grd2.Visibility = Visibility.Visible;
                        grd3.Visibility = Visibility.Collapsed;

                        Line1.Visibility = Visibility.Visible;
                        Line2.Visibility = Visibility.Visible;
                        Line3.Visibility = Visibility.Collapsed;
                        Line4.Visibility = Visibility.Collapsed;

                        RoundImage.Source = BlackRoundImage;
                        RoundImage1.Source = OrangeRoundImage;

                        NumberTextBlock1.Text = "7";
                        NumberTextBlock2.Text = "8";
                        //    GuidTextBlock.Visibility = Visibility.Visible;
                        //   GuidTextBlock.Text = "Take a video in any other room of your choice.";
                        //   StartScaningButton.Visibility = Visibility.Visible;
                        //   FocasImage.Visibility = Visibility.Visible;
                        //    grdSwapCamera.Visibility = Visibility.Visible;
                        break;





                }
            }

        }

        void cam_Initialized(object sender, Microsoft.Devices.CameraOperationCompletedEventArgs e)
        {
            try
            {


                if (e.Succeeded)
                {
                    for (int i = 4; i > 0; i--)
                    {

                        Thread.Sleep(1000);

                        this.Dispatcher.BeginInvoke(delegate()
                        {
                            numcounter.Text = i.ToString();
                        });



                    }


                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        progressRing.Visibility = Visibility.Collapsed;
                        progressRing.IsActive = false;
                        numcounter.Visibility = Visibility.Collapsed;
                        MainImage.Visibility = Visibility.Visible;
                        MainImageSlideIn.Begin();
                        MainImageSlideIn.RepeatBehavior = RepeatBehavior.Forever;


                    });


                    PumpARGBFrames();
                }
                else
                {
                    this.Dispatcher.BeginInvoke(delegate()
                    {

                        MessageBox.Show("Please Restart App");
                    });
                }


            }
            catch (Exception)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {

                    MessageBox.Show("Please Restart App");
                });
            }


        }

        private void SkipBotton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Skiptag = Convert.ToInt16(SkipBotton.Tag);

            if (Skiptag == 1)
            {

                grd3.Visibility = Visibility.Visible;
                grd2.Visibility = Visibility.Visible;
                BlackRoundImage = new BitmapImage(mul.RoundImageBlack);
                RoundImage2.Source = BlackRoundImage;
                Line3.Visibility = Visibility.Visible;
                Line4.Visibility = Visibility.Visible;
                NumberTextBlock3.Text = "2";

                grdCongratulation.Visibility = Visibility.Collapsed;
                FocasImage.Visibility = Visibility.Visible;
                GuidTextBlock.Visibility = Visibility.Visible;
                GuidTextBlock.Text = "Take a video facing an interior window.";
                StartScaningButton.Visibility = Visibility.Visible;

                grdSwapCamera.Visibility = Visibility.Visible;
            }

            else if (Skiptag == 0)
            {
                BackButton.Visibility = Visibility.Visible;
                CongratulationTextBlock1.Text = page3;
                CongratulationTextBlock2.Text = page4;
//                CongratulationTextBlock3.Text = page5;
                SkipBotton.Tag = 1;
            }

        }

        private void BackButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            BackButton.Visibility = Visibility.Collapsed;
            CongratulationTextBlock1.Text = page1;
            CongratulationTextBlock2.Text = page2;
            CongratulationTextBlock3.Text = "";
            SkipBotton.Tag = 0;
        }

        private void StartScaningButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {

                grdSwapCamera.Visibility = Visibility.Collapsed;
                GuidTextBlock.Visibility = Visibility.Collapsed;

                if (isNetworkConnected())
                {


                    if (cam.CameraType == CameraType.FrontFacing)
                    {
                        cam = new Microsoft.Devices.PhotoCamera(CameraType.FrontFacing);
                        viewfinderBrush.RelativeTransform = new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 90, ScaleX = -1 };
                    }
                    else
                    {
                        cam = new Microsoft.Devices.PhotoCamera(CameraType.Primary);
                        viewfinderBrush.RelativeTransform = new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 90, };
                    }
                    //Event is fired when the PhotoCamera object has been initialized
                    cam.Initialized += new EventHandler<Microsoft.Devices.CameraOperationCompletedEventArgs>(cam_Initialized);

                   
                    viewfinderBrush.SetSource(cam);


                    FocasImage.Visibility = Visibility.Collapsed;
                    RectangleBox.Visibility = Visibility.Visible;
                    StartScaningButton.Visibility = Visibility.Collapsed;
                    progressRing.Visibility = Visibility.Visible;
                    progressRing.IsActive = true;

                    numcounter.Visibility = Visibility.Visible;
                    numcounter.Text = "";
                }

                else
                {

                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        MessageBox.Show("Please Check Internet Connection");
                    });

                }


            }
            catch (Exception)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    MessageBox.Show("Internal Error! Restart App");
                });


            }


        }

        private void SwapCamera_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {

                if (cam.CameraType == CameraType.FrontFacing)
                {
                    cam = new Microsoft.Devices.PhotoCamera(CameraType.Primary);
                    viewfinderBrush.RelativeTransform = new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 90, };

                    viewfinderBrush.SetSource(cam);

                }

                else
                {

                    if ((PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing) == true))
                    {

                        cam = new Microsoft.Devices.PhotoCamera(CameraType.FrontFacing);
                        //viewfinderBrush.RelativeTransform = new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 270, };
                        viewfinderBrush.RelativeTransform = new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 90, ScaleX = -1 };
                        viewfinderBrush.SetSource(cam);
                    }
                    else
                    {
                        this.Dispatcher.BeginInvoke(delegate()
                        {
                            MessageBox.Show("Your Phone Doesn't Support Front Camera");
                        });
                    }
                }


            }
            catch (Exception)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    MessageBox.Show("Internal Error! Restart App");
                });
            }

        }


        private async void DoneImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {

                RectangleBox.Visibility = Visibility.Collapsed;
                FocasImage.Visibility = Visibility.Visible;
                DoneImage.Visibility = Visibility.Collapsed;
                GuidTextBlock.Visibility = Visibility.Collapsed;
                AcceptedImage.Visibility = Visibility.Collapsed;

                if (App.settings.Contains("ListOfMemberIds"))
                {
                    int countmember = ((List<String>)App.settings["ListOfMemberIds"]).Count;

                    if (countmember == 8)
                    {
                        this.Dispatcher.BeginInvoke(delegate()
                        {
                            grdProgress.Visibility = Visibility.Visible;
                            ProgressBar.IsIndeterminate = true;

                        });

                        bool status = false;
                        status = await CreateIdentityId();

                        if (status)
                        {

                            this.Dispatcher.BeginInvoke(delegate()
                            {
                                //DoneImage.Visibility = Visibility.Visible;
                                // AcceptedImage.Visibility = Visibility.Visible;

                                // GuidTextBlock.Visibility = Visibility.Visible;
                                //GuidTextBlock.Text = "Registration successful";
                                MessageBox.Show("Congratulation Your Identity Has Been Created");
                                MainImage.Visibility = Visibility.Collapsed;
                                MainImageSlideIn.Stop();

                                grdProgress.Visibility = Visibility.Collapsed;
                                ProgressBar.IsIndeterminate = false;

                                // GuidTextBlock.Visibility = Visibility.Visible;
                                // GuidTextBlock.Text = "Registration successful";

                                //  txtDebug.Text = "Camera initialized";

                            });
                            App.settings.Remove("ListOfMemberIds");
                            App.LoginDetail.Remove("ID");

                            NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            //List<string> listofmemberid = ((List<String>) App.settings["ListOfMemberIds"]);

                            //listofmemberid.RemoveAt(listofmemberid.Count()-1);
                            //App.settings["ListOfMemberIds"] = listofmemberid;
                            this.Dispatcher.BeginInvoke(delegate()
                               {
                                   MessageBox.Show("Registration Failed");
                                   MainImage.Visibility = Visibility.Collapsed;
                                   MainImageSlideIn.Stop();
                                   grdProgress.Visibility = Visibility.Collapsed;
                                   ProgressBar.IsIndeterminate = false;
                                   DoneImage.Visibility = Visibility.Visible;
                                   AcceptedImage.Visibility = Visibility.Visible;

                                   MainImage.Visibility = Visibility.Collapsed;
                                   MainImageSlideIn.Stop();

                                   grdProgress.Visibility = Visibility.Collapsed;
                                   ProgressBar.IsIndeterminate = false;


                                   grd1.Visibility = Visibility.Visible;
                                   grd2.Visibility = Visibility.Visible;
                                   grd3.Visibility = Visibility.Collapsed;

                                   Line1.Visibility = Visibility.Visible;
                                   Line2.Visibility = Visibility.Visible;
                                   Line3.Visibility = Visibility.Collapsed;
                                   Line4.Visibility = Visibility.Collapsed;


                                   RoundImage.Source = BlackRoundImage;
                                   RoundImage1.Source = OrangeRoundImage;


                                   NumberTextBlock1.Text = "7";
                                   NumberTextBlock2.Text = "8";


                               });



                        }

                        //   NavigationService.Navigate(new Uri("/User/HomePage.xaml", UriKind.RelativeOrAbsolute));

                    }
                    else if (App.settings.Contains("ListOfMemberIds") && countmember < 8)
                    {

                        BlackRoundImage = new BitmapImage(mul.RoundImageBlack);
                        OrangeRoundImage = new BitmapImage(mul.RoundImage);

                        int countmemberIds = ((List<String>)App.settings["ListOfMemberIds"]).Count;
                        //List<String> items = (List<String>)App.settings["ListOfMemberIds"];

                        //string strCSV = string.Empty;
                        //foreach (var item in items)
                        //{
                        //    strCSV += item + ",";
                        //}
                        //strCSV = strCSV.Trim(',');

                        // strModelNum = App.settings["ListOfMemberIds"].ToString();


                        switch (countmemberIds)
                        {
                            case 1:

                                grdCongratulation.Visibility = Visibility.Collapsed;
                                grd1.Visibility = Visibility.Visible;
                                grd2.Visibility = Visibility.Visible;
                                grd3.Visibility = Visibility.Visible;

                                Line1.Visibility = Visibility.Visible;
                                Line2.Visibility = Visibility.Visible;
                                Line3.Visibility = Visibility.Visible;
                                Line4.Visibility = Visibility.Visible;


                                RoundImage.Source = BlackRoundImage;
                                RoundImage1.Source = OrangeRoundImage;
                                RoundImage2.Source = BlackRoundImage;

                                NumberTextBlock1.Text = "1";
                                NumberTextBlock2.Text = "2";
                                NumberTextBlock3.Text = "3";

                                GuidTextBlock.Visibility = Visibility.Visible;
                                GuidTextBlock.Text = "Step into the middle of the same room, well away from the window.";

                                StartScaningButton.Visibility = Visibility.Visible;
                                FocasImage.Visibility = Visibility.Visible;
                                grdSwapCamera.Visibility = Visibility.Visible;


                                break;

                            case 2:
                                grd1.Visibility = Visibility.Visible;
                                grd2.Visibility = Visibility.Visible;
                                grd3.Visibility = Visibility.Visible;

                                Line1.Visibility = Visibility.Visible;
                                Line2.Visibility = Visibility.Visible;
                                Line3.Visibility = Visibility.Visible;
                                Line4.Visibility = Visibility.Visible;


                                RoundImage.Source = BlackRoundImage;
                                RoundImage1.Source = OrangeRoundImage;
                                RoundImage2.Source = BlackRoundImage;

                                NumberTextBlock1.Text = "2";
                                NumberTextBlock2.Text = "3";
                                NumberTextBlock3.Text = "4";

                                GuidTextBlock.Visibility = Visibility.Visible;
                                GuidTextBlock.Text = "Take a video in the bathroom but, please face a window.";

                                StartScaningButton.Visibility = Visibility.Visible;
                                FocasImage.Visibility = Visibility.Visible;
                                grdSwapCamera.Visibility = Visibility.Visible;
                                break;


                            case 3:
                                grd1.Visibility = Visibility.Visible;
                                grd2.Visibility = Visibility.Visible;
                                grd3.Visibility = Visibility.Visible;

                                Line1.Visibility = Visibility.Visible;
                                Line2.Visibility = Visibility.Visible;
                                Line3.Visibility = Visibility.Visible;
                                Line4.Visibility = Visibility.Visible;


                                RoundImage.Source = BlackRoundImage;
                                RoundImage1.Source = OrangeRoundImage;
                                RoundImage2.Source = BlackRoundImage;

                                NumberTextBlock1.Text = "3";
                                NumberTextBlock2.Text = "4";
                                NumberTextBlock3.Text = "5";

                                GuidTextBlock.Visibility = Visibility.Visible;
                                GuidTextBlock.Text = "Now in the hallway (without a window ideally) with the lights ON.";
                                StartScaningButton.Visibility = Visibility.Visible;
                                FocasImage.Visibility = Visibility.Visible;
                                grdSwapCamera.Visibility = Visibility.Visible;
                                break;

                            case 4:
                                grd1.Visibility = Visibility.Visible;
                                grd2.Visibility = Visibility.Visible;
                                grd3.Visibility = Visibility.Visible;

                                Line1.Visibility = Visibility.Visible;
                                Line2.Visibility = Visibility.Visible;
                                Line3.Visibility = Visibility.Visible;
                                Line4.Visibility = Visibility.Visible;


                                RoundImage.Source = BlackRoundImage;
                                RoundImage1.Source = OrangeRoundImage;
                                RoundImage2.Source = BlackRoundImage;

                                NumberTextBlock1.Text = "4";
                                NumberTextBlock2.Text = "5";
                                NumberTextBlock3.Text = "6";
                                GuidTextBlock.Visibility = Visibility.Visible;
                                GuidTextBlock.Text = "Now in any room, with the curtains CLOSED and lights OFF.";
                                StartScaningButton.Visibility = Visibility.Visible;
                                FocasImage.Visibility = Visibility.Visible;
                                grdSwapCamera.Visibility = Visibility.Visible;
                                break;

                            case 5:
                                grd1.Visibility = Visibility.Visible;
                                grd2.Visibility = Visibility.Visible;
                                grd3.Visibility = Visibility.Visible;

                                Line1.Visibility = Visibility.Visible;
                                Line2.Visibility = Visibility.Visible;
                                Line3.Visibility = Visibility.Visible;
                                Line4.Visibility = Visibility.Visible;


                                RoundImage.Source = BlackRoundImage;
                                RoundImage1.Source = OrangeRoundImage;
                                RoundImage2.Source = BlackRoundImage;

                                NumberTextBlock1.Text = "5";
                                NumberTextBlock2.Text = "6";
                                NumberTextBlock3.Text = "7";
                                GuidTextBlock.Visibility = Visibility.Visible;
                                GuidTextBlock.Text = "Now in the same room as before, with the curtains CLOSED but with the lights ON.";
                                StartScaningButton.Visibility = Visibility.Visible;
                                FocasImage.Visibility = Visibility.Visible;
                                grdSwapCamera.Visibility = Visibility.Visible;
                                break;

                            case 6:
                                grd1.Visibility = Visibility.Visible;
                                grd2.Visibility = Visibility.Visible;
                                grd3.Visibility = Visibility.Visible;

                                Line1.Visibility = Visibility.Visible;
                                Line2.Visibility = Visibility.Visible;
                                Line3.Visibility = Visibility.Visible;
                                Line4.Visibility = Visibility.Visible;


                                RoundImage.Source = BlackRoundImage;
                                RoundImage1.Source = OrangeRoundImage;
                                RoundImage2.Source = BlackRoundImage;

                                NumberTextBlock1.Text = "6";
                                NumberTextBlock2.Text = "7";
                                NumberTextBlock3.Text = "8";
                                GuidTextBlock.Visibility = Visibility.Visible;
                                GuidTextBlock.Text = "Now in a bright room with the lights ON and the curtains OPEN.";
                                StartScaningButton.Visibility = Visibility.Visible;
                                FocasImage.Visibility = Visibility.Visible;
                                grdSwapCamera.Visibility = Visibility.Visible;
                                break;

                            case 7:
                                grd1.Visibility = Visibility.Visible;
                                grd2.Visibility = Visibility.Visible;
                                grd3.Visibility = Visibility.Collapsed;

                                Line1.Visibility = Visibility.Visible;
                                Line2.Visibility = Visibility.Visible;
                                Line3.Visibility = Visibility.Collapsed;
                                Line4.Visibility = Visibility.Collapsed;


                                RoundImage.Source = BlackRoundImage;
                                RoundImage1.Source = OrangeRoundImage;


                                NumberTextBlock1.Text = "7";
                                NumberTextBlock2.Text = "8";
                                GuidTextBlock.Visibility = Visibility.Visible;
                                GuidTextBlock.Text = "Take a video in any other room of your choice.";
                                StartScaningButton.Visibility = Visibility.Visible;
                                FocasImage.Visibility = Visibility.Visible;
                                grdSwapCamera.Visibility = Visibility.Visible;
                                break;



                        }
                    }
                    else
                    {

                        //created

                    }
                }
                else
                {

                    StartScaningButton.Visibility = Visibility.Visible;

                }

            }
            catch (Exception)
            {


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


        #endregion

        #region Methods


        public void PumpARGBFrames()
        {
            try
            {
                int i = 0;

                // Create capture buffer.
                int[] ARGBPx = new int[(int)cam.PreviewResolution.Width * (int)cam.PreviewResolution.Height];


                PhotoCamera phCam = (PhotoCamera)cam;

                for (i = 1; i < 12; i++)
                {
                    pauseFramesEvent.WaitOne();
                    //if (i == 3)
                    //{
                    //    pauseFramesEvent.WaitOne();
                    //}
                    //else
                    //{
                    //    pauseFramesEvent.WaitOne(500);
                    //}

                    // Copies the current viewfinder frame into a buffer for further manipulation.
                    phCam.GetPreviewBufferArgb32(ARGBPx);

                    pauseFramesEvent.Reset();


                    Deployment.Current.Dispatcher.BeginInvoke(delegate()
                    {
                        // Copy to WriteableBitmap.
                        wb = new WriteableBitmap((int)cam.PreviewResolution.Width, (int)cam.PreviewResolution.Height);
                        ARGBPx.CopyTo(wb.Pixels, 0);

                        MediaLibrary library = new MediaLibrary();
                        int angle;
                        if (cam.CameraType == CameraType.FrontFacing)
                        {

                            angle = 270;
                        }

                        else
                        {

                            angle = 90;
                        }
                        WriteableBitmap wbTarget = null;
                        if (angle % 180 == 0)
                        {
                            wbTarget = new WriteableBitmap(wb.PixelWidth, wb.PixelHeight);
                        }
                        else
                        {
                            wbTarget = new WriteableBitmap(wb.PixelHeight, wb.PixelWidth);
                        }

                        for (int x = 0; x < wb.PixelWidth; x++)
                        {
                            for (int y = 0; y < wb.PixelHeight; y++)
                            {
                                switch (angle % 360)
                                {
                                    case 90:
                                        wbTarget.Pixels[(wb.PixelHeight - y - 1) + x * wbTarget.PixelWidth] = wb.Pixels[x + y * wb.PixelWidth];
                                        break;
                                    case 180:
                                        wbTarget.Pixels[(wb.PixelWidth - x - 1) + (wb.PixelHeight - y - 1) * wb.PixelWidth] = wb.Pixels[x + y * wb.PixelWidth];
                                        break;
                                    case 270:
                                        wbTarget.Pixels[y + (wb.PixelWidth - x - 1) * wbTarget.PixelWidth] = wb.Pixels[x + y * wb.PixelWidth];
                                        break;
                                }
                            }
                        }


                        var fileStream = new MemoryStream();
                        // wbTarget.SaveJpeg(fileStream, wb.PixelWidth, wb.PixelHeight, 0, 100);
                        wbTarget.SaveJpeg(fileStream, 480, 640, 0, 80);
                        fileStream.Seek(0, SeekOrigin.Begin);
                        wb.Invalidate();


                        // library.SavePictureToCameraRoll("eredf", fileStream);
                        string filename = i + ".jpg";
                        using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication())
                        {
                            using (IsolatedStorageFileStream targetStream = isStore.OpenFile(filename, FileMode.Create, FileAccess.Write))
                            {
                                // Initialize the buffer for 4KB disk pages.
                                byte[] readBuffer = new byte[4096];
                                int bytesRead = -1;

                                // Copy the thumbnail to isolated storage. 
                                while ((bytesRead = fileStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
                                {
                                    targetStream.Write(readBuffer, 0, bytesRead);
                                }
                            }
                        }

                        //library.SavePictureToCameraRoll("eredf", fileStream);
                        pauseFramesEvent.Set();
                    });

                }



                this.Dispatcher.BeginInvoke(delegate()
                {
                    MainImageSlideIn.Stop();
                    MainImage.Visibility = Visibility.Collapsed;



                });

                Services();

            }
            catch (Exception)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {

                    MessageBox.Show("Please Restart App");
                    // Display error message.
                    // txtDebug.Text = e.Message;
                });
            }
        }

        public ModelCreate()
        {
            InitializeComponent();


            App.settings = IsolatedStorageSettings.ApplicationSettings;
            App.LoginDetail = IsolatedStorageSettings.ApplicationSettings;

            if (!App.settings.Contains("ListOfMemberIds"))
            {


                grdCongratulation.Visibility = Visibility.Visible;
                CongratulationTextBlock1.Text = page1;
                CongratulationTextBlock2.Text = page2;




            }
            //img.ImageSource = new BitmapImage(mul.RoundImageBlack);
            //grd1.Background = img;
            // grd2.Background = img;
            //grd3.Background = img;
        }

        public async void Services()
        {
            try
            {


                var httpClientHandler = new HttpClientHandler();

                using (HttpClient client = new HttpClient(httpClientHandler))

                using (System.Net.Http.MultipartFormDataContent multipart = new MultipartFormDataContent())
                {
                    client.BaseAddress = new Uri("https://klws.keylemon.com/");

                    string url = "api/model/";


                    string FilePath = "";
                    byte[] ByteArrayImage;
                    // string FilePath = GetAbsolutePath(i+".jpg");
                    ByteArrayContent bac;
                    try
                    {


                        for (int i = 3; i < 11; i++)
                        {
                            FilePath = GetAbsolutePath(i + ".jpg");
                            ByteArrayImage = FileToByteArray.Convert(FilePath);
                            bac = new ByteArrayContent(ByteArrayImage);


                            bac.Headers.Add("Content-Type", "application/octet-stream");
                            multipart.Add(bac,
                                  String.Format("\"{0}\"", "faces"), "image.jpg");
                        }
                    }
                    catch (Exception)
                    {


                    }
                    //bac.Headers.Add("Content-Type", "application/octet-stream");
                    // BitmapImage bmpNew = new BitmapImage(new Uri(FilePath));

                    // img.Visibility = Visibility.Visible;
                    //   img.Source = bmpNew;


                    multipart.Add((new StringContent("approvedid")),
                       String.Format("\"{0}\"", "user"));


                    multipart.Add((new StringContent("Ycsss77jU4UG3AURETdDmua3B3GaaxGIRJzIT1s0bqbIGLfAIn1pQ7")),
                      String.Format("\"{0}\"", "key"));

                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        grdProgress.Visibility = Visibility.Visible;
                        ProgressBar.IsIndeterminate = true;
                    });

                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.PostAsync(url, multipart);



                    //this.Dispatcher.BeginInvoke(delegate()
                    // {
                    //     grdProgress.Visibility = Visibility.Collapsed;
                    //     ProgressBar.IsIndeterminate = false;
                    // });

                    if (response.IsSuccessStatusCode)
                    {


                        this.Dispatcher.BeginInvoke(delegate()
                        {
                            grdSwapCamera.Visibility = Visibility.Visible;

                        });

                        var data = response.Content.ReadAsStringAsync();

                        var objData = JsonConvert.DeserializeObject<Response>(data.Result.ToString());
                        int NumberOfFaces = Convert.ToInt16(objData.nb_faces);


                        //check the no. of faces must be grater than or equal to 4
                        if (NumberOfFaces >= 4)
                        {


                            if (App.settings != null && App.settings.Contains("ListOfMemberIds"))
                            {
                                // int countmemberIds = ((List<String>)App.settings["ListOfMemberIds"]).Count;
                                List<String> li = (List<String>)App.settings["ListOfMemberIds"];
                                int intLi = Convert.ToInt16(li.Count);
                                if (li != null && intLi > 0)
                                {
                                    li.Add(objData.model_id);

                                    //  List<string> list = new List<string>();
                                    // string[][] jaggedArray = new string[3][];
                                    //   jaggedArray = (string[][])    App.settings.Values;

                                }

                                App.settings["ListOfMemberIds"] = li;

                                this.Dispatcher.BeginInvoke(delegate()
                                {
                                    MessageBox.Show("Model Created");

                                });
                                int countmemberIds = ((List<String>)App.settings["ListOfMemberIds"]).Count;

                                //   if (countmemberIds == 8)
                                // {


                                // bool status = await CreateIdentityId();

                                //if (status)
                                //{

                                //    this.Dispatcher.BeginInvoke(delegate()
                                //    {
                                //        DoneImage.Visibility = Visibility.Visible;
                                //        AcceptedImage.Visibility = Visibility.Visible;

                                //        GuidTextBlock.Visibility = Visibility.Visible;
                                //        GuidTextBlock.Text = "Registration successful";
                                //        MessageBox.Show("Congratulation Your Identity Has Been Created");
                                //        MainImage.Visibility = Visibility.Collapsed;
                                //        MainImageSlideIn.Stop();

                                //        grdProgress.Visibility = Visibility.Collapsed;
                                //        ProgressBar.IsIndeterminate = false;

                                //        GuidTextBlock.Visibility = Visibility.Visible;
                                //        GuidTextBlock.Text = "Registration successful";

                                //        txtDebug.Text = "Camera initialized";
                                //    });

                                //}
                                //else
                                //{
                                //    this.Dispatcher.BeginInvoke(delegate()
                                //    {
                                //        MessageBox.Show("Registration Failed");
                                //        MainImage.Visibility = Visibility.Collapsed;
                                //        MainImageSlideIn.Stop();
                                //        grdProgress.Visibility = Visibility.Collapsed;
                                //        ProgressBar.IsIndeterminate = false;


                                //    });

                                //}

                                //  }


                            }

                                //add first model to isolatedstorage-- ("ListOfMemberIds")
                            else
                            {

                                List<String> liFirst = new List<string>();
                                liFirst.Add(objData.model_id);
                                App.settings.Add("ListOfMemberIds", liFirst);

                                this.Dispatcher.BeginInvoke(delegate()
                                {
                                    MessageBox.Show("Model Created");

                                });
                            }


                        }

                            //if faces less than 4
                        else
                        {

                            this.Dispatcher.BeginInvoke(delegate()
                        {

                            MessageBox.Show("No Face Detected, Please try again");
                            DoneImage.Visibility = Visibility.Visible;
                            AcceptedImage.Visibility = Visibility.Visible;

                            MainImage.Visibility = Visibility.Collapsed;
                            MainImageSlideIn.Stop();
                            grdProgress.Visibility = Visibility.Collapsed;
                            ProgressBar.IsIndeterminate = false;

                        });

                        }


                        this.Dispatcher.BeginInvoke(delegate()
                        {
                            //  MessageBox.Show("Model Created");

                            DoneImage.Visibility = Visibility.Visible;
                            AcceptedImage.Visibility = Visibility.Visible;

                            MainImage.Visibility = Visibility.Collapsed;
                            MainImageSlideIn.Stop();

                            grdProgress.Visibility = Visibility.Collapsed;
                            ProgressBar.IsIndeterminate = false;

                            //  txtDebug.Text = "Camera initialized";
                        });

                    }


                        //if un-success
                    else
                    {

                        this.Dispatcher.BeginInvoke(delegate()
     {
         MessageBox.Show("No Face Detected, Please try again");
         DoneImage.Visibility = Visibility.Visible;
         AcceptedImage.Visibility = Visibility.Visible;
         grdSwapCamera.Visibility = Visibility.Visible;
         MainImage.Visibility = Visibility.Collapsed;
         MainImageSlideIn.Stop();

         grdProgress.Visibility = Visibility.Collapsed;
         ProgressBar.IsIndeterminate = false;

     });


                    }




                }


            }
            catch (Exception)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    MessageBox.Show("Internal Error! Restart App");
                });
            }

        }

        private string GetAbsolutePath(string filename)
        {
            string absoulutePath = null;
            try
            {



                //IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

                if (isStore.FileExists(filename))
                {
                    IsolatedStorageFileStream output = new IsolatedStorageFileStream(filename, FileMode.Open, isStore);
                    absoulutePath = output.Name;
                    output.Close();
                    output = null;
                }
                return absoulutePath;

            }
            catch (Exception)
            {

                return absoulutePath;
            }


        }

        public bool isNetworkConnected()
        {
            return DeviceNetworkInformation.IsNetworkAvailable;
        }
        public static class FileToByteArray
        {
            public static byte[] Convert(string FileName)
            {
                try
                {


                    System.IO.FileStream fileStream = File.OpenRead(FileName);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        fileStream.Close();
                        return memoryStream.ToArray();
                    }

                }
                catch (Exception)
                {

                    return null;
                }
            }
        }






        public async Task<bool> CreateIdentityId()
        {
            try
            {


                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://klws.keylemon.com/");
                var url = "api/identity/";

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                List<String> items = (List<String>)App.settings["ListOfMemberIds"];

                string strCSV = string.Empty;
                foreach (var item in items)
                {
                    strCSV += item + ",";
                }
                strCSV = strCSV.Trim(',');

                string Models = strCSV;
                var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user", "approvedid"),
                 new KeyValuePair<string, string>("key","Ycsss77jU4UG3AURETdDmua3B3GaaxGIRJzIT1s0bqbIGLfAIn1pQ7"),
                  new KeyValuePair<string, string>("models", Models)
            });

                HttpResponseMessage res = await client.PostAsync(url, formContent);

                if (res.IsSuccessStatusCode)
                {
                    var data = res.Content.ReadAsStringAsync();
                    ModelCreatedRespose objResponse = JsonConvert.DeserializeObject<ModelCreatedRespose>(data.Result.ToString());
                    String IdentityId = objResponse.identity_id;

                    bool status = await CreateRegistraton(IdentityId, Models);

                    return status;
                }

                else
                {
                    return false;
                }



            }
            catch (Exception)
            {
                return false;
            }

        }


        public async Task<bool> CreateRegistraton(string IdentityId, string Models)
        {
            try
            {



                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(" http://face.id/");
                var url = "faceid1/index.php/webservice/addUserModels/";

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string user_id = "";
                //if (App.LoginDetail.Contains("MemberId"))
                //{
                //    user_id = (string)App.LoginDetail["MemberId"];
                //}
                if (App.LoginDetail.Contains("ID"))
                {
                    try
                    {


                        user_id = Convert.ToString(App.LoginDetail["ID"]);

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }

                var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("user_id", user_id),
                 new KeyValuePair<string, string>("identity_id", IdentityId),
                  new KeyValuePair<string, string>("models_id", Models)
            });


                HttpResponseMessage res = await client.PostAsync(url, formContent);

                if (res.IsSuccessStatusCode)
                {




                    //   App.settings.Remove("ListOfMemberIds");
                    // App.LoginDetail.Remove("ID");


                    var data = res.Content.ReadAsStringAsync();

                    return true;
                }
                else
                {

                    return false;
                }

            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion


        //create model2
        //Step into the middle of the same room, well away from the window.

        //model 3
        //Take a video in the bathroom but, please face a window.

        //model 4

        //Now in the hallway(without a window ideally) with the lights ON.

        //5
        //Now in any room, with the curtains CLOSED and lights OFF.

        //6
        //Now in the same room as before, with the curtains CLOSED but with the lights ON.

        //7
        //Now in a bright room with the lights ON and the curtains OPEN.

        //8
        //Take a video in any other room of your choice.
    }
}