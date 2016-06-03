using System;
using UIKit;
using POCFlorence.iOS;

namespace POCFlorence.iOS.NavigationMenu
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
			var cell = tableView.DequeueReusableCell ("MenuTableCell") as MenuTableCell ?? MenuTableCell.Create ();


			var catTitle = titles [indexPath.Row];
			cell.BindData (catTitle);

			return cell;

		}
		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			AppDelegate.selectedChannelName = titles [indexPath.Row];
			NewPageEvent(this, new EventArgs());
			tableView.DeselectRow (indexPath, false);
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

