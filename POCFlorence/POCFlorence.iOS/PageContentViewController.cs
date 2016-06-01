using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Linq;

namespace POCFlorence.iOS
{
	partial class PageContentViewController : UIViewController
	{
		public int pageIndex;
		public PageContentViewController (IntPtr handle) : base (handle)
		{
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var data =  AppDelegate.ContentList.ElementAt(pageIndex);
			labelContent.Text = data.Title;

		}

	}
}
