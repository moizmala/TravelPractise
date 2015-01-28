<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_AllTasks_Header"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" CodeFile="Header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Staging/FormControls/ServerSelector.ascx" TagName="ServerSelector"
    TagPrefix="cms" %>

<asp:Content ID="plcContent" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlTitle" runat="server" CssClass="PageHeader">
        <cms:pagetitle id="titleElem" runat="server" HideTitle="true" />
    </asp:Panel>
    <asp:Panel ID="pnlServers" runat="server" CssClass="cms-edit-menu">
        <div class="form-horizontal form-filter selector-right">
            <div>
                <div class="filter-form-label-cell">
                     <cms:localizedlabel id="lblServers" CssClass="control-label" runat="server" enableviewstate="false" resourcestring="Tasks.SelectServer" />
                </div>
                <div class="filter-form-value-cell-wide">
                    <cms:serverselector id="selectorElem" runat="server" islivesite="false" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>