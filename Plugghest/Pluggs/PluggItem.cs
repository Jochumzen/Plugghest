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
        public int PluggId;
        public string Title;
        public string CreatedInCultureCode;
        public int WhoCanEdit;
        public DateTime CreatedOnDate;
        public int CreatedByUserId;
        public DateTime ModifiedOnDate;
        public int ModifiedByUserId;
        public int? Subject;

        //For now show UserName on Grid
        public string UserName;

        public Plugg(int PluggId, string Title, string CreatedInCultureCode, int WhoCanEdit, DateTime CreatedOnDate, int CreatedByUserId, DateTime ModifiedOnDate, int ModifiedByUserId, int? nullable, string UserName=null)
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
            this.UserName = UserName;
        }

    }

    [TableName("PluggsContent")]
    [PrimaryKey("PluggId", AutoIncrement = false)]
    public class PluggContent
    {

        public int PluggId;
        public string CultureCode;
        public string YouTubeString;
        public string HtmlText;
        public string LatexText;
        public string LatexTextInHtml;


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