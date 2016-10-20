
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using SQLite;

namespace ExamensApp
{
	/// <summary>
	/// The Class that is used to hold Highscore-entries in the database.
	/// </summary>

	public class HighScoreEntry
	{
		[PrimaryKey, AutoIncrement]
		public int Rank { get; set; }
		public string NameValue { get; set; }
		public int ScoreValue { get; set; }
	}
}
