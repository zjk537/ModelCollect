<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pageMask.aspx.cs" Inherits="MaskEffect.pageMask" %>

<%@ Register Assembly="ModalUpdateProgress" Namespace="Jeffz.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .modalBackground
        {
            background-color: gray;
            filter: alpha(opacity=30);
            opacity: 0.7;
        }
        #updateAnimation
        {
            color: Black;
            background-color: #ffffae;
            font-family: Arial;
            font-size: 8pt;
            font-weight: bold;
            line-height: 30px;
            height: 30px;
            padding-left: 5px;
            padding-right: 5px;
        }
    </style>
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
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                        DisplayAfter="50" >
                        <ProgressTemplate>
                            <div class="popup1">
                                <div class="popup1top">
                                </div>
                                <div class="updateAnimation">
                                    <img src="images/busy.gif" style="margin-bottom: -10px;" />正在执行您的操作，请稍等...
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <%= DateTime.Now %>
                    用户名:&nbsp;<asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                    <asp:Button ID="btnPageMask" runat="server" Text="Page遮罩" Width="70px" 
                        onclick="btnPageMask_Click" />
                        <asp:Button ID="btnNotice" runat="server" Text="不显示遮罩" Width="70px" 
                        onclick="btnNotice_Click" />
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
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPageMask" EventName="btnPageMask_Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnNotice" EventName="btnNotice_Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        &nbsp; &nbsp; &nbsp;
        <cc1:ModalUpdateProgess ID="ModalUpdateProgess1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            BackgroundCssClass="modalBackground" DropShadow="true" Drag="true" X="300" Y="200">
            <ProgressTemplate>
                <div id="updateAnimation">
                    &nbsp;<img src="images/busy.gif" />正在执行您的操作，请稍等...
                </div>
            </ProgressTemplate>
        </cc1:ModalUpdateProgess>
        </form>
    </div>
</body>
</html>
