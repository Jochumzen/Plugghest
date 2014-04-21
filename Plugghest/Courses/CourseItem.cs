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
using System.Collections.Generic;

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

        public string Description { get; set; }

    }

    public class CourseInfoForDNNGrid
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string UserName { get; set; }
    }


    [TableName("CourseItems")]
    [PrimaryKey("CourseItemID", AutoIncrement = true)]
    [Cacheable("CourseItems", CacheItemPriority.Normal, 20)]
    public class CourseItems
    {

        public int CourseItemID { get; set; }

        public int CourseID { get; set; }

        public int ItemID { get; set; }

        public int Order { get; set; }

        public int ItemType { get; set; }

        public int Mother { get; set; }
    }

    //class for Create Course tree...
    public class Course_Tree
    {
        public int CourseId { get; set; }

        public int ItemID { get; set; }

        public int? Mother { get; set; }

        public int CourseItemID { get; set; }

        public List<Course_Tree> children { get; set; }

        public int Order { get; set; }

        public string label { get; set; }
        public string Title { get; set; }

    }

}