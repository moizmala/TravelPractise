<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Staging_Tools_AllTasks_Frameset"
    EnableViewState="false" CodeFile="Frameset.aspx.cs" %>

<!DOCTYPE html>
<html>
<head runat="server" enableviewstate="false">
    <title>Content staging</title>
</head>
<frameset border="0" rows="48, *" id="rowsFrameset">
    <frame name="tasksHeader" src="Header.aspx" frameborder="0" scrolling="no" noresize="noresize" />
    <frame name="tasksContent" src="Tasks.aspx" frameborder="0" runat="server" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
