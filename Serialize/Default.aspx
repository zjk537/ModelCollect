<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Serialize._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    body
    {
        font-size:12px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        序列化格式：
        <asp:DropDownList ID="ddlFileType" runat="server">
            <asp:ListItem Value="bat">二进制文件</asp:ListItem>
            <asp:ListItem Value="xml">Xml文件</asp:ListItem>
        </asp:DropDownList> <br /><br />
        保存路径：
        <asp:TextBox ID="txtFilePath" runat="server"  Width="80%" Enabled="false"></asp:TextBox>
        <br /><br />
        <asp:Button ID="btnSerialize" runat="server" OnClick="btnSerialize_Click" Text="序列化" />
        <asp:Button ID="btnDeSerialize" runat="server" OnClick="btnDeSerialize_Click" Text="反序列化" />
        <br />
        <br />
        <div>原文件中内容为： 【ID】100  【Name】zjk 【Sex】man  【MobileNumber】13825200137(不可序列化) <br />
             序列化时对文件中内容更改为： 【ID】100  【Name】zjk 【Sex】woman  【MobileNumber】15818526539
        </div>
        <br />
        <br />
        <asp:Literal ID="lbResult" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
