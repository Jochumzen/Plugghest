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
using DotNetNuke.UI.Utilities;
using Plugghest.Subjects;
using Plugghest.DNN;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using Plugghest.Pluggs;
using System.Web;
using Latex2MathML;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Entities.Modules.Definitions;

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
                //Show tree();
                BindTree();

                ViewState["PID"] = null;
                string CurrentUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl;
                System.Uri uri = new System.Uri(CurrentUrl);
                if (string.IsNullOrEmpty(uri.Query))
                    LoadCultureDropDownList();
                else
                    LoadPlugg(uri);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void LoadPlugg(System.Uri uri)
        {

            string PID = uri.Query;
            PID = PID.Replace("?PID=", "");
            int PluggIDToEdit;
            bool isNumeric = int.TryParse(PID, out PluggIDToEdit);//check Pid value is number
            if (isNumeric)
            {
                PluggHandler ph = new PluggHandler();
                Plugg p = ph.GetPlugg(PluggIDToEdit);
                if (p != null)
                {

                    if (p.WhoCanEdit == 1 || p.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"))
                    { //Check that either WhoCanEdit is anyone or the current user is the one who created the Plugg or the current user is a SuperUser.

                        txtTitle.Text = p.Title;

                        if (p.WhoCanEdit == 2) //check whocanedit is 2 then check 'only me' otherwise default..
                            rdEditPlug.Items[1].Selected = true;

                        PluggContent pc = ph.GetPluggContent(p.PluggId, p.CreatedInCultureCode);
                        if (pc != null)
                        {
                            string str = pc.YouTubeString;
                            if (str != null && str != "")
                            {
                                str = str.Replace("<iframe width='640' height='390' src='http://www.youtube.com/embed/", "");
                                str = str.Replace("<iframe width=640 height=390 src=http://www.youtube.com/embed/", "");
                                str = str.Replace("<iframe width='640' height='390' src='https://www.youtube.com/embed/", "");
                                str = str.Replace("<iframe width=640 height=390 src=https://www.youtube.com/embed/", "");

                                string Code = str.Substring(0, 11);//get 11 keyword character code of youtube.... 

                                txtYouTube.Text = Code;
                            }
                            txtHtmlText.Text = pc.HtmlText;
                            txtDescription.Text = pc.LatexTextInHtml;

                            ViewState["PID"] = PID;
                        }
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
        public IList<Subject_Tree> BuildTree(IEnumerable<Subject_Tree> source)
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
        private void AddChildren(Subject_Tree node, IDictionary<int, List<Subject_Tree>> source)
        {
            if (source.ContainsKey(node.SubjectID))
            {
                node.children = source[node.SubjectID];
                for (int i = 0; i < node.children.Count; i++)
                    AddChildren(node.children[i], source);
            }
            else
            {
                node.children = new List<Subject_Tree>();
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
            try
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
                Response.Redirect("/" + (Page as DotNetNuke.Framework.PageBase).PageCulture.Name + "/" + p.PluggId + ".aspx");
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private Plugg SavePlugg()
        {
            PluggHandler ph = new PluggHandler();

            int whocanedit = 2;//For only me
            if (rdEditPlug.Text == "Any registered user")
                whocanedit = 1;

            //if (!string.IsNullOrEmpty(hdnNodeSubjectId.Value))
            //    Subject = Convert.ToInt32(hdnNodeSubjectId.Value);

            Plugg p = new Plugg();
            p.Title = txtTitle.Text;
            p.CreatedInCultureCode = DDLanguage.SelectedValue;
            p.WhoCanEdit = whocanedit;
            p.CreatedOnDate = DateTime.Now;
            p.CreatedByUserId = UserId;
            p.ModifiedOnDate = DateTime.Now;
            p.ModifiedByUserId = UserId;
            p.Subject = 0;
            ph.CreatePlugg(p);

            //Save same Pluggcontent in all languages 
            for (int i = 0; i < DDLanguage.Items.Count; i++)
            {
                SavePluggContent(p , DDLanguage.Items[i].Value);
            }

            DNNHelper d = new DNNHelper();
            d.AddPage(PortalId, p.PluggId.ToString());

            return p;
        }

        private Plugg UpdatePlugg()
        {
            PluggHandler ph = new PluggHandler();
            Plugg p = ph.GetPlugg(Convert.ToInt32(ViewState["PID"].ToString()));
            
            p.Title = txtTitle.Text;

            int whocanedit = 2;//For only me
            if (rdEditPlug.Text == "Any registered user")
                whocanedit = 1;
            p.WhoCanEdit = whocanedit;
            p.ModifiedOnDate = DateTime.Now;
            p.ModifiedByUserId = UserId;

            ph.UpdatePlugg(p);
            //To get all Language
            for (int i = 0; i < DDLanguage.Items.Count; i++)
            {
                UpdatePluggContent(p, DDLanguage.Items[i].Value);
            }

            return p;
        }

        protected void SavePluggContent(Plugg p, string cultureCode)
        {
            PluggHandler ph = new PluggHandler();
            PluggContent pc = new PluggContent();
            ReadPluggContent(pc, p, cultureCode);

            ph.CreatePluggContent(pc); 
        }

        protected void UpdatePluggContent(Plugg p, string cultureCode)
        {
            PluggHandler ph = new PluggHandler();
            PluggContent pc = ph.GetPluggContent(p.PluggId, cultureCode);
            ReadPluggContent(pc, p, cultureCode);

            ph.UpdatePluggContent(pc); 
        }

        protected void ReadPluggContent(PluggContent pc, Plugg p, string cultureCode)
        {
            //manage culture code: CultureCode = en-us => CultureCodePart = en
            string cultureCodePart = cultureCode;
            int index = cultureCodePart.IndexOf("-");
            if (index > 0)
                cultureCodePart = cultureCodePart.Substring(0, index);
            pc.CultureCode = cultureCode;

            string link = txtYouTube.Text.Trim();
            if (!string.IsNullOrEmpty(link))
            {

                if (link.Length == 11)
                {
                    link = "http://www.youtube.com/embed/" + link;
                }
                else
                {
                    //replace watch?v to embed
                    link = link.Replace("watch?v=", "embed/");
                }

                link = link + "?cc_load_policy=1&amp;cc_lang_pref=" + cultureCodePart;
                //Add Iframe....
                //link = "<iframe width='640' height='390' src='" + link + "' frameborder='0'></iframe>";
                link = "<iframe width=" + 640 + " height=" + 390 + " src=" + link + " frameborder=" + 0 + "></iframe>";
            }
            pc.YouTubeString = link;

            pc.PluggId = p.PluggId;
            pc.HtmlText = txtHtmlText.Text;
            if (txtDescription.Text.Trim() != "")
            {
                pc.LatexText = txtDescription.Text;
                LatexToMathMLConverter myConverter = new LatexToMathMLConverter(txtDescription.Text);
                myConverter.Convert();
                pc.LatexTextInHtml = myConverter.HTMLOutput;
            }
            else
            {
                pc.LatexText = "";
                pc.LatexTextInHtml = "";
            } 
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
            string link = txtYouTube.Text.Trim();

            if ((link.Length == 11))
            {
                e.IsValid = true;//take 11 character as youtube code...
                return;
            }
            else
            {
                if (link.Contains("www.youtube.com"))
                {
                    //Remove protocol(http://,https://) from url
                    if (link.StartsWith("http://") || link.StartsWith("https://"))
                    {
                        System.Uri uri = new Uri(link);
                        link = uri.Host + uri.PathAndQuery;
                    }

                    if (link.Length == 35)
                    {
                        //read character
                        string str = link.Substring(0, 24);
                        if (str == "http://www.youtube.com/watch?v=")
                        {
                            e.IsValid = false;
                        }
                        else
                        {
                            e.IsValid = true;
                            return;
                        }
                    }
                }

                e.IsValid = false;
            }
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