using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BoardGames.TagHelpers { 
    public class BargainTagHelper : TagHelper {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
            var content = await output.GetChildContentAsync();
            var target = Int32.Parse(content.GetContent());
            if (target < 30) {
               // output.TagName = "bargain";
                output.PreContent.SetHtmlContent("<font color='green'>Promocja! Tylko dziś</font> ");
            } else if (target > 80) {
                output.PreContent.SetHtmlContent("<font color='red'>Nowość w ofercie!</font> ");
            }
        }
    }
}
