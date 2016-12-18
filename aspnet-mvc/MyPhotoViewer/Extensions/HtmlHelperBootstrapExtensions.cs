using MyPhotoViewer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoViewer.Extensions
{
    public static class HtmlHelperBootstrapExtensions
    {
        public static MvcHtmlString BootstrapDropdownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, SelectList selectList, string label)
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return helper.BootstrapDropdownList(selectList, name, label);
        }

        public static MvcHtmlString BootstrapDropdownList<TModel>(this HtmlHelper<TModel> helper, SelectList selectList, string name, string label)
        {
            var button = new TagBuilder("button")
            {
                Attributes =
            {
                {"id", name},
                {"type", "button"},
                {"data-toggle", "dropdown"}
            }
            };

            button.AddCssClass("btn");
            button.AddCssClass("btn-default");
            button.AddCssClass("dropdown-toggle");

            button.SetInnerText(label);
            button.InnerHtml += " " + BuildCaret();

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("dropdown");

            wrapper.InnerHtml += button;
            wrapper.InnerHtml += BuildDropdown(name, selectList.Items.Cast<SelectListItem>());

            return new MvcHtmlString(wrapper.ToString());
        }

        private static string BuildCaret()
        {
            var caret = new TagBuilder("span");
            caret.AddCssClass("caret");

            return caret.ToString();
        }

        private static string BuildDropdown(string id, IEnumerable<SelectListItem> items)
        {
            var list = new TagBuilder("ul")
            {
                Attributes =
            {
                {"class", "dropdown-menu"},
                {"role", "menu"},
                {"aria-labelledby", id}
            }
            };

            items.ForEach(x => list.InnerHtml += "<li role=\"presentation\">" + BuildListRow(x) + "</li>");

            return list.ToString();
        }

        private static string BuildListRow(SelectListItem item)
        {
            var anchor = new TagBuilder("a")
            {
                Attributes =
            {
                {"role", "menuitem"},
                {"tabindex", "-1"},
                {"href", item.Value}
            }
            };

            anchor.SetInnerText(item.Text);

            return anchor.ToString();
        }
    }
}