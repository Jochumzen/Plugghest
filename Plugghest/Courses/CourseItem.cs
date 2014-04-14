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
    [TableName("Courses")]
    //setup the primary key for table
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

    public class CourseInDisplayCourse
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PluggId { get; set; }
    }

    public class CourseInViewCourses
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int CreatedByUserId { get; set; }

        public string UserName { get; set; }
    }

    public class CourseInCourseMenu
    {

        public int CourseId { get; set; }

        public string PluggId { get; set; }

        public string PluggName { get; set; }

        public int Orders { get; set; }

    }

    [TableName("CoursePlugg")]
    //setup the primary key for table
    [PrimaryKey("PluggId", AutoIncrement = false)]
    public class CoursePlugg
    {
        public int PluggId { get; set; }

        public int CourseId { get; set; }

        public int Orders { get; set; }
    }


    [TableName("ModuleDefinitions")]
    [PrimaryKey("ModuleDefId", AutoIncrement = true)]
    class ModuleDef
    {
        public int ModuleDefID { get; set; }
        public string FriendlyName { get; set; }
    }
}