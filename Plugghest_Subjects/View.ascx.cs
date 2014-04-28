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
using Plugghest.Subjects;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;
using System.IO;


namespace Plugghest.Modules.Plugghest_Subjects
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from Plugghest_SubjectsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : Plugghest_SubjectsModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindTree();
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
        public IList<SubjectTree> BuildTree(IEnumerable<SubjectTree> source)
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
        private void AddChildren(SubjectTree node, IDictionary<int, List<SubjectTree>> source)
        {
            if (source.ContainsKey(node.SubjectID))
            {
                node.children = source[node.SubjectID];
                for (int i = 0; i < node.children.Count; i++)
                    AddChildren(node.children[i], source);
            }
            else
            {
                node.children = new List<SubjectTree>();
            }
        }

        #endregion

        protected void btnSaveSubjects_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = hdnGetJosnResult.Value;
            var person = js.Deserialize<SubjectTree[]>(json).ToList();

            SubjectController objcontroller = new SubjectController();
            var abc = objcontroller.GetSubject_Item();

            var tree = BuildTreeItem(person);
        }

        #region Retrive items of Tree

        #endregion

        //Recursive function for create tree....
        public IList<SubjectTree> BuildTreeItem(IEnumerable<SubjectTree> source)
        {
            var groups = source.GroupBy(i => i.Mother);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                //get child list........
                var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChildrenItem(roots[i], dict);
            }

            return roots;
        }

        //To Add Child
        private void AddChildrenItem(SubjectTree node, IDictionary<int, List<SubjectTree>> source)
        {
            if (source.ContainsKey(node.SubjectID))
            {
                File.AppendAllText("G:\\abc.txt", node.SubjectID + " Mother : Null" + Environment.NewLine);
                node.children = source[node.SubjectID];
                for (int i = 0; i < node.children.Count; i++)
                {
                    AddChildrenItem(node.children[i], source);
                    File.AppendAllText("G:\\abc.txt", node.children[i].SubjectID + " Mother : " + node.children[i].Mother + Environment.NewLine);
                }
            }
            else
            {
                node.children = new List<SubjectTree>();
            }
        }

        protected void btnAddSubject_Click(object sender, EventArgs e)
        {
            if (hdnNodeSubjectId.Value != "")
            {
                SubjectController controll = new SubjectController();
                Subject subitem = new Subject();

                subitem = controll.GetSubject(Convert.ToInt32(hdnNodeSubjectId.Value));

                subitem.Title  = txtAddSubject.Text;

                subitem.SubjectOrder = subitem.SubjectOrder + 1;

                subitem.Mother = subitem.Mother;

                //Add Subject...........
                controll.CreateSubject(subitem);

                //update order for other nodes....
                var Records = controll.GetSubjectFromMother(subitem.Mother, subitem.SubjectOrder);
                if (Records != null)
                {
                    foreach (var item in Records)
                    {
                        subitem.SubjectOrder = subitem.SubjectOrder + 1;
                        controll.UpdateSubjectOrder(item.SubjectID, subitem.SubjectOrder);
                    }
                }

                //Bind Tree...
                BindTree();
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