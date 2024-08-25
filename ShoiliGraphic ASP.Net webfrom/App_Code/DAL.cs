using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI;
using System.Data.Common;
using System.Data.SqlClient;

public class DAL
{
    SqlCommand cmd = null;

    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
    public void con()
    {
        if (cn.State == ConnectionState.Open)
        {
            cn.Close();
        }
        cn.Open();
    }
    bool returnbool = false;

    int r_count;
    public DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public SqlConnection Connection
    {
        get
        {
            return cn;
        }
    }
    public DataSet GetDataSet(string sql)
    {
        SqlCommand cmd = new SqlCommand(sql, cn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        adp.Fill(ds);
        Connection.Close();

        return ds;
    }
    public DataTable GetDataTable(string sql)
    {
        DataSet ds = GetDataSet(sql);

        if (ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }

    public DataTable GetClient()
    {
        con();
        string id = @"  SELECT * FROM ClientInfo order by Name ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable LoadData()
    {
        con();
        string id = @"  SELECT * FROM VoucherList ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetDiryClient()
    {
        con();
        string id = @"  SELECT * FROM DairyMaintain order by Name ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetClientAutoId()
    {
        con();
        string id = @"SELECT ISNULL(Max(id),0)+1 FROM ClientInfo ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GeVoucher(string p)
    {
        con();
        string id = @"  SELECT * FROM VoucherList where Name='" + p + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GetVoucherAmount(string p)
    {
        con();
        string id = @"select SUM(Amount) from Income   where Vid='" + p + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GetClientById(string p)
    {
        con();
        string id = @"select * from ClientInfo  where Name='" + p + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetPayment(string p)
    {
        con();
        string id = @"select * from BillCollection  where ClientId='" + p + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetPaymentAdvance(string p)
    {
        con();
        string id = @"select * from AdvancePayment  where ClientId='" + p + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public bool InsertClient(string[] fival)
    {
        try
        {
            con();            
            string insert = "Insert Into ClientInfo(ClientId,Name,Address,ContactNo,Designation)Values(@ClientId,@Name,@Address,@ContactNo,@Designation)";
            cmd = new SqlCommand(insert, cn);
            cmd.Parameters.AddWithValue("@ClientId", fival[0]);
            cmd.Parameters.AddWithValue("@Name", fival[1]);
            cmd.Parameters.AddWithValue("@Address", fival[2]);
            cmd.Parameters.AddWithValue("@ContactNo", fival[3]);
            cmd.Parameters.AddWithValue("@Designation", fival[4]);           
            cmd.ExecuteNonQuery();          
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public bool InsertData(string clientid, string date, string amount, string voucherid, string discount)
    {
        try
        {
            con();
            string insert = "INSERT INTO BillCollection (ClientId,Date,Amount,voucherno,discount)values(@ClientId,@Date,@Amount,@voucherno,@discount)";
            cmd = new SqlCommand(insert, cn);
            cmd.Parameters.AddWithValue("@ClientId", clientid);
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(date));
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@voucherno",voucherid);
            cmd.Parameters.AddWithValue("@discount", discount);
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
    public bool InsertPayment(string clientid, string date, string amount)
    {
        try
        {
            con();
            string insert = "INSERT INTO AdvancePayment (ClientId,Date,Amount)values(@ClientId,@Date,@Amount)";
            cmd = new SqlCommand(insert, cn);
            cmd.Parameters.AddWithValue("@ClientId", clientid);
            cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(date));
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
    public bool insertvoucher(string vname, string name, string ClientId, string designation, string address, string phone, string date, string total, string advance, string DueAmount)
    {
        
        try
        {
            con();
            string insert = "INSERT INTO VoucherList (Vid,Name,ClientId,Designation,Address,Phone,date,Total,advance,DueAmount,PayStatus)values('" + vname + "','" + name + "','" + ClientId + "','" + designation + "','" + address + "','" + phone + "','" + Convert.ToDateTime(date) + "','" + total + "','" + advance + "','" + DueAmount + "','Not Paid')";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool Income(string vname, string type, string dec, string disc, string amount)
    {      
        try
        {
            con();
            string insert = "INSERT INTO Income (Vid,Description,Quantity,Discount,Amount)values('" + vname + "','" + type + "','" + dec + "','" + disc + "','" + Convert.ToDouble(amount) + "')";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool Update(double total, double advance, string voucher)
    {       
        try
        {
            con();
            string insert = "Update VoucherList set Total='" + total + "', advance='"+advance+"' where Vid='" + voucher + "'";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool Expense(string vname, string type, string dec,string disc, string amount)
    {       
        try
        {
            con();
            string insert = "INSERT INTO Expense (Vid,Type,Description,Discount,Amount)values('" + vname + "','" + type + "','" + dec + "','"+disc+"','" + Convert.ToDouble(amount) + "')";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable VoucherList()
    {
        con();
        string id = @"SELECT Max(id)+1 FROM VoucherList";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GetReport(string p)
    {
        con();
        string id = @"select * from Income inner join VoucherList on Income.Vid=VoucherList.Vid where VoucherList.Vid='" + p + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable getInvoice(string p)
    {
        con();
        string id = @"Select * from VoucherList inner join Income on Income.Vid=VoucherList.Vid where VoucherList.Vid='" + p + "'";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GetPaidAmound(string client, string vno)
    {
        con();
        string id = @"Select ISNULL(Sum(Amount),0) as Amount from BillCollection where ClientId='" + client + "' and voucherno='" + vno + "'";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetNextPay(string client)
    {
        con();
        string id = @"Select ISNULL(Sum(Amount),0) as Amount from BillCollection where ClientId='" + client + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetTotalAmountbyClient(string client)
    {
        con();
        string id = @"Select ISNULL(Sum(Total),0) as Amount from VoucherList where Name='" + client + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GetAdvanceAmountbyClient(string client)
    {
        con();
        string id = @"Select ISNULL(Sum(advance),0) as Amount from VoucherList where Name='"+client+"' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetDueAmountbyClient(string client)
    {
        con();
        string id = @"Select SUM(DueAmount) as DueAmount from VoucherList where Name='" + client + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetAdvanceAmountbyVoucher(string client, string voucher)
    {
        con();
        string id = @"Select ISNULL(Sum(advance),0) as Amount from VoucherList where Name='" + client + "'and Vid='" + voucher + "'";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetPaidAmoundbyClient(string p)
    {
        con();
        string id = @"Select * from BillCollection where ClientId='" + p + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetClientAmount(string p)
    {
        con();
        string id = @"Select * from VoucherList where Name='" + p + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public bool EditMachineId(string Id, string Description, string Quantity, string Amouunt, string Discount)
    {
        try
        {
            con();
            string insert = "Update Income set Description='" + Description + "', Quantity='" + Quantity + "',Amount='" + Amouunt + "',Discount='" + Discount + "' where Incomeid='" + Id + "'";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool DeleteVoucher(string Id)
    {
        try
        {
            con();
            string insert = "Delete from Income  where Incomeid='" + Id + "'";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool SaveInvoice(string VId, string Description, string Quantity, string Amouunt, string Discount)
    {
        try
        {
            con();
            string insert = "Insert into Income (Vid,Description,Quantity,Amount,Discount)Values('" + VId + "','" + Description + "','" + Quantity + "','" + Amouunt + "','" + Discount + "')";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool EditVL(string VID, string Amouunt)
    {
        try
        {
            con();
            string insert = "Update VoucherList set Total='" + Amouunt + "' where Vid='" + VID + "'";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool deleteUser(string p)
    {
        try
        {
            con();
            string insert = "Delete from ClientInfo where ClientId='" + p + "'";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable getMaxid(string p)
    {
        con();
        string id = @"Select * from VoucherList where Name='" + p + "'";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public bool InsertDairy(DateTime date, string subject, string comments, string name)
    {
        try
        {
            con();
            string insert = "Insert into DairyMaintain(Date,Subject,Comments,Name)values('" + date + "','" + subject + "','" + comments + "','"+name+"') ";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable getDiary()
    {
        con();
        string id = @"Select * from DairyMaintain order by Date desc ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable getDiaryByName( string name)
    {
        con();
        string id = @"Select * from DairyMaintain where Name='" + name + "' order by Date desc ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public bool DeleteComments(string Id)
    {
        try
        {
            con();
            string insert = "Delete from DairyMaintain where id='"+Id+"' ";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool EditComments(string Id, string date, string subject, string comments, string name)
    {
        try
        {
            con();
            string insert = "Update DairyMaintain set date='" + date + "', Subject='" + subject + "', Comments='" + comments + "',Name='"+name+"' where id='" + Id + "' ";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable getClientInfo()
    {
        con();
        string id = @"Select * from ClientInfo order by Name ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public bool EditClient(string txtClientid, string txtName, string txtAddress, string txtContactNo, string txtDesignation)
    {
        try
        {
            con();
            string insert = "Update ClientInfo set ClientId='" + txtClientid + "', Name='" + txtName + "', Address='" + txtAddress + "',ContactNo='" + txtContactNo + "',Designation='" + txtDesignation + "' where ClientId='" + txtClientid + "' ";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable GetDiscountbyVoucher(string client, string vno)
    {
        con();
        string id = @"Select ISNULL(Sum(discount),0) as Amount from BillCollection where ClientId='" + client + "' and voucherno='" + vno + "'";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }
    public DataTable GetDiscount(string client)
    {
        con();
        string id = @"Select ISNULL(Sum(discount),0) as Amount from BillCollection where ClientId='" + client + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GetUser(string p, string p_2)
    {
        con();
        string id = @"Select * from Users where UserName='" + p + "' and Password='" + p_2 + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GetDairyNmae(string p)
    {
        con();
        string id = @"Select * from DairyMaintain where Name='" + p + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }


    public DataTable getDiaryByName()
    {
        throw new NotImplementedException();
    }

    public bool DeleteVoucher(string client, string voucher)
    {
        try
        {
            con();

            string insert = "Delete from Income where Vid='" + voucher + "'";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();

            insert = "Delete from VoucherList where Name='"+client+"' and Vid='" + voucher + "'";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool SaveSMS(string phone)
    {
        try
        {
            con();
            string insert = "insert into SMS_COUNT(MOBILE_NO,STATUS,INSERT_TIME) VALUES('" + phone + "','Success',getdate())";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable getSMSTotal()
    {
        SqlCommand sqlcmd = null;
        DataTable _dt = new DataTable();
        try
        {

            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.Open();
            string sql = "Select * from SMS_COUNT";
            sqlcmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(_dt);
        }
        catch (Exception)
        {
            throw;
        }
        return _dt;
    }

    public DataTable getSMSTotalByDate(string p, string p_2)
    {
        SqlCommand sqlcmd = null;
        DataTable _dt = new DataTable();
        try
        {

            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.Open();
            string sql = "Select * from SMS_COUNT where INSERT_TIME>='" + p + "' and INSERT_TIME<='" + p_2 + "'";
            sqlcmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(_dt);
        }
        catch (Exception)
        {
            throw;
        }
        return _dt;
    }

    public DataTable getSMS()
    {
        con();
        string id = @"Select Count(MOBILE_NO) from SMS_COUNT where STATUS='Success'";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GetSMSBydate(string date1, string date2)
    {
        con();
        string id = @"Select Count(MOBILE_NO) from SMS_COUNT where STATUS='Success' and INSERT_TIME>='" + date1 + "' and INSERT_TIME<='" + date2 + "' ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public bool DeleteClient(string Id)
    {
        try
        {
            con();
            string insert = "delete from ClientInfo where ClientId='"+Id+"'";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable getClientInfoByClient(string p)
    {   
        con();
        string id = @"Select * from ClientInfo where ClientId='" + p + "' order by Name ";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable GetDueAmount()
    {
        con();
        string id = @"with cat as (
Select VoucherList.Name ,ISNULL(SUM(Total),0) as TotalAmount,ISNULL(SUM(advance),0) as Advance,
(Select ISNULL(SUM(amount),0) from BillCollection where BillCollection.ClientId=VoucherList.ClientId)  as Collect,
((Select ISNULL(SUM(amount),0) from BillCollection where BillCollection.ClientId=VoucherList.ClientId)+ISNULL(SUM(advance),0))  as CollectAmount,
(Select ISNULL(SUM(discount),0) from BillCollection where BillCollection.ClientId=VoucherList.ClientId)  as DiscountAmount,
ISNULL(SUM(DueAmount),0) as DueAmount
from VoucherList inner join ClientInfo on ClientInfo.Name=VoucherList.Name  Group by VoucherList.Name,VoucherList.ClientId 
) select * from cat where DueAmount>0
order by DueAmount desc";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public DataTable getDueListByClient(string p)
    {
        con();
        string id = @"with cat as (
Select VoucherList.Name ,ISNULL(SUM(Total),0) as TotalAmount,ISNULL(SUM(advance),0) as Advance,
(Select ISNULL(SUM(amount),0) from BillCollection where BillCollection.ClientId=VoucherList.ClientId)  as CollectAmount,
(Select ISNULL(SUM(discount),0) from BillCollection where BillCollection.ClientId=VoucherList.ClientId)  as DiscountAmount,
ISNULL(SUM(DueAmount),0) as DueAmount
from VoucherList inner join ClientInfo on ClientInfo.Name=VoucherList.Name  Group by VoucherList.Name,VoucherList.ClientId 
) select * from cat where DueAmount>0 and Name='" + p+"' order by DueAmount desc";
        DataTable dta = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(id, cn);
        da.Fill(dta);
        return dta;
    }

    public bool UpdateClientid(string[] fiva)
    {
        try
        {
            con();
            string insert = "Update VoucherList set ClientId='" + fiva[0] + "' where Name='" + fiva[1] + "'";
            cmd = new SqlCommand(insert, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public DataTable bindReceiveTypeBNyClient(string cid)
    {
        SqlCommand sqlcmd = null;
        DataTable _dt = new DataTable();
        try
        {
            con();
            string sql = "select * from VoucherList where ClientId='" + cid + "' and PayStatus='Not Paid' order by date asc";
            sqlcmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(_dt);
        }
        catch (Exception)
        {
            throw;
        }
        return _dt;
    }
    public DataTable bindInvoice(string invoice)
    {
        SqlCommand sqlcmd = null;
        DataTable _dt = new DataTable();
        try
        {
            con();
            string sql = "select * from Income where Vid='" + invoice + "'";
            sqlcmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(_dt);
        }
        catch (Exception)
        {
            throw;
        }
        return _dt;
    }

    public DataTable bindReceiveTypeBNyInvoice(string invoice)
    {
        SqlCommand sqlcmd = null;
        DataTable _dt = new DataTable();
        try
        {
            con();
            string sql = "select * from VoucherList where Vid='" + invoice + "' and PayStatus='Not Paid' order by date asc";
            sqlcmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(_dt);
        }
        catch (Exception)
        {
            throw;
        }
        return _dt;
    }

    public bool Updatesalesvoucher(string p, string paidamount, string dueamount)
    {

        try
        {
            string paidStatus = "Paid";
            if (int.Parse(dueamount) > 0)
            {
                paidStatus = "Not Paid";
            }
            con();
            string update = "Update VoucherList set PayStatus='" + paidStatus + "', advance='" + paidamount + "', DueAmount='" + dueamount + "' where Vid='" + p + "'";
            cmd = new SqlCommand(update, cn);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            throw;
        }       
    }
}