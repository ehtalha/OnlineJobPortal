using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Job_Online_Portal.User
{
    public partial class Usermaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                lbRegisterOrProfile.Text = "Profile";
                lbLoginOrLogout.Text = "Logout";
            }
            else
            {
                lbRegisterOrProfile.Text = "Register";
                lbLoginOrLogout.Text = "Login";
            }
        }

        protected void lbRegisterOrProfile_Click(object sender, EventArgs e)
        {
           // if (lbRegisterOrProfile_Click(sender,e)) { }
           if(lbRegisterOrProfile.Text == "Profile")
            {
                Response.Redirect("Prpfile.aspx");
            }
            else
            {
                Response.Redirect("Register.aspx");

            }

        }

        protected void lbLoginOrLogout_Click(object sender, EventArgs e)
        {
            if (lbLoginOrLogout.Text == "Login")
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("Login.aspx");

            }

        }
    }
}