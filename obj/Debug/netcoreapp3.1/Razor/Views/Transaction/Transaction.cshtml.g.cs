#pragma checksum "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Transaction\Transaction.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "46452e245c4ee697e080329ab9b969bc84561be4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Transaction_Transaction), @"mvc.1.0.view", @"/Views/Transaction/Transaction.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Transaction\Transaction.cshtml"
using AuctionApp.Models.Database;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"46452e245c4ee697e080329ab9b969bc84561be4", @"/Views/Transaction/Transaction.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"328ab3b4efbc41915256bbe0b8319147ebe5f6e4", @"/Views/_ViewImports.cshtml")]
    public class Views_Transaction_Transaction : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<Transaction>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-xl-8\">\r\n        <ul class=\"list-group list-group-flush\"></ul>\r\n");
#nullable restore
#line 8 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Transaction\Transaction.cshtml"
         foreach (var trans in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li class=\"list-group-item\">ID: ");
#nullable restore
#line 10 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Transaction\Transaction.cshtml"
                                       Write(trans.id);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ------------------------------ Date: ");
#nullable restore
#line 10 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Transaction\Transaction.cshtml"
                                                                                      Write(trans.date);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                ------------------------- Tokens: ");
#nullable restore
#line 11 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Transaction\Transaction.cshtml"
                                             Write(trans.tokens);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 12 "C:\Users\Vladimir Siskin\Desktop\iep_p\AuctionApp\Views\Transaction\Transaction.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<Transaction>> Html { get; private set; }
    }
}
#pragma warning restore 1591
