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
    public class Plugg
    {
        public int PluggId { get; set; }
        public string Title { get; set; }
        public string CreatedInCultureCode { get; set; }
        public int WhoCanEdit { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime ModifiedOnDate { get; set; }
        public int ModifiedByUserId { get; set; }
        public int? Subject { get; set; }

        public Plugg()
        {}

        public Plugg(int PluggId, string Title, string CreatedInCultureCode, int WhoCanEdit, DateTime CreatedOnDate, int CreatedByUserId, DateTime ModifiedOnDate, int ModifiedByUserId, int? nullable)
        {
            // TODO: Complete member initialization
            this.PluggId = PluggId;
            this.Title = Title;
            this.CreatedInCultureCode = CreatedInCultureCode;
            this.WhoCanEdit = WhoCanEdit;
            this.CreatedOnDate = CreatedOnDate;
            this.CreatedByUserId = CreatedByUserId;
            this.ModifiedOnDate = ModifiedOnDate;
            this.ModifiedByUserId = ModifiedByUserId;
            this.Subject = nullable;
        }
    }

    public class PluggInfoForDNNGrid
    {
        public int PluggId{get;set;}
        public string PluggName { get; set; }
        public string UserName { get; set; }
    }



    [TableName("PluggsContent")]
    [PrimaryKey("PluggId", AutoIncrement = false)]
    public class PluggContent
    {

        public int PluggId { get; set; }
        public string CultureCode { get; set; }
        public string YouTubeString { get; set; }
        public string HtmlText { get; set; }
        public string LatexText { get; set; }
        public string LatexTextInHtml { get; set; }

        public PluggContent()
        { }

        public PluggContent(int PluggId, string CultureCode, string YouTubeString, string HtmlText, string LatexText, string LatexTextInHtml)
        {
            // TODO: Complete member initialization
            this.PluggId = PluggId;
            this.CultureCode = CultureCode;
            this.YouTubeString = YouTubeString;
            this.HtmlText = HtmlText;
            this.LatexText = LatexText;
            this.LatexTextInHtml = LatexTextInHtml;
        }

    }

}