using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;

namespace Plugghest.DNN
{
    [TableName("TabUrls")]
    //setup the primary key for table
    [PrimaryKey("TabId", AutoIncrement = false)]
    //configure caching using PetaPoco
    [Cacheable("TabUrls", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    //[Scope("ModuleId")]
    public class TabUrl
    {
        ///<summary>
        /// 
        ///</summary>
        public int TabId { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public int SeqNum { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public string Url { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public string QueryString { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public string HttpStatus { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public string CultureCode { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public bool IsSystem { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public int? PortalAliasId { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public int PortalAliasUsage { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public int CreatedByUserID { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public DateTime CreatedOnDate { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public int LastModifiedByUserID { get; set; }

        ///<summary>
        /// 
        ///</summary>
        public DateTime LastModifiedOnDate { get; set; }

    }
}
