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
using Plugghest.Modules.CreatePlugg3.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Base2;
using System.Collections.Generic;

namespace Plugghest.Modules.CreatePlugg3
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from CreatePlugg3ModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CreatePlugg3ModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
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

        protected void btnOk_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtTitle.Text.Trim()=="")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Err", "alert('Please enter title !')", true);
                    return;
                }
                BaseHandler bh = new BaseHandler();
                List<object> cmpData = new List<object>();
                PluggContainer pc = new PluggContainer(new Localization().CurrentCulture);
                pc.ThePlugg.CreatedByUserId = this.UserId;
                pc.ThePlugg.ModifiedByUserId = this.UserId;
                pc.ThePlugg.PluggId = 0;
                pc.SetTitle(txtTitle.Text);

                //pc.SetDescription(txtDescription.Text);

                PHText RichRichText = new PHText();
                RichRichText.ItemType = ETextItemType.PluggComponentRichRichText;
                cmpData.Add(RichRichText);

                Plugghest.Base2.YouTube objYouTube = new Plugghest.Base2.YouTube();
                cmpData.Add(objYouTube);

                bh.SavePlugg(pc, cmpData);

                txtTitle.Text = "";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Success", "alert('New Plugg is created successfully')", true);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", "alert('Error : "+ex.Message+"')", true);
            }
        }
    }
}