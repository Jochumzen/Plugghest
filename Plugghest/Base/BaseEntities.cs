using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;


namespace Plugghest.Base
{
    public enum EWhoCanEdit
    {
        Anyone = 1,
        OnlyMe
    }

    public enum ECourseItemType
    {
        Plugg = 0,
        Heading
    }

    // https://github.com/Jochumzen/Plugghest/wiki/Plugg
    [TableName("Pluggs")]
    //setup the primary key for table
    [PrimaryKey("PluggId", AutoIncrement = true)]
    public class Plugg
    {
        public int PluggId { get; set; }
        public string Title { get; set; }
        public string CreatedInCultureCode { get; set; }
        public string YouTubeCode { get; set; }
        public EWhoCanEdit WhoCanEdit { get; set; }
        public int TabId { get; set; }
        public int? SubjectId { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime ModifiedOnDate { get; set; }
        public int ModifiedByUserId { get; set; }

        public Plugg()
        { }
    }

    // https://github.com/Jochumzen/Plugghest/wiki/PluggContent
    [TableName("PluggContents")]
    [PrimaryKey("PluggContentId", AutoIncrement = true)]
    public class PluggContent
    {
        public int PluggContentId { get; set; }
        public int PluggId { get; set; }
        public string CultureCode { get; set; }
        public string Title { get; set; }
        public string HtmlText { get; set; }
        public string LatexText { get; set; }
        public string LatexTextInHtml { get; set; }

        public PluggContent()
        { }
    }

    [TableName("Courses")]
    [PrimaryKey("CourseId", AutoIncrement = true)]
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string CreatedInCultureCode { get; set; }
        public EWhoCanEdit WhoCanEdit { get; set; }
        public int TabId { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime ModifiedOnDate { get; set; }
        public int ModifiedByUserId { get; set; }
        public string Description { get; set; }
    }

    [TableName("CourseItems")]
    [PrimaryKey("CourseItemId", AutoIncrement = true)]
    [Cacheable("CourseItems", CacheItemPriority.Normal, 20)]
    public class CourseItemEntity
    {

        public int CourseItemId { get; set; }
        public int CourseId { get; set; }
        public int ItemId { get; set; }
        public int CIOrder { get; set; }
        public ECourseItemType ItemType { get; set; }
        public int MotherId { get; set; }
    }

    public class CourseItem : CourseItemEntity
    {
        public CourseItem Mother { get; set; }
        public IList<CourseItem> children { get; set; }
        public string label { get; set; }
        public string name { get; set; }
    }

    #region TemporaryDNN

    public class PluggInfoForDNNGrid
    {
        public int PluggId { get; set; }
        public string PluggName { get; set; }
        public string UserName { get; set; }
    }

    public class CourseInfoForDNNGrid
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string UserName { get; set; }
    }
    #endregion

    //class for Create Course tree...


    #region  CourseHeading
    [TableName("CourseHeadings")]
    [PrimaryKey("HeadingID", AutoIncrement = true)]
    public class CourseHeadings
    {
        public int HeadingID { get; set; }

        public string Title { get; set; }
    }
    #endregion
}
