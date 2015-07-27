<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ReadFiles._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%--<asp:Label ID="lbTest" Text="test" runat="server"></asp:Label>--%>
        <asp:Repeater ID="rptFileList" runat="server" Visible="true">
            <HeaderTemplate>
                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#639AC3"
                    class="table_info">
                    <tr>
                        <td class="table_infotil2">
                            文件名
                        </td>
                        <td class="table_infotil2">
                            文件大小
                        </td>
                        <td class="table_infotil2">
                            创建时间
                        </td>
                        <td class="table_infotil2">
                            操作
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td bgcolor="#FFFFFF" class="per_tab" style="vnd.ms-excel.numberformat: @">
                        <%#Eval("FileName") %>
                    </td>
                    <td bgcolor="#FFFFFF" class="per_tab">
                        <%#Eval("FileSize") %>Kb
                    </td>
                    <td bgcolor="#FFFFFF" class="per_tab" style="vnd.ms-excel.numberformat: @">
                        <%#Eval("CreateDate") %>
                    </td>
                    <td bgcolor="#FFFFFF" class="per_tab" style="vnd.ms-excel.numberformat: @">
                        <a href='DLFile.aspx?dlfile=<%#Eval("UrlQueryString",null)%>&dapfix=true'>
                        下载</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
    </div>
    </form>
</body>
</html>
