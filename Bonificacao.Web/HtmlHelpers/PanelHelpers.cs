using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Bonificacao.Web.HtmlHelpers
{
    public static class PanelHelpers
    {
        public static void Box(this HtmlHelper htmlHelper,string title, string description, Func<object, object> panelMarkup = null)
        {
            Box(htmlHelper, title, description, (panelMarkup == null ? "" : panelMarkup.DynamicInvoke(htmlHelper.ViewContext).ToString()));
        }

        public static void Box(this HtmlHelper html, string title, string description, string content)
        {
            using (var writer = new HtmlTextWriter(html.ViewContext.Writer))
            {
                //string result =
                writer.WriteLine("<div class=\"row\">");
                writer.WriteLine("<div class=\"col-lg-12\">");
                writer.WriteLine("<div class=\"ibox\">");
                writer.WriteLine("<div class=\"ibox-content\">");
                writer.WriteLine("<h2>" + title + "</h2>");
                writer.WriteLine((!string.IsNullOrEmpty(description) ? "<small>" + description + "</small>" : string.Empty));
                writer.WriteLine("<div class=\"padding-top\">");
                writer.WriteLine(content);
                writer.WriteLine("</div>");
                writer.WriteLine("</div>");
                writer.WriteLine("</div>");
                writer.WriteLine("</div>");
                writer.WriteLine("</div>");
            }
            //return new HtmlString(result.ToString());
        }

        //public void Render(this HtmlHelper htmlHelper)
        //{
        //    using (var writer = new HtmlTextWriter(htmlHelper.ViewContext.Writer))
        //        WriteHtml(writer);
        //}
    }
}