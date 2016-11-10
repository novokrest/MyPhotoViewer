using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    internal class PhotoCollectionOverviewReader
    {
        private const string OverviewFileName = "overview.txt";

        public static bool TryReadOverview(string photosDirectoryPath, out PhotoAlbumOverview photosOverview)
        {
            string overviewFilePath = Path.Combine(photosDirectoryPath, OverviewFileName);
            if (!File.Exists(overviewFilePath))
            {
                photosOverview = null;
                return false;
            }

            photosOverview = ReadOverview(overviewFilePath);
            return true;
        }

        private static PhotoAlbumOverview ReadOverview(string overviewFilePath)
        {
            using (var fileReader = new StreamReader(overviewFilePath, Encoding.Unicode))
            using (var jsonReader = new JsonTextReader(fileReader))
            {
                var jsonSerializer = new JsonSerializer();
                var photosOverview = jsonSerializer.Deserialize<PhotoAlbumOverview>(jsonReader);

                return photosOverview;
            }
        }
    }
}