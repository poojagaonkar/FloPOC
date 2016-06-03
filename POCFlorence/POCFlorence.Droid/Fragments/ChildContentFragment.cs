using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V4.App;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace POCFlorence.Droid.Fragments
{
    public class ChildContentFragment : Fragment
    {
        private List<PageContentModel> contentList;
        private TextView txtTitle, txtContnet;
        private ImageView imgContent;
        private int position;
        private Context mContext;

        
        public ChildContentFragment(List<PageContentModel> contentList, int position)
        {
            // TODO: Complete member initialization
            this.contentList = contentList;
            this.position = position;
        }

        public ChildContentFragment(List<PageContentModel> contentList, int position, FragmentActivity Activity)
        {
            // TODO: Complete member initialization
            this.contentList = contentList;
            this.position = position;
            this.mContext = Activity;
        }
        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.childcontentlayout, container, false);
            return view;
        }
        public override void OnViewCreated(Android.Views.View view, Android.OS.Bundle savedInstanceState)
        {
            
            txtTitle = view.FindViewById<TextView>(Resource.Id.txtTitle);
            txtContnet = view.FindViewById<TextView>(Resource.Id.contentBody);
            imgContent = view.FindViewById<ImageView>(Resource.Id.contentImage);

            var content = contentList.ElementAt(position);

            txtTitle.Text = content.Title;
            txtContnet.Text = content.Body;

            var imageName = Path.GetFileNameWithoutExtension(content.ImageName);
            var image = GetImage(mContext, imageName);
            imgContent.SetImageDrawable(image);

        }
        public static Drawable GetImage(Context c, String ImageName)
        {
            return c.Resources.GetDrawable(c.Resources.GetIdentifier(ImageName, "drawable", c.PackageName));
        }
         
    }
}
