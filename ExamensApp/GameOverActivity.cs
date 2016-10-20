
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

namespace ExamensApp
{
	[Activity (Label = "GameOverActivity", Theme = "@android:style/Theme.NoTitleBar", 
		ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class GameOverActivity : Activity
	{
		TextView scoreHeader;
		TextView finalScoreText;
		EditText nameEdit;
		Button nameButton;
		Button replayButton;
		Button menuButton;

		HighScoreManager manager = new HighScoreManager();
		List <HighScoreEntry> list = new List<HighScoreEntry>();
		int finalScore;
		int getRank;

		/// <summary>
		/// Gets the current highscore-list and the score from the game-activity, sets the layout-elements and click-events 
		/// and calls the checkScore-function.
		/// </summary>
		/// <param name="bundle">Bundle.</param>

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.GameOver);

			list = manager.getList();
			finalScore = Intent.GetIntExtra("finalScore", 0);

			scoreHeader = FindViewById<TextView>(Resource.Id.GameOver_ScoreHeader);
			finalScoreText = FindViewById<TextView>(Resource.Id.GameOver_Score);
			nameEdit = FindViewById<EditText>(Resource.Id.GameOver_EditText);
			nameButton = FindViewById<Button>(Resource.Id.GameOver_NameButton);
			replayButton = FindViewById<Button>(Resource.Id.GameOver_ReplayButton);
			menuButton = FindViewById<Button>(Resource.Id.GameOver_QuitButton);

			finalScoreText.Text = finalScore + "p";

			nameButton.Click += delegate {
				if (nameEdit.Text.Length > 0) {
					saveEntry(nameEdit.Text);
				} else {
					Toast.MakeText(this, "Inget namn inskrivet", ToastLength.Short).Show();
				}
			};

			replayButton.Click += delegate {
				Intent intent = new Intent (this, typeof(GameActivity));
				intent.PutExtra("level", 3);
				Finish();
				StartActivity(intent);
			};

			menuButton.Click += delegate {
				Intent intent = new Intent (this, typeof(MainActivity));
				Finish();
				StartActivity(intent);
			};
				
			nameEdit.Visibility = ViewStates.Gone;
			nameButton.Visibility = ViewStates.Gone;

			checkScore();
		}

		/// <summary>
		/// checkScore compares the final score of the game to that of the ones in the highscore-database and calls the 
		/// saveHighScore-function with a rank-number if the score has earned it.
		/// </summary>

		private void checkScore()
		{
			if (finalScore >= list [0].ScoreValue) {
				saveHighscore (1);
			} else if (finalScore >= list [1].ScoreValue) {
				saveHighscore (2);
			} else if (finalScore >= list [2].ScoreValue) {
				saveHighscore (3);
			} else if (finalScore >= list [3].ScoreValue) {
				saveHighscore (4);
			} else if (finalScore >= list [4].ScoreValue) {
				saveHighscore (5);
			} else if (finalScore >= list [5].ScoreValue) {
				saveHighscore (6);
			} else if (finalScore >= list [6].ScoreValue) {
				saveHighscore (7);
			} else if (finalScore >= list [7].ScoreValue) {
				saveHighscore (8);
			} else if (finalScore >= list [8].ScoreValue) {
				saveHighscore (9);
			} else if (finalScore >= list [9].ScoreValue) {
				saveHighscore (10);
			}
		}

		/// <summary>
		/// saveHighscore updates and prepares the highscore-list with the new entry, removes the bottom one and prepares the 
		/// layout to receive a name for the entry.
		/// </summary>
		/// <param name="setRank">Set rank.</param>

		private void saveHighscore(int setRank)
		{
			scoreHeader.Text = "Rank " + setRank + " - Slutpoäng:";

			nameEdit.Visibility = ViewStates.Visible;
			nameButton.Visibility = ViewStates.Visible;
			replayButton.Visibility = ViewStates.Gone;
			menuButton.Visibility = ViewStates.Gone;

			HighScoreEntry newEntry = new HighScoreEntry ();
			getRank = setRank;

			newEntry.NameValue = "Inget givet namn";
			newEntry.ScoreValue = finalScore;

			Console.WriteLine("Gained rank: " + setRank);
			list.Insert(setRank-1, newEntry);

			for (int i = 0; i < list.Count; i++) {
				list [i].Rank = i+1;
				Console.WriteLine("Setting ranks: " + i + " - " + list [i].Rank);
			}

			for (int i = 0; i < list.Count; i++) {
				if (list[i].Rank > 10) {
					Console.WriteLine ("Entry removed: " + list[i].Rank + " - " + list[i].NameValue + " - " + list[i].ScoreValue);
					list.RemoveAt(i);
					i--;
				}
			}
		}

		/// <summary>
		/// saveName inserts the given name from nameEdit into the earned highscore-slot and saves the entry by calling the 
		/// updateList-function in the HighScoreManager-activity.
		/// </summary>
		/// <param name="name">Name.</param>

		private void saveEntry(string name)
		{
			nameEdit.Visibility = ViewStates.Gone;
			nameButton.Visibility = ViewStates.Gone;
			replayButton.Visibility = ViewStates.Visible;
			menuButton.Visibility = ViewStates.Visible;

			list [getRank-1].NameValue = name;
			manager.updateList (list);

			Toast.MakeText(this, "Highscore sparat", ToastLength.Short).Show();
		}
	}
}

