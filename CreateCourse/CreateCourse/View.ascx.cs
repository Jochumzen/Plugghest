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
using Plugghest.Modules.CreateCourse.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Modules.CreatePlugg.Components;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Entities.Modules.Definitions;

namespace Plugghest.Modules.CreateCourse
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from CreateCourseModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CreateCourseModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //load Culture language....
                LoadLanguage();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


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



        protected void btnCheck_Click(object sender, EventArgs e)
        {
           bool ischeck = checkPlugg();
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                Boolean ischeck=checkPlugg();

                if (ischeck)//check validation
                {
                    int CourseId = InsertCourses();

                    //Add NEW PAGE(TAB).....
                    CreatePage(CourseId.ToString(), CourseId.ToString(), null, null, null);

                    Response.Redirect("/" + (Page as DotNetNuke.Framework.PageBase).PageCulture.Name + "/" +"C"+CourseId + ".aspx");
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }



        protected int InsertCourses()
        {

            var course = new Course();
            course.CourseId = 0;

            var plugc = new CourseController();

            course.Title = txtTitle.Text;

            course.CreatedInCultureCode = DDLanguage.SelectedValue;

            int whocanedit = 2;//For only me
            if (rdEditPlug.Text == "Any registered user")
                whocanedit = 1;

            course.WhoCanEdit = whocanedit;
            course.CreatedOnDate = DateTime.Now;
            course.CreatedByUserId = this.UserId;
            course.ModifiedOnDate = DateTime.Now; ;
            course.ModifiedByUserId = this.UserId;

            course.Description = txtHtmlText.Text;

            plugc.CreateCourse(course); //Create plugg

            //Create Course Plugg
            InsertCoursePlugg(course.CourseId);


            //return CourseId 
            return course.CourseId;

        }



        protected void InsertCoursePlugg(int CourseId)
        {
            var courseplugg = new CoursePlugg();

            var plugc = new CourseController();

            courseplugg.CourseId = CourseId;

            string pluggtext = txtPluggs.Text.Trim();
            if (!string.IsNullOrEmpty(pluggtext))
            {
                string[] itempluggs = pluggtext.Split(',');

                for (int i = 0; i < itempluggs.Length; i++)
                {
                    courseplugg.PluggId = Convert.ToInt32(itempluggs[i].ToString());
                    courseplugg.Orders = i + 1;
                    plugc.CreateCoursePlugg(courseplugg);//create puggin content           
                }
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



        protected Boolean checkPlugg()
        {
            bool ischecked=true;

            lblplugss.Text = "";
            CourseController PC = new CourseController();

            //string pluggtext = "12,56,34,45k,56";
            string pluggtext = txtPluggs.Text.Trim();
            if (!string.IsNullOrEmpty(pluggtext))
            {
                string[] itempluggs = pluggtext.Split(',');

                for (int i = 0; i < itempluggs.Length; i++)
                {
                    int num;
                    bool isNumeric = int.TryParse(itempluggs[i], out num);//check number.....
                    if (isNumeric)
                    {
                        string pluggTitle = PC.GetPlugTitle(num);

                        if (!string.IsNullOrEmpty(pluggTitle))
                        {
                            lblplugss.Text += num + ": " + pluggTitle + "<br />";
                        }
                        else
                        {
                            lblplugss.Text += num + ": Error – Plugg " + num + " does not exist" + "<br />";
                            ischecked = false;
                        }
                    }
                    else
                    {
                        lblplugss.Text = "Pluggs in wrong format.";
                        ischecked = false;
                    }
                }
            }

            return ischecked;
        }



        #region create page and add module.....



        private void CreatePage(string PageName, string PageTitle, string Description, string Keywords, TabPermissionCollection Permissions, bool LoadDefaultModules = true)
        {
            TabController controller = new TabController();
            TabInfo newTab = new DotNetNuke.Entities.Tabs.TabInfo();
            TabPermissionCollection newPermissions = newTab.TabPermissions;
            PermissionProvider permissionProvider = new PermissionProvider();


            // set new page properties
            newTab.PortalID = this.PortalId;

            newTab.TabName = "C" + PageName;

            newTab.Title = "C" + PageTitle;

            newTab.Description = Description;
            newTab.KeyWords = Keywords;
            newTab.IsDeleted = false;
            newTab.IsSuperTab = false;
            newTab.IsVisible = false;//for menu...
            newTab.DisableLink = false;
            newTab.IconFile = "";
            newTab.Url = "";

            //newTab.ParentId = Convert.ToInt32(1);

            // create new page
            int tabId = controller.AddTab(newTab, LoadDefaultModules);

            //Set Tab Permission
            TabPermissionInfo tpi = new TabPermissionInfo();
            tpi.TabID = tabId;
            tpi.PermissionID = 3;//for view
            tpi.PermissionKey = "VIEW";
            tpi.PermissionName = "View Tab";
            tpi.AllowAccess = true;
            tpi.RoleID = 0; //Role ID of administrator         
            newPermissions.Add(tpi, true);
            permissionProvider.SaveTabPermissions(newTab);

            TabPermissionInfo tpi1 = new TabPermissionInfo();
            tpi1.TabID = tabId;
            tpi1.PermissionID = 4;//for edit
            tpi1.PermissionKey = "EDIT";
            tpi1.PermissionName = "Edit Tab";
            tpi1.AllowAccess = true;
            tpi1.RoleID = 0; //Role ID of administrator       
            newPermissions.Add(tpi, true);
            permissionProvider.SaveTabPermissions(newTab);


            //create module on page
            CreateModule(tabId);

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
            CourseController pc = new CourseController();
            int MDId = pc.GetModuleDefId("DisplayCourses");
            moduleInfo.ModuleDefID = MDId;
            ///////////////////////..............

            moduleInfo.CacheTime = moduleDefinitionInfo.DefaultCacheTime;//Default Cache Time is 0
            moduleInfo.InheritViewPermissions = true;//Inherit View Permissions from Tab
            moduleInfo.AllTabs = false;
            moduleInfo.Alignment = "Top";

            ModuleController moduleController = new ModuleController();
            int moduleId = moduleController.AddModule(moduleInfo);
        }


        #endregion
 
    }
}