using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

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

			btnToggle.TouchUpInside += BtnToggle_TouchUpInside;
		}

		void BtnToggle_TouchUpInside (object sender, EventArgs e)
		{
			AppDelegate.flyoutController.ToggleMenu ();
		}
	}
}
