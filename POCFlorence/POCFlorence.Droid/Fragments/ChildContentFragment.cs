using Android.Support.V4.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POCFlorence.Droid.Fragments
{
    public class ChildContentFragment : Fragment
    {
        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.childcontentlayout, container, false);
            return view;
        }
    }
}
