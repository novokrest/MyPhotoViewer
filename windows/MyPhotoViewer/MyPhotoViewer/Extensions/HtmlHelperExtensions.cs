using MyPhotoViewer.Core.Extensions;
using MyPhotoViewer.ViewModels.Album;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MyPhotoViewer.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DisplayPeriod<Model>(this HtmlHelper<Model> html, BaseAlbumViewModel album)
        {
            string formatString = "dd.MM.yyyy";
            return new MvcHtmlString(album.From.ToFormatString(formatString) + " - " + album.To.ToFormatString(formatString));
        }

        public static MvcHtmlString DisplayPlace<Model>(this HtmlHelper<Model> html, BaseAlbumViewModel album)
        {
            string placeString = string.Join(", ", new[] { album.Place, album.City, album.Country }.Where(str => !string.IsNullOrEmpty(str)));
            return new MvcHtmlString(placeString);
        }

        public static MvcHtmlString DateTimePeriodFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string partialViewName)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            object model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            var viewData = new ViewDataDictionary(helper.ViewData)
            {
                TemplateInfo = new TemplateInfo { HtmlFieldPrefix = name }
            };
            return helper.Partial(partialViewName, model, viewData);
        }
    }
}