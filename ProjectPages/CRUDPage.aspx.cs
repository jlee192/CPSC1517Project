using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProjectSystem.BLL;
using ProjectSystem.ENTITIES;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

namespace CPSC1517Project.ProjectPages
{
    public partial class CRUDPage : System.Web.UI.Page
    {
        //static string pagenum = "";
        static string pid = "";
        static string add = "";
        List<string> errormsgs = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            errormsgs.Clear();
            Message.DataSource = null;
            Message.DataBind();
            if (!Page.IsPostBack)
            {
                //errormsgs.Add("IsPostBack = False");
                //LoadMessageDisplay(errormsgs, "alert alert-info");
                //pagenum = Request.QueryString["page"];
                pid = Request.QueryString["pid"];
                add = Request.QueryString["add"];
                BindSchoolList();
                if (string.IsNullOrEmpty(pid))
                {
                    Response.Redirect("~/Default.aspx");
                }
                else if (add == "yes")
                {
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;
                }
                else
                {
                    AddButton.Enabled = false;
                    ProgramController sysmgr = new ProgramController();
                    Programs info = null;
                    info = sysmgr.FindByPKID(int.Parse(pid));
                    if (info == null)
                    {
                        errormsgs.Add("Record is not in Database.");
                        LoadMessageDisplay(errormsgs, "alert alert-info");
                        Clear(sender, e);
                    }
                    else
                    {
                        ProgramID.Text = info.ProgramID.ToString();
                        ProgramName.Text = info.ProgramName;
                        DiplomaName.Text = info.DiplomaName;
                        SchoolList.SelectedValue = info.SchoolCode;
                        Tuition.Text = info.Tuition.ToString();
                        InternationalTuition.Text = info.InternationalTuition.ToString();
                    }
                }
            }
            //else
            //{
            //    errormsgs.Add("IsPostBack = True");
            //}
        }
        protected Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
        protected void LoadMessageDisplay(List<string> errormsglist, string cssclass)
        {
            Message.CssClass = cssclass;
            Message.DataSource = errormsglist;
            Message.DataBind();
            LabelMessage1.InnerHtml = "";
            for (int i = 0; i <= errormsglist.Count - 1; i++)
            {
                LabelMessage1.InnerHtml += "<br />"
                                        + errormsglist[i];
            }
        }
        protected void BindSchoolList()
        {
            try
            {
                SchoolController sysmgr = new SchoolController();
                List<Schools> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.SchoolName.CompareTo(y.SchoolName));
                SchoolList.DataSource = info;
                SchoolList.DataTextField = nameof(Schools.SchoolName);
                SchoolList.DataValueField = nameof(Schools.SchoolCode);
                SchoolList.DataBind();
                SchoolList.Items.Insert(0, "select...");
            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
        protected void Validation(object sender, EventArgs e)
        {
            //Program name validation
            if (string.IsNullOrEmpty(ProgramName.Text))
            {
                errormsgs.Add("Program name is required.");
            }
            else
            {
                if (ProgramName.Text.Length > 50)
                {
                    errormsgs.Add("Program name cannot exceed 50 characters.");
                }
            }
            //Diploma name validation
            if (DiplomaName.Text.Length > 100 && !string.IsNullOrEmpty(DiplomaName.Text))
            {
                errormsgs.Add("Diploma name cannot exceed 50 characters.");
            }
            //School validation
            if (SchoolList.SelectedIndex == 0)
            {
                errormsgs.Add("School is required");
            }
            //Tuition validation
            double tuition;
            if (!string.IsNullOrEmpty(Tuition.Text))
            {
                if (double.TryParse(Tuition.Text, out tuition))
                {
                    if (tuition < 0.00)
                    {
                        errormsgs.Add("Tuition must be greater than or equal to 0.");
                    }
                }
                else
                {
                    errormsgs.Add("Tuition must be a real number");
                }
            }
            else
            {
                errormsgs.Add("Tuition is required");
            }
            //International Tuition validation
            double internationalTuition;
            if (!string.IsNullOrEmpty(InternationalTuition.Text))
            {
                if (double.TryParse(InternationalTuition.Text, out internationalTuition))
                {
                    if (internationalTuition < 0.00)
                    {
                        errormsgs.Add("International tuition must be greater than or equal to 0.");
                    }
                }
                else
                {
                    errormsgs.Add("International tuition must be a real number");
                }
            }
            else
            {
                errormsgs.Add("International tuition is required");
            }
        }
        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProgramPage.aspx");
        }
        protected void Clear(object sender, EventArgs e)
        {
            ProgramID.Text = "";
            ProgramName.Text = "";
            DiplomaName.Text = "";
            Tuition.Text = "";
            InternationalTuition.Text = "";
            SchoolList.ClearSelection();
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            Validation(sender, e);
            if (errormsgs.Count > 1)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    ProgramController sysmgr = new ProgramController();
                    Programs item = new Programs();
                    item.ProgramName = ProgramName.Text;
                    item.SchoolCode = SchoolList.SelectedValue;
                    item.DiplomaName = DiplomaName.Text;
                    item.Tuition = decimal.Parse(Tuition.Text);
                    item.InternationalTuition = decimal.Parse(Tuition.Text);
                    int newID = sysmgr.Add(item);
                    ProgramID.Text = newID.ToString();
                    errormsgs.Add("Record has been added");
                    LoadMessageDisplay(errormsgs, "alert alert-success");
                    UpdateButton.Enabled = true;
                    DeleteButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
        protected void Update_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (string.IsNullOrEmpty(ProgramID.Text))
            {
                errormsgs.Add("Search for a record to update");
            }
            else if (!int.TryParse(ProgramID.Text, out id))
            {
                errormsgs.Add("Id is invalid");
            }
            Validation(sender, e);
            if (errormsgs.Count > 1)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    ProgramController sysmgr = new ProgramController();
                    Programs item = new Programs();
                    item.ProgramID = int.Parse(ProgramID.Text);
                    item.ProgramName = ProgramName.Text;
                    item.DiplomaName = DiplomaName.Text;
                    item.SchoolCode = SchoolList.SelectedValue;
                    item.Tuition = decimal.Parse(Tuition.Text);
                    item.InternationalTuition = decimal.Parse(InternationalTuition.Text);
                    int rowsaffected = sysmgr.Update(item);
                    if (rowsaffected > 0)
                    {
                        errormsgs.Add("Record has been updated");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                    }
                    else
                    {
                        errormsgs.Add("Record was not found");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProgramID.Text))
            {
                errormsgs.Add("Search for a record to delete");
            }
            if (errormsgs.Count > 1)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    ProgramController sysmgr = new ProgramController();
                    int rowsaffected = sysmgr.Delete(int.Parse(ProgramID.Text));
                    if (rowsaffected > 0)
                    {
                        errormsgs.Add("Record has been deleted");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                        Clear(sender, e);
                    }
                    else
                    {
                        errormsgs.Add("Record was not found");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                    UpdateButton.Enabled = false;
                    DeleteButton.Enabled = false;
                    AddButton.Enabled = true;
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
    }
}