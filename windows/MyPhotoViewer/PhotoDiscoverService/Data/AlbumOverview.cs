using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace PhotoDiscoverService.Data
{
    public class AlbumOverview
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Place { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }

    internal class AlbumOverviewReader
    {
        private const string OverviewFileName = "overview.txt";

        private readonly string _directory;
        private AlbumOverview _overview;

        public AlbumOverviewReader(string directory)
        {
            _directory = directory;
        }

        public AlbumOverview Overview => _overview;

        public bool ReadOverview()
        {
            string overviewFilePath = Path.Combine(_directory, OverviewFileName);
            if (File.Exists(overviewFilePath))
            {
                ReadOverview(overviewFilePath);
                return true;
            }

            _overview = GenerateDefaultOverview();
            return true;
        }

        private AlbumOverview GenerateDefaultOverview()
        {
            return new AlbumOverview
            {
                Title = new DirectoryInfo(_directory).Name,
                Description = "No Description",
                Place = "Unknown Place"
            };
        }

        private void ReadOverview(string overviewFilePath)
        {
            using (var fileReader = new StreamReader(overviewFilePath, Encoding.Unicode))
            using (var jsonReader = new JsonTextReader(fileReader))
            {
                var jsonSerializer = new JsonSerializer();
                _overview = jsonSerializer.Deserialize<AlbumOverview>(jsonReader);
            }
        }
    }
}