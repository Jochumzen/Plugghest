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

using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace Plugghest.Courses
{
    //Poko classes as a  Entity Layer.......

    [TableName("Courses")]
    [PrimaryKey("CourseId", AutoIncrement = true)]
    public class Course
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public string CreatedInCultureCode { get; set; }

        public int WhoCanEdit { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime ModifiedOnDate { get; set; }

        public int ModifiedByUserId { get; set; }

        public string  Description { get; set; }

    }

    public class PluggInfoForDNNGrid
    {
        public int CourseId;
        public string CourseName;
        public string UserName;

        public PluggInfoForDNNGrid(int CourseId, string CourseName, string UserName)
        {
            this.CourseId = CourseId;
            this.CourseName = CourseName;
            this.UserName = UserName;
        }
    }


    [TableName("CoursePlugg")]
    [PrimaryKey("PluggId", AutoIncrement = false)]
    public class CoursePlugg
    {
        public int PluggId { get; set; }

        public int CourseId { get; set; }

        public int Orders { get; set; }

        //CourseName add for menu
        public string PluggName { get; set; }

    }

}