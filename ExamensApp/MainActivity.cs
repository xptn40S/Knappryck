using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ExamensApp
{
	[Activity (Label = "Knappryck", MainLauncher = true, Icon = "@drawable/icon", 
		Theme = "@android:style/Theme.NoTitleBar", 
		ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{
		Button startButton;
		Button highscoreButton;

		HighScoreManager manager = new HighScoreManager();

		/// <summary>
		/// Raises the create event. Initializes the database if it's empty & sets button-clicks to start intents.
		/// </summary>
		/// <param name="bundle">Bundle.</param>

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			if (manager.getList().Count < 10) {
				Console.WriteLine("Initializing database");
				manager.firstTimeSetup();
			}

			startButton = FindViewById<Button>(Resource.Id.Main_StartButton);
			highscoreButton = FindViewById<Button>(Resource.Id.Main_HighScoreButton);

			startButton.Click += delegate {
				Intent intent = new Intent (this, typeof(GameActivity));
				intent.PutExtra("level", 3);
				StartActivity(intent);
			};

			highscoreButton.Click += delegate {
				Intent intent = new Intent (this, typeof(HighScoreActivity));
				StartActivity(intent);
			};
		}
	}
}


