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
using System.Web;
using System.Collections.Generic;
using Plugghest.Courses;
using Plugghest.Pluggs;

namespace Plugghest.Modules.CourseMenu
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from CourseMenuModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CourseMenuModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetMenu();
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public void GetMenu()
        {

            string CurrentUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl;
            System.Uri uri = new System.Uri(CurrentUrl);
            string uriQuery = uri.Query;
            string str = uri.LocalPath;

            //To Get PluggID......from the url : url is always in this form http://xxx.xxx/xxxx/PluggID?c=CourseID
            string PluggId = str.Substring(str.IndexOf('/') + 7);
            PluggId = PluggId.Replace(".aspx", "");


            if (!string.IsNullOrEmpty(uriQuery)) //check query string i.e. course exist or not...
            {
                string CourseId = uriQuery.Replace("?c=", "");

                CourseHandler ch = new CourseHandler();
                Course c = ch.GetCourse(Convert.ToInt32(CourseId));

                //if course exist in the database...
                if (c != null)
                {
                    PluggHandler ph = new PluggHandler();
                    IEnumerable<CoursePlugg> cps = ch.GetCoursePluggsForCourse(Convert.ToInt32(CourseId));

                    if (cps != null)
                    {
                        foreach (CoursePlugg cp in cps)
                        {
                            Plugg p = ph.GetPlugg(cp.PluggId);
                            Menu_Pluggs.Items.Add(new MenuItem(p.PluggId.ToString() + ": " + p.Title.ToString(), "", "", "/" + (Page as DotNetNuke.Framework.PageBase).PageCulture.Name.ToString().ToLower() + "/" + p.PluggId + "?c=" + CourseId));


                            if (PluggId == p.PluggId.ToString())
                            {
                                int index = cp.Orders - 1;
                                Menu_Pluggs.Items[index].Selected = true; //active order in menu
                            }
                        }
                    }
                    else
                    {
                        lbltest.Text = "This Plugg is not in this course";
                    }
                }
                else
                {
                    lbltest.Text = "No such course";
                }
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