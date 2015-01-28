<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReadExcelToCustomTable.ascx.cs"
    Inherits="CMSWebParts_BaseSite81_ReadExcelToCustomTable" %>
<%@ Register Src="../../CMSFormControls/MediaSelector.ascx" TagName="MediaSelector"
    TagPrefix="uc1" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<link href="/CMSPages/GetResource.ashx?stylesheetfile=/App_Themes/Default/DesignMode.css" type="text/css" rel="stylesheet" />
<link href="/CMSPages/GetResource.ashx?stylesheetfile=/App_Themes/Default/bootstrap.css" type="text/css" rel="stylesheet" />

<div class="PageContent">
    <asp:Panel ID="pnl1" runat="server" GroupingText="Please select file for Customer Information" BackColor="Cornsilk">
    <cms:MessagesPlaceHolder ID="plcMess" runat="server" />
        <div class="editing-form-category-fields">
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <uc1:MediaSelector ID="MediaSelector1" runat="server" />
                    <asp:Button ID="btnReadCustomerInformation" Text="Read Customer Information" runat="server" CausesValidation="false"
                        CssClass="btn btn-primary" OnClick="btnReadCustomerInformation_Click" />
                    <asp:GridView ID="gridUserInformation" runat="server" AutoGenerateColumns="true"
                        AllowSorting="False" EmptyDataText="No data found">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </asp:Panel>
    <hr />
    <asp:Panel ID="pnl2" runat="server" GroupingText="Please provide Custom table name and Push data" BackColor="Azure">
    <asp:UpdatePanel ID="pnl3" runat="server" UpdateMode="Always">
    <ContentTemplate>
    <cms:MessagesPlaceHolder ID="plcMessCustomTable" runat="server" />
        <div class="form-horizontal">
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" DisplayColon="True" runat="server" ID="lblDisplayName"
                        AssociatedControlID="txtDisplayName" />
                </div>
                <div class="editing-form-value-cell">
                    <cms:LocalizableTextBox runat="server" ID="txtDisplayName" MaxLength="100" Text="TransactionLog" />
                    <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:cntrlContainer:textbox"
                        Display="dynamic" />
                </div>
            </div>
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" DisplayColon="True" runat="server" ID="lblNamespaceName" />
                </div>
                <div class="editing-form-value-cell">
                    <cms:CMSTextBox runat="server" ID="txtNamespaceName" MaxLength="49" />
                    <cms:CMSRequiredFieldValidator ID="rfvNamespaceName" runat="server" EnableViewState="false"
                        ControlToValidate="txtNamespaceName" Display="dynamic" />
                    <cms:CMSRegularExpressionValidator ID="revNameSpaceName" runat="server" EnableViewState="false"
                        Display="dynamic" ControlToValidate="txtNamespaceName" />
                </div>
            </div>
            <div class="form-group">
                <div class="editing-form-label-cell">
                    <cms:LocalizedLabel CssClass="control-label" DisplayColon="True" runat="server" ID="lblCodeName" />
                </div>
                <div class="editing-form-value-cell">
                    <cms:CMSTextBox runat="server" ID="txtCodeName" MaxLength="50" />
                    <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" EnableViewState="false"
                        ControlToValidate="txtCodeName" Display="dynamic" />
                    <cms:CMSRegularExpressionValidator ID="revCodeName" runat="server" EnableViewState="false"
                        Display="dynamic" ControlToValidate="txtCodeName" />
                </div>
                <div class="editing-form-value-cell">
                    <asp:Button ID="btnCreateCustomTable" Text="Create Custom Table" runat="server" CssClass="btn btn-primary"  OnClick="btnCreateCustomTable_Click" />
                </div>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</div>
