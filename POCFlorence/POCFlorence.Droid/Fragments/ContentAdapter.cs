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
        private int pageCount = 3;

        public ContentAdapter(Android.Support.V4.App.FragmentManager ChildFragmentManager) : base (ChildFragmentManager)
        {
            // TODO: Complete member initialization
            this.ChildFragmentManager = ChildFragmentManager;
        }
        public override Fragment GetItem(int position)
        {
            return new ChildContentFragment();
        }
        public override int Count
        {
            get { return pageCount; }
        }
    }
}
