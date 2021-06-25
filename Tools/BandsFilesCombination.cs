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
				throw new FileNotFoundException("File not found at path: " + _fromFilePath);
			if (!File.Exists(_toFilePath))
				throw new FileNotFoundException("File not found at path: " + _toFilePath);



			//  TODO: At the end - to be deleted
			if (_deleteFromFile)
				File.Delete(_fromFilePath);
		}
	}
}
