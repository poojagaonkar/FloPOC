using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using AttiniMobile.iOS;
using POCFlorence.iOS.Utilities;

namespace POCFlorence.iOS
{
	partial class HomeViewController : UIViewController
	{
		private MasterPageViewController masterFlipper;
		private string[] titles = new string[]{ "Title1", "Title2", "Title3", "Title4" };

		public HomeViewController (IntPtr handle) : base (handle)
		{
		}

		public override bool PrefersStatusBarHidden ()
		{
			return true;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.NavigationController.NavigationBarHidden = false;
			this.NavigationController.NavigationBar.Hidden = false;
			this.NavigationController.NavigationBar.SetBackgroundImage (UIImage.FromFile ("navbanner.png"), UIBarMetrics.Default);

			masterFlipper = this.Storyboard.InstantiateViewController ("Master_VC") as MasterPageViewController;

			AppDelegate.flyoutController = new FlyoutNavigation.FlyoutNavigationController ();
			AppDelegate.flyoutController.NavigationRoot = new MonoTouch.Dialog.RootElement ("Home");

			MenuTableSource dataSource = new MenuTableSource (titles);
			dataSource.NewPageEvent += DataSource_NewPageEvent;
			AppDelegate.flyoutController.NavigationTableView.Source = dataSource;

			AppDelegate.flyoutController.ViewControllers = new UIViewController[] {

				masterFlipper
			};
			AppDelegate.flyoutController.HideShadow = false;
			AppDelegate.flyoutController.NavigationTableView.BackgroundColor = UIColor.Clear.FromHexString (AppDelegate.MenuTableColor);
			AppDelegate.flyoutController.NavigationTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			AppDelegate.flyoutController.ShouldReceiveTouch += (sender, touch) => {
				return false;
			};



			View.AddSubview (AppDelegate.flyoutController.View);

			NavigationItem.LeftBarButtonItem = new UIBarButtonItem (UIImage.FromFile ("toggle.png"),UIBarButtonItemStyle.Plain, delegate {
				AppDelegate.flyoutController.ToggleMenu ();
			});
			btnToggle.TouchUpInside += BtnToggle_TouchUpInside;
		}

		void BtnToggle_TouchUpInside (object sender, EventArgs e)
		{
			AppDelegate.flyoutController.ToggleMenu ();
		}

		void DataSource_NewPageEvent (object sender, EventArgs e)
		{
			
		}
	}
}
