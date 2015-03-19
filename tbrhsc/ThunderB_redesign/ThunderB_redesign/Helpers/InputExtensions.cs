using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThunderB_redesign.Helpers
{
    public static class LabelExtension
    {
        public static MvcHtmlString RichTextEditorFor(this HtmlHelper helper, string name, string text = "")
        {
            return new MvcHtmlString(String.Format("<textarea name='{0}' rows='30' cols='80' class='tinymce'>{1}</textarea>", name, text));
        }
    }
}