using System;
using System.IO;
using MP3FilesIP3TagExtraction.Tools;

namespace MP3FilesIP3TagExtraction
{
	class Program
	{
		static void Main(string[] args)
		{
			//  Uncommment to use the MP3 IP3 tag extraction tool
			const string filePath = @"D:\AnyRecover 2021-02-23 at 00.39.13\E(NTFS)\Lost Location";
			var extractionTool = new Tools.MP3FilesIP3TagExtraction(filePath);
			extractionTool.Extract();
			
			//  Uncomment to use the file combination tool
			/*
			const string fromFilePath = @"C:\toMoveToHDDs\Music\bands.txt";
			const string toFilePath = @"D:\Yfirfarid\Music\bands.txt";
			var combinationTool = new BandsFilesCombination(fromFilePath, toFilePath, true);

			try
			{
				combinationTool.Combine();
			}
			catch (FileNotFoundException e)
			{
				using StreamWriter errorFile = new StreamWriter(@"C:\tmp\errors.txt", true);
				if (!string.IsNullOrWhiteSpace(e.Message))
					errorFile.WriteLine("Error: " + e.Message + Environment.NewLine);
				else
					errorFile.WriteLine("Exception cast without a message!");
				errorFile.Close();
			}
			*/
		}
	}
}
