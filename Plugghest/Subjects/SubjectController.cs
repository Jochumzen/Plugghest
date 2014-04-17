/*
' Copyright (c) 2014 Plugghest.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/
using System.Collections.Generic;
using DotNetNuke.Data;
using System.Data;
using DotNetNuke.Entities.Users;

namespace Plugghest.Subjects
{
    public class SubjectController
    {


        public void UpdateItem(SubjectItem t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<SubjectItem>();
                rep.Update(t);
            }
        }


        public List<Subject_Tree> GetSubject_Item()
        {
            List<Subject_Tree> objsubjectitem = new List<Subject_Tree>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Subject_Tree>(CommandType.TableDirect, "select * from SubjectItems order by [Order]");
                foreach (var val in rec)
                {
                    objsubjectitem.Add(new Subject_Tree { SubjectID = val.SubjectID, Subject = val.Subject, label = val.Subject, Mother = val.Mother, Order = val.Order });
                }
            }
            return objsubjectitem;
        }


        //insert on subject
        public void CreateSubject(SubjectItem t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<SubjectItem>();
                rep.Insert(t);
            }
        }

        public SubjectItem GetSubject(int SubjectId)
        {
            SubjectItem subitem = new SubjectItem();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<SubjectItem>(CommandType.TableDirect, "select * from SubjectItems where subjectid=" + SubjectId);
                foreach (var val in rec)
                {
                    subitem.Subject = val.Subject; subitem.Mother = val.Mother; subitem.Order = val.Order;
                }
            }

            return subitem;
        }

        public List<SubjectItem> GetSubjectFromMother(int? MotherName, int order)
        {
            List<SubjectItem> sublist = new List<SubjectItem>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<SubjectItem>(CommandType.TableDirect, "select * from SubjectItems where Mother=" + MotherName + "AND [ORDER] >=" + order + " order by [order]");
                foreach (var val in rec)
                {
                    sublist.Add(new SubjectItem(val.SubjectID,val.Subject,val.Mother,val.Order));
                }
            }
            return sublist;
        }

        public void UpdateSubjectOrder(int SubjectId, int Order)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(CommandType.Text, "update SubjectItems set [Order]=" + Order + " where Subjectid=" + SubjectId);
            }
        }

    }
}
