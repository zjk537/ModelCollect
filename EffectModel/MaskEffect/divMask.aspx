<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="divMask.aspx.cs" Inherits="MaskEffect.divMask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="js/jquery.blockUI.js"></script>
    <script type="text/javascript" language="javascript">

        // 这是一个轻量级的，异步调用后台程序的 Ajax
        // ------------------------------------------------------
        var mycss =
            {
                border: '3px solid #888',
                cursor: '',
                opacity: '.5',
                padding: '15px',
                width: 'auto'
            };
            $(function() {
            $('#btnTest').bind('click', function() {
                $('div#divOverlay').block({
                        message: '<span><img src="images/busy.gif" />   正在处理中，请稍候...</span>',
                        css: mycss,
                        overlayCSS: { backgroundColor: '#666' }
                    });
                    clientClick();
                });
            });

            function unLock(content) {
                alert(content);
                $('div#divOverlay').unblock();
            }
            // ------------------------------------------------------
            
            // 这里，利用服务器端控件，只能实现遮罩，不能解除遮罩，因为客户端绑定，返回false，没有去执行服务器端代码
         // 为什么这个方法返回 false 呢
            $(function() {
            $('#<%=btnDivMask.ClientID %>').bind('click', function() {
                $("#divOverlay").block({
                        message: '<span><img src="images/busy.gif" />   正在处理中，请稍候...</span>',
                        css: mycss,
                        overlayCSS: { backgroundColor: '#666' }
                    });
                });
            });

    </script>

</head>
<body>
    <div>
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="divOverlay" style="width: 433px; border-color: Blue; border-width: 10px;">
            <input type="button" id="btnTest" value="test" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <%= DateTime.Now %>
                    用户名:&nbsp;<asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                    <asp:Button ID="btnDivMask" runat="server" Text="div遮罩" Width="70px"  OnClick="btnDivMask_Click" />
                    &nbsp;<br />
                    <br />
                    &nbsp; &nbsp;<br />
                    <br />
                    &nbsp; &nbsp;<br />
                    <br />
                    &nbsp; &nbsp;<br />
                    <br />
                    &nbsp;
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </form>
    </div>
</body>
</html>
