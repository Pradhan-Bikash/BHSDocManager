using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPWEBAccessControl
{
    public partial class About : Page
    {
        #region Form's control event 
        protected void Page_Load(object sender, EventArgs e)
        {
            lblfrmName.Text = "About";
            Master.UpdateBody("bg-offwhite");
            this.LinkButton1.CssClass = "btn pillButton";
        }

        protected void lblClose_Click(object sender, EventArgs e)
        {
            //Response.Redirect("AppControlPanel.aspx");
        }
        #endregion
    }
}
