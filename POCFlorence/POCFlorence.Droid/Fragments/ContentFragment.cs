using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V4.View;

namespace POCFlorence.Droid.Fragments
{
    public class ContentFragment : Android.Support.V4.App.Fragment
    {
        public static TabLayout tabLayout;
        public static ViewPager viewPager;
        public static int int_items = 3;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            
            View view = inflater.Inflate(Resource.Layout.contentlayout, container, false);
          
            viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
 
            var contentAdapter = new ContentAdapter(ChildFragmentManager);
            viewPager.Adapter = contentAdapter;

            return view;
        }
    }
}