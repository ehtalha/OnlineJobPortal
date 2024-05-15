using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Job_Online_Portal.Admin
{
    public partial class Newjob : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        string query;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            Session["title"] = "Add Job";
            if (!IsPostBack)
            {
                fillData();
            }

        }

        private void fillData()
        {
            if (Request.QueryString["id"]!= null)
            {
                con = new SqlConnection(str);
                query = "Select * from Jobs where JobId = '" + Request.QueryString["id"] +"' ";
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if(sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        txtJobTitle.Text = sdr["Titel"].ToString();
                        txtNoOfPost.Text = sdr["NoOfPost"].ToString();
                        txtDescription.Text = sdr["Description"].ToString();
                        txtQualification.Text = sdr["Qualification"].ToString();
                        txtExperience.Text = sdr["Experience"].ToString();
                        txtSpecialization.Text = sdr["Specialization"].ToString();
                        txtLastDate.Text = Convert.ToDateTime( sdr["LastDateToApply"]).ToString("yyyy-MM-dd");
                        txtSalary.Text = sdr["Salary"].ToString();
                        ddlJobType.SelectedValue = sdr["JobType"].ToString();
                        txtWebsite.Text = sdr["Website"].ToString();
                        txtEmail.Text = sdr["Email"].ToString();
                        txtAddress.Text = sdr["Address"].ToString();
                        txtCompany.Text = sdr["CompanyName"].ToString();
                        // txtJobTitle.Text = sdr["CreateDate"].ToString();
                        btnAdd.Text = "Update";
                        LinkBack.Visible = true;
                        Session["title"] = "Edit Job";

                    }
                }
                else
                {
                    lblmsg.Text = "Job Not Found";
                    lblmsg.CssClass = "alter alter-danger";
                }
                sdr.Close();
                con.Close();
            }
    
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                string concatQuery,type,imagePath = string.Empty;
                bool isValidToExecute = false;
                con = new SqlConnection(str);
                if (Request.QueryString["id"] != null)
                {
                  if (fuCompanyLogo.HasFile)
                  {
                         if (IsValidExtention(fuCompanyLogo.FileName))
                         {
                            concatQuery = "CompanyImage = @CompanyImage";
                         }
                        else
                        {
                            concatQuery = string.Empty;

                         
                        }
                  }
                  else
                  {
                        concatQuery = string.Empty;
 
                  }
                  query = "Update Jobs Values Title=@Title, NoOfPost=@NoOfPost,Description=@Description,Qualification=@Qualification," +
                          "Experience=@Experience,Specialization=@Specialization,LastDateToApply=@LastDateToApply,Salary=@Salary,JobType=@JobType," +
                          "CompanyName=@CompanyName,"+ concatQuery + "Website=@Website,Email=@Email," +
                          "Address=@Address,CreateDate=@CreateDate where JobId=@id";

                    type = "updated";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfPost", txtNoOfPost.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                    // cmd.Parameters.AddWithValue("@CompanyImage", txtSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"].ToString());
                    if (fuCompanyLogo.HasFile)
                    {
                        if (IsValidExtention(fuCompanyLogo.FileName))
                        {
                            Guid obj = Guid.NewGuid();
                            imagePath = "Image/" + obj.ToString() + fuCompanyLogo.FileName;
                            fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fuCompanyLogo.FileName);

                            cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                            isValidToExecute = true;
                        }
                        else
                        {
                            lblmsg.Text = "Please Select .jpg, .png, .jpeg file for Logo";
                            lblmsg.CssClass = "alert alert-denger";
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                        isValidToExecute = true;
                    }


                }
                else
                {

                    query = "Insert into Jobs Values(@Title,@NoOfPost,@Description,@Qualification,@Experience,@Specialization,@LastDateToApply,@Salary,@JobType," +
                                 "@CompanyName,@CompanyImage,@Website,@Email,@Address,@CreateDate)";
                    type = "saved";
                    DateTime time = DateTime.Now;
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Title", txtJobTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoOfPost", txtNoOfPost.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastDateToApply", txtLastDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@Salary", txtSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@JobType", ddlJobType.SelectedValue);
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompany.Text.Trim());
                    // cmd.Parameters.AddWithValue("@CompanyImage", txtSalary.Text.Trim());
                    cmd.Parameters.AddWithValue("@Website", txtWebsite.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@CreateDate", time.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (fuCompanyLogo.HasFile)
                    {
                        if (IsValidExtention(fuCompanyLogo.FileName))
                        {
                            Guid obj = Guid.NewGuid();
                            imagePath = "Image/" + obj.ToString() + fuCompanyLogo.FileName;
                            fuCompanyLogo.PostedFile.SaveAs(Server.MapPath("~/Images/") + obj.ToString() + fuCompanyLogo.FileName);

                            cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                            isValidToExecute = true;
                        }
                        else
                        {
                            lblmsg.Text = "Please Select .jpg, .png, .jpeg file for Logo";
                            lblmsg.CssClass = "alert alert-denger";
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CompanyImage", imagePath);
                        isValidToExecute = true;
                    }



                }
                if (isValidToExecute)
                {
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        lblmsg.Text = "Job " + type + " Successfully";
                        lblmsg.CssClass = "alert alert-denger";
                        Clear();

                    }
                    else
                    {
                        lblmsg.Text = "Cannot " + type + " the records,please try after sometimes....!";
                        lblmsg.CssClass = "alert alert-denger";

                    }
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
            txtJobTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtNoOfPost.Text = string.Empty;
            txtQualification.Text = string.Empty;
            txtExperience.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtLastDate.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtSpecialization.Text = string.Empty;
            txtExperience.Text = string.Empty;
            ddlJobType.ClearSelection();

        }

        private bool IsValidExtention(string fileName)
        {
           bool isValid = false;
            string[] fillExtension = { ".jpg", ".png", ".jpeg" };
            for(int i = 0; i < fillExtension.Length-1; i++) 
            {
                if (fileName.Contains(fillExtension[i]))
                {
                    isValid = true; 
                    break;  
                }

            
            
            }
            return isValid;
        }
    }
}