using System;

using CMS.ExtendedControls;
using CMS.Helpers;
using CMS.Membership;
using CMS.UIControls;

[UIElement("CMS.Staging", "Documents")]
public partial class CMSModules_Staging_Tools_Tasks_Frameset : CMSStagingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        // check 'Manage servers' permission
        if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.staging", "ManageDocumentsTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageDocumentsTasks");
        }

        ScriptHelper.HideVerticalTabs(this);
    }
}