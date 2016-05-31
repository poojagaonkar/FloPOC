using System;
using CoreGraphics;
using Foundation;
using MonoTouch.Dialog;

using UIKit;

using System.Threading.Tasks;
using POCFlorence.iOS.Utilities;

namespace FlyoutNavigation
{
    public enum FlyOutNavigationPosition
    {
        Left = 0,

       
        Right
    }
        ;

    [Register("FlyoutNavigationController")]
    public class FlyoutNavigationController : UIViewController
    {
        private const float sidebarFlickVelocity = 1000.0f;
        public const int menuWidth = 250;

       
        private UIButton closeButton;

       
        private FlyOutNavigationPosition position;

        private DialogViewController navigation;
        private int selectedIndex;
        private UIView shadowView;
        private nfloat startX;
        private UIColor tintColor;
        private UIView statusImage;
        protected UIViewController[] viewControllers;
        private bool hideShadow;
		private UIButton footerButton;

        public FlyoutNavigationController(IntPtr handle)
            : base(handle)
        {
            Initialize();
        }

        public FlyoutNavigationController(UITableViewStyle navigationStyle = UITableViewStyle.Plain)
        {
            Initialize(navigationStyle);
        }

        public UIColor TintColor
        {
            get { return tintColor; }
            set
            {
                if (tintColor == value)
                    return;
                tintColor = value;
                Console.WriteLine("New tint color: " + tintColor.ToString());
            }
        }

        public FlyOutNavigationPosition Position
        {
            get { return position; }
            set
            {
                position = value;
                shadowView.Layer.ShadowOffset = new CGSize(Position == FlyOutNavigationPosition.Left ? -5 : 5, -1);
            }
        }
		public override bool PrefersStatusBarHidden ()
		{
			return true;
		}
        public Action SelectedIndexChanged { get; set; }

        public bool AlwaysShowLandscapeMenu { get; set; }

        public bool ForceMenuOpen { get; set; }

        public bool HideShadow
        {
            get { return hideShadow; }
            set
            {
                if (value == hideShadow)
                    return;
                hideShadow = value;
                if (hideShadow)
                {
                    if (mainView != null)
                        View.InsertSubviewBelow(shadowView, mainView);
                }
                else
                {
                    shadowView.RemoveFromSuperview();
                }
            }
        }

        public UIColor ShadowViewColor
        {
            get { return shadowView.BackgroundColor; }
            set { shadowView.BackgroundColor = value; }
        }

        public UIViewController CurrentViewController { get; private set; }

        private UIView mainView
        {
            get
            {
                if (CurrentViewController == null)
                    return null;
                return CurrentViewController.View;
            }
        }

        public RootElement NavigationRoot
        {
            get { return navigation.Root; }
            set { EnsureInvokedOnMainThread(delegate { navigation.Root = value; }); }
        }

        public UITableView NavigationTableView
        {
            get { return navigation.TableView; }
        }

        public UIViewController[] ViewControllers
        {
            get { return viewControllers; }
            set
            {
                EnsureInvokedOnMainThread(delegate
                {
                    viewControllers = value;
                    NavigationItemSelected(GetIndexPath(SelectedIndex));
                });
            }
        }

        public bool IsOpen
        {
            get
            {
                if (Position == FlyOutNavigationPosition.Left)
                {
                    return mainView.Frame.X == menuWidth;
                }
                else
                {
                    return mainView.Frame.X == -menuWidth;
                }
            }
            set
            {
                if (value)
                    HideMenu();
                else
                    ShowMenu();
            }
        }
        //To force open menu on landscape mode
        private bool ShouldStayOpen
        {
            get
            {
                if (ForceMenuOpen )//|| (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad && AlwaysShowLandscapeMenu &&(InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)))
                    return true;
                return false;
            }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (selectedIndex == value)
                    return;
                selectedIndex = value;
                EnsureInvokedOnMainThread(delegate { NavigationItemSelected(value); });
            }
        }

        public bool DisableRotation { get; set; }

        public override bool ShouldAutomaticallyForwardRotationMethods
        {
            get { return true; }
        }

        private bool isIos7 = false;

        private  void Initialize(UITableViewStyle navigationStyle = UITableViewStyle.Plain)
        {
            DisableStatusBarMoving = true;
            statusImage = new UIView {ClipsToBounds = true}.SetAccessibilityId("statusbar");
            navigation = new MonoTouch.Dialog.DialogViewController(navigationStyle, null);
            navigation.OnSelection += NavigationItemSelected;
            CGRect navFrame = navigation.View.Frame;
            navFrame.Width = menuWidth;
            if (Position == FlyOutNavigationPosition.Right)
                navFrame.X = mainView.Frame.Width - menuWidth;
            navigation.View.Frame = navFrame;
            View.AddSubview(navigation.View);

            TintColor = UIColor.Black;
            var version = new System.Version(UIDevice.CurrentDevice.SystemVersion);
            isIos7 = version.Major >= 7;
            if (isIos7)
            {
            }

//			var screenSize = UIScreen.MainScreen.Bounds.Size;
//
//			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
//				if (screenSize.Height < 667.0f) { // If < iphone 6
//
//					 footerButton = new UIButton (new CGRect (0, this.View.Frame.Height - 50, menuWidth, 50)) {
//						BackgroundColor = UIColor.Clear.FromHexString(AppDelegate.MenuSecondaryColor),
//						HorizontalAlignment = UIControlContentHorizontalAlignment.Center,
//						VerticalAlignment = UIControlContentVerticalAlignment.Center,
//						Font = FontHelper.RobotoBold13 ()
//					};
//					navigation.TableView.TableFooterView = footerButton;
//				} else {
//
//					CreateFooterView ();
//				}
//			} else {
//				CreateFooterView ();
//			}
//			footerButton.SetImage (UIImage.FromFile ("logoutbutton.png"), UIControlState.Normal);
//			footerButton.SetTitleColor (UIColor.LightGray, UIControlState.Highlighted);
//			footerButton.SetTitle ("LOGOUT", UIControlState.Normal);
//			var versionName = NSBundle.MainBundle.InfoDictionary ["CFBundleShortVersionString"].Description;
//			footerButton.TouchUpInside += FooterButton_TouchDown;

			navigation.TableView.ScrollsToTop = false;
           
            
            shadowView = new UIView();
            shadowView.BackgroundColor = UIColor.White;

            closeButton = new UIButton();
            closeButton.TouchUpInside += delegate { HideMenu(); };
            AlwaysShowLandscapeMenu = true;

            View.AddGestureRecognizer(new OpenMenuGestureRecognizer(DragContentView, shouldReceiveTouch));
        }
//		private void CreateFooterView()
//		{
//			var footerView = new UIView (new CGRect (0, this.View.Frame.Height - 50, menuWidth, 50)) {
//				BackgroundColor = UIColor.Clear
//			};
//			footerButton = new UIButton (new CGRect (0, footerView.Frame.Y, footerView.Frame.Width, 50)) {
//				BackgroundColor = UIColor.Clear.FromHexString(AppDelegate.MenuSecondaryColor),
//				HorizontalAlignment = UIControlContentHorizontalAlignment.Center,
//				VerticalAlignment = UIControlContentVerticalAlignment.Center,
//				Font = FontHelper.RobotoBold13 ()
//			};
//
//			footerView.ClipsToBounds = true;
//			footerView.Add (footerButton);
//			View.AddSubview (footerButton);
//		}

//		 void FooterButton_TouchDown (object sender, EventArgs e)
//        {
//			new System.Threading.Thread (new System.Threading.ThreadStart (() => {
//
//				InvokeOnMainThread (async() => {
//					var buttonIndex = await ShowAlert ("Logout", "Are you sure?", new string[]{ "Yes", "No" });
//					switch (buttonIndex) {
//					case 0:
//						AppDelegate.Logout ();
//						break;
//					default:
//						break;
//					}
//				});
//			})).Start();
//
//
//        }
		public static Task<int> ShowAlert (string title, string message, params string [] buttons)
		{
			var tcs = new TaskCompletionSource<int> ();
			var alert = new UIAlertView {
				Title = title,
				Message = message
			};
			foreach (var button in buttons)
				alert.AddButton (button);
			alert.Clicked += (s, e) => tcs.TrySetResult ((int)e.ButtonIndex);
			alert.Show ();
			return tcs.Task;
		}

        public event UITouchEventArgs ShouldReceiveTouch;

        internal bool shouldReceiveTouch(UIGestureRecognizer gesture, UITouch touch)
        {
            if (ShouldReceiveTouch != null)
                return ShouldReceiveTouch(gesture, touch);
            return true;
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            CGRect navFrame = View.Bounds;
            navFrame.Width = menuWidth;
            if (Position == FlyOutNavigationPosition.Right)
                navFrame.X = mainView.Frame.Width - menuWidth;
            if (navigation.View.Frame != navFrame)
                navigation.View.Frame = navFrame;
        }

        public void DragContentView(UIPanGestureRecognizer panGesture)
        {
            if (ShouldStayOpen || mainView == null)
                return;
            if (!HideShadow)
                View.InsertSubviewBelow(shadowView, mainView);
            navigation.View.Hidden = false;
            CGRect frame = mainView.Frame;
            shadowView.Frame = frame;
            nfloat translation = panGesture.TranslationInView(View).X;
            if (panGesture.State == UIGestureRecognizerState.Began)
            {
                startX = frame.X;
            }
            else if (panGesture.State == UIGestureRecognizerState.Changed)
            {
                frame.X = translation + startX;
                if (Position == FlyOutNavigationPosition.Left)
                {
                    if (frame.X < 0)
                        frame.X = 0;
                    else if (frame.X > menuWidth)
                        frame.X = menuWidth;
                }
                else
                {
                    if (frame.X > 0)
                        frame.X = 0;
                    else if (frame.X < -menuWidth)
                        frame.X = -menuWidth;
                }
                SetLocation(frame);
            }
            else if (panGesture.State == UIGestureRecognizerState.Ended)
            {
#if __UNIFIED__
                nfloat velocity = panGesture.VelocityInView(View).X;
                nfloat newX = translation + startX;
#else
				float velocity = panGesture.VelocityInView (View).X;
				float newX = translation + startX;
#endif
                bool show = Math.Abs(velocity) > sidebarFlickVelocity ? velocity > 0 : newX > (menuWidth/2);
                if (Position == FlyOutNavigationPosition.Right)
                {
                    show = Math.Abs(velocity) > sidebarFlickVelocity ? velocity < 0 : newX < -(menuWidth/2);
                }
                if (show)
                {
                    ShowMenu();
                }
                else
                {
                    HideMenu();
                }
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            CGRect navFrame = navigation.View.Frame;
            navFrame.Width = menuWidth;
            if (Position == FlyOutNavigationPosition.Right)
                navFrame.X = mainView.Frame.Width - menuWidth;
            navFrame.Location = CGPoint.Empty;
            navigation.View.Frame = navFrame;
            View.BackgroundColor = NavigationTableView.BackgroundColor;
            var frame = mainView.Frame;
            setViewSize();
            SetLocation(frame);
            base.ViewWillAppear(animated);
        }

        protected void NavigationItemSelected(Foundation.NSIndexPath indexPath)
        {
            int index = GetIndex(indexPath);
            NavigationItemSelected(index);
        }

        protected void NavigationItemSelected(int index)
        {
            selectedIndex = index;
            if (viewControllers == null || viewControllers.Length <= index || index < 0)
            {
                if (SelectedIndexChanged != null)
                    SelectedIndexChanged();
                return;
            }
            if (ViewControllers[index] == null)
            {
                if (SelectedIndexChanged != null)
                    SelectedIndexChanged();
                return;
            }
            if (!DisableStatusBarMoving && !ShouldStayOpen)
				UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.Fade);

            bool isOpen = false;
            if (mainView != null)
            {
                mainView.RemoveFromSuperview();
                isOpen = IsOpen;
            }
            CurrentViewController = ViewControllers[SelectedIndex];
            CGRect frame = View.Bounds;
            if (isOpen || ShouldStayOpen)
                frame.X = Position == FlyOutNavigationPosition.Left ? menuWidth : -menuWidth;

            setViewSize();
            SetLocation(frame);
            View.AddSubview(mainView);
            AddChildViewController(CurrentViewController);
            CurrentViewController.ViewWillAppear(true);
            if (!ShouldStayOpen)
                HideMenu();
            if (SelectedIndexChanged != null)
                SelectedIndexChanged();
        }

        public void ShowMenu()
        {
            if (mainView == null)
                return;
            EnsureInvokedOnMainThread(delegate
            {
                navigation.View.Hidden = false;
                closeButton.Frame = mainView.Frame;
                shadowView.Frame = mainView.Frame;
                var statusFrame = statusImage.Frame;
                statusFrame.X = mainView.Frame.X;
                statusImage.Frame = statusFrame;
                if (!ShouldStayOpen)
                    View.AddSubview(closeButton);
                if (!HideShadow)
                    View.InsertSubviewBelow(shadowView, mainView);
                UIView.BeginAnimations("slideMenu");
                UIView.SetAnimationCurve(UIViewAnimationCurve.EaseIn);
              
                setViewSize();
                CGRect frame = mainView.Frame;
                frame.X = Position == FlyOutNavigationPosition.Left ? menuWidth : -menuWidth;
                SetLocation(frame);
                setViewSize();
                frame = mainView.Frame;
                shadowView.Frame = frame;
                closeButton.Frame = frame;
                statusFrame.X = mainView.Frame.X;
                statusImage.Frame = statusFrame;
                UIView.CommitAnimations();
            });
        }

        private void setViewSize()
        {
            CGRect frame = View.Bounds;

            if (ShouldStayOpen)
                frame.Width -= menuWidth;
            if (mainView.Bounds == frame)
                return;
            mainView.Bounds = frame;
        }

        private void SetLocation(CGRect frame)
        {
            mainView.Layer.AnchorPoint = new CGPoint(.5f, .5f);
            frame.Y = 0;
            if (mainView.Frame.Location == frame.Location)
                return;
            frame.Size = mainView.Frame.Size;
            var center = new CGPoint(frame.Left + frame.Width/2, frame.Top + frame.Height/2);

            mainView.Center = center;
            shadowView.Center = center;

            if (Math.Abs(frame.X - 0) > float.Epsilon)
            {
                getStatus();
                var statusFrame = statusImage.Frame;
                statusFrame.X = mainView.Frame.X;
                statusImage.Frame = statusFrame;
            }
        }

        public bool DisableStatusBarMoving { get; set; }

        private void getStatus()
        {
            if (DisableStatusBarMoving || !isIos7 || statusImage.Superview != null || ShouldStayOpen)
                return;
            var image = captureStatusBarImage();
            if (image == null)
                return;
            this.View.AddSubview(statusImage);
            foreach (var view in statusImage.Subviews)
                view.RemoveFromSuperview();
            statusImage.AddSubview(image);
            statusImage.Frame = UIApplication.SharedApplication.StatusBarFrame;
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        private UIView captureStatusBarImage()
        {
            try
            {
                UIView screenShot = UIScreen.MainScreen.SnapshotView(false);
                return screenShot;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception with Flyout " + ex.Message);
                return null;
            }
        }

        private void hideStatus()
        {
            if (!isIos7)
                return;
            statusImage.RemoveFromSuperview();
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        public void HideMenu()
        {
            if (mainView == null || mainView.Frame.X == 0)
                return;

            EnsureInvokedOnMainThread(delegate
            {
                navigation.FinishSearch();
                closeButton.RemoveFromSuperview();
                shadowView.Frame = mainView.Frame;
                var statusFrame = statusImage.Frame;
                statusFrame.X = mainView.Frame.X;
                statusImage.Frame = statusFrame;
                UIView.Animate(.2, () =>
                {
                    UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
                    CGRect frame = View.Bounds;
                    frame.X = 0;
                    setViewSize();
                    SetLocation(frame);
                    shadowView.Frame = frame;
                    statusFrame.X = 0;
                    statusImage.Frame = statusFrame;
                }, hideComplete);
            });
        }

        [Export("animationEnded")]
        private void hideComplete()
        {
            hideStatus();
            shadowView.RemoveFromSuperview();
            navigation.View.Hidden = true;
        }

        public void ResignFirstResponders(UIView view)
        {
            if (view.Subviews == null)
                return;
            foreach (UIView subview in view.Subviews)
            {
                if (subview.IsFirstResponder)
                    subview.ResignFirstResponder();
                ResignFirstResponders(subview);
            }
        }

        public void ToggleMenu()
        {
            EnsureInvokedOnMainThread(delegate
            {
                if (!IsOpen && CurrentViewController != null && CurrentViewController.IsViewLoaded)
                    ResignFirstResponders(CurrentViewController.View);
                if (IsOpen)
                    HideMenu();
                else
                    ShowMenu();
            });
        }

        private int GetIndex(Foundation.NSIndexPath indexPath)
        {
            int section = 0;
            int rowCount = 0;
            while (section < indexPath.Section)
            {
                rowCount += navigation.Root[section].Count;
                section++;
            }
            return rowCount + indexPath.Row;
        }

        protected Foundation.NSIndexPath GetIndexPath(int index)
        {
            if (navigation.Root == null)
                return Foundation.NSIndexPath.FromRowSection(0, 0);
            int currentCount = 0;
            int section = 0;
            foreach (Section element in navigation.Root)
            {
                if (element.Count + currentCount > index)
                    break;
                currentCount += element.Count;
                section++;
            }

            int row = index - currentCount;
            return Foundation.NSIndexPath.FromRowSection(row, section);
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            if (CurrentViewController != null)
                return CurrentViewController.GetSupportedInterfaceOrientations();
            return UIInterfaceOrientationMask.All;
        }

        public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
        {
            base.WillRotate(toInterfaceOrientation, duration);
        }

        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            base.DidRotate(fromInterfaceOrientation);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                return;
            switch (InterfaceOrientation)
            {
                case UIInterfaceOrientation.LandscapeLeft:
                case UIInterfaceOrientation.LandscapeRight:
                    ShowMenu();
                    return;

                default:
                    HideMenu();
                    return;
            }
        }

        public override void WillAnimateRotation(UIInterfaceOrientation toInterfaceOrientation, double duration)
        {
            base.WillAnimateRotation(toInterfaceOrientation, duration);
        }

        protected void EnsureInvokedOnMainThread(Action action)
        {
            if (IsMainThread())
            {
                action();
                return;
            }
            BeginInvokeOnMainThread(() =>
                action()
                );
        }

        private static bool IsMainThread()
        {
            return NSThread.Current.IsMainThread;
        }
    }

    internal static class Helpers
    {
        public static UIView SetAccessibilityId(this UIView view, string id)
        {
#if __UNIFIED__
            
#else
			Messaging.void_objc_msgSend_IntPtr (view.Handle, selAccessibilityIdentifier_Handle, nsId);
#endif
            return view;
        }
    }
    
}