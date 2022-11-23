using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FlightMVC.Helpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("p", Attributes="asp-for")]
    public class DemosTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(context == null || output == null) {
                throw new ArgumentNullException();
            }

            var text = For.ModelExplorer.GetSimpleDisplayText();
            output.Content.SetContent(text);
        }
    }
}
