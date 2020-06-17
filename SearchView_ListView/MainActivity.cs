using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using System.Collections.Generic;
using System.Linq;

using V7Toolbar = Android.Support.V7.Widget.Toolbar;

namespace SearchView_ListView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView myListView;
        private V7Toolbar mainToolbar;
        private List<string> myStrings;
        private ArrayAdapter myAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //
            mainToolbar = FindViewById<V7Toolbar>(Resource.Id.mainToolbar);
            SetSupportActionBar(mainToolbar);
            myListView = FindViewById<ListView>(Resource.Id.myListView);
            //
            myStrings = Resources.GetStringArray(Resource.Array.myArray).ToList();
            myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, myStrings);
            myListView.Adapter = myAdapter;

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);

            IMenuItem searchItem = menu.FindItem(Resource.Id.SearchId);
            Android.Support.V7.Widget.SearchView mySsearchView = searchItem.ActionView as Android.Support.V7.Widget.SearchView;

            mySsearchView.QueryTextChange += (ee, ss) =>
            {
                List<string> searchWords = new List<string>();
                foreach (string item in myStrings)
                {
                    if (item.Contains(ss.NewText))
                    {
                        searchWords.Add(item);
                    }
                }
                myAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, searchWords);
                myListView.Adapter = myAdapter;
            };

            return base.OnCreateOptionsMenu(menu);
        }
    }
}