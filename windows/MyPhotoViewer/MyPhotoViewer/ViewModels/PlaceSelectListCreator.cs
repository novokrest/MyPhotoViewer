using MyPhotoViewer.Converters;
using MyPhotoViewer.Core;
using MyPhotoViewer.DAL.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPhotoViewer.ViewModels
{
    public interface IPlaceSelectListCreator
    {
        SelectList CreatePlaceList();
    }

    public class PlaceSelectListCreator : IPlaceSelectListCreator
    {
        private readonly IPlaceRepository _placeRepository;

        public PlaceSelectListCreator(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public SelectList CreatePlaceList()
        {
            Func<IPlace, SelectListItem> placeItemCreator = place => new SelectListItem
            {
                Text = PrettyString.ToString(place),
                Value = place.Id.ToString()
            };

            return new SelectList(_placeRepository.GetPlaces().Select(placeItemCreator), "Value", "Text");
        }
    }
}