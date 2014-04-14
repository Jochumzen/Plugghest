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

namespace Plugghest.Pluggs
{
    [TableName("Pluggs")]
    //setup the primary key for table
    [PrimaryKey("PluggId", AutoIncrement = true)]
    //configure caching using PetaPoco
    //[Cacheable("Items", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    //[Scope("ModuleId")]

    public class Plugg
    {
        ///<summary>
        /// The ID of your object with the name of the ItemName
        ///</summary>
        public int PluggId { get; set; }

        public string Title { get; set; }

        public string CreatedInCultureCode { get; set; }

        public int WhoCanEdit { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime ModifiedOnDate { get; set; }

        public int ModifiedByUserId { get; set; }

        public int? Subject { get; set; }

    }

    //P.J. This class should go.
    public class PluggInView
    {
        public int PluggId { get; set; }

        public string PluggName { get; set; }

        public int CreatedByUserId { get; set; }

        public string UserName { get; set; }
    }

    [TableName("PluggsContent")]
    //setup the primary key for table
    [PrimaryKey("PluggId", AutoIncrement = false)]
    //[PrimaryKey("LanguageId", AutoIncrement = false)]
    //configure caching using PetaPoco
    //[Cacheable("Items", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    //[Scope("ModuleId")]
    public class PluggContent
    {
        ///<summary>
        /// The ID of your object with the name of the ItemName
        ///</summary>
        public int PluggId { get; set; }
        ///<summary>
        /// A string with the name of the ItemName
        ///</summary>
        public string CultureCode { get; set; }

        ///<summary>
        /// A string with the description of the object
        ///</summary>
        public string YouTubeString { get; set; }

        ///<summary>
        /// An integer with the user id of the assigned user for the object
        ///</summary>
        public string HtmlText { get; set; }

        ///<summary>
        /// The ModuleId of where the object was created and gets displayed
        ///</summary>
        public string LatexText { get; set; }


        ///<summary>
        /// An integer for the user id of the user who last updated the object
        ///</summary>
        public string LatexTextInHtml { get; set; }
    }

    //P.J. This class should go.
    public class PluggContentInDisplayPlugg
    {
        ///<summary>
        /// The ID of your object with the name of the ItemName
        ///</summary>
        public int PluggId { get; set; }
        ///<summary>
        /// A string with the name of the ItemName
        ///</summary>
        ///
        public string Title { get; set; }

        public string CultureCode { get; set; }


        ///<summary>
        /// A string with the description of the object
        ///</summary>
        public string YouTubeString { get; set; }

        ///<summary>
        /// An integer with the user id of the assigned user for the object
        ///</summary>
        public string HtmlText { get; set; }

        ///<summary>
        /// The ModuleId of where the object was created and gets displayed
        ///</summary>
        public string LatexText { get; set; }


        ///<summary>
        /// An integer for the user id of the user who last updated the object
        ///</summary>
        public string LatexTextInHtml { get; set; }


    }

    //P.J. This class should go.
    [TableName("ModuleDefinitions")]
    [PrimaryKey("ModuleDefId", AutoIncrement = true)]
    class ModuleDef
    {
        public int ModuleDefID { get; set; }
        public string FriendlyName { get; set; }
    }
}