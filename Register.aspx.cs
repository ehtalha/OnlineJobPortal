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
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(str);
                string query = @"Insert into [user](UserName,Password,Name,Address,Mobile,Email,Country) Values 
                                                (@UserName,@Password,@Name,@Address,@Mobile,@Email,@Country)";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("Password", txtConfirmPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                cmd.Parameters.AddWithValue("Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                con.Open();
                // cmd.ExecuteNonQuery();

                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    // in contact.aspx a lebel should be implement named lblMsg
                    lblMsg.Visible = true;
                    lblMsg.Text = "Register Successfull";
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
            catch (SqlException ex)
            {
                if(ex.Message.Contains("Vaiolation of UNIQUE KEY constraint"))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>"+ txtUserName.Text.Trim() + "</b> user name already exixt,try new one..";
                    lblMsg.CssClass = "alert alert-warning";
                }
                else 
                {

                    Response.Write("<script>alert('" + ex.Message + "');</script>");


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
            txtUserName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            ddlCountry.ClearSelection();


        }
    }
}