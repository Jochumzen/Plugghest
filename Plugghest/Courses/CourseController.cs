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

        public void CreateCoursePlugg(CoursePlugg t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CoursePlugg>();
                rep.Insert(t);
            }
        }

        public IEnumerable<CoursePlugg> GetCoursePluggsForCourse(int courseId)
        {
            IEnumerable<CoursePlugg> cps;
            using (IDataContext context = DataContext.Instance())
            {
                var repository = context.GetRepository<CoursePlugg>();
                cps = repository.Find("WHERE CourseID = @0 ORDER BY 'ORDERS'", courseId);
            }
            return cps;
        }

        public IEnumerable<CoursePlugg> GetCoursePlugg(int courseId, int pluggId)
        {
            IEnumerable<CoursePlugg> cp;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CoursePlugg>();
                cp = rep.Find("WHERE CourseId = @0 AND PluggId = @1", courseId,pluggId );
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
                    cs.Add(new CourseInfoForDNNGrid() {CourseId = item.CourseId, CourseName = item.CourseName , UserName = item.UserName });
                }
            }

            return cs;
        }
    }
}