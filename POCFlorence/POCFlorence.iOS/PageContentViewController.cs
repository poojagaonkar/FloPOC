using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Linq;
using POCFlorence.iOS.Utilities;

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
			var bodyHtml = HtmlHelper.BuildCompleteViewHtml (data.Body,data.Title, data.ImageName);
			wvContent.LoadHtmlString (bodyHtml, NSBundle.MainBundle.BundleUrl);

		}

	}
}
