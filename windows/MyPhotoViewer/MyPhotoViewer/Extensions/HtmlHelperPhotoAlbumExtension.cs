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
        public static MvcHtmlString DisplayPeriod<Model>(this HtmlHelper<Model> html, PhotoAlbum photoAlbum)
        {
            return new MvcHtmlString(html.DisplayFor(model => photoAlbum.Period.From).ToHtmlString() + html.DisplayFor(model => photoAlbum.Period.To).ToHtmlString());
        }
    }
}