﻿using System;

using UIKit;

namespace POCFlorence.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			btnNext.TouchUpInside += BtnNext_TouchUpInside;
		}

		void BtnNext_TouchUpInside (object sender, EventArgs e)
		{
			var vc = this.Storyboard.InstantiateViewController ("Home_VC") as HomeViewController;
			this.PresentViewController (vc,true,null);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

