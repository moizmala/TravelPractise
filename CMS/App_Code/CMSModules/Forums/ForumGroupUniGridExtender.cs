using CMS;
using CMS.ExtendedControls;
using CMS.UIControls;

[assembly: RegisterCustomClass("ForumGroupUniGridExtender", typeof(ForumGroupUniGridExtender))]

/// <summary>
/// This extender is no longer necessary
/// </summary>
public class ForumGroupUniGridExtender : ControlExtender<UniGrid>
{
    /// <summary>
    /// Obligatory member. Empty intentionally
    /// </summary>
    public override void OnInit()
    {
    }
}