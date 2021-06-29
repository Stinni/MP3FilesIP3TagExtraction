using System;
using System.Collections.Generic;
using System.IO;

namespace MP3FilesIP3TagExtraction.Tools
{
	class BandsFilesCombination
	{
		//  Variables:
		private readonly string _fromFilePath;
		private readonly string _toFilePath;
		private readonly bool _deleteFromFile;

		//  Constructors:
		public BandsFilesCombination(string path1, string path2, bool del)
		{
			_fromFilePath = path1;
			_toFilePath = path2;
			_deleteFromFile = del;
		}

		//  Methods:
		public void Combine()
		{
			if (!File.Exists(_fromFilePath))
				throw new FileNotFoundException("File not found: " + _fromFilePath);
			if (!File.Exists(_toFilePath))
				throw new FileNotFoundException("File not found: " + _toFilePath);

			using StreamReader toFile = new StreamReader(_toFilePath);
			using StreamReader fromFile = new StreamReader(_fromFilePath);
			string line;
			List<string> bands = new List<string>();

			while ((line = toFile.ReadLine()) != null)
			{
				bands.Add(line);
			}
			while ((line = fromFile.ReadLine()) != null)
			{
				if (bands.Contains(line)) continue;
				bands.Add(line);
			}
			toFile.Close();
			fromFile.Close();

			using StreamWriter outFile = new StreamWriter(_toFilePath, false);
			foreach (string b in bands)
				outFile.WriteLine(b);
			outFile.Close();

			if (_deleteFromFile)
				File.Delete(_fromFilePath);
		}
	}
}
