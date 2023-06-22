using System.Collections.Generic;
using System.IO;
using System.Text;

using UnityEngine;

/// <summary>
/// Performs a depth-first recursive search from a given root directory (the directory from which this is launched)
/// and counts the number of files in the end-nodes of the search tree.
/// </summary>
public class FileCounter : MonoBehaviour
{
	#region INTERNAL TYPES
	/// <summary>
	/// Stores a directory string and the number of files in said directory
	/// </summary>
	class DirFileCount
	{
		public string directory { get; set; }
		public int files { get; set; }
	}
	#endregion


	#region VARIABLES, CONSTANTS
	/// Starting directory
	const string WORKING_DIR = ".";
	// Ignore lists
	string[] ignoreFilesList = { ".meta", ".DS_Store" };
	string[] ignoreDirsList = { ".app", "/." };

	/// Directories inspected
	List<DirFileCount> directories = new List<DirFileCount>();
	#endregion


	#region ENTRY POINT
	void Start()
	{
		// Count files
		RecursivelyFindDirectories(WORKING_DIR);
		CountDirectorySize();
		PrintResults();

		Application.Quit();
	}
	#endregion


	#region FUNCTIONALITY
	/// <summary>
	/// Recursively search all subdirectories, adding all directories without children to the list.
	/// </summary>
	/// <param name="root">Root directory</param>
	void RecursivelyFindDirectories(string root)
	{
		string[] dirs = Directory.GetDirectories(root);

		if (dirs != null && dirs.Length > 0)
		{
			foreach (string dir in dirs)
			{
				bool ignore = false;

				foreach (string ignoreDir in ignoreDirsList)
				{
					if (dir.Contains(ignoreDir))
					{
						ignore = true;
					}
				}

				if (!ignore)
				{
					RecursivelyFindDirectories(dir);
				}
			}
		}
		else
		{
			directories.Add(new DirFileCount() { directory = root });
		}
	}

	/// <summary>
	/// Counts the number of files in each directory stored in the directories list.
	/// </summary>
	void CountDirectorySize()
	{
		foreach (DirFileCount dir in directories)
		{
			int count = 0;
			string[] files = Directory.GetFiles(dir.directory);

			foreach (string file in files)
			{
				bool ignore = false;

				foreach (string ignoreFile in ignoreFilesList)
				{
					if (file.Contains(ignoreFile))
					{
						ignore = true;
					}
				}

				if (!ignore)
				{
					count++;
				}
			}

			dir.files = count;
		}
	}

	/// <summary>
	/// Outputs the directories along with their file count into a nicely formatted text document.
	/// </summary>
	void PrintResults()
	{
		StringBuilder sb = new StringBuilder();

		foreach (DirFileCount dir in directories)
		{
			sb.AppendLine(string.Format("{0}\t{1}", dir.files, dir.directory));
		}

		File.WriteAllText(Path.Combine(WORKING_DIR, "FileCount_Results.txt"), sb.ToString());
	}
	#endregion
}
