using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using POCFlorence.Droid.Fragments;

namespace POCFlorence.Droid
{
	[Activity(Label = "HomeActivity", MainLauncher =  true, Theme = "@style/MyTheme")]
	public class HomeActivity : AppCompatActivity
	{
        private DrawerLayout mDrawerLayout;
        private NavigationView mNavigationView;
        private Android.Support.V4.App.FragmentManager mFragmentManager;
        private Android.Support.V4.App.FragmentTransaction mFragmentTransaction;
        private Android.Support.V7.Widget.Toolbar mToolBar;
        private ActionBarDrawerToggle mDrawerToggle;
        private List<PageContentModel> contentList;

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
            SetContentView(Resource.Layout.home_layout);

            mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            mNavigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            mToolBar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);

            SetSupportActionBar(mToolBar);


            mDrawerToggle = new ActionBarDrawerToggle(
                            this,							//Host Activity
                            mDrawerLayout,					//DrawerLayout
                            Resource.String.open_drawer,		//Opened Message
                            Resource.String.close_drawer		//Closed Message
                        );
            mDrawerLayout.SetDrawerListener(mDrawerToggle);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            mDrawerToggle.SyncState();

            if (bundle != null)
            {
                if (bundle.GetString("DrawerState") == "Opened")
                {
                    SupportActionBar.SetTitle(Resource.String.open_drawer);
                }

                else
                {
                    SupportActionBar.SetTitle(Resource.String.close_drawer);
                }
            }

            else
            {
                //This is the first the time the activity is ran
                SupportActionBar.SetTitle(Resource.String.close_drawer);
            }

            mNavigationView.NavigationItemSelected += mNavigationView_NavigationItemSelected;

            GetContentList("Title1");
			// Create your application here
            mFragmentManager = SupportFragmentManager;
                mFragmentTransaction = mFragmentManager.BeginTransaction();
                mFragmentTransaction.Replace(Resource.Id.HomeFrameLayout, new ContentFragment(contentList));
                mFragmentTransaction.Commit();
		}

        void mNavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            mDrawerLayout.CloseDrawers();
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.title1:
                    GetContentList("Title1");
                    break;
                case Resource.Id.title2:
                    GetContentList("Title2");
                    break;
                case Resource.Id.title3:
                    GetContentList("Title3");
                    break;
                case Resource.Id.title4:
                    GetContentList("Title4");
                    break;
            }
            mFragmentTransaction = mFragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.HomeFrameLayout, new ContentFragment(contentList));
            mFragmentTransaction.Commit();
           
        }
        private void GetContentList(string channelName)
        {
            switch (channelName)
            {
                case "Title1":
                    contentList = new List<PageContentModel>();
                    contentList.Add(new PageContentModel { Title = "One", Body = "Something", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "two", Body = "Something", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "three", Body = "Something", ImageName = "hand.png" });

                    break;
                case "Title2":
                    contentList = new List<PageContentModel>();
                    contentList.Add(new PageContentModel { Title = "1", Body = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "2", Body = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "3", Body = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, Lorem ipsum dolor sit amet..comes from a line in section 1.10.32.", ImageName = "hand.png" });

                    break;
                case "Title3":
                    contentList = new List<PageContentModel>();
                    contentList.Add(new PageContentModel { Title = "Menu item 3a", Body = "Something", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "Menu item 3b", Body = "Something", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "Menu item 3c", Body = "Something", ImageName = "hand.png" });

                    break;
                case "Title4":
                    contentList = new List<PageContentModel>();
                    contentList.Add(new PageContentModel { Title = "Menu item 4a", Body = "Something", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "Menu item 4b", Body = "Something", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "Menu item 4c", Body = "Something", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "Menu item 4d", Body = "Something", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "Menu item 4e", Body = "Something", ImageName = "hand.png" });
                    contentList.Add(new PageContentModel { Title = "Menu item 4f", Body = "Something", ImageName = "hand.png" });

                    break;

            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Android.Resource.Id.Home:
                    //The hamburger icon was clicked which means the drawer toggle will handle the event


                    mDrawerToggle.OnOptionsItemSelected(item);
                    return true;


                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }

            else
            {
                outState.PutString("DrawerState", "Closed");
            }

            base.OnSaveInstanceState(outState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            mDrawerToggle.OnConfigurationChanged(newConfig);
        }
	}
}