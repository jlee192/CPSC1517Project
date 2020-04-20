﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ProjectSystem.BLL;
using ProjectSystem.ENTITIES;

namespace CPSC1517Project.ProjectPages
{
    public partial class ProgramPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if (!Page.IsPostBack)
            {
                BindList();
            }
        }
        protected void BindList()
        {
            try
            {
                Fetch02.Enabled = false;
                List02.Enabled = false;
                SchoolController sysmgr = new SchoolController();
                List<Schools> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.SchoolName.CompareTo(y.SchoolName));
                List01.DataSource = info;
                List01.DataTextField = nameof(Schools.SchoolName);
                List01.DataValueField = nameof(Schools.SchoolCode);
                List01.DataBind();
                List01.Items.Insert(0, "select...");
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }
        }
        protected void Fetch_Click01(object sender, EventArgs e)
        {
            if (List01.SelectedIndex == 0)
            {
                MessageLabel.Text = "Select school from list";
            }
            else
            {
                try
                {
                    ProgramController sysmgr02 = new ProgramController();
                    List<Programs> info02 = null;
                    info02 = sysmgr02.FindBySchoolCode(List01.SelectedValue);
                    info02.Sort((x, y) => x.ProgramName.CompareTo(y.ProgramName));
                    Fetch02.Enabled = true;
                    List02.Enabled = true;
                    List02.DataSource = info02;
                    List02.DataTextField = nameof(Programs.ProgramName);
                    List02.DataValueField = nameof(Programs.ProgramID);
                    List02.DataBind();
                    List02.Items.Insert(0, "select...");
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
            }
        }
        protected void Fetch_Click02(object sender, EventArgs e)
        {
            if (List02.SelectedIndex == 0)
            {
                MessageLabel.Text = "Select a program.";
            }
            else
            {
                try
                {
                    string programid = List02.SelectedValue;
                    Response.Redirect("CRUDPage.aspx?pid=" + programid);
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
            }
        }
    }
}