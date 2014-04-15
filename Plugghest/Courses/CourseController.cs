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

        //For Insert Records into Course
        public Course CreateCourse(Course t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Course>();
                rep.Insert(t);
            }
            return t;
        }

        //For Insert Records into CoursePlugg
        public Boolean CreateCoursePlugg(CoursePlugg t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CoursePlugg>();
                rep.Insert(t);
                return true;
            }
        }

        //Get Course Detail.....
        public List<Course> GetCourseDetail(int CourseId)
        {
            List<Course> crs = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, @"select Title,description from Courses where CourseId=" + CourseId);

                foreach (var item in rec)
                {
                    crs.Add(new Course { CourseId = item.CourseId, Title = item.Title, Description = item.Description });
                }
            }
            return crs;
        }

        //Get Course.....
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

        //Check Course Exist or not
        public bool IsCourseIdExist(int CourseID)
        {
            Boolean isexist;
            using (IDataContext ctx = DataContext.Instance())
            {
                isexist = ctx.ExecuteScalar<Boolean>(CommandType.Text, "select COUNT(courseid) as 'isexist' from Courses where CourseId=" + CourseID);
            }
            return isexist;
        }

    }
}