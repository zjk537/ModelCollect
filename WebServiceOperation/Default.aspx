<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebServiceOperation._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblInfo" runat="server"></asp:Label>
        <asp:Label ID="lblError" runat="server"></asp:Label>
        <asp:DropDownList ID="ddlDepartment" runat="server">
            <asp:ListItem Value="1">软件部</asp:ListItem>
            <asp:ListItem Value="2">市场部</asp:ListItem>
            <asp:ListItem Value="3">运营部</asp:ListItem>
            <asp:ListItem Value="4">财务部</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="新增" />
    </div>
    </form>
</body>
</html>
