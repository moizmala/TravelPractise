using System;

using CMS.Helpers;
using CMS.UIControls;

public partial class CMSAdminControls_Debug_WebFarmLog : WebFarmLog
{
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        Visible = false;

        var dt = GetLogData();
        if (dt != null)
        {
            Visible = true;

            gridQueries.Columns[1].HeaderText = GetString("WebFarmLog.TaskType");
            gridQueries.Columns[2].HeaderText = GetString("WebFarmLog.Target");
            gridQueries.Columns[3].HeaderText = GetString("WebFarmLog.TextData");
            gridQueries.Columns[4].HeaderText = GetString("General.Context");

            if (DisplayHeader)
            {
                ltlInfo.Text = "<div class=\"LogInfo\">" + GetString("WebFarmLog.Info") + "</div>";
            }

            gridQueries.DataSource = dt;
            gridQueries.DataBind();
        }
    }
}