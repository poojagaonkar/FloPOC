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
		UIImageView imgBody { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel labelBody { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel labelContent { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (imgBody != null) {
				imgBody.Dispose ();
				imgBody = null;
			}
			if (labelBody != null) {
				labelBody.Dispose ();
				labelBody = null;
			}
			if (labelContent != null) {
				labelContent.Dispose ();
				labelContent = null;
			}
		}
	}
}
