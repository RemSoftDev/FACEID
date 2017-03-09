using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
namespace FACEID
{
    public class MultiResImageChooserUri
    {
        public Uri BestResolutionImage
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/MyImage.screen-720p.jpg", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/MyImage.screen-wxga.jpg", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/MyImage.screen-wvga.jpg", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri Image
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/MyImage.screen-720p.jpg", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/MyImage.screen-wxga.jpg", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/AlignmentGrid.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }

        public Uri bg
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/bg.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/bg.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/bg.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri Logo
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/logo.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/logo.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/logo.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri ButtonBackground
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/sign_in_btn.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/sign_in_btn.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/sign_in_btn.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri EmailImage
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/email_icon.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/email_shape.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/email_icon.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri CaptureVideo
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/capture_video_icon.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/capture_video.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/capture_video_icon.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }

        public Uri LogoutImage
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/sign_out_shape.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/sign_out_btn.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/sign_out_shape.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }

        public Uri RoundImage
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/on.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/on.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/on.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri RoundImageBlack
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/off.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/off.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/off.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }



        public Uri StartScaning
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/start_scanning.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/start_scanning.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/start_scanning.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri FocasImage
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/focus_no_corners.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/capture_image_no_corners.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/focus_no_corners.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri AcceptedImage
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/accepted.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/approved_shape.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/accepted.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }




        public Uri Rectangleframe
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/frame.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/frame.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/frame.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }

        public Uri SwapCamera
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/camera.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/refresh_btn.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/camera.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


     

        public Uri ScanerImage
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/scanner.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/shadow_line.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/scanner.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }

       

        public Uri BackImage
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/back.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/arrow_btn.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/back.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri successful_shape
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/successful_shape.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/successful_shape.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/successful_shape.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri failed_shape
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/failed_shape.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/failed_shape.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/failed_shape.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }



        public Uri ContactBox
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/contact_rectangle.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/box_shape.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/contact_rectangle.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }



        public Uri call_shape
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/call_shape.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/call_shape.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/call_shape.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }


        public Uri focaslarge
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        //return new Uri("../Assets/Images/720p/720.png", UriKind.Relative);
                        return new Uri("../Assets/Images/720p/720_no-corners.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/foaca_long.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        // return new Uri("../Assets/Images/wvga/480.png", UriKind.Relative);
                        return new Uri("../Assets/Images/wvga/480_no-corners.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }

        public Uri rectangle
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("../Assets/Images/720p/rectangle1.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("../Assets/Images/wxga/rectangle1.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("../Assets/Images/wvga/rectangle1.png", UriKind.Relative);
                    default:
                        throw new InvalidOperationException("Unknown resolution type");
                }
            }
        }

       




    }
}
