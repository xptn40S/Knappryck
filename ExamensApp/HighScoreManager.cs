
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

using SQLite;

namespace ExamensApp
{	
	public sealed class HighScoreManager
	{
		//private static HighScoreManager Instance = null;
		//private static readonly object padlock = new object();

		private string dbPath;
		public SQLiteConnection db;

		/// <summary>
		/// Sets the path of the database and creates a HighScoreEntry-Table.
		/// </summary>

		public HighScoreManager()
		{
			dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			dbPath += "\\database.db";
			db = new SQLiteConnection(dbPath);
			db.CreateTable<HighScoreEntry>();
			db.Close();
		}

		/// <summary>
		/// firstTimeSetup inserts ten preset-entries into the database the first time that you start the app.
		/// </summary>

		public void firstTimeSetup()
		{
			db = new SQLiteConnection(dbPath);

			HighScoreEntry newEntry = new HighScoreEntry();
			newEntry.Rank = 1;
			newEntry.NameValue = "The Best";
			newEntry.ScoreValue = 100;
			db.Insert(newEntry);

			newEntry = new HighScoreEntry();
			newEntry.Rank = 2;
			newEntry.NameValue = "Färgfantasten";
			newEntry.ScoreValue = 70;
			db.Insert(newEntry);

			newEntry = new HighScoreEntry();
			newEntry.Rank = 3;
			newEntry.NameValue = "Stan";
			newEntry.ScoreValue = 66;
			db.Insert(newEntry);

			newEntry = new HighScoreEntry();
			newEntry.Rank = 4;
			newEntry.NameValue = "Anja";
			newEntry.ScoreValue = 60;
			db.Insert(newEntry);

			newEntry = new HighScoreEntry();
			newEntry.Rank = 5;
			newEntry.NameValue = "Medelmåttiga Morgan";
			newEntry.ScoreValue = 50;
			db.Insert(newEntry);

			newEntry = new HighScoreEntry();
			newEntry.Rank = 6;
			newEntry.NameValue = "Tim";
			newEntry.ScoreValue = 40;
			db.Insert(newEntry);

			newEntry = new HighScoreEntry();
			newEntry.Rank = 7;
			newEntry.NameValue = "Lata Larry";
			newEntry.ScoreValue = 20;
			db.Insert(newEntry);

			newEntry = new HighScoreEntry();
			newEntry.Rank = 8;
			newEntry.NameValue = "Bob";
			newEntry.ScoreValue = 11;
			db.Insert(newEntry);

			newEntry = new HighScoreEntry();
			newEntry.Rank = 9;
			newEntry.NameValue = "Färgblinde Bob";
			newEntry.ScoreValue = 3;
			db.Insert(newEntry);

			newEntry = new HighScoreEntry();
			newEntry.Rank = 10;
			newEntry.NameValue = "Jerry";
			newEntry.ScoreValue = 0;
			db.Insert(newEntry);

			db.Close();
		}

		/// <summary>
		/// getList gets an ordered list of the entries in the database.
		/// </summary>
		/// <returns>The list.</returns>

		public List<HighScoreEntry> getList()
		{
			db = new SQLiteConnection(dbPath);
			IEnumerable<HighScoreEntry> highscores = db.Table<HighScoreEntry>();
			List<HighScoreEntry> list = highscores.Select (r => r).ToList();
			list = list.OrderBy(r => r.Rank).ToList();
			db.Close();

			return list;
		}

		/// <summary>
		/// updateList deletes all of the current entries in the database and updates it with a new list from the argument.
		/// </summary>
		/// <param name="newList">New list.</param>

		public void updateList(List<HighScoreEntry> newList)
		{
			db = new SQLiteConnection (dbPath);

			db.DeleteAll<HighScoreEntry>();
			db.CreateTable<HighScoreEntry>();

			for (int i = 0; i < newList.Count; i++) {
				Console.WriteLine ("Saving entry: " + i);
				db.Insert(newList[i]);
			}

			db.Close();
		}
	}
}

