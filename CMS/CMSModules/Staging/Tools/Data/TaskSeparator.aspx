<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Data_TaskSeparator"
    Theme="Default" EnableViewState="false" CodeFile="TaskSeparator.aspx.cs" %>

<!DOCTYPE html>
<html>
<head runat="server" enableviewstate="false">
    <title>Staging - Task</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
        }
    </style>
</head>
<body class="<%=mBodyClass%>">
    <form id="form1" runat="server" enableviewstate="false">
    <asp:Panel ID="PanelBody" runat="server" CssClass="PageBody" EnableViewState="false">
        <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent" EnableViewState="false">
            <cms:MessagesPlaceHolder ID="plcMess" runat="server" IsLiveSite="false" />
        </asp:Panel>
    </asp:Panel>
    </form>
</body>
</html>
