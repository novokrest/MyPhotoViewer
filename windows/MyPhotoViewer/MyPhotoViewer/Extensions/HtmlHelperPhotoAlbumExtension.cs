using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MyPhotoViewer.Extensions
{
    public static class HtmlHelperPhotoAlbumExtension
    {
        public static MvcHtmlString DisplayPeriod<Model>(this HtmlHelper<Model> html, IPhotoAlbum photoAlbum)
        {
            return new MvcHtmlString(html.DisplayFor(model => photoAlbum.Period.From).ToHtmlString() + html.DisplayFor(model => photoAlbum.Period.To).ToHtmlString());
        }

        public static MvcHtmlString DisplayPlace<Model>(this HtmlHelper<Model> html, IPlace place)
        {
            string placeString = string.Join(", ", new[] { place.Name, place.City, place.Country }.Where(str => !string.IsNullOrEmpty(str)));
            return new MvcHtmlString(placeString);
        }
    }
}