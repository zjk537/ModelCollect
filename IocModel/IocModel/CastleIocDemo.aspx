﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CastleIocDemo.aspx.cs"
    Inherits="IocModel.CastleIocDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnCastleIoc" runat="server" Text="CastleIoc" OnClick="btnCastleIoc_Click" /><br />
        <asp:Label ID="lbMessage" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
