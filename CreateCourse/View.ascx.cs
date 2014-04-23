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
using Plugghest.Courses;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Entities.Modules.Definitions;
using Plugghest.DNN;
using Plugghest.Pluggs;

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
                LoadCultureDropDownList();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
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

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            bool ischeck = CheckPlugg();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                Boolean ischeck = CheckPlugg();

                if (ischeck)//check validation
                {
                    Course c = CreateCourses();

                    DNNHelper h = new DNNHelper();
                    h.AddPage("C" + c.CourseId.ToString() + ": " + c.Title, "C" + c.CourseId);

                    Response.Redirect("/" + (Page as DotNetNuke.Framework.PageBase).PageCulture.Name + "/" + "C" + c.CourseId + ".aspx");
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }

        protected Course CreateCourses()
        {
            CourseHandler ch = new CourseHandler();
            Course c = new Course();

            c.Title = txtTitle.Text;
            c.CreatedInCultureCode = DDLanguage.SelectedValue;

            int whocanedit = 2;//For only me
            if (rdEditPlug.Text == "Any registered user")
                whocanedit = 1;

            c.WhoCanEdit = whocanedit;
            c.CreatedOnDate = DateTime.Now;
            c.CreatedByUserId = this.UserId;
            c.ModifiedOnDate = DateTime.Now; ;
            c.ModifiedByUserId = this.UserId;
            c.Description = txtHtmlText.Text;

            ch.CreateCourse(c);

            //Create Course Plugg
            InsertCoursePlugg(c);

            return c;
        }

        protected void InsertCoursePlugg(Course c)
        {
            CourseHandler ch = new CourseHandler();
            CourseItem cp = new CourseItem();

            cp.CourseID = c.CourseId;

            string pluggtext = txtPluggs.Text.Trim();
            if (!string.IsNullOrEmpty(pluggtext))
            {
                string[] itempluggs = pluggtext.Split(',');

                for (int i = 0; i < itempluggs.Length; i++)
                {
                    cp.ItemType = 0;
                    cp.ItemID = Convert.ToInt32(itempluggs[i].ToString());
                    cp.Order = i + 1;
                    cp.ItemType = 0;
                    cp.Mother = 0;
                    ch.CreateCoursePlugg(cp);
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

        protected Boolean CheckPlugg()
        {
            bool ischecked = true;

            lblplugss.Text = "";
            PluggHandler ph = new PluggHandler();

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
                        Plugg p = ph.GetPlugg(num);

                        if (p != null)
                        {
                            lblplugss.Text += num + ": " + p.Title + "<br />";
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