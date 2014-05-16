﻿/*
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
using Plugghest.Base2;
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
            int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);
            string curlan = (Page as DotNetNuke.Framework.PageBase).PageCulture.Name;
            BaseHandler plugghandler = new BaseHandler();
            PluggContainer p = new PluggContainer(curlan,pluggid);
            bool IsAuthorized = (p.ThePlugg.WhoCanEdit == EWhoCanEdit.Anyone || p.ThePlugg.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"));

            if (p.CultureCode == p.ThePlugg.CreatedInCultureCode )
            SetPageText(curlan, p);
        }

        private void SetPageText(string curlan, PluggContainer p)
        {
            //if (p.ThePlugg.YouTubeCode == null)
            //{
            //    lblYoutube.Text = "[No Video]";
            //}
            //else
            //{
            //    p.TheVideo = new Youtube(p.ThePlugg.YouTubeCode);
            //    if (p.TheVideo.IsValid)
            //        lblYoutube.Text = p.TheVideo.GetIframeString(curlan.Substring(3, 2));
            //}
            //p.LoadAllText();
            //if (p.TheLatex != null)
            //{
            //    hdLatextText.Value = p.TheLatex.Text;
            //    lblLatexTextInHtml.Text = Server.HtmlDecode(p.TheLatex.HtmlText);                
            //}
            //if (p.TheHtmlText != null)
            //    lblHtmlText.Text = Server.HtmlDecode(p.TheLatex.HtmlText); ;

            //bool IsAuthorized = (p.ThePlugg.WhoCanEdit == EWhoCanEdit.Anyone || p.ThePlugg.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"));
            //string editQS = Request.QueryString["edit"];
            //if (editQS != null && !string.IsNullOrWhiteSpace(editQS) && editQS.ToLower() == "true" && IsAuthorized)
            //{
            //    divTitle.Style.Add("display", "block");
            //    lblLatexSepretor.Style.Add("display", "block");
            //    btnEditLatextText.Style.Add("display", "block");
            //    btnEditHtmlText.Style.Add("display", "block");
            //    btnExitEditMode.Visible = true;
            //    lblTitle.Text = p.TheTitle.Text;
            //}
            //else
            //{
            //    if (IsAuthorized)
            //    {
            //        btnEditPlugg.Visible = true;
            //    }
            //    divTitle.Style.Add("display", "none");
            //    lblLatexSepretor.Style.Add("display", "none");
            //    btnEditLatextText.Style.Add("display", "none");
            //    btnExitEditMode.Visible = false;
            //    btnEditHtmlText.Style.Add("display", "none");
            //}
            //btnCancelLatext.Style.Add("display", "none");
            //btnCancelTitle.Style.Add("display", "none");
            //btnCancelHtmlText.Style.Add("display", "none");

            //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "inithide", "$('.dnnForm.dnnTextEditor.dnnClear').hide();", true);
        }

        protected void btnSaveTitle_Click(object sender, EventArgs e)
        {
            //BaseHandler plugghandler = new BaseHandler();
            //int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            //PluggContainer p = new PluggContainer();
            //p.ThePlugg = plugghandler.GetPlugg(pluggid);
            //string curlan = (Page as PageBase).PageCulture.Name;
            //p.CultureCode = curlan;
            //p.LoadTitle();
            //if (p.TheTitle.Text != txtSaveTitle.Text)
            //{
            //    p.TheTitle.Text = txtSaveTitle.Text;
            //    plugghandler.SavePhText(p.TheTitle);
            //}
            //SetPageText(curlan, p);

            //btnSaveTitle.Style.Add("display", "none");
            //txtSaveTitle.Style.Add("display", "none");
        }

        protected void btnSaveLatext_Click(object sender, EventArgs e)
        {
            //BaseHandler plugghandler = new BaseHandler();
            //int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            //PluggContainer p = new PluggContainer();
            //p.ThePlugg = plugghandler.GetPlugg(pluggid);
            //string curlan = (Page as PageBase).PageCulture.Name;
            //p.CultureCode = curlan;
            //p.LoadLatexText() ;
            //if (p.TheLatex.Text != txtLatextText.Text)
            //{
            //    p.TheTitle.Text = txtLatextText.Text;
            //    plugghandler.SaveLatexText(p.TheLatex);
            //}

            //SetPageText(curlan, p);
            //btnSaveLatext.Style.Add("display", "none");
            //txtLatextText.Style.Add("display", "none");
        }

        protected void btnSaveHtmltext_Click(object sender, EventArgs e)
        {
            //BaseHandler plugghandler = new BaseHandler();
            //int pluggid = Convert.ToInt32(((DotNetNuke.Framework.CDefault)this.Page).Title);

            //PluggContainer p = new PluggContainer();
            //p.ThePlugg = plugghandler.GetPlugg(pluggid);
            //string curlan = (Page as PageBase).PageCulture.Name;
            //p.CultureCode = curlan;
            //p.LoadHtmlText();
            //if (p.TheHtmlText.Text != txtHtmlText.Text)
            //{
            //    p.TheHtmlText.Text = txtHtmlText.Text;
            //    plugghandler.SavePhText(p.TheHtmlText);
            //}

            //SetPageText(curlan, p);
            //btnSaveHtmltext.Style.Add("display", "none");
        }

        protected void btnEditPlugg_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "", "edit=true"));
        }

        protected void btnExitEditMode_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, ""));
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


    }
}