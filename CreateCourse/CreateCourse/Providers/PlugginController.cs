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

namespace Plugghest.Modules.CreatePlugg.Components
{
    class CourseController
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


       
    }
}