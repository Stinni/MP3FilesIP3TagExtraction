using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Id3;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace MP3FilesIP3TagExtraction.Tools
{
	// ReSharper disable once InconsistentNaming
	class MP3FilesIP3TagExtraction
	{
		//  Variables:
		private string[] _musicFiles;
		private string _errorFilePath;
		private string _bandsFilePath;

		//  Constructor:
		public MP3FilesIP3TagExtraction(string filePath)
		{
			_musicFiles = Directory.GetFiles(filePath, "*.mp3", SearchOption.AllDirectories);
			_errorFilePath = @"C:\tmp\errors.txt";
			_bandsFilePath = @"C:\tmp\bands.txt";
		}


		//  Methods:
		public void Extract()
		{
			if (File.Exists(_errorFilePath))
				File.Delete(_errorFilePath);

			using StreamWriter errorFile = new StreamWriter(_errorFilePath, true);
			List<string> bands = new List<string>();
			List<string> filesToDelete = new List<string>();
			int v1Tags = 0;

			foreach (string musicFile in _musicFiles)
			{
				using Mp3 mp3 = new Mp3(musicFile);
				try
				{
					Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);
					Id3Tag tmp = mp3.GetTag(Id3TagFamily.Version1X);
					if (tmp != null)
						v1Tags++;

					if (tag != null)
					{
						if (!string.IsNullOrWhiteSpace(tag.Artists))
						{
							filesToDelete.Add(musicFile);
							string tmpArtist = tag.Artists;
							string artist = tmpArtist.Replace("\0", string.Empty);
							if (!bands.Contains(artist))
							{
								bands.Add(artist);
							}
						}

						if (!string.IsNullOrWhiteSpace(tag.Band))
						{
							filesToDelete.Add(musicFile);
							string tmpBand = tag.Band;
							string band = tmpBand.Replace("\0", string.Empty);
							if (!bands.Contains(band))
							{
								bands.Add(band);
							}
						}
					}
				}
				catch (Exception e)
				{
					errorFile.WriteLine(musicFile);
					if (!string.IsNullOrWhiteSpace(e.Message))
						errorFile.WriteLine("Error: " + e.Message + Environment.NewLine);
					else
						errorFile.WriteLine("Exception cast without a message!");
				}
			}

			bands.Sort();
			using StreamWriter bandsFile = new StreamWriter(_bandsFilePath, false, Encoding.UTF8);
			foreach (string b in bands)
				bandsFile.WriteLine(b);
			bandsFile.Close();

			errorFile.WriteLine("Total number of V1 tags: " + v1Tags);
			errorFile.Close();

			foreach (string f in filesToDelete)
			{
				if (File.Exists(f))
					File.Delete(f);
			}
		}
	}
}
