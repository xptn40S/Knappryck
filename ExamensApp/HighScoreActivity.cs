
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
	[Activity (Label = "HighScoreActivity")]			
	public class HighScoreActivity : Activity
	{
		TextView name01; TextView score01;
		TextView name02; TextView score02;
		TextView name03; TextView score03;
		TextView name04; TextView score04;
		TextView name05; TextView score05;
		TextView name06; TextView score06;
		TextView name07; TextView score07;
		TextView name08; TextView score08;
		TextView name09; TextView score09;
		TextView name10; TextView score10;
		Button backButton;

		HighScoreManager manager = new HighScoreManager();
		List<HighScoreEntry> list;

		/// <summary>
		/// Gets the list from the database and calls the setTextValues-function.
		/// </summary>
		/// <param name="bundle">Bundle.</param>

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.HighScore);

			list = manager.getList();

			name01 = FindViewById<TextView>(Resource.Id.Highscore_Name01);
			name02 = FindViewById<TextView>(Resource.Id.Highscore_Name02);
			name03 = FindViewById<TextView>(Resource.Id.Highscore_Name03);
			name04 = FindViewById<TextView>(Resource.Id.Highscore_Name04);
			name05 = FindViewById<TextView>(Resource.Id.Highscore_Name05);
			name06 = FindViewById<TextView>(Resource.Id.Highscore_Name06);
			name07 = FindViewById<TextView>(Resource.Id.Highscore_Name07);
			name08 = FindViewById<TextView>(Resource.Id.Highscore_Name08);
			name09 = FindViewById<TextView>(Resource.Id.Highscore_Name09);
			name10 = FindViewById<TextView>(Resource.Id.Highscore_Name10);

			score01 = FindViewById<TextView>(Resource.Id.Highscore_Score01);
			score02 = FindViewById<TextView>(Resource.Id.Highscore_Score02);
			score03 = FindViewById<TextView>(Resource.Id.Highscore_Score03);
			score04 = FindViewById<TextView>(Resource.Id.Highscore_Score04);
			score05 = FindViewById<TextView>(Resource.Id.Highscore_Score05);
			score06 = FindViewById<TextView>(Resource.Id.Highscore_Score06);
			score07 = FindViewById<TextView>(Resource.Id.Highscore_Score07);
			score08 = FindViewById<TextView>(Resource.Id.Highscore_Score08);
			score09 = FindViewById<TextView>(Resource.Id.Highscore_Score09);
			score10 = FindViewById<TextView>(Resource.Id.Highscore_Score10);

			backButton = FindViewById<Button>(Resource.Id.Highscore_BackButton);

			backButton.Click += delegate {
				Intent intent = new Intent (this, typeof(MainActivity));
				StartActivity (intent);
			};

			setTextValues();
		}

		/// <summary>
		/// Sets the name- and score-values of the entries in the highscore-table.
		/// </summary>

		private void setTextValues()
		{
			name01.Text = list[0].NameValue;
			name02.Text = list[1].NameValue;
			name03.Text = list[2].NameValue;
			name04.Text = list[3].NameValue;
			name05.Text = list[4].NameValue;
			name06.Text = list[5].NameValue;
			name07.Text = list[6].NameValue;
			name08.Text = list[7].NameValue;
			name09.Text = list[8].NameValue;
			name10.Text = list[9].NameValue;

			score01.Text = list[0].ScoreValue + "p";
			score02.Text = list[1].ScoreValue + "p";
			score03.Text = list[2].ScoreValue + "p";
			score04.Text = list[3].ScoreValue + "p";
			score05.Text = list[4].ScoreValue + "p";
			score06.Text = list[5].ScoreValue + "p";
			score07.Text = list[6].ScoreValue + "p";
			score08.Text = list[7].ScoreValue + "p";
			score09.Text = list[8].ScoreValue + "p";
			score10.Text = list[9].ScoreValue + "p";
		}
	}
}

