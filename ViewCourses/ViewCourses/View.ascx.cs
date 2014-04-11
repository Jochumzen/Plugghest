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
using Plugghest.Modules.ViewCourses.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Telerik.Web.UI;
using Plugghest.Modules.CreatePlugg.Components;

namespace Plugghest.Modules.ViewCourses
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ViewCoursesModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ViewCoursesModuleBase, IActionable
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bindRadGrid(); ;
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
            
        }



        public void bindRadGrid()
        {
            CourseController pc = new CourseController();
            RadGrid_ViewPlugg.DataSource = pc.GetPluggRecords();
        }



        protected void RadGrid_ViewPlugg_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            bindRadGrid();
        }

        protected void RadGrid_ViewPlugg_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            bindRadGrid();
        }

        protected void RadGrid_ViewPlugg_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            bindRadGrid();
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