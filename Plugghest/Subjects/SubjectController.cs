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


        public void UpdateItem(Subject t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Subject>();
                rep.Update(t);
            }
        }


        public List<SubjectTree> GetSubject_Item()
        {
            List<SubjectTree> objsubjectitem = new List<SubjectTree>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<SubjectTree>(CommandType.TableDirect, "select * from Subjects order by SubjectOrder");
                foreach (var val in rec)
                {
                    objsubjectitem.Add(new SubjectTree { SubjectID = val.SubjectID, Title = val.Title, label = val.Title, Mother = val.Mother, SubjectOrder = val.SubjectOrder });
                }
            }
            return objsubjectitem;
        }


        //insert on subject
        public void CreateSubject(Subject t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Subject>();
                rep.Insert(t);
            }
        }

        public Subject GetSubject(int SubjectId)
        {
            Subject subitem = new Subject();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Subject>(CommandType.TableDirect, "select * from SubjectItems where subjectid=" + SubjectId);
                foreach (var val in rec)
                {
                    subitem.Title = val.Title; subitem.Mother = val.Mother; subitem.SubjectOrder = val.SubjectOrder;
                }
            }

            return subitem;
        }

        public List<Subject> GetSubjectFromMother(int? MotherName, int order)
        {
            List<Subject> sublist = new List<Subject>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Subject>(CommandType.TableDirect, "select * from SubjectItems where Mother=" + MotherName + "AND [ORDER] >=" + order + " order by [order]");
                foreach (var val in rec)
                {
                    sublist.Add(new Subject(val.SubjectID, val.Title, val.Mother, val.SubjectOrder));
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
