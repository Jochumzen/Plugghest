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
using System;

namespace Plugghest.Courses
{
    public class CourseController
    {

        public void CreateCourse(Course t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Course>();
                rep.Insert(t);
            }
        }

        public Course GetCourse(int? courseId)
        {
            Course p;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Course>();
                p = rep.GetById(courseId);
            }
            return p;
        }

        //CoursePluggs

        public void CreateCoursePlugg(CourseItem t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CourseItem>();
                rep.Insert(t);
            }
        }

        public IEnumerable<CourseItem> GetCoursePluggsForCourse(int CourseID)
        {
            IEnumerable<CourseItem> cps;
            using (IDataContext context = DataContext.Instance())
            {
                var repository = context.GetRepository<CourseItem>();
                cps = repository.Find("WHERE CourseID = @0 ORDER BY [ORDER]", CourseID);
            }
            return cps;
        }

        public IEnumerable<CourseItem> GetCoursePlugg(int courseId, int pluggId)
        {
            IEnumerable<CourseItem> cp;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CourseItem>();
                cp = rep.Find("WHERE CourseId = @0 AND PluggId = @1", courseId, pluggId);
            }
            return cp;
        }

        //CourseForDNN

        public List<CourseInfoForDNNGrid> GetCoursesForDNN()
        {
            List<CourseInfoForDNNGrid> cs = new List<CourseInfoForDNNGrid>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<CourseInfoForDNNGrid>(CommandType.TableDirect, @"select CourseId, Title as CourseName, Username from courses join Users on Users.UserID=Courses.CreatedByUserId ");

                foreach (var item in rec)
                {
                    cs.Add(new CourseInfoForDNNGrid() { CourseId = item.CourseId, CourseName = item.CourseName, UserName = item.UserName });
                }
            }

            return cs;
        }


        public List<Course_Tree> GetCourseItems(int CourseID)
        {
            List<Course_Tree> objsubjectitem = new List<Course_Tree>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course_Tree>(CommandType.TableDirect, @"select Pluggs.Title,Mother,ItemType,CourseItemID, [Order] from CourseItems join Pluggs on Pluggs.PluggId=CourseItems.Itemid  where CourseID=" + CourseID + " and ItemType = 0 union select CourseHeadings.Title,Mother,ItemType,CourseItemID,[Order] from CourseItems join CourseHeadings on CourseHeadings.HeadingID = CourseItems.ItemID where CourseID=" + CourseID + " and ItemType = 1 order by [Order]");

                foreach (var val in rec)
                {
                    objsubjectitem.Add(new Course_Tree { label = val.Title, Title = val.Title, Mother = val.Mother, Order = val.Order, CourseItemID = val.CourseItemID });
                }
            }
            return objsubjectitem;
        }

    }
}