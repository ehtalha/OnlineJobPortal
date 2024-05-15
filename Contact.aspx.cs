using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Job_Online_Portal.User
{
    public partial class Contact: System.Web.UI.Page
    {
        // SqlConnection con = new SqlConnection("Data Source=EMDADULHAQUETAL;Initial Catalog=sampleDb;Persist Security Info=True;User ID=sa;Password=admin123;");
        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        protected void Page_load(object sender, EventArgs e)
        {
            if (!IsPostBack) { }

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(str);
                string query = @"Insert into Contact Values (@Name,@Email,@Subject,@Message)";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name.Value.Trim());
                cmd.Parameters.AddWithValue("Email", email.Value.Trim());
                cmd.Parameters.AddWithValue("@Subject", subject.Value.Trim());
                cmd.Parameters.AddWithValue("@Message", message.Value.Trim());
                con.Open();
                // cmd.ExecuteNonQuery();

                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    // in contact.aspx a lebel should be implement named lblMsg
                    lblMsg.Visible = true;
                    lblMsg.Text = "Thanks for reaching out  will look into your query!";
                    lblMsg.CssClass = "alert alert-success";
                    Clear();

                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Can not save record right now,please try letter!";
                    lblMsg.CssClass = "alert alert-warning";

                }



            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
            finally
            {

                con.Close();
            }
        }

        private void Clear()
        {
            // throw new NotImplementedException();
            name.Value = string.Empty;
            email.Value = string.Empty;
            subject.Value = string.Empty;
            message.Value = string.Empty;
        }
    }
}