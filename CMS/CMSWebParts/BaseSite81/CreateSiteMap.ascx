<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateSiteMap.ascx.cs" Inherits="CMSWebParts_BaseSite_CreateSiteMap" %>
<%@ Register src="../../CMSFormControls/MediaSelector.ascx" tagname="MediaSelector" tagprefix="uc1" %>
<link href="/CMSPages/GetResource.ashx?stylesheetfile=/App_Themes/Default/DesignMode.css" type="text/css" rel="stylesheet" />
<link href="/CMSPages/GetResource.ashx?stylesheetfile=/App_Themes/Default/bootstrap.css" type="text/css" rel="stylesheet" />
<cms:MessagesPlaceHolder ID="plcMess" runat="server" />
<div class="PageContent">
    <div class="editing-form-category-fields">
        <div class="form-group">
            <div class="editing-form-label-cell">
            <uc1:MediaSelector ID="MediaSelector1" runat="server" />
            <asp:Button ID="btnStartMapping" Text="Start Mapping" runat="server" 
                    CssClass="btn btn-primary" onclick="btnStartMapping_Click"  />
                
            </div>
        </div>
    </div>
</div>