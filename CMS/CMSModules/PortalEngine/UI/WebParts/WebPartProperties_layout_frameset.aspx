<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_WebPartProperties_layout_frameset"
    ValidateRequest="false" CodeFile="WebPartProperties_layout_frameset.aspx.cs" %>

<!DOCTYPE html>
<html>
<head runat="server" enableviewstate="false">
    <title>Untitled Page</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
    </style>
</head>
<frameset border="0" runat="server" rows="49,*,60" id="rowsFrameset">
    <frame name="webpartpropertiesmenu" frameborder="0" noresize="noresize" scrolling="no"
        runat="server" id="frameMenu" />
    <frame name="webpartpropertiescontent" frameborder="0" noresize="noresize" scrolling="no"
        runat="server" id="frameContent" />
    <frame name="webpartpropertiesbuttons" frameborder="0" noresize="noresize" scrolling="no"
        runat="server" id="frameButtons" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
