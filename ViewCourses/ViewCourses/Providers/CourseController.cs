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


        public Course GetPlugg(int? plugid)
        {
            Course p;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Course>();
                p = rep.GetById(plugid);
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



        
       
    }
}