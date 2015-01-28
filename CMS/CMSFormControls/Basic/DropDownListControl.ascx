<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DropDownListControl.ascx.cs"
    Inherits="CMSFormControls_Basic_DropDownListControl" %>
    <cms:CMSDropDownList ID="dropDownList" runat="server" CssClass="DropDownField" />
<div class="autocomplete">
    <cms:CMSTextBox ID="txtCombo" runat="server" Visible="false" />
    <i runat="server" id="btnAutocomplete" class="autocomplete-icon icon-ellipsis" Visible="false" />
</div>