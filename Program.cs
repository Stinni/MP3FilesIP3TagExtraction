using MP3FilesIP3TagExtraction.Tools;

namespace MP3FilesIP3TagExtraction
{
	class Program
	{
		static void Main(string[] args)
		{
			//  Uncommment to use the MP3 IP3 tag extraction tool
			/*
			const string filePath = @"D:\AnyRecover 2021-02-23 at 00.39.13\E(Raw files)\MP3";
			var extractionTool = new MP3FilesIP3TagExtraction(filePath);
			extractionTool.Extract();
			*/

			/* C:\toMoveToHDDs\Music\bands.txt */
			const string fromFilePath = "";
			const string toFilePath = "";
			var combinationTool = new BandsFilesCombination(fromFilePath, toFilePath, true);
			combinationTool.Combine();
		}
	}
}
