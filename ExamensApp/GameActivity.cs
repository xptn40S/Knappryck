
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

using System.Timers;

namespace ExamensApp
{
	[Activity (Label = "GameActivity", Theme = "@android:style/Theme.NoTitleBar", 
		ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class GameActivity : Activity
	{
		ImageView currentColor;
		TextView timeText;
		TextView scoreText;
		ImageView strike1;
		ImageView strike2;
		ImageView strike3;
		TableLayout gameTable;
		Button pauseButton;
		TextView pauseText;
		Timer time;

		Button but1_1; Button but1_2; Button but1_3;

		Button but2_1; Button but2_2; Button but2_3;

		Button but3_1; Button but3_2; Button but3_3;

		bool paused = false;
		int timeLimit = 10;
		int score = 0;
		int colorNumber;
		int maxColors = 2;
		int maxSpaces = 9;
		int strikes = 0;
		int maxColorInterval = 0;

		List<Button> buttonList = new List<Button>();
		List<int> setList = new List<int>();

		/// <summary>
		/// Initializes the game by running the initGameLayout-function, starting a new round and setting
		/// </summary>
		/// <param name="bundle">Bundle.</param>

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Game);

			int levelType = Intent.GetIntExtra("level", 0);

			pauseButton = FindViewById<Button>(Resource.Id.Game_PauseButton);
			pauseText = FindViewById<TextView>(Resource.Id.Game_PauseText);
			currentColor = FindViewById<ImageView>(Resource.Id.Game_CurrentColor);
			timeText = FindViewById<TextView>(Resource.Id.Game_TimerText);
			scoreText = FindViewById<TextView> (Resource.Id.Game_ScoreText);
			gameTable = FindViewById<TableLayout>(Resource.Id.Game_Table);

			strike1 = FindViewById<ImageView> (Resource.Id.Game_StrikeSpace1);
			strike2 = FindViewById<ImageView> (Resource.Id.Game_StrikeSpace2);
			strike3 = FindViewById<ImageView> (Resource.Id.Game_StrikeSpace3);

			initGameLayout(levelType);
			newRound();

			time = new Timer();
			time.Interval = 1000;
			time.Elapsed += OnTimedEvent;
			time.Enabled = true;
		}

		/// <summary>
		/// Initializes the layout of the game by inserting the row-templates into the table, setting the values of the 
		/// layout-elements and applying click-events to the buttons.
		/// </summary>
		/// <param name="level">Level.</param>

		private void initGameLayout(int level) {
			if (level == 3) 
			{
				for (int i = 0; i < level; i++) {
					View newRow = LayoutInflater.From (this).Inflate (Resource.Drawable.ThreeGame, null, false);
					gameTable.AddView(newRow);
				}

				View row1 = gameTable.GetChildAt(0);
				View row2 = gameTable.GetChildAt(1);
				View row3 = gameTable.GetChildAt(2);

				but1_1 = row1.FindViewById<Button> (Resource.Id.Three_1);
				but1_2 = row1.FindViewById<Button> (Resource.Id.Three_2);
				but1_3 = row1.FindViewById<Button> (Resource.Id.Three_3);

				but2_1 = row2.FindViewById<Button> (Resource.Id.Three_1);
				but2_2 = row2.FindViewById<Button> (Resource.Id.Three_2);
				but2_3 = row2.FindViewById<Button> (Resource.Id.Three_3);

				but3_1 = row3.FindViewById<Button> (Resource.Id.Three_1);
				but3_2 = row3.FindViewById<Button> (Resource.Id.Three_2);
				but3_3 = row3.FindViewById<Button> (Resource.Id.Three_3);

				buttonList.Add(but1_1);
				buttonList.Add(but1_2);
				buttonList.Add(but1_3);

				buttonList.Add(but2_1);
				buttonList.Add(but2_2);
				buttonList.Add(but2_3);

				buttonList.Add(but3_1);
				buttonList.Add(but3_2);
				buttonList.Add(but3_3);

				but1_1.Click += delegate { clickEvent(but1_1, 0); };
				but1_2.Click += delegate { clickEvent(but1_2, 1); };
				but1_3.Click += delegate { clickEvent(but1_3, 2); };

				but2_1.Click += delegate { clickEvent(but2_1, 3); };
				but2_2.Click += delegate { clickEvent(but2_2, 4); };
				but2_3.Click += delegate { clickEvent(but2_3, 5); };

				but3_1.Click += delegate { clickEvent(but3_1, 6); };
				but3_2.Click += delegate { clickEvent(but3_2, 7); };
				but3_3.Click += delegate { clickEvent(but3_3, 8); };

				pauseButton.Click += delegate {
					pauseGame(paused);
				};
			}
		}

		/// <summary>
		/// This function should be called when you click a button in the game-activity.
		/// It receives the clicked button and its position in the numbered list, and if this 
		/// number matches the current number/color of the round, this button will, in a way, be 
		/// deactivated, and then checks if there are any remaining entries in the list that 
		/// match the current number/color. If not, a new round is started and a point is gained.
		/// </summary>
		/// <param name="clickedButton">Clicked button.</param>
		/// <param name="listNum">List number.</param>

		private void clickEvent(Button clickedButton, int listNum) {
			clickedButton.SetBackgroundResource(Resource.Drawable.Color0White);

			if (setList[listNum] == colorNumber) {
				setList[listNum] = 0;

				bool remains = false;
				for (int i = 0; i < maxSpaces; i++) {
					if (setList[i] == colorNumber) {
						remains = true;
						break;
					}
				}
				if (!remains) {
					score++;

					if (maxColors < 7) {
						maxColorInterval++;
					}

					if (maxColorInterval == 10) {
						maxColors++;
						maxColorInterval = 0;
					}

					if (timeLimit > 20) {
						timeLimit += 1;
					} else {
						timeLimit += 3;
					}

					scoreText.Text = "Poäng: " + score;
					timeText.Text = "Tid: "+timeLimit;
					clearTableButtonValues();
					newRound();
				}
			} else if (setList[listNum] != 0) {
				setList[listNum] = 0;
				strikes++;

				if (strikes == 1) {
					strike3.Visibility = ViewStates.Invisible;
				} else if (strikes == 2) {
					strike2.Visibility = ViewStates.Invisible;
				} else if (strikes == 3) {
					strike1.Visibility = ViewStates.Invisible;
					loseActivity();
				}
			}
		}

		/// <summary>
		/// newRound starts a new round of the game by randomly selecting the current color that has to be pressed and 
		/// then setting random number-values into a list and the color of the corresponding button, 
		/// which will enable the activity to see which button has been pressed and whether you'll be 
		/// given you a strike or not.
		/// 
		/// ValidateGame checks if it is a valid round by checking if any of the buttons have a matching value to the 
		/// one that's currently set. If it doesn't, a new round is started instead to prevent an unwinnable round.
		/// </summary>

		private void newRound() {
			Random rand = new Random ();
			colorNumber = rand.Next (1, maxColors+1);
			setList = new List<int>();

			if (colorNumber == 1) {
				currentColor.SetBackgroundResource(Resource.Drawable.Color1Blue);
			} else if (colorNumber == 2) {
				currentColor.SetBackgroundResource(Resource.Drawable.Color2Red);
			} else if (colorNumber == 3) {
				currentColor.SetBackgroundResource(Resource.Drawable.Color3Yellow);
			} else if (colorNumber == 4) {
				currentColor.SetBackgroundResource(Resource.Drawable.Color4Green);
			} else if (colorNumber == 5) {
				currentColor.SetBackgroundResource(Resource.Drawable.Color5Purple);
			} else if (colorNumber == 6) {
				currentColor.SetBackgroundResource(Resource.Drawable.Color6Cyan);
			} else if (colorNumber == 7) {
				currentColor.SetBackgroundResource(Resource.Drawable.Color7Gray);
			}

			for (int i = 0; i < maxSpaces; i++) {
				int setColor = rand.Next (1, maxColors+1);
				setList.Add(setColor);
				Console.WriteLine("Button "+i+": "+setColor);

				if (setColor == 1) {
					buttonList[i].SetBackgroundResource(Resource.Drawable.Color1Blue);
				} else if (setColor == 2) {
					buttonList[i].SetBackgroundResource(Resource.Drawable.Color2Red);
				} else if (setColor == 3) {
					buttonList[i].SetBackgroundResource(Resource.Drawable.Color3Yellow);
				} else if (setColor == 4) {
					buttonList[i].SetBackgroundResource(Resource.Drawable.Color4Green);
				} else if (setColor == 5) {
					buttonList[i].SetBackgroundResource(Resource.Drawable.Color5Purple);
				} else if (setColor == 6) {
					buttonList[i].SetBackgroundResource(Resource.Drawable.Color6Cyan);
				} else if (setColor == 7) {
					buttonList[i].SetBackgroundResource(Resource.Drawable.Color7Gray);
				}
			}

			if (!validateGame ()) {
				clearTableButtonValues();
				newRound();
			}
		}

		/// <summary>
		/// validateGame is a bool-function that is used to check if there are any matching button-values in the current round.
		/// If true,true is returned, otherwise false.
		/// </summary>
		/// <returns><c>true</c>, if game was validated, <c>false</c> otherwise.</returns>

		private bool validateGame() {
			bool validGame = false;

			for (int i = 0; i < maxSpaces; i++) {
				if (setList [i] == colorNumber) {
					validGame = true;
				}
			}
			return validGame;
		}

		/// <summary>
		/// Turns all of the backgrounds of the table-buttons white.
		/// </summary>

		private void clearTableButtonValues() {
			but1_1.SetBackgroundResource(Resource.Drawable.Color0White);
			but1_2.SetBackgroundResource(Resource.Drawable.Color0White);
			but1_3.SetBackgroundResource(Resource.Drawable.Color0White);

			but2_1.SetBackgroundResource(Resource.Drawable.Color0White);
			but2_2.SetBackgroundResource(Resource.Drawable.Color0White);
			but2_2.SetBackgroundResource(Resource.Drawable.Color0White);

			but3_1.SetBackgroundResource(Resource.Drawable.Color0White);
			but3_2.SetBackgroundResource(Resource.Drawable.Color0White);
			but3_3.SetBackgroundResource(Resource.Drawable.Color0White);
		}

		/// <summary>
		/// OnTimedEvent is called every time that the timer passes a second.
		/// Lowers the timer and updates the layout's timer-text, and when the timer reaches zero, loseActivity is called.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>

		private void OnTimedEvent(object sender, ElapsedEventArgs e)
		{
			timeLimit--;
			RunOnUiThread (() => timeText.Text = "Tid: " + timeLimit);

			if (timeLimit == 0) {
				loseActivity();
			}
		}

		/// <summary>
		/// pauseGame pauses or resumes the game by hiding and showing specific layout-elements and stopping/starting the timer.
		/// </summary>
		/// <param name="isPaused">If set to <c>true</c> is paused.</param>

		private void pauseGame(bool isPaused) {
			if (!isPaused) {
				paused = true;
				pauseButton.Text = "Fortsätt";
				currentColor.Visibility = ViewStates.Invisible;
				gameTable.Visibility = ViewStates.Gone;

				pauseText.Visibility = ViewStates.Visible;
				time.Stop();
			} else {
				paused = false;
				pauseButton.Text = "Pausa";
				currentColor.Visibility = ViewStates.Visible;
				gameTable.Visibility = ViewStates.Visible;

				pauseText.Visibility = ViewStates.Gone;
				time.Start();
			}
		}

		/// <summary>
		/// loseActivity is run when the game is lost, either by losing your "lives" or by running out of time.
		/// Stops and disposes of the timer and starts the GameOver-Activity.
		/// </summary>

		private void loseActivity(){
			time.Stop();
			time.Dispose();
			Intent intent = new Intent (this, typeof(GameOverActivity));
			intent.PutExtra("finalScore", score);
			Finish();
			StartActivity (intent);
		}

		public override void OnBackPressed ()
		{
			base.OnBackPressed();
			time.Stop ();
			time.Dispose ();
		}

		/// <summary>
		/// The timer is stopped when the activity becomes inactive.
		/// </summary>

		protected override void OnPause ()
		{
			base.OnPause ();
			time.Stop ();
		}

		/// <summary>
		/// The timer resumes when the activity becomes active again.
		/// </summary>

		protected override void OnResume ()
		{
			base.OnResume();
			time.Start ();
		}
	}
}

