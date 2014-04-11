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

namespace Christoc.Modules.CreatePlugg.Components
{
    class PluggController
    {

        public List<Course> GetPluggsByCourseID(int CourseID)
        {
            List<Course> plug = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, "select CourseId,Pluggs.PluggId,pluggs.Title as 'PluggName',Orders from CoursePlugg join Pluggs on CoursePlugg.PluggId=Pluggs.PluggId where CourseId=" + CourseID + "order by Orders");
                foreach (var item in rec)
                {
                    plug.Add(new Course { CourseId=item.CourseId,PluggId=item.PluggId,PluggName=item.PluggName,Orders=item.Orders });
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