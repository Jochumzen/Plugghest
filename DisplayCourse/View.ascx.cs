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
using System.Linq;
using System.Web.UI.WebControls;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Base;
using System.Collections.Generic;
using DotNetNuke.Entities.Tabs;

namespace Plugghest.Modules.DisplayCourse
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from DisplayCourseModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : DisplayCourseModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BaseHandler bh = new BaseHandler();

                    string CourseTitle = ((DotNetNuke.Framework.CDefault)this.Page).Title;//get Course from page title
                    CourseTitle = CourseTitle.Replace("C", "");

                    if (!string.IsNullOrEmpty(CourseTitle))
                    {
                        int courseId = Convert.ToInt32(CourseTitle);
                        Course c = bh.GetCourse(courseId);

                        lblDescription.Text = Server.HtmlDecode(c.Description); ;

                        var tc = new TabController();

                        //Todo: Fix "Begin Course" button. Link to first Plugg in course. First Item may be a Heading.
                        //IEnumerable<CourseItem> cps = bh.GetItemsInCourse(c.CourseId);
                        //if (cps != null)
                        //{
                        //    Plugg p = bh.GetPlugg(cps.First().ItemId);
                        //    TabInfo ti = tc.GetTabByName(p.PluggId.ToString() + ": " + p.Title , PortalId);
                        //    if (ti != null)
                        //        LnkBeginCourse.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(ti.TabID, "", "",
                        //            "&c=" + courseId);
                        //    else
                        //    {
                        //        LnkBeginCourse.Text = "Could not find PluggPage for first Plugg";
                        //        LnkBeginCourse.CssClass = "btn btn-warning";
                        //    }
                        //}
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