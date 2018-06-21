using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YuanHai.BlogUI.TagHelpers
{
    [HtmlTargetElement(Attributes = "asp-photo")]
    public class PhotoHelpers : TagHelper
    {
        public string AspPhoto { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "ddd");
        }
    }
}
