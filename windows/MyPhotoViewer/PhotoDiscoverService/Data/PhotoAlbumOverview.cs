using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace PhotoDiscoverService.Data
{
    public class PhotoAlbumOverview
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }

    internal class PhotoAlbumOverviewReader
    {
        private const string OverviewFileName = "overview.txt";

        private readonly string _directory;
        private PhotoAlbumOverview _overview;

        public PhotoAlbumOverviewReader(string directory)
        {
            _directory = directory;
        }

        public PhotoAlbumOverview Overview => _overview;

        public bool ReadOverview()
        {
            string overviewFilePath = Path.Combine(_directory, OverviewFileName);
            if (File.Exists(overviewFilePath))
            {
                ReadOverview(overviewFilePath);
                return true;
            }

            return false;
        }

        private void ReadOverview(string overviewFilePath)
        {
            using (var fileReader = new StreamReader(overviewFilePath, Encoding.Unicode))
            using (var jsonReader = new JsonTextReader(fileReader))
            {
                var jsonSerializer = new JsonSerializer();
                _overview = jsonSerializer.Deserialize<PhotoAlbumOverview>(jsonReader);
            }
        }
    }
}