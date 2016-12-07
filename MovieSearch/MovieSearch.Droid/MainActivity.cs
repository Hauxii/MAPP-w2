using Android.App;
using Android.Runtime;
using Android.Widget;
using Android.OS;

namespace MovieSearch.Droid
{
    using Android.Support.Design.Widget;
    using Android.Support.V4.App;
    using Android.Support.V4.View;

    [Activity(Theme = "@style/MyTheme", Label = "HelloWorld.Droid", Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);

            var fragments = new Fragment[]
                                {
                                    new NameInputFragment(),
                                    new OtherFragment()
                                };
            var titles = CharSequence.ArrayFromStringArray(new[]
                                                               {
                                                                   "People",
                                                                   "Other"
                                                               });

            var viewPager = this.FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            // Give the TabLayout the ViewPager
            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            //var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            //var toolbar = getMenuInflater().inflate(R.menu.menu_display_high_scores, menu);

            //this.SetActionBar(toolbar);
            //this.ActionBar.Title = "My Toolbar";
        }
    }
}