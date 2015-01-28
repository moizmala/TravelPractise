using System;

using CMS.Membership;
using CMS.UIControls;

[UIElement("CMS.Staging", "All")]
public partial class CMSModules_Staging_Tools_AllTasks_Frameset : CMSStagingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Manage object tasks' permission
        if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.staging", "ManageAllTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageAllTasks");
        }
    }
}