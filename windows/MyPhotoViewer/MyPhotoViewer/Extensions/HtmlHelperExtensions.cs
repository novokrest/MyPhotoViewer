using MyPhotoViewer.Core;
using MyPhotoViewer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MyPhotoViewer.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DisplayPeriod<Model>(this HtmlHelper<Model> html, IAlbum photoAlbum)
        {
            return new MvcHtmlString(html.DisplayFor(model => photoAlbum.Period.From).ToHtmlString() + html.DisplayFor(model => photoAlbum.Period.To).ToHtmlString());
        }

        public static MvcHtmlString DisplayPlace<Model>(this HtmlHelper<Model> html, IPlace place)
        {
            string placeString = string.Join(", ", new[] { place.Name, place.City, place.Country }.Where(str => !string.IsNullOrEmpty(str)));
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