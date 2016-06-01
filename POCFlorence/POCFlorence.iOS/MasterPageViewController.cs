using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;

namespace POCFlorence.iOS
{
	partial class MasterPageViewController : UIPageViewController, IUIPageViewControllerDataSource
	{
		private List<PageContentModel> contentList;
		private int numberOfPages;
		private PageContentViewController pageContentController;

		public MasterPageViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			AppDelegate.flyoutController.ForceMenuOpen = false;

			AppDelegate.ContentList = new List<PageContentModel> ();
			AppDelegate.ContentList.Add (new PageContentModel { Title = "One", Body = "Something", ImageName = "hand.png" });
			AppDelegate.ContentList.Add (new PageContentModel { Title = "two", Body = "Something", ImageName = "hand.png" });
			AppDelegate.ContentList.Add (new PageContentModel { Title = "three", Body = "Something", ImageName = "hand.png" });

			numberOfPages = AppDelegate.ContentList.Count;
			this.DataSource = this;
			var startingViewController = this.ViewControllerAtIndex (0);
			var viewControllers = new UIViewController []{ startingViewController };
			if (startingViewController != null) {
				this.SetViewControllers (viewControllers, UIPageViewControllerNavigationDirection.Forward, false, null);
			}
		}

		public new UIViewController GetNextViewController (UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			AppDelegate.flyoutController.ForceMenuOpen = false;

			var page = referenceViewController as PageContentViewController;
			if (page.pageIndex < 0 || page.pageIndex > numberOfPages) {
				return null;
			} 
			page.pageIndex++;

			if (page.pageIndex == numberOfPages) {
				return null;
			}
			return ViewControllerAtIndex (page.pageIndex);

		}

		public new UIViewController GetPreviousViewController (UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			AppDelegate.flyoutController.ForceMenuOpen = false;
			var page = referenceViewController as PageContentViewController;
			if (page.pageIndex == 0) {
				return null;
			}
			page.pageIndex--;
			if (page.pageIndex == numberOfPages) {
				page.pageIndex = numberOfPages - 1;
			}

			return ViewControllerAtIndex (page.pageIndex);

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

		public void ReloadPages (UIViewController[] viewControllers, UIPageViewControllerNavigationDirection direction, bool animated, UICompletionHandler completion, List<PageContentModel> mviewPages, int totalPages)
		{
			AppDelegate.ContentList = mviewPages;
			numberOfPages = totalPages;
			this.SetViewControllers (viewControllers, direction, animated, completion);
		}
	}
}
