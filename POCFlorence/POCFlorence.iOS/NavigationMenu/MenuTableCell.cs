using System;

using Foundation;
using UIKit;
using CoreGraphics;

namespace POCFlorence.iOS
{
	public partial class MenuTableCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("MenuTableCell");
		public static readonly UINib Nib = UINib.FromName ("MenuTableCell", NSBundle.MainBundle);

		public static MenuTableCell Create()
		{
			return (MenuTableCell) Nib.Instantiate (null,null)[0];
		}

		public MenuTableCell (IntPtr handle) : base (handle)
		{
		}

		internal void BindData (string strLabel)
		{ 
			try {
				

				labelTitle.Text = strLabel;
			} catch (Exception ex) {
				Console.WriteLine ((ex.Message));
			}
		}

		public override NSString ReuseIdentifier {
			get {
				return base.ReuseIdentifier;
			}
		}
	}
}
