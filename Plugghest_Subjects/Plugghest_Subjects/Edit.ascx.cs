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
using DotNetNuke.Entities.Users;
using Plugghest.Modules.Plugghest_Subjects.Components;
using DotNetNuke.Services.Exceptions;

namespace Plugghest.Modules.Plugghest_Subjects
{
    /// -----------------------------------------------------------------------------
    /// <summary>   
    /// The Edit class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from Plugghest_SubjectsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : Plugghest_SubjectsModuleBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Implement your edit logic for your module
                if (!Page.IsPostBack)
                {

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var t = new SubjectItem();
            var tc = new ItemController();

            if (ItemId > 0)
            {
                t = tc.GetItem(ItemId, ModuleId);
                //t.ItemName = txtName.Text.Trim();
                //t.ItemDescription = txtDescription.Text.Trim();
                //t.LastModifiedByUserId = UserId;
                //t.LastModifiedOnDate = DateTime.Now;
                //t.AssignedUserId = Convert.ToInt32(ddlAssignedUser.SelectedValue);
            }
            else
            {
                t = new SubjectItem()
                {
                    //AssignedUserId = Convert.ToInt32(ddlAssignedUser.SelectedValue),
                    //CreatedByUserId = UserId,
                    //CreatedOnDate = DateTime.Now,
                    //ItemName = txtName.Text.Trim(),
                    //ItemDescription = txtDescription.Text.Trim(),

                };
            }

            //t.LastModifiedOnDate = DateTime.Now;
            //t.LastModifiedByUserId = UserId;
            //t.ModuleId = ModuleId;

            //if (t.ItemId > 0)
            //{
            //    tc.UpdateItem(t);
            //}
            //else
            //{
            //    tc.CreateItem(t);
            //}
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

    }
}