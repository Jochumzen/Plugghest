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

namespace Christoc.Modules.CreatePlugg.Components
{
    class CourseController
    {
       
        //Get Course Detail.....
        public List<Course> GetCourseDetail(int CourseId)
        {
            List<Course> crs = new List<Course>();
            using (IDataContext ctx = DataContext.Instance())
            {
                var rec = ctx.ExecuteQuery<Course>(CommandType.TableDirect, @"select Title,description from Courses where CourseId=" + CourseId);

                foreach (var item in rec)
                {
                    crs.Add(new Course { CourseId=item.CourseId, Title = item.Title, Description=item.Description });
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


        
       
    }
}