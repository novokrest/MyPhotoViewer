using MyPhotoViewer.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoDiscoverService.Data
{
    public class PlaceRegister
    {
        private readonly IPhotosDbContext _context;
        private int _registeredPlaceCount;

        public PlaceRegister(IPhotosDbContext context)
        {
            _context = context;
        }

        public PlaceEntity Register(string name, string city, string country)
        {
            return Register(new PlaceEntity { Name = name, City = city, Country = country });
        }

        private PlaceEntity Register(PlaceEntity newPlace)
        {
            PlaceEntity place = _context.Places
                                        .AsEnumerable()
                                        .FirstOrDefault(registeredPlace => new PlaceEqualityComparer().Equals(registeredPlace, newPlace));

            if (place == null)
            {
                newPlace.Id = _registeredPlaceCount++;
                place = _context.Places.Add(newPlace);
                _context.SaveChanges();
            }

            return place;
        }

        private class PlaceEqualityComparer : IEqualityComparer<PlaceEntity>
        {
            private static readonly Func<string, string, bool> StringEqualityComparer = (s1, s2) => 
            {
                return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
            };

            public bool Equals(PlaceEntity x, PlaceEntity y)
            {
                return StringEqualityComparer(x.Name, y.Name) 
                    && StringEqualityComparer(x.City, y.City) 
                    && StringEqualityComparer(x.Country, y.Country);
            }

            public int GetHashCode(PlaceEntity place)
            {
                return place.Name.GetHashCode();
            }
        }
    }
}
