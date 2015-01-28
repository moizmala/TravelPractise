<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_Data_Header"
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
                        <cms:LocalizedLabel CssClass="control-label" ID="lblServers" runat="server" EnableViewState="false" ResourceString="Tasks.SelectServer" />
                    </div>
                    <div class="filter-form-value-cell-wide">
                        <cms:ServerSelector ID="selectorElem" runat="server" IsLiveSite="false" />
                    </div>
                </div>
            </div>
            <cms:LocalizedButton ID="btnComplete" runat="server" OnClientClick="return CompleteSync();"
                EnableViewState="false" ButtonStyle="Primary" ResourceString="Tasks.CompleteSync" />
        </div>
    </asp:Panel>
    <script type="text/javascript">
        //<![CDATA[

        var currentServerId = 0;
        var currentNodeId = '';

        function ChangeServer(value) {
            currentServerId = value;
            SelectNode(currentNodeId);
        }

        function SelectNode(nodeId) {
            currentNodeId = nodeId;
            parent.frames['tasksContent'].location = 'Tasks.aspx?serverid=' + currentServerId + '&objecttype=' + nodeId;
        }

        function CompleteSync() {
            parent.frames['tasksContent'].CompleteSync();
            return false;
        }
        //]]>
    </script>
</asp:Content>
