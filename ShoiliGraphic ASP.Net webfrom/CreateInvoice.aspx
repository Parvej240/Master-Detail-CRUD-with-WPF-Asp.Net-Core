<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CreateInvoice.aspx.cs" Inherits="CreateInvoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div style="overflow: scroll; height: 500px;">
       
        <div>
            <table class="table" width="100%">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="label7" runat="server" Text="Select Client"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlClient" runat="server"  Width="200px" CssClass="form-control js-example-placeholder-single">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="label" runat="server" Text="Voucher No"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVoucherName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtVoucherName"
                                        ErrorMessage="Client Name is required." ToolTip="Client Name is required."
                                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="label1" runat="server" Text="Name of Reciver"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="label3" runat="server" Text="Designation" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="label4" runat="server" Text="Address"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                     <asp:Label ID="lblBeforeDue" Visible="false" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="label2" runat="server" Text="Phone"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtphone" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="label5" runat="server" Text="Date"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="DateClanaderExtender" runat="server" TargetControlID="txtDate"
                                        Format="yyyy/MM/dd">
                                    </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                        CssClass="failureNotification" ErrorMessage="Date required." ToolTip="Date required."
                                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                           
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div>
                <asp:Label ID="lblTotal" runat="server"></asp:Label>
            </div>
            <br />
            <asp:GridView ID="Gridview1" runat="server" ShowFooter="True" Width="100%" AutoGenerateColumns="False"
                OnRowCommand="Gridview1_RowCommand" OnRowDataBound="Gridview1_RowDataBound" BackColor="#FFCCFF">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="Row Number" />
                     <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:TextBox ID="txtName" Width="150px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDec" Width="150px" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false" HeaderText="Discount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDiscount" Text="0" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Left" />
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click1" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


        </div>
        <br />
        <div style="height: 50px; text-align: center; padding: 5px;">
         Total Amount :
            <asp:TextBox ID="txtTotal" runat="server" Enabled="false"></asp:TextBox>
            Advance:
            <asp:TextBox ID="txtAdvance" runat="server" AutoPostBack="true" Text="0" 
                ontextchanged="txtAdvance_TextChanged"></asp:TextBox>
            Due Amount: 
            <asp:TextBox ID="txtDueAmount" runat="server" Enabled="false" Text="0"></asp:TextBox>

            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </div>
     <asp:GridView ID="Gridview2" runat="server" ShowFooter="True" Width="100%" AutoGenerateColumns="False"
                BackColor="#FFCCFF">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="Total" HeaderText="Total Amount" />
                  
                </Columns>
            </asp:GridView>
     <%--For Auto complete dropdown--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/css/select2.min.css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".js-example-placeholder-single").select2({
                placeholder: "Select",
                allowClear: true
            });
        });
    </script>
</asp:Content>
