#pragma checksum "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Admin\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6bfc00cc2972a7325305a8d6a5c83a81ddb9a9a7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Index), @"mvc.1.0.view", @"/Views/Admin/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\_ViewImports.cshtml"
using AuctionApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\_ViewImports.cshtml"
using AuctionApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6bfc00cc2972a7325305a8d6a5c83a81ddb9a9a7", @"/Views/Admin/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"328ab3b4efbc41915256bbe0b8319147ebe5f6e4", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Int32>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Admin\Index.cshtml"
  
    ViewData["Title"] = "Admin Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>User auctions</h1>\r\n\r\n<script>\r\n    var auctions = parseInt(JSON.parse(\'");
#nullable restore
#line 10 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Admin\Index.cshtml"
                                   Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'));
    var num = 0;
    $(""document"").ready(function () {
        if (auctions <= 12) $(""#next"").attr(""disabled"", true);
        updateDrafts(num);
    })


    function nextPage() {
        if ((num + 1) * 12 > auctions) return;
        num++;
        $(""#prev"").attr(""disabled"", false);
        if ((num + 1) * 12 > auctions) $(""#next"").attr(""disabled"", true);
        //connection.invoke(""UpdateAuctions"", ""all"").catch(handleError);
        updateDrafts(num);
    }

    function prevPage() {
        if (num == 0) return;
        num--;
        $(""#next"").attr(""disabled"", false);
        if (num == 0) $(""#prev"").attr(""disabled"", true);
        // connection.invoke(""UpdateAuctions"", ""all"").catch(handleError);
        updateDrafts(num);
    }

</script>

<div class=""row"">
    <div class=""col-xl-10"" id=""auctions"">

    </div>
</div>
<hr>
<div class=""row"">
    <div class=""col-xl-5"">
        <button disabled onclick=""prevPage()"" class='btn btn-secondary float-left' id='prev'>
 ");
            WriteLiteral("           Previous\r\n        </button>\r\n\r\n    </div>\r\n    <div class=\"col-xl-5\">\r\n        <button onclick=\"nextPage()\" class=\'btn btn-secondary float-right\' id=\'next\'>\r\n            Next\r\n        </button>\r\n    </div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Int32> Html { get; private set; }
    }
}
#pragma warning restore 1591
