#pragma checksum "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f51861bf81ca13c7438fc82c5728dc3785aba938"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EMS_Register), @"mvc.1.0.view", @"/Views/EMS/Register.cshtml")]
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
#line 1 "D:\.NET\EMS\Employee Management System\Views\_ViewImports.cshtml"
using Employee_Management_System;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\.NET\EMS\Employee Management System\Views\_ViewImports.cshtml"
using Employee_Management_System.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f51861bf81ca13c7438fc82c5728dc3785aba938", @"/Views/EMS/Register.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"734bf9f493a4ec5040262c53aa43701c0ec27140", @"/Views/_ViewImports.cshtml")]
    public class Views_EMS_Register : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RegisterViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
  
    ViewBag.Title = "Register";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>Register</h2>\r\n\r\n\r\n<div>\r\n");
#nullable restore
#line 13 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
     using (Html.BeginForm("SaveRegisterDetails", "EMS"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div style=\"color:red;\">");
#nullable restore
#line 15 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                           Write(Html.ValidationSummary());

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        <div class=\"row\">\r\n            <!--Show details are saved successfully message-->\r\n            <div class=\"col-lg-12\">");
#nullable restore
#line 18 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                              Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        </div><br />\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-2\">");
#nullable restore
#line 21 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                             Write(Html.LabelFor(a => a.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            <div class=\"col-lg-10\">");
#nullable restore
#line 22 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                              Write(Html.TextBoxFor(a => a.FirstName, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        </div><br />\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-2\">");
#nullable restore
#line 25 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                             Write(Html.LabelFor(a => a.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            <div class=\"col-lg-10\">");
#nullable restore
#line 26 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                              Write(Html.TextBoxFor(a => a.LastName, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        </div><br />\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-2\">");
#nullable restore
#line 29 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                             Write(Html.LabelFor(a => a.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            <div class=\"col-lg-10\">");
#nullable restore
#line 30 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                              Write(Html.TextBoxFor(a => a.Email, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        </div><br />\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-2\">");
#nullable restore
#line 33 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                             Write(Html.LabelFor(a => a.Password));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            <div class=\"col-lg-10\">");
#nullable restore
#line 34 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
                              Write(Html.TextBoxFor(a => a.Password, new { @class = "form-control", type = "Password" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        </div><br />\r\n        <input type=\"submit\" value=\"Register\" class=\"btn btn-primary\" />\r\n");
#nullable restore
#line 37 "D:\.NET\EMS\Employee Management System\Views\EMS\Register.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RegisterViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
