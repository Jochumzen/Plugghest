/*
' Copyright (c) 2014  Plugghest.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Framework;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Helpers;
using Plugghest.Base;
using System.Collections.Generic;
using Plugghest.DNN;

namespace Plugghest.Modules.DisplayPlugg
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from DisplayPluggModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : DisplayPluggModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PageLoadFun();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void PageLoadFun()
        {
            BaseHandler plugghandler = new BaseHandler();

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            //Get current culture
            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;

            Plugg p = plugghandler.GetPlugg(pluggid);
            PluggContent pc = plugghandler.GetPluggContent(pluggid, curlan);

            SetPageText(curlan, p, pc);
        }

        private void SetPageText(string curlan, Plugg p, PluggContent pc)
        {
            if (p.YouTubeCode == null)
            {
                lblYoutube.Text = "[No Video]";
            }
            else
            {
                Youtube myYouTube = new Youtube(p.YouTubeCode);
                if (myYouTube.IsValid)
                    lblYoutube.Text = myYouTube.GetIframeString(curlan.Substring(3, 2));
            }
            hdLatextText.Value = pc.LatexText;
            lblHtmlText.Text = Server.HtmlDecode(pc.HtmlText); ;
            lblLatexTextInHtml.Text = Server.HtmlDecode(pc.LatexTextInHtml);

            //lblLatexText.Text = pc.LatexText;
            if (Request.QueryString["c"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["c"]) && Request.QueryString["c"].ToString().ToLower() == "edit")
            {
                lblTitle.Text = p.Title;
                divTitle.Style.Add("display", "block");
                lblLatexSepretor.Style.Add("display", "block");

            }
            else
            {
                divTitle.Style.Add("display", "none");
                lblLatexSepretor.Style.Add("display", "none");
            }
            //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "init", @" $('#' + '<%=btnSaveTitle.ClientID%>').hide();
            //        $('#' + '<%=txtSaveTitle.ClientID%>').hide();
            //
            //        $('#' + '<%=btnSaveLatext.ClientID%>').hide();
            //        $('#' + '<%=txtLatextText.ClientID%>').hide();
            //
            //        $('#' + '<%=btnSaveHtmltext.ClientID%>').hide();
            //        $('#' + '<%=txtHtmlText.ClientID%>').hide();",true);

            //            System.Web.UI.ScriptManager.RegisterClientScriptBlock(
            //          this,
            //          typeof(System.Web.UI.Page),
            //          "ToggleScript",
            //          @" $('#' + '<%=btnSaveTitle.ClientID%>').hide();
            //        $('#' + '<%=txtSaveTitle.ClientID%>').hide();
            //        $('#' + '<%=btnSaveLatext.ClientID%>').hide();
            //        $('#' + '<%=txtLatextText.ClientID%>').hide();
            //        $('#' + '<%=btnSaveHtmltext.ClientID%>').hide();
            //        $('#' + '<%=txtHtmlText.ClientID%>').hide();",
            //          true);
            //txtHtmlText.Visible = false;

        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        protected void btnSaveTitle_Click(object sender, EventArgs e)
        {
            BaseHandler plugghandler = new BaseHandler();

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;

            Plugg p = plugghandler.GetPlugg(pluggid);
            PluggContent pc = plugghandler.GetPluggContent(pluggid, curlan);

            p.Title = txtSaveTitle.Text;
            plugghandler.UpdatePlugg(p, pc);
            SetPageText(curlan, p, pc);

            btnSaveTitle.Style.Add("display", "none");
            txtSaveTitle.Style.Add("display", "none");

        }

        protected void btnSaveLatext_Click(object sender, EventArgs e)
        {
            BaseHandler plugghandler = new BaseHandler();

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;

            Plugg p = plugghandler.GetPlugg(pluggid);
            PluggContent pc = plugghandler.GetPluggContent(pluggid, curlan);

            pc.LatexText = txtLatextText.Text;
            plugghandler.UpdatePlugg(p, pc);
            SetPageText(curlan, p, pc);
            btnSaveLatext.Style.Add("display", "none");
            txtLatextText.Style.Add("display", "none");
        }

        protected void btnSaveHtmltext_Click(object sender, EventArgs e)
        {
            BaseHandler plugghandler = new BaseHandler();

            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;

            Plugg p = plugghandler.GetPlugg(pluggid);
            PluggContent pc = plugghandler.GetPluggContent(pluggid, curlan);

            pc.HtmlText = txtHtmlText.Text;
            plugghandler.UpdatePlugg(p, pc);
            SetPageText(curlan, p, pc);
            btnSaveHtmltext.Style.Add("display", "none");
            //txtHtmlText.Style.Add("display", "none");
            //txtHtmlText.Visible = false;
        }



    }
}