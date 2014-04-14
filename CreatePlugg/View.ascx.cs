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
            //Show tree();
            BindTree();

            try
            {
                if (!IsPostBack)
                {
                    ViewState["PID"] = null;
                    string CurrentUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl;
                    System.Uri uri = new System.Uri(CurrentUrl);

                    if (!string.IsNullOrEmpty(uri.Query))
                    {
                        string PID = uri.Query;
                        PID = PID.Replace("?PID=", "");
                        int n;
                        bool isNumeric = int.TryParse(PID, out n);//check Pid value is number
                        if (isNumeric)
                        {
                            PluggController plugc = new PluggController();
                            Boolean IsExist = plugc.CheckIsPlugExist(Convert.ToInt32(PID));
                            if (IsExist)
                            {
                                Plugg plugg = new Plugg();
                                plugg = plugc.GetPlug(Convert.ToInt32(PID));
                                if (plugg.WhoCanEdit == 1 || plugg.CreatedByUserId == this.UserId || UserInfo.IsInRole("Administator"))
                                { //Check that either WhoCanEdit is anyone or the current user is the one who created the Plugg or the current user is a SuperUser.

                                    txtTitle.Text = plugg.Title;

                                    if (plugg.WhoCanEdit == 2) //check whocanedit is 2 then check 'only me' otherwise default..
                                        rdEditPlug.Items[1].Selected = true;

                                    PluggContent plugcont = new PluggContent();
                                    plugcont = plugc.GetPlugContent(Convert.ToInt32(PID), plugg.CreatedInCultureCode);
                                    if (plugcont != null)
                                    {
                                        string str = plugcont.YouTubeString; ;
                                        str = str.Replace("<iframe width='640' height='390' src='http://www.youtube.com/embed/", "");
                                        str = str.Replace("<iframe width=640 height=390 src=http://www.youtube.com/embed/", "");
                                        str = str.Replace("<iframe width='640' height='390' src='https://www.youtube.com/embed/", "");
                                        str = str.Replace("<iframe width=640 height=390 src=https://www.youtube.com/embed/", "");

                                        string Code = str.Substring(0, 11);//get 11 keyword character code of youtube.... 

                                        txtYouTube.Text = Code;
                                        txtHtmlText.Text = plugcont.HtmlText;
                                        txtDescription.Text = plugcont.LatexTextInHtml;

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

                    LoadLanguage();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public void BindTree()
        {
            SubjectController objcontroller = new SubjectController();
            var subjectlist = objcontroller.GetSubject_Item();

            var tree = BuildTree(subjectlist);

            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            hdnTreeData.Value = TheSerializer.Serialize(tree);
        }

        #region Create Tree

        //Recursive function for create tree....
        public IList<Subject_Item> BuildTree(IEnumerable<Subject_Item> source)
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
        private void AddChildren(Subject_Item node, IDictionary<int, List<Subject_Item>> source)
        {
            if (source.ContainsKey(node.SubjectID))
            {
                node.children = source[node.SubjectID];
                for (int i = 0; i < node.children.Count; i++)
                    AddChildren(node.children[i], source);
            }
            else
            {
                node.children = new List<Subject_Item>();
            }
        }

        #endregion

        private void LoadLanguage()
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
                if (Page.IsValid)//check validation
                {
                    //For Update....
                    if (ViewState["PID"] != null)
                    {
                        var plugc = new PluggController();
                        Plugg plug = new Plugg();
                        plug.PluggId = Convert.ToInt32(ViewState["PID"].ToString());
                        plug.Title = txtTitle.Text;

                        plug.CreatedInCultureCode = DDLanguage.SelectedValue;

                        int whocanedit = 2;//For only me
                        if (rdEditPlug.Text == "Any registered user")
                            whocanedit = 1;

                        plug.WhoCanEdit = whocanedit;
                        plug.CreatedOnDate = DateTime.Now;
                        plug.CreatedByUserId = this.UserId;
                        plug.ModifiedOnDate = DateTime.Now; ;
                        plug.ModifiedByUserId = this.UserId;
                        plugc.UpdatePlugg(plug);
                        //To get all Language
                        for (int i = 0; i < DDLanguage.Items.Count; i++)
                        {
                            UpdatePlugginContent(plug.PluggId, DDLanguage.Items[i].Value);
                        }

                        Response.Redirect("/" + (Page as DotNetNuke.Framework.PageBase).PageCulture.Name + "/" + Convert.ToInt32(ViewState["PID"].ToString()) + ".aspx");
                    }
                    else //For Insert.....
                    {
                        int Plugid = InsertPluggin();

                        //Add NEW PAGE(TAB).....
                        CreatePage(Plugid.ToString());

                        Response.Redirect("/" + (Page as DotNetNuke.Framework.PageBase).PageCulture.Name + "/" + Plugid + ".aspx");
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected int InsertPluggin()
        {

            var plug = new Plugg();
            plug.PluggId = 0;

            var plugc = new PluggController();

            plug.Title = txtTitle.Text;

            plug.CreatedInCultureCode = DDLanguage.SelectedValue;

            int whocanedit = 2;//For only me
            if (rdEditPlug.Text == "Any registered user")
                whocanedit = 1;

            plug.WhoCanEdit = whocanedit;
            plug.CreatedOnDate = DateTime.Now;
            plug.CreatedByUserId = this.UserId;
            plug.ModifiedOnDate = DateTime.Now; ;
            plug.ModifiedByUserId = this.UserId;
            plugc.CreatePlug(plug); //Create plugg

            //To get all Language
            for (int i = 0; i < DDLanguage.Items.Count; i++)
            {
                //Insert on PluggContent
                InsertPlugginContent(plug.PluggId, DDLanguage.Items[i].Value);
            }

            //return plugid 
            return plug.PluggId;

            //InsertPlugginContent(plug.PluggId);
        }



        protected void InsertPlugginContent(int PluggId, string CultureCode)
        {
            var plugContent = new PluggContent();

            var plugc = new PluggController();

            plugContent.PluggId = PluggId;

            plugContent.CultureCode = CultureCode;


            //youtube.. Add langugage

            //manage culture code
            string cul_code = CultureCode;
            int index = cul_code.IndexOf("-");
            if (index > 0)
                cul_code = cul_code.Substring(0, index);


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

                link = link + "?cc_load_policy=1&amp;cc_lang_pref=" + cul_code;
                //Add Iframe....
                //link = "<iframe width='640' height='390' src='" + link + "' frameborder='0'></iframe>";
                link = "<iframe width=" + 640 + " height=" + 390 + " src=" + link + " frameborder=" + 0 + "></iframe>";
            }

            plugContent.YouTubeString = link;

            plugContent.HtmlText = txtHtmlText.Text;


            //plugContent.LatexText = txtDescription.Text; 
            //plugContent.LatexTextInHtml = txtDescription.Text;

            if (txtDescription.Text.Trim() != "")
            {
                plugContent.LatexText = txtDescription.Text;
                LatexToMathMLConverter myConverter = new LatexToMathMLConverter(txtDescription.Text);
                myConverter.Convert();
                plugContent.LatexTextInHtml = myConverter.HTMLOutput;
            }
            else
            {
                plugContent.LatexText = "";
                plugContent.LatexTextInHtml = "";
            }

            plugc.CreatePlugginContent(plugContent);//create puggin content

        }

        protected void UpdatePlugginContent(int PluggId, string CultureCode)
        {
            var plugContent = new PluggContent();

            var plugc = new PluggController();

            plugContent.PluggId = PluggId;

            plugContent.CultureCode = CultureCode;


            //youtube.. Add langugage

            //manage culture code
            string cul_code = CultureCode;
            int index = cul_code.IndexOf("-");
            if (index > 0)
                cul_code = cul_code.Substring(0, index);


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

                link = link + "?cc_load_policy=1&amp;cc_lang_pref=" + cul_code;

                //Add Iframe....
                //link = "<iframe width='640' height='390' src='" + link + "' frameborder='0'></iframe>";
                link = "<iframe width=" + 640 + " height=" + 390 + " src=" + link + " frameborder=" + 0 + "></iframe> ";

            }

            plugContent.YouTubeString = link;

            plugContent.HtmlText = txtHtmlText.Text;


            //plugContent.LatexText = txtDescription.Text; 
            //plugContent.LatexTextInHtml = txtDescription.Text;

            if (txtDescription.Text.Trim() != "")
            {
                plugContent.LatexText = txtDescription.Text;
                LatexToMathMLConverter myConverter = new LatexToMathMLConverter(txtDescription.Text);
                myConverter.Convert();
                plugContent.LatexTextInHtml = myConverter.HTMLOutput;
            }
            else
            {
                plugContent.LatexText = "";
                plugContent.LatexTextInHtml = "";
            }

            plugc.UpdatePluggContent(plugContent);//update puggin content

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



        #region create page and add module.....



        private void CreatePage(string PageName)
        {
            TabInfo newTab = new TabInfo();

            // set new page properties
            newTab.PortalID = this.PortalId;
            newTab.TabName = PageName;
            newTab.Title = PageName;
            newTab.Description = "";
            newTab.KeyWords = "";
            newTab.IsDeleted = false;
            newTab.IsSuperTab = false;
            newTab.IsVisible = false;//for menu...
            newTab.DisableLink = false;
            newTab.IconFile = "";
            newTab.Url = "";

            //Add permission to the page so that all users can view it
            foreach (PermissionInfo p in PermissionController.GetPermissionsByTab())
            {
                if (p.PermissionKey == "VIEW")
                {
                    TabPermissionInfo tpi = new TabPermissionInfo();
                    tpi.PermissionID = p.PermissionID;
                    tpi.PermissionKey = p.PermissionKey;
                    tpi.PermissionName = p.PermissionName;
                    tpi.AllowAccess = true;
                    tpi.RoleID = -1; //ID of all users
                    newTab.TabPermissions.Add(tpi);
                }
            }

            // create new page
            TabController controller = new TabController();
            int tabId = controller.AddTab(newTab, true);
            DotNetNuke.Common.Utilities.DataCache.ClearModuleCache(tabId);


            //create module on page
            CreateModule(tabId);


            //Clear Cache
            DotNetNuke.Common.Utilities.DataCache.ClearModuleCache(TabId);
            DotNetNuke.Common.Utilities.DataCache.ClearTabsCache(PortalId);
            DotNetNuke.Common.Utilities.DataCache.ClearPortalCache(PortalId, false);
            //////..................................

            //create couuse module on page
            CreateCourseMenuModule(tabId);


            //Clear Cache
            DotNetNuke.Common.Utilities.DataCache.ClearModuleCache(TabId);
            DotNetNuke.Common.Utilities.DataCache.ClearTabsCache(PortalId);
            DotNetNuke.Common.Utilities.DataCache.ClearPortalCache(PortalId, false);
            //////..................................

        }


        public void CreateModule(int tabId)
        {
            ModuleDefinitionInfo moduleDefinitionInfo = new ModuleDefinitionInfo();
            ModuleInfo moduleInfo = new ModuleInfo();
            moduleInfo.PortalID = this.PortalId;
            moduleInfo.TabID = tabId;
            moduleInfo.ModuleOrder = 1;
            moduleInfo.ModuleTitle = "";
            moduleInfo.PaneName = "";


            //Get ModuleDefinationId.............
            PluggController pc = new PluggController();
            int MDId = pc.GetModuleDefId("DisplayPlugg");
            moduleInfo.ModuleDefID = MDId;
            ///////////////////////..............

            moduleInfo.CacheTime = moduleDefinitionInfo.DefaultCacheTime;//Default Cache Time is 0
            moduleInfo.InheritViewPermissions = true;//Inherit View Permissions from Tab
            moduleInfo.AllTabs = false;
            moduleInfo.Alignment = "Top";

            ModuleController moduleController = new ModuleController();
            int moduleId = moduleController.AddModule(moduleInfo);

        }



        public void CreateCourseMenuModule(int tabId)
        {
            ModuleDefinitionInfo moduleDefinitionInfo = new ModuleDefinitionInfo();
            ModuleInfo moduleInfo = new ModuleInfo();
            moduleInfo.PortalID = this.PortalId;
            moduleInfo.TabID = tabId;
            moduleInfo.ModuleOrder = 1;
            moduleInfo.ModuleTitle = "";
            moduleInfo.PaneName = "";


            //Get ModuleDefinationId.............
            PluggController pc = new PluggController();
            int MDId = pc.GetModuleDefId("CourseMenu");
            moduleInfo.ModuleDefID = MDId;
            ///////////////////////..............

            moduleInfo.CacheTime = moduleDefinitionInfo.DefaultCacheTime;//Default Cache Time is 0
            moduleInfo.InheritViewPermissions = true;//Inherit View Permissions from Tab
            moduleInfo.AllTabs = false;
            moduleInfo.Alignment = "Left";

            ModuleController moduleController = new ModuleController();
            int moduleId = moduleController.AddModule(moduleInfo);

        }


        #endregion


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