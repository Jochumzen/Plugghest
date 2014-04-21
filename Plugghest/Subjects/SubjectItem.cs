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

namespace Plugghest.Subjects
{
    [TableName("SubjectItems")]
    [PrimaryKey("SubjectID", AutoIncrement = true)]
    public class SubjectItem
    {
        public int SubjectID;
        public string Subject;
        public int? Mother;
        public int Order;

        public SubjectItem()
        { }

        public SubjectItem(int SubjectID, string Subject, int? Mother, int Order)
        {
            // TODO: Complete member initialization
            this.SubjectID = SubjectID;
            this.Subject = Subject;
            this.Mother = Mother;
            this.Order = Order;
        }
    }


    //class for Create Subject tree...
    public class Subject_Tree
    {
        public int SubjectID { get; set; }

        public string Subject { get; set; }

        public int? Mother { get; set; }

        public List<Subject_Tree> children { get; set; }

        public int Order { get; set; }

        public string label { get; set; }

    }

}
