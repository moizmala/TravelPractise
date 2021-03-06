using System;

using CMS.Base;
using CMS.ExtendedControls;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.Synchronization;
using CMS.UIControls;

[UIElement("CMS.Staging", "All")]
public partial class CMSModules_Staging_Tools_AllTasks_TaskSeparator : CMSStagingPage
{
    #region "Properties"

    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check enabled servers
        if (!ServerInfoProvider.IsEnabledServer(SiteContext.CurrentSiteID))
        {
            ShowInformation(GetString("ObjectStaging.NoEnabledServer"));
        }
        else
        {
            // Check DLL required for for staging
            if (SystemContext.IsFullTrustLevel)
            {
                URLHelper.Redirect("Frameset.aspx");
            }

            ShowInformation(GetString("objectstaging.fulltrustrequired"));
        }
    }
}