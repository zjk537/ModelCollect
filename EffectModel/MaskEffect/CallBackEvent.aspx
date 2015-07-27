<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallBackEvent.aspx.cs" Inherits="MaskEffect.CallBackEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>轻量级回调例子</title>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span id="king">
        <asp:DropDownList ID="drpList1" runat="server">
          <asp:ListItem>asa</asp:ListItem>
        </asp:DropDownList>
        </span>
        <a href="javascript:CallTheServer1('', '')">变更</a>
    </div>
    </form>
</body>
</html>
