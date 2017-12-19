using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace NetCommunitySolution.Web.Framework.Html
{

    public static class HtmlExtensions
    {
        #region Html Extensions
        public static MvcHtmlString NETLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper,
        Expression<Func<TModel, TValue>> expression, bool displayHint = true)
        {
            return helper.LabelFor(expression, new {@class = "layui-form-label" });
        }


        public static string FieldIdFor<T, TResult>(this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
        {
            var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return id.Replace('[', '_').Replace(']', '_');
        }

        public static MvcHtmlString NETEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression, 
           bool renderFormControlClass = true)
        {
            var result = new StringBuilder();

            var htmlAttributes = new
            {
                @class = renderFormControlClass ? "layui-input" : "",
            };
            
            
            result.Append(helper.EditorFor(expression, new { htmlAttributes }));

            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString NETDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> helper,
          Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> itemList,
          object htmlAttributes = null, string optionLabel = null, string layfilter = null,
          bool required = false)
        {
            var result = new StringBuilder();

            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attrs.Add("class", "layui-input layui-unselect");
            if (!String.IsNullOrWhiteSpace(layfilter))
                attrs.Add("lay-filter", layfilter);            

            if (required)
                result.AppendFormat("<div class=\"input-group input-group-required\">{0}<div class=\"input-group-btn\"><span class=\"required\">*</span></div></div>",
                    helper.DropDownListFor(expression, itemList, optionLabel, attrs).ToHtmlString());
            else
                result.Append(helper.DropDownListFor(expression, itemList, optionLabel, attrs).ToHtmlString());


            return MvcHtmlString.Create(result.ToString());
        }


        public static MvcHtmlString NETCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, object htmlAttributes = null, string layfilter = null)
        {
            var attributes =
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (!attributes.ContainsKey("lay-filter"))
                attributes["lay-filter"] = layfilter;
            return htmlHelper.CheckBoxFor(expression, attributes);
            
        }

        public static MvcHtmlString NETRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, object value, object htmlAttributes = null, string layName =null)
        {
            var attributes =
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (!attributes.ContainsKey("lay-filter"))
                attributes["lay-filter"] = layName;
            return htmlHelper.RadioButtonFor(expression, value, attributes);
        }


        /// <summary>
        /// 获取保存的tab页面
        /// </summary>
        /// <returns>Name</returns>
        public static string GetSelectedTabName(this HtmlHelper helper)
        {
            var tabName = string.Empty;
            const string dataKey = "net.selected-tab-name";

            if (helper.ViewData.ContainsKey(dataKey))
                tabName = helper.ViewData[dataKey].ToString();

            //TODO test in asp.net core
            if (helper.ViewContext.TempData.ContainsKey(dataKey))
                tabName = helper.ViewContext.TempData[dataKey].ToString();

            return tabName;
        }

        public static MvcHtmlString RenderBootstrapTabHeader(this HtmlHelper helper, string currentTabName,
           string title, bool isDefaultTab = false, string tabNameToSelect = "", string customCssClass = "")
        {
            if (helper == null)
                throw new ArgumentNullException("helper");

            if (string.IsNullOrEmpty(tabNameToSelect))
                tabNameToSelect = helper.GetSelectedTabName();

            if (string.IsNullOrEmpty(tabNameToSelect) && isDefaultTab)
                tabNameToSelect = currentTabName;

            var a = new TagBuilder("a")
            {
                Attributes =
                {
                    new KeyValuePair<string, string>("data-tab-name", currentTabName),
                    new KeyValuePair<string, string>("href", string.Format("#{0}", currentTabName)),
                    new KeyValuePair<string, string>("data-toggle", "tab"),
                }
            };
            a.InnerHtml = title;

            var liClassValue = "";
            if (tabNameToSelect == currentTabName)
            {
                liClassValue = "active";
            }
            if (!String.IsNullOrEmpty(customCssClass))
            {
                if (!String.IsNullOrEmpty(liClassValue))
                    liClassValue += " ";
                liClassValue += customCssClass;
            }

            var li = new TagBuilder("li")
            {
                Attributes =
                {
                    new KeyValuePair<string, string>("class", liClassValue),
                },
            };
            li.InnerHtml = a.ToString();
            return MvcHtmlString.Create(li.ToString());
        }

        #endregion

    }
}