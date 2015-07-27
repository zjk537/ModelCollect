<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PfxDemo.aspx.cs" Inherits="ReadFiles.PfxDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnToPfx" runat="server" Text="导出证书" onclick="btnToPfx_Click" />
        <asp:Button ID="btnCreatePfx" runat="server" Text="创建私钥" onclick="btnCreatePfx_Click" />
        <asp:Button ID="btnReadPfxFile" runat="server" Text="读证书" onclick="btnReadPfxFile_Click" />
        
    </div>
    </form>
</body>
</html>
