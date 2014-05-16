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
using Plugghest.Base2;
using System.Collections.Generic;
using Plugghest.DNN;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Text;

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
        //private void AddPlug_Click(object sender, EventArgs e)
        //{ 
        
        //}

        protected void AddPlug_Click(Object sender, EventArgs e)
        {
          
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


            List<PluggComponent> comps = p.GetComponentList();
            BaseHandler bh = new BaseHandler();

            string ddl = "";
            foreach (string name in Enum.GetNames(typeof(EComponentType)))
            {
                if (name != "NotSet")
                {
                   string dl = "<option value=" + name + " >" + name + "</option>";
                   ddl = ddl + dl;
                   
                }
            }

            
            foreach (PluggComponent comp in comps)
            {
                Label dynamicLabel = new Label();
                int i = 0;
                switch (comp.ComponentType)
                {
                
                       
                    case EComponentType.Label:
                        PHText lbl = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentLabel);

                        string yourHTMLstring0 = "";
                         if (lbl == null)
                             yourHTMLstring0 = "<div  id='Label" + i + "' class='Main'>Label:</br><select id='ddlLbl"+i+"'>";
                        else
                             yourHTMLstring0 = "<div id='Label' " + i + " class='Main'> Label:" + lbl.Text + "</br><select>";
                           
                        yourHTMLstring0 = yourHTMLstring0 + ddl;
                        string n1 = "</select> <input type='button' value='Add' id='btladd' OnClick='AddPlug_Click()' /></div>";
                        yourHTMLstring0 = yourHTMLstring0 + n1;

                        Button btnToadd = new Button();
                        object sender = new object();
                        EventArgs e = new EventArgs();
                        btnToadd.Click += new System.EventHandler(btnExitEditMode_Click);;
                        divTitle.Controls.Add(btnToadd);
                     
                        break;
                    case EComponentType.RichText:
                        PHText rt = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentRichText);
                        //Handle rich text

                        string yourHTMLstring = "";
                        if (rt == null)
                            yourHTMLstring = "<div  id='RichText" + i + "' class='Main'>RichText:</br><select>";
                        else
                            yourHTMLstring = "<div id='RichText' " + i + " class='Main'> RichText:" + rt.Text + "</br><select>";
                            
                        yourHTMLstring = yourHTMLstring + ddl;

                        string n2 = "</select> <button id='btladd' class='cls' OnClick='AddPlug_Click()'  >Add</button></div>";
                        yourHTMLstring = yourHTMLstring + n2;
                        
                        divTitle.Controls.Add(new LiteralControl(yourHTMLstring));

                        break;
                    case EComponentType.RichRichText:
                        PHText rrt = bh.GetCurrentVersionText(curlan, comp.PluggComponentId, ETextItemType.PluggComponentRichRichText);
                        //Handle richrich text
                        string yourHTMLstring1 = "";
                        if (rrt == null)
                            yourHTMLstring1 = "<div  id='RichRichText" + i + "' class='Main'>RichRichText:</br><select>";
                        else
                            yourHTMLstring1 = "<div id='RichRichText' " + i + " class='Main'> RichRichText:" + rrt.Text + "</br><select>";
                                              
                        yourHTMLstring1 = yourHTMLstring1 + ddl;
                      
                        string n3 = "</select> <button id='btladd' class='cls'  >Add</button></div>";
                        yourHTMLstring1 = yourHTMLstring1 + n3;
                        
                        divTitle.Controls.Add(new LiteralControl(yourHTMLstring1));
                        break;
                    case EComponentType.Latex:
                        PHLatex lt = bh.GetCurrentVersionLatexText(curlan, comp.PluggComponentId, ELatexItemType.PluggComponentLatex);
                        //Handle Latex text
                        string yourHTMLstring2 = "";
                        if (lt == null)
                            yourHTMLstring2 = "<div  id='Latex" + i + "' class='Main'>Latex:</br><select>";   
                        else
                            yourHTMLstring2 = "<div id='Latex' " + i + " class='Main'> Latex:" + lt.Text + "</br><select>";
                        yourHTMLstring2 = yourHTMLstring2 + ddl;                      
                        string n4 = "</select> <button id='btladd' class='cls'  >Add</button></div>";
                        yourHTMLstring2 = yourHTMLstring2 + n4;

                        divTitle.Controls.Add(new LiteralControl(yourHTMLstring2));
                        break;


                    case EComponentType.YouTube:
                        YouTube yt = bh.GetYouTubeByComponentId(comp.PluggComponentId);
                        //Handle YouTube. The iframe is in:
                        //public string GetIframeString(string p.CultureCode) 
                        break;
                       
                }
                i++;
            }


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
            //p.LoadLatexText();
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