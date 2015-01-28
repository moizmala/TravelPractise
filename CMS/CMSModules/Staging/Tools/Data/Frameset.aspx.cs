using System;

using CMS.ExtendedControls;
using CMS.Helpers;
using CMS.Membership;
using CMS.UIControls;

[UIElement("CMS.Staging", "Data")]
public partial class CMSModules_Staging_Tools_Data_Frameset : CMSStagingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        // Check 'Manage object tasks' permission
        if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.staging", "ManageDataTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageDataTasks");
        }

        ScriptHelper.HideVerticalTabs(this);
    }
}