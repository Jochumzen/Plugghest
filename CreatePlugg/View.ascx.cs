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
using System.Web.UI.WebControls;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using Plugghest.Helpers;
using Plugghest.Subjects;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using Plugghest.Base;
using DotNetNuke.Common;

namespace Plugghest.Modules.CreatePlugg
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from CreatePluggModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CreatePluggModuleBase, IActionable
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            try
            {
                BindTree();
                ViewState["PID"] = null;
                LoadCultureDropDownList();

                string pluggIdStr = Page.Request.QueryString["PID"];
                if (pluggIdStr != null)
                {
                    int pluggId;
                    bool isNum = int.TryParse(pluggIdStr, out pluggId);
                    if (isNum)
                        LoadPlugg(pluggId);
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void LoadPlugg(int pluggId)
        {
            BaseHandler bh = new BaseHandler();
            PluggContainer p = new PluggContainer();
            p.ThePlugg = bh.GetPlugg(pluggId);

            if (p.ThePlugg == null)
            {
                lblError.Text = "No such Plugg";
                HideControl();
                return;
            }

            if (p.ThePlugg.WhoCanEdit == EWhoCanEdit.OnlyMe && p.ThePlugg.CreatedByUserId != this.UserId && !UserInfo.IsInRole("Administator"))
            { //Check that either WhoCanEdit is anyone or the current user is the one who created the Plugg or the current user is a SuperUser.
                lblError.Text = "You do not have permissions to edit this Plugg.";
                HideControl();
                return;
            }

            Localization loc = new Localization();
            if (p.ThePlugg.CreatedInCultureCode != loc.CurrentCulture)
            {
                lblError.Text = "You can only edit the Plugg in the languare it was created. Switch to " + p.ThePlugg.CreatedInCultureCode + " to edit.";
                HideControl();
                return;
            }

            DDLanguage.SelectedValue = p.ThePlugg.CreatedInCultureCode;
            p.CultureCode = p.ThePlugg.CreatedInCultureCode;
            p.LoadAllText();

            txtTitle.Text = p.TheTitle.Text;

            if (p.ThePlugg.WhoCanEdit == EWhoCanEdit.OnlyMe)
                rdEditPlug.Items[1].Selected = true;

            if (p.ThePlugg.YouTubeCode != null)
                txtYouTube.Text = p.ThePlugg.YouTubeCode;

            if(p.TheHtmlText != null)
                txtHtmlText.Text = p.TheHtmlText.Text;

            if (p.TheLatex != null)
                txtDescription.Text = p.TheLatex.Text;

            ViewState["PID"] = pluggId.ToString();

            //You cannot update in which language Plugg is created or the YouTube
            DDLanguage.Enabled = false;
            txtYouTube.Enabled = false;
        }

        public void BindTree()
        {
            SubjectHandler objsubhandler = new SubjectHandler();

            var tree = objsubhandler.GetSubjectsAsTree();
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            hdnTreeData.Value = TheSerializer.Serialize(tree);
        }

        private void LoadCultureDropDownList()
        {
            try
            {
                Localization myLoc = new Localization();
                Localization.LoadCultureDropDownList(DDLanguage, CultureDropDownTypes.NativeName, myLoc.CurrentCulture);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) //check validation
                return;

            PluggContainer p;
            if (ViewState["PID"] != null)
            {
                p = UpdatePlugg();
            }
            else
            {
                p = SavePlugg();
            }
            //if (p.ThePlugg.PluggId != 0) //0 means Error in Update/Save
            //    Response.Redirect(Globals.NavigateURL(p.TabId));
        }

        private PluggContainer SavePlugg()
        {
            BaseHandler ph = new BaseHandler();
            PluggContainer p = new PluggContainer();

            ReadFromControls(p);
            p.ThePlugg.CreatedOnDate = DateTime.Now;
            p.ThePlugg.CreatedByUserId = UserId;

            try
            {
                ph.SavePlugg(p);  //Create PluggPage, Plugg and PluggContent (same for every language)
            }
            catch (Exception ex)
            {
                lblError.Text = "Failed to create a Plugg: " + ex.Message;
                Exceptions.LogException(ex);
                HideControl();
                p.ThePlugg.PluggId = 0;
            }

            return p;
        }

        private PluggContainer UpdatePlugg()
        {
            //BaseHandler ph = new BaseHandler();
            //Plugg p = ph.GetPlugg(Convert.ToInt32(ViewState["PID"].ToString()));  //We know from LoadPlugg that Plugg exists

            ////For now, update all PluggContent with content from controls. Fix this when we can deal with translations
            //PluggContent pc = new PluggContent();

            //ReadFromControls(p, pc);

            //try
            //{
            //    ph.UpdatePlugg(p, pc);  //Update PluggPage, Plugg and PluggContent (same for every language)
            //}
            //catch (Exception ex)
            //{
            //    lblError.Text = "Failed to update Plugg: " + ex.Message;
            //    Exceptions.LogException(ex);
            //    HideControl();
            //    p.PluggId = 0;
            //}

            //return p;
            return new PluggContainer();
        }

        protected void ReadFromControls(PluggContainer p)
        {
            p.ThePlugg.CreatedInCultureCode = DDLanguage.SelectedValue;
            if (rdEditPlug.Text == "Any registered user")
                p.ThePlugg.WhoCanEdit = EWhoCanEdit.Anyone;
            else
                p.ThePlugg.WhoCanEdit = EWhoCanEdit.OnlyMe;

            if (!string.IsNullOrEmpty(hdnNodeSubjectId.Value))
                p.ThePlugg.SubjectId = Convert.ToInt32(hdnNodeSubjectId.Value);

            p.TheVideo = new Youtube(txtYouTube.Text);
            if (p.TheVideo.IsValid)
                p.ThePlugg.YouTubeCode = p.TheVideo.YouTubeCode;

            p.SetTitle(txtTitle.Text);
            p.ThePlugg.ModifiedOnDate = DateTime.Now;
            p.ThePlugg.ModifiedByUserId = UserId;

            if (txtHtmlText.Text.Trim() != "")
                p.SetHtmlText(txtHtmlText.Text);
            if (txtDescription.Text.Trim() != "")
                p.SetLatexText(txtDescription.Text);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void cusCustom_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            Youtube y = new Youtube(txtYouTube.Text);
            e.IsValid = y.IsValid;
        }

        public void HideControl()
        {
            txtTitle.Visible = false;
            txtDescription.Visible = false;
            txtHtmlText.Visible = false;
            txtYouTube.Visible = false;
            lblEditPlug.Visible = false;
            lblLanguage.Visible = false;
            lblTitle.Visible = false;
            lblYouTube.Visible = false;
            DDLanguage.Visible = false;
            rdEditPlug.Visible = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
            lbldescription.Visible = false;
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