using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using AttiniMobile.iOS;

namespace POCFlorence.iOS
{
	partial class HomeViewController : UIViewController
	{
		private MasterPageViewController masterFlipper;
		private string[] titles = new string[]{ "Title1", "Title2", "Title3", "Title4" };

		public HomeViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			masterFlipper = this.Storyboard.InstantiateViewController ("Master_VC") as MasterPageViewController;

			AppDelegate.flyoutController = new FlyoutNavigation.FlyoutNavigationController ();
			AppDelegate.flyoutController.NavigationRoot = new MonoTouch.Dialog.RootElement ("Home");

			MenuTableSource dataSource = new MenuTableSource (titles);
			dataSource.NewPageEvent += DataSource_NewPageEvent;

			AppDelegate.flyoutController.ViewControllers = new UIViewController[] {

				masterFlipper
			};
			AppDelegate.flyoutController.HideShadow = false;
			//AppDelegate.flyoutController.NavigationTableView.BackgroundColor = UIColor.Clear.FromHexString (AppDelegate.MenuPrimaryColor);
			AppDelegate.flyoutController.NavigationTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			AppDelegate.flyoutController.ShouldReceiveTouch += (sender, touch) => {
				return false;
			};



			View.AddSubview (AppDelegate.flyoutController.View);
		}

		void DataSource_NewPageEvent (object sender, EventArgs e)
		{
			
		}
	}
}
