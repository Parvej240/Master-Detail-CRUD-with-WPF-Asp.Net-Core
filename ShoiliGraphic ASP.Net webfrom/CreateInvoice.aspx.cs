using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Text;
using System.IO;

public partial class CreateInvoice : System.Web.UI.Page
{
    DAL mydal = new DAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {           
            Client();
            SetInitialRow();
            InvoiceNo();
            txtDate.Text = DateTime.Now.ToShortDateString();

            LoadData();
        }       
    }

    public void LoadData()
    {
        DataTable dt = mydal.LoadData();
        Gridview2.DataSource = dt;
        Gridview2.DataBind();
    }
    public void InvoiceNo()
    {
        DataTable n = mydal.VoucherList();
        txtVoucherName.Text = "INC-" + n.Rows[0][0].ToString();        
    }
    public void Client()
    {    
        DataTable dt =  mydal.GetClient();
        ddlClient.DataSource = dt;
        ddlClient.DataTextField = "Name";
        ddlClient.DataValueField = "ClientId";
        ddlClient.DataBind();
        ddlClient.Items.Insert(0,"--Select--");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDate.Text == "" && txtTotal.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Please select Date or Total amount is blank!!');", true);
        }
        else
        {
            bool IsSuccess = mydal.insertvoucher(txtVoucherName.Text, txtName.Text, ddlClient.SelectedValue, txtdesignation.Text, txtAddress.Text, txtphone.Text, txtDate.Text, txtTotal.Text, txtAdvance.Text, txtDueAmount.Text);

            if (IsSuccess == true)
            { 
                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            //DropDownList box1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("ddltype");

                            TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtDec");
                            TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtQty");
                            TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtDiscount");
                            TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtAmount");

                            bool Success = mydal.Income(txtVoucherName.Text, box1.Text, box2.Text, box3.Text, box4.Text);

                           // bool update = mydal.Update(total, advance, txtVoucherName.Text);
                            rowIndex++;
                        }
                    }
                }
            }         
            ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Save Information Successfully!!!!');", true);
            SetInitialRow();
            cleantext();
        }

        // Report();
        cleantext();
        Gridview1.DataSource = null;      
    }   
  
    public void cleantext()
    {
        txtName.Text = "";
        txtdesignation.Text = "";
        txtAddress.Text = "";
        txtDate.Text = "";
        txtphone.Text = "";       
    }
    private void SetInitialRow()
    {

        DataTable dt = new DataTable();

        DataRow dr = null;

        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        Gridview1.DataSource = dt;
        Gridview1.DataBind();

    }
    private void AddNewRowToGrid()
    {

        int rowIndex = 0;



        if (ViewState["CurrentTable"] != null)
        {

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {

                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    //extract the TextBox values

                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtName");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtDec");

                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtQty");

                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtDiscount");
                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtAmount");

                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;

                    dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;

                    dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;

                    dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;



                    rowIndex++;

                }

                dtCurrentTable.Rows.Add(drCurrentRow);

                ViewState["CurrentTable"] = dtCurrentTable;



                Gridview1.DataSource = dtCurrentTable;

                Gridview1.DataBind();

            }

        }
        else
        {

            Response.Write("ViewState is null");

        }

        //Set Previous Data on Postbacks

        SetPreviousData();

    }
    private void SetPreviousData()
    {

        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtName");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtDec");

                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtQty");

                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtDiscount");

                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtAmount");

                    box1.Text = dt.Rows[i]["Column1"].ToString();

                    box2.Text = dt.Rows[i]["Column2"].ToString();

                    box3.Text = dt.Rows[i]["Column3"].ToString();

                    box4.Text = dt.Rows[i]["Column4"].ToString();
                    box5.Text = dt.Rows[i]["Column5"].ToString();

                    rowIndex++;

                }

            }

        }

    }
    protected void ButtonAdd_Click1(object sender, EventArgs e)
    {
        AddNewRowToGrid();

    }   
    protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        int index = Convert.ToInt32(e.RowIndex);
        Gridview1.DeleteRow(index);

    }
    private void SetPreviousData1(DataTable dtCurrentTable)
    {
        int rowIndex = 0;
        if (dtCurrentTable != null)
        {
            DataTable dt = dtCurrentTable;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box5 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtName");
                    TextBox box1 = (TextBox)Gridview1.Rows[rowIndex].Cells[1].FindControl("txtDec");

                    TextBox box2 = (TextBox)Gridview1.Rows[rowIndex].Cells[2].FindControl("txtQty");

                    TextBox box3 = (TextBox)Gridview1.Rows[rowIndex].Cells[3].FindControl("txtDiscount");

                    TextBox box4 = (TextBox)Gridview1.Rows[rowIndex].Cells[4].FindControl("txtAmount");

                    // box1.Text = dt.Rows[i]["Column1"].ToString();
                    box2.Text = dt.Rows[i]["Column2"].ToString();
                    box3.Text = dt.Rows[i]["Column3"].ToString();
                    box4.Text = dt.Rows[i]["Column4"].ToString();
                    box5.Text = dt.Rows[i]["Column5"].ToString();

                    rowIndex++;
                }
            }
        }
    }
    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "r")
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            Gridview1.DeleteRow(rowIndex);
        }
    }
    protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        double sum = 0;
        for (int index = 0; index < Gridview1.Rows.Count; index++)
        {
            sum += Convert.ToDouble((Gridview1.Rows[index].FindControl("txtAmount") as TextBox).Text);
        }

        txtTotal.Text = sum.ToString();
        txtDueAmount.Text = sum.ToString();
    }
    protected void txtAdvance_TextChanged(object sender, EventArgs e)
    {
        txtDueAmount.Text = (int.Parse(txtTotal.Text) - int.Parse(txtAdvance.Text)).ToString();
    }
}