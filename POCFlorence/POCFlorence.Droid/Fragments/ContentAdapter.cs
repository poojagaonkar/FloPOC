using Android.Support.V4.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POCFlorence.Droid.Fragments
{
    public class ContentAdapter : FragmentPagerAdapter
    {
        private Android.Support.V4.App.FragmentManager ChildFragmentManager;
       
        private List<PageContentModel> contentList;
        private FragmentActivity Activity;

      

        public ContentAdapter(FragmentManager ChildFragmentManager, List<PageContentModel> contentList, FragmentActivity Activity)
            : base(ChildFragmentManager)
        {
            // TODO: Complete member initialization
            this.ChildFragmentManager = ChildFragmentManager;
            this.contentList = contentList;
            this.Activity = Activity;
        }
        public override Fragment GetItem(int position)
        {
            return new ChildContentFragment(contentList, position, Activity);
        }
        public override int Count
        {
            get { return contentList.Count; }
        }
    }
}
