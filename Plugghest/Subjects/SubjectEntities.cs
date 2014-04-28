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
    [TableName("Subjects")]
    [PrimaryKey("SubjectID", AutoIncrement = true)]
    public class Subject
    {
        public int SubjectID;
        public string Title;
        public int? Mother;
        public int SubjectOrder;

        public Subject()
        { }

        public Subject(int SubjectID, string Title, int? Mother, int Order)
        {
            // TODO: Complete member initialization
            this.SubjectID = SubjectID;
            this.Title = Title;
            this.Mother = Mother;
            this.SubjectOrder = Order;
        }
    }

    //class for Create Subject tree...
    public class SubjectTree
    {
        public int SubjectID { get; set; }

        public string Title { get; set; }

        public int? Mother { get; set; }

        public List<SubjectTree> children { get; set; }

        public int SubjectOrder { get; set; }

        public string label { get; set; }

    }

}
