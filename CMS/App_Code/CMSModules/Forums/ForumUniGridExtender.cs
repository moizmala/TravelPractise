using CMS;
using CMS.ExtendedControls;
using CMS.UIControls;

[assembly: RegisterCustomClass("ForumUniGridExtender", typeof(ForumUniGridExtender))]

/// <summary>
/// This extender is no longer necessary
/// </summary>
public class ForumUniGridExtender : ControlExtender<UniGrid>
{
    /// <summary>
    /// Obligatory member. Empty intentionally
    /// </summary>
    public override void OnInit()
    {
    }
}