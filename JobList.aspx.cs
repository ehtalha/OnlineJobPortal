using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Job_Online_Portal.Admin
{
    public partial class JobList : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string query;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                ShowJob();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowJob();
        }

        private void ShowJob()
        {
           
     
            string query = string.Empty;
            con = new SqlConnection(str);
            query = @"Select Row_Number() over(Order by(Select 1)) as [Sr.No],JobId,Titel,NoOfPost,Qualification," +
                 "Experience,LastDateToApply,CompanyName,CreateDate from Jobs";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            dt = new DataTable();   
            sda.Fill(dt);
            GridView1.DataSource = dt;
           GridView1.DataBind();   

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ShowJob();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int jobId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                con = new SqlConnection(str);
                cmd = new SqlCommand("Delete from Jobs where JobId=@id", con);
                cmd.Parameters.AddWithValue("@id", jobId);
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblmsg.Text = "Job Deleted Successfully";
                    lblmsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblmsg.Text = "Cannot Deleted this record";
                    lblmsg.CssClass = "alert alert-danger";

                }
                con.Close();
                GridView1.EditIndex = -1;
                ShowJob();

            }
            catch (Exception ex) 
            {

                con.Close();
                Response.Write("<script>alert('" + ex.Message + "');</script>");



            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "EditJob")
            {
                Response.Redirect("NewJob.aspx?id=" +  e.CommandArgument.ToString());
            }
        }
    }
}