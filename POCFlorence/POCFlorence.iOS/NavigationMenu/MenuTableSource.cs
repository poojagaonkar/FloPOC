using System;
using UIKit;
using AttiniMobile.iOS.Utilities;

namespace AttiniMobile.iOS
{
	public class MenuTableSource : UITableViewSource
	{
		private string[] titles;
		public delegate void RowClickedEvent(object sender, EventArgs e);
		public event RowClickedEvent NewPageEvent;

		public MenuTableSource (string[] titles)
		{
			this.titles = titles;
		}
		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("Menu_Cell") as MenuCell;

			var catTitle = titles [indexPath.Row];

			if (catTitle == "SORT") {

				cell.UserInteractionEnabled = false;
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				cell.BackgroundColor = UIColor.Clear.FromHexString ("#d5d5d5");
			} else {
				cell.BackgroundColor = UIColor.Clear.FromHexString (AppDelegate.MenuPrimaryColor);
			}

			if (catTitle == "SORT" || catTitle == "All News" || catTitle == "Most Recent" || catTitle == "Most Commented" || catTitle == "Most Liked" || catTitle == "Most Viewed") {
				cell.UpdateCell(catTitle, "#d5d5d5");
			} else {
				cell.UpdateCell(catTitle, AppDelegate.colorDict[catTitle]);
			}


			return cell;

		}
		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			AppDelegate.selectedChannelName = titles [indexPath.Row];
			NewPageEvent(this, new EventArgs());
			tableView.DeselectRow (indexPath, true);
		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return titles.Length;
		}
		public override nint NumberOfSections (UITableView tableView)
		{
			return 1 ;
		}
	}
}

