// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace POCFlorence.iOS
{
	[Register ("PageContentViewController")]
	partial class PageContentViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel labelContent { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (labelContent != null) {
				labelContent.Dispose ();
				labelContent = null;
			}
		}
	}
}