using Id3;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace MP3FilesIP3TagExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(@"C:\tmp\errors.txt"))
                File.Delete(@"C:\tmp\errors.txt");

            string[] musicFiles = Directory.GetFiles(@"D:\AnyRecover 2021-02-23 at 00.39.13\E(Raw files)\MP3", "*.mp3", SearchOption.AllDirectories);
            using StreamWriter errorFile = new StreamWriter(@"C:\tmp\errors.txt", true);
            List<string> bands = new List<string>();
            int v1Tags = 0;

            foreach (string musicFile in musicFiles)
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
                            string tmpArtist = tag.Artists;
                            string artist = tmpArtist.Replace("\0", string.Empty);
                            if (!bands.Contains(artist))
                            {
                                bands.Add(artist);
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(tag.Band))
                        {
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
            using StreamWriter bandsFile = new StreamWriter(@"C:\tmp\bands.txt", false, Encoding.UTF8);
            foreach (string b in bands)
                bandsFile.WriteLine(b);
            bandsFile.Close();

            errorFile.WriteLine("Total number of V1 tags: " + v1Tags);
            errorFile.Close();
        }
    }
}
