<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Objects_Header"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" CodeFile="Header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Staging/FormControls/ServerSelector.ascx" TagName="ServerSelector"
    TagPrefix="cms" %>

<asp:Content ID="plcContent" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlTitle" runat="server" CssClass="PageHeader">
        <cms:PageTitle ID="titleElem" runat="server" EnableViewState="false" HideTitle="true" />
    </asp:Panel>
    <asp:Panel ID="pnlServers" runat="server" CssClass="header-container">
        <div class="cms-edit-menu">
            <div class="form-horizontal form-filter selector-right">
                <div class="form-group">
                    <div class="filter-form-label-cell">
                        <cms:LocalizedLabel ID="lblServers" runat="server" CssClass="control-label" EnableViewState="false" ResourceString="Tasks.SelectServer" />
                    </div>
                    <div class="filter-form-value-cell-wide">
                        <cms:ServerSelector ID="selectorElem" runat="server" IsLiveSite="false" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        //<![CDATA[
        var serversElem = document.getElementById('<%=selectorElem.DropDownList.ClientID%>');
        var currentServerId = 0;
        var currentNodeId = '';
        var currentSiteId = -1;

        function ChangeServer(value) {
            currentServerId = value;
            SelectNode(currentNodeId, currentSiteId);
        }

        function SelectNode(nodeId, siteId) {
            currentNodeId = nodeId;
            currentSiteId = siteId;
            parent.frames['tasksContent'].location = 'Tasks.aspx?serverid=' + currentServerId + '&objecttype=' + nodeId + '&siteid=' + siteId;
        }
        //]]>
    </script>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
