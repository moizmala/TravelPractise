<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationDashboard.aspx.cs" Inherits="CMSModules_ApplicationDashboard_ApplicationDashboard"
    Theme="Default" EnableEventValidation="false"
    MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="ApplicationDashboard" EnableViewState="false" %>

<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <%-- Welcome tile --%>
    <script type="text/ng-template" id="welcomeTileTemplate.html">
        <div class="tile" data-ng-show="model.visible">
            <div class="welcome-tile">
                <a href="javascript:void(0)" data-ng-click="model.hide()">
                    <i aria-hidden="true" class="icon-modal-close"></i>
                </a>
                <h2>{{model.header}}</h2>
                <p class="lead">{{model.description}}</p>
                <ul>
                    <li><i aria-hidden="true" class="icon-kentico"></i><span><a class="js-app-list-link" href="javascript:void(0)" data-ng-click="model.showApplicationList()">{{model.browseApplicationsText}}</a></span></li>
                    <li><i aria-hidden="true" class="icon-question-circle"></i><span><a class="js-context-help-link" href="javascript:void(0)" data-ng-click="model.showContextHelp()">{{model.openHelpText}}</a></span></li>
                </ul>
            </div>
        </div>
    </script>
    
    <%-- General tile --%>
    <script type="text/ng-template" id="tileTemplate.html">
        <div data-ng-class="{'editable-mode':isEditableMode, 'shrinked':shrinked }" >
            <div data-ng-class="{'tile-shrink':isEditableMode && !(hover || active )}" class="tile-background" >
            </div>      
            <div class="tile-header-panel" data-ng-show="isEditableMode && (hover || active)" >
                <button type="button" class="icon-only btn-icon btn" title="<%= GetString("cms.dashboard.removeApplication") %>" data-ng-click="removeTile()">
                    <i aria-hidden="true" class="icon-modal-close"></i><span class="sr-only"><%= GetString("cms.dashboard.removeApplication") %></span>
                </button>
            </div>
            <div class="tile-mask" data-ng-class="{'tile-shrink': isEditableMode}" >
                <div class="tile-wrapper">        
                    <div class="tile-dead">
                        <a class="tile-btn tile-dead-btn {{ tileModel.ListItemCssClass }}" data-ng-class="{'editable-mode':isEditableMode}" data-ng-href="{{ tileModel.Path }}" target="_top">
                            <i aria-hidden="true" class="{{ tileModel.IconCssClass }} cms-icon-200 tile-icon tile-dead-icon"></i>
                            <h3>{{ tileModel.DisplayName }}</h3>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </script>

    <%-- Live part of the tile --%>
    <script type="text/ng-template" id="liveTileTemplate.html">
        <div class="tile-live">
            <a class="tile-btn tile-live-btn {{ tileModel.ListItemCssClass }}" data-ng-href="{{ tileModel.Path }}" target="_top">
                <div class="clearfix">
                    <i aria-hidden="true" class="{{ tileModel.IconCssClass }} cms-icon-150 tile-icon tile-live-icon"></i>
                    <span class="tile-live-title-container">
                        <h3>{{ tileModel.DisplayName }}</h3>
                    </span>
                </div>
                <h4 class="tile-live-property tile-live-value">{{ liveTileModel.ShortenedValue }}</h4>
                <div class="tile-live-property tile-live-description">{{ liveTileModel.Description }}</div>
            </a>
        </div>      
    </script>

    <%-- Dashboard layout --%>
    <script type="text/ng-template" id="dashboard.html">
        <div class="dashboard">
            <div id="dashboard-drag-area" class="dashboard-inner" data-ng-class="{'edit-mode' : model.isEditableMode}">
                <div data-welcometile></div>
                <ul data-ui-sortable="model.sortableOptions" data-ng-model="model.tiles">
                    <li class="tile" data-ng-repeat="tile in model.tiles" data-remove="model.removeTile($index)">
                        <div data-tile="tile" data-guid="{{tile.Guid}}" class="tile-outer-wrapper" >
                        </div>
                    </li>
            
                    <%-- Add 'new app' tile --%>
                    <li data-ng-show="model.isEditableMode" data-ng-click="model.openApplicationsList()" class="tile tile-shrink" data-no-drag="no-drag">
                        <a class="tile-dead-btn tile-btn tile-btn-add" href="javascript:void(0);" title="<%= GetString("cms.dashboard.addapp") %>">
                            <i aria-hidden="true" class="icon-plus cms-icon-200 tile-icon"></i><span class="sr-only"><%= GetString("cms.dashboard.addapp") %></span>
                        </a>
                    </li>
            

                </ul>
            </div>
            <%-- Edit mode button --%>
            <div class="btn-group dropup anchor-dropup pull-right">
                <button data-ng-click="model.toggleEditableMode()" type="button" class="btn btn-edit-mode btn-default icon-only {{model.isEditableMode ? 'active' : ''}}" data-edit-btn="edit-btn" data-ng-disabled="model.isEditableModeButtonDisabled" title="<%= GetString("cms.dashboard.edit") %>">
                    <i aria-hidden="true" class="icon-cogwheel"></i><span class="sr-only"><%= GetString("cms.dashboard.edit") %></span>
                </button>
            </div>
        </div>   
    </script>
    
    <div id="container" data-ng-view="ng-view"></div>

    <asp:Panel runat="server" ID="pnlLicenseOwner" Visible="False">
        <cms:LocalizedLabel runat="server" ID="lblLicenseOwner" CssClass="license-owner-info" />
    </asp:Panel>
</asp:Content>