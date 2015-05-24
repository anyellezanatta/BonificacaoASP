using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bonificacao.Web.HtmlHelpers
{
    public static class PanelHelpers
    {
        public static IHtmlString Box(this HtmlHelper htmlHelper,string title, string description, Func<object, object> panelMarkup = null)
        {
            return Box(htmlHelper, title, description, (panelMarkup == null ? "" : panelMarkup.DynamicInvoke(htmlHelper.ViewContext).ToString()));
        }

        public static IHtmlString Box(this HtmlHelper html, string title, string description, string content)
        {
            string result =
                "<div class=\"row\">" +
                    "<div class=\"col-lg-12\">" +
                        "<div class=\"ibox\">" +
                            "<div class=\"ibox-content\">" +
                                "<h2>" + title + "</h2>" +
                                (!string.IsNullOrEmpty(description) ? "<small>" + description + "</small>" : string.Empty) +
                                "<div class=\"padding-top\">" +
                                content +
                                "</div>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                "</div>";
            return new HtmlString(result.ToString());
        }
    }
}