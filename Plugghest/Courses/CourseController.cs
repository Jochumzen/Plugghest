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

        //For Insert Records into Coures
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
        public void CreateCoursePlugg(CoursePlugg t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<CoursePlugg>();
                rep.Insert(t);
            }
        }

        //To Get ModuleDefId....
        public int GetModuleDefId(string FriendlyName)
        {
            List<ModuleDef> plug = new List<ModuleDef>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<ModuleDef>(CommandType.TableDirect, "select ModuleDefId from ModuleDefinitions where FriendlyName='" + FriendlyName + "'");
                foreach (var item in rec)
                {
                    plug.Add(new ModuleDef { ModuleDefID = item.ModuleDefID });
                }
            }
            return plug[0].ModuleDefID;

        }

        public string GetPlugTitle(int PluggId)
        {
            string plugtitle = "";
            List<Course> plug = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, "select Title from Pluggs where PluggId=" + PluggId);
                foreach (var item in rec)
                {
                    plug.Add(new Course { Title = item.Title });
                }
                if (plug.Count > 0)
                {
                    plugtitle = plug[0].Title; ;
                }
            }
            return plugtitle;
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


        public List<Course> GetPluggsByCourseID(int CourseID)
        {
            List<Course> plug = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, "select CourseId,Pluggs.PluggId,pluggs.Title as 'PluggName',Orders from CoursePlugg join Pluggs on CoursePlugg.PluggId=Pluggs.PluggId where CourseId=" + CourseID + "order by Orders");
                foreach (var item in rec)
                {
                    plug.Add(new Course { CourseId = item.CourseId, PluggId = item.PluggId });
                }
            }
            return plug;
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


        public List<Course> GetPluggRecords()
        {
            List<Course> plug = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, @"select CourseId,Title as 'CourseName',Courses.CreatedByUserId,Username from Courses join Users on users.UserID=Courses.CreatedByUserId ");

                foreach (var item in rec)
                {
                    plug.Add(new Course { CourseName = item.CourseName, CourseId = item.CourseId, CreatedByUserId = item.CreatedByUserId, UserName = item.UserName });
                }
            }

            return plug;
        }

        public List<Course> GetPluggsByCourseIDForMenu(int CourseID)
        {
            List<Course> plug = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, "select CourseId,Pluggs.PluggId,pluggs.Title as 'PluggName',Orders from CoursePlugg join Pluggs on CoursePlugg.PluggId=Pluggs.PluggId where CourseId=" + CourseID + "order by Orders");
                foreach (var item in rec)
                {
                    plug.Add(new Course { CourseId = item.CourseId, PluggId = item.PluggId, PluggName = item.PluggName, Orders = item.Orders });
                }
            }
            return plug;
        }


        public bool IsCourseIdExist(int CourseID)
        {
            List<IsExist> isexist = new List<IsExist>();
            bool val = false;
            using (IDataContext ctx = DataContext.Instance())
            {
                IEnumerable<IsExist> items = ctx.ExecuteQuery<IsExist>(CommandType.Text, "select COUNT(courseid) as 'isexist' from Courses where CourseId=" + CourseID);

                foreach (var item in items)
                {
                    isexist.Add(new IsExist { isexist = item.isexist });
                }
                if (items != null)
                {
                    val = isexist[0].isexist;
                }
            }
            return val;

        }

        class IsExist
        {
            public Boolean isexist { get; set; }
        }
    }
}