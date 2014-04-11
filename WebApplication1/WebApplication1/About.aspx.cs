using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cusCustom_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            string link = txtCustom.Text;

            if ((link.Length == 11))
            {
                e.IsValid = true;//take 11 character as youtube code...
                return;
            }
            else
            {
                if (link.Contains("www.youtube.com"))
                {
                    //Remove protocol(http://,https://) from url
                    if (link.StartsWith("http://") || link.StartsWith("https://") )
                    {
                        System.Uri uri = new Uri(link); 
                        link = uri.Host + uri.PathAndQuery;
                    }

                    if (link.Length == 35)
                    {
                        //read character
                        string str = link.Substring(0, 24);
                        if (str == "http://www.youtube.com/watch?v=")
                        {
                            e.IsValid = false;
                        }
                        else
                        {
                            e.IsValid = true;
                            return;
                        }
                    }
                }

                e.IsValid = false;
            }
        }
    }
}