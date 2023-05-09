using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPWEBAccessControl
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblTitle.Text = BPWEBAccessControl.Global.TitleText();
        }

        public void UpdateBody(string strhtml)
        {
            //Default top nav color 
            var panHeadbgColor = "background-color:" + "#14274E";
            //Default footer bg color and setting 
            var panfooterbgColor = "background-color: none; padding-top: 10px; padding-bottom: 10px;";
            //Default footer class setting 
            var panfooterAttr = "fixedfooter w3-center w3-round-medium";
            var panParaTextAttr = "w3-tiny w3-opacity-min";

            // font color on below footer bar - default - 01-DEC-2021
            //linInputHint.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            //lblcontact.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            var centerPTextColor = "color:#CCCCCC;";

            // Change background 
            this.bdOfSiteMaster.Attributes.Add("class", strhtml);

            if (strhtml == "bg")
            {
                panHeadbgColor = "background-color:" + "#4A5133";
                panfooterbgColor = "background-color: #4A5133; padding-top: 0px; padding-bottom: 0px;";

                panfooterAttr = "fixedfooter w3-center w3-round-medium";
                panParaTextAttr = "w3-tiny w3-opacity-min";
                // font color on below footer bar - default - 01 - DEC - 2021
                //linInputHint.ForeColor = System.Drawing.ColorTranslator.FromHtml("#63A1F5");
                //lblcontact.ForeColor = System.Drawing.ColorTranslator.FromHtml("#63A1F5");
                centerPTextColor = "color:#141414;";
            }
            if (strhtml == "bgDefault")
            {
                panHeadbgColor = "background-color:" + "#23374D";
                panfooterbgColor = "background-color: #EEEEEE; padding-top: 0px; padding-bottom: 0px;";

                panfooterAttr = "fixedfooter w3-center w3-round-medium";
                panParaTextAttr = "w3-tiny w3-opacity-min";
                // font color on below footer bar - default - 01 - DEC - 2021
                //linInputHint.ForeColor = System.Drawing.ColorTranslator.FromHtml("#63A1F5");
                //lblcontact.ForeColor = System.Drawing.ColorTranslator.FromHtml("#63A1F5");
                centerPTextColor = "color:#3E3D32;";
            }
            if (strhtml == "bgLogIn")
            {
                panHeadbgColor = "background-color:" + "#23374D"; //#1573F0 (blue glow) //#1089FF
                panfooterbgColor = "background-color: #F2F2F2; padding-top: 10px; padding-bottom: 10px;";
                updatefooterbase.Visible = false;
            }
            if (strhtml == "bg-offwhite") // about - contcat case
            {
                panHead.Visible = false;
                updatefooterbase.Visible = false;
            }

            //Final update... 
                panHead.Attributes.Add("style", panHeadbgColor);
            updatefooterbase.Attributes.Add("style", panfooterbgColor);
            updatefooterbase.Attributes.Add("class", panfooterAttr);
            pCenterText.Attributes.Add("style", centerPTextColor);
            pCenterText.Attributes.Add("class", panParaTextAttr);
        }

    }
}