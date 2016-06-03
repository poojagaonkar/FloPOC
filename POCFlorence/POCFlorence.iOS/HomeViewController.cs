using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using POCFlorence.iOS.NavigationMenu;
using POCFlorence.iOS.Utilities;
using System.Collections.Generic;

namespace POCFlorence.iOS
{
	partial class HomeViewController : UIViewController
	{
		private MasterPageViewController masterFlipper;
		private string[] titles = new string[]{ "Title1", "Title2", "Title3", "Title4" };
		private int numberOfPages;
		private PageContentViewController pageContentController;



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
			AppDelegate.flyoutController.NavigationTableView.ContentInset = new UIEdgeInsets(44,0,0,0);
			AppDelegate.flyoutController.ShouldReceiveTouch += (sender, touch) => {
				return false;
			};



			View.AddSubview (AppDelegate.flyoutController.View);

			NavigationItem.LeftBarButtonItem = new UIBarButtonItem (UIImage.FromFile ("toggle.png"),UIBarButtonItemStyle.Plain, delegate {
				AppDelegate.flyoutController.ToggleMenu ();
			});
		}



		void DataSource_NewPageEvent (object sender, EventArgs e)
		{
			GetContentList(AppDelegate.selectedChannelName);

			numberOfPages = AppDelegate.ContentList.Count;
			var startingViewController = this.ViewControllerAtIndex (0);
			var viewControllers = new UIViewController []{ startingViewController };
			masterFlipper.ReloadPages (viewControllers,UIPageViewControllerNavigationDirection.Forward,false, null, AppDelegate.ContentList, numberOfPages);
			AppDelegate.flyoutController.ToggleMenu ();
		}

		private void GetContentList(string channelName)
		{
			AppDelegate.ContentList =  ContentList.GetContentList (channelName);
		}

		private PageContentViewController ViewControllerAtIndex (int index)
		{
			if (numberOfPages == 0 || index > numberOfPages) {
				return null;
			} else {
				pageContentController = (PageContentViewController)this.Storyboard.InstantiateViewController ("PageContent_VC");

				pageContentController.pageIndex = index;

			}
			return pageContentController;
		}
	}
}
