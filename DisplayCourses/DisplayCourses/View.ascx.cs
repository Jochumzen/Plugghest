/*
' Copyright (c) 2014  Plugghes.com
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
using Plugghes.Modules.DisplayCourses.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Christoc.Modules.CreatePlugg.Components;
using System.Collections.Generic;

namespace Plugghes.Modules.DisplayCourses
{

    public partial class View : DisplayCoursesModuleBase, IActionable
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CourseController CourceCtrl = new CourseController();

                    string CourseTitle = ((DotNetNuke.Framework.CDefault)this.Page).Title;//get Course from page title
                    CourseTitle = CourseTitle.Replace("C", "");

                    if (!string.IsNullOrEmpty(CourseTitle))
                    {
                        int CourseId = Convert.ToInt32(CourseTitle);

                         List<Course> course = CourceCtrl.GetCourseDetail(CourseId);

                        foreach (var item in course)
                        {
                          lblTitle.Text = item.Title;
                          lblDescription.Text = Server.HtmlDecode(item.Description); ;
                        }

                        List<Course> coursePluggs = CourceCtrl.GetPluggsByCourseID(CourseId);
                        if (coursePluggs.Count > 0)
                        {
                            LnkBeginCourse.NavigateUrl = "/" + (Page as DotNetNuke.Framework.PageBase).PageCulture.Name.ToString().ToLower() + "/" +coursePluggs[0].PluggId + "?c=" + CourseId;
                        }
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
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


    }
}