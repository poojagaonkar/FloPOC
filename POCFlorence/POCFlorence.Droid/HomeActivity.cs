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

			// Create your application here
            mFragmentManager = SupportFragmentManager;
            mFragmentTransaction = mFragmentManager.BeginTransaction();
            mFragmentTransaction.Replace(Resource.Id.HomeFrameLayout, new ContentFragment());
            mFragmentTransaction.Commit();
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