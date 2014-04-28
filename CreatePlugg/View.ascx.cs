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
            Plugg p = bh.GetPlugg(pluggId);
            if (p != null)
            {

                if (p.WhoCanEdit == EWhoCanEdit.Anyone || p.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"))
                { //Check that either WhoCanEdit is anyone or the current user is the one who created the Plugg or the current user is a SuperUser.

                    DDLanguage.SelectedValue = p.CreatedInCultureCode;
                    txtTitle.Text = p.Title;

                    if (p.WhoCanEdit == EWhoCanEdit.OnlyMe)
                        rdEditPlug.Items[1].Selected = true;

                    if (p.YouTubeCode != null)
                        txtYouTube.Text = p.YouTubeCode;

                    PluggContent pc = bh.GetPluggContent(p.PluggId, p.CreatedInCultureCode);
                    if (pc != null)
                    {
                        txtHtmlText.Text = pc.HtmlText;
                        txtDescription.Text = pc.LatexText;

                        ViewState["PID"] = pluggId.ToString();
                    }

                    //You cannot update in which language Plugg is created or the YouTube
                    DDLanguage.Enabled = false;
                    txtYouTube.Enabled = false;
                }
                else
                {
                    lblError.Text = "You do not have permissions to edit this Plugg.";
                    HideControl();
                }
            }
            else
            {
                lblError.Text = "No such Plugg";
                HideControl();
            }
        }

        public void BindTree()
        {
            SubjectHandler objsubhandler = new SubjectHandler();
            var subjectlist = objsubhandler.GetSubject_Item();

            var tree = BuildTree(subjectlist);

            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            hdnTreeData.Value = TheSerializer.Serialize(tree);
        }

        #region Create Tree

        //Recursive function for create tree....
        public IList<SubjectTree> BuildTree(IEnumerable<SubjectTree> source)
        {
            var groups = source.GroupBy(i => i.Mother);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildren(roots[i], dict);
            }

            return roots;
        }

        //To Add Child
        private void AddChildren(SubjectTree node, IDictionary<int, List<SubjectTree>> source)
        {
            if (source.ContainsKey(node.SubjectID))
            {
                node.children = source[node.SubjectID];
                for (int i = 0; i < node.children.Count; i++)
                    AddChildren(node.children[i], source);
            }
            else
            {
                node.children = new List<SubjectTree>();
            }
        }

        #endregion

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

            Plugg p;
            if (ViewState["PID"] != null)
            {
                p = UpdatePlugg();
            }
            else
            {
                p = SavePlugg();
            }
            if (p.PluggId != 0) //0 means Error in Update/Save
                Response.Redirect(Globals.NavigateURL(p.TabId));
        }

        private Plugg SavePlugg()
        {
            BaseHandler ph = new BaseHandler();
            Plugg p = new Plugg();
            PluggContent pc = new PluggContent();

            ReadFromControls(p, pc);
            p.CreatedOnDate = DateTime.Now;
            p.CreatedByUserId = UserId;

            try
            {
                ph.CreatePlugg(p, pc);  //Create PluggPage, Plugg and PluggContent (same for every language)
            }
            catch (Exception ex)
            {
                lblError.Text = "Failed to create a Plugg: " + ex.Message;
                Exceptions.LogException(ex);
                HideControl();
                p.PluggId = 0;
            }

            return p;
        }

        private Plugg UpdatePlugg()
        {
            BaseHandler ph = new BaseHandler();
            Plugg p = ph.GetPlugg(Convert.ToInt32(ViewState["PID"].ToString()));  //We know from LoadPlugg that Plugg exists

            //For now, update all PluggContent with content from controls. Fix this when we can deal with translations
            PluggContent pc = new PluggContent();

            ReadFromControls(p, pc);

            try
            {
                ph.UpdatePlugg(p, pc);  //Update PluggPage, Plugg and PluggContent (same for every language)
            }
            catch (Exception ex)
            {
                lblError.Text = "Failed to update Plugg: " + ex.Message;
                Exceptions.LogException(ex);
                HideControl();
                p.PluggId = 0;
            }

            return p;
        }

        protected void ReadFromControls(Plugg p, PluggContent pc)
        {
            p.CreatedInCultureCode = DDLanguage.SelectedValue;
            if (rdEditPlug.Text == "Any registered user")
                p.WhoCanEdit = EWhoCanEdit.Anyone;
            else
                p.WhoCanEdit = EWhoCanEdit.OnlyMe;

            if (!string.IsNullOrEmpty(hdnNodeSubjectId.Value))
                p.SubjectId = Convert.ToInt32(hdnNodeSubjectId.Value);

            Youtube myYouTube = new Youtube(txtYouTube.Text);
            if (myYouTube.IsValid)
                p.YouTubeCode = myYouTube.YouTubeCode;

            p.Title = txtTitle.Text;
            p.ModifiedOnDate = DateTime.Now;
            p.ModifiedByUserId = UserId;

            if (txtHtmlText.Text.Trim() != "")
                pc.HtmlText = txtHtmlText.Text;
            if (txtDescription.Text.Trim() != "")
                pc.LatexText = txtDescription.Text;
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
            LinkButton2.Visible = false;
            LinkButton3.Visible = false;
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