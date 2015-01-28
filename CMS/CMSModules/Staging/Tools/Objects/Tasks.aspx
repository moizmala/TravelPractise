<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Staging_Tools_Objects_Tasks" Theme="Default" Title="Staging - Tasks"
    CodeFile="Tasks.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncBackground.ascx" TagName="AsyncBackground"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Basic/DisabledModuleInfo.ascx" TagPrefix="cms"
    TagName="DisabledModule" %>

<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Panel runat="server" ID="pnlLog" Visible="false">
        <cms:AsyncBackground ID="backgroundElem" runat="server" />
        <div class="AsyncLogArea">
            <asp:Panel ID="pnlAsyncBody" runat="server" CssClass="PageBody">
                <asp:Panel ID="pnlTitle" runat="server" CssClass="PageHeader">
                    <cms:PageTitle ID="titleElem" runat="server" HideTitle="true" />
                </asp:Panel>
                <asp:Panel ID="pnlCancel" runat="server" CssClass="header-panel">
                    <cms:LocalizedButton runat="server" ButtonStyle="Primary" ID="btnCancel" ResourceString="general.cancel" />
                </asp:Panel>
                <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                    <cms:AsyncControl ID="ctlAsync" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlNotLogged" CssClass="PageContent">
        <cms:DisabledModule runat="server" ID="ucDisabledModule" />
    </asp:Panel>
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:UniGrid ID="gridTasks" runat="server" GridName="~/CMSModules/Staging/Tools/Objects/Tasks.xml"
            IsLiveSite="false" OrderBy="TaskTime, TaskID" DelayedReload="false" ExportFileName="staging_task" />
        <br />
        <asp:Panel ID="pnlFooter" runat="server" Style="clear: both;">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <cms:LocalizedButton runat="server" ID="btnSyncSelected" ButtonStyle="Default" OnClick="btnSyncSelected_Click"
                            ResourceString="Tasks.SyncSelected" EnableViewState="false" /><cms:LocalizedButton
                                runat="server" ID="btnSyncAll" ButtonStyle="Default" OnClick="btnSyncAll_Click"
                                ResourceString="Tasks.SyncAll" EnableViewState="false" />
                    </td>
                    <td class="TextRight">
                        <cms:LocalizedButton runat="server" ID="btnDeleteSelected" ButtonStyle="Default"
                            OnClick="btnDeleteSelected_Click" ResourceString="Tasks.DeleteSelected" EnableViewState="false" /><cms:LocalizedButton
                                runat="server" ID="btnDeleteAll" ButtonStyle="Default" OnClick="btnDeleteAll_Click"
                                ResourceString="Tasks.DeleteAll" EnableViewState="false" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:PlaceHolder>
</asp:Content>
