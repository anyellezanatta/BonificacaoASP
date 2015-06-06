using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Bonificacao.Web.HtmlHelpers
{
    public static class PanelHelpers
    {
        public static MyBox MyBox(this HtmlHelper helper)
        {
            return new MyBox(helper.ViewContext);
        }

        public static void Box(this HtmlHelper htmlHelper, string title, string description, Func<object, object> panelMarkup = null)
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

    public class MyBox : IHtmlString
    {
        private readonly ViewContext _viewContext;
        private string _content;
        private string _title;
        private string _description;

        public MyBox(ViewContext viewViewContext)
        {
            _viewContext = viewViewContext;
        }

        public MyBox Content(Func<object, object> value)
        {
            _content = value.DynamicInvoke(_viewContext).ToString();
            return this;
        }


        public MyBox Content(MvcHtmlString content)
        {
            _content = content.ToString();
            return this;
        }

        public MyBox Title(string title)
        {
            _title = title;
            return this;
        }

        public MyBox Description(string description)
        {
            _description = description;
            return this;
        }

        public string ToHtmlString()
        {
            using (var stringWriter = new StringWriter())
            {
                WriteHtml((TextWriter)stringWriter);
                return stringWriter.ToString();
            }
        }

        public void Render()
        {
            using (var writer = new HtmlTextWriter(_viewContext.Writer))
                WriteHtml(writer);
        }

        protected virtual void WriteHtml(TextWriter writer)
        {
            writer.WriteLine("<div class=\"row\">");
            writer.WriteLine("<div class=\"col-lg-12\">");
            writer.WriteLine("<div class=\"ibox\">");
            writer.WriteLine("<div class=\"ibox-content\">");
            writer.WriteLine("<h2>" + _title + "</h2>");
            writer.WriteLine((!string.IsNullOrEmpty(_description) ? "<small>" + _description + "</small>" : string.Empty));
            writer.WriteLine("<div class=\"padding-top\">");
            writer.WriteLine(_content);
            writer.WriteLine("</div>");
            writer.WriteLine("</div>");
            writer.WriteLine("</div>");
            writer.WriteLine("</div>");
            writer.WriteLine("</div>");
        }
    }
}