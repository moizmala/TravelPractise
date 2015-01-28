using System;

using CMS.DataEngine;
using CMS.FormControls;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSFormControls_Classes_SelectClass : FormEngineUserControl
{
    #region "Variables"

    private bool mOnlyCustomTables;
    private bool mOnlyDocumentTypes = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the selector where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            object obj = ViewState["WhereCondition"];
            return (obj == null) ? string.Empty : (string)obj;
        }

        set
        {
            ViewState["WhereCondition"] = value;
        }
    }


    /// <summary>
    /// Indicates whether singledropdownlist uses auto-complete mode.
    /// </summary>
    public bool UseUniSelectorAutocomplete
    {
        get
        {
            return uniSelector.UseUniSelectorAutocomplete;
        }
        set
        {
            uniSelector.UseUniSelectorAutocomplete = value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            if (uniSelector != null)
            {
                uniSelector.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Returns ClientID of the textbox with class names.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return uniSelector.TextBoxSelect.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return uniSelector.Value;
        }
        set
        {
            if (uniSelector == null)
            {
                pnlUpdate.LoadContainer();
            }
            uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets inner uni selector.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return uniSelector;
        }
    }


    /// <summary>
    /// Gets the inner DDL control.
    /// </summary>
    public CMSDropDownList DropDownSingleSelect
    {
        get
        {
            return uniSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// If true, only Custom tables are loaded.
    /// </summary>
    public bool OnlyCustomTables
    {
        get
        {
            return mOnlyCustomTables;
        }
        set
        {
            mOnlyCustomTables = value;
            mOnlyDocumentTypes = !value;
        }
    }


    /// <summary>
    /// If true, only Document types are loaded.
    /// </summary>
    public bool OnlyDocumentTypes
    {
        get
        {
            return mOnlyDocumentTypes;
        }
        set
        {
            mOnlyDocumentTypes = value;
            mOnlyCustomTables = !value;
        }
    }


    /// <summary>
    /// Gets or sets whether (all) field is in drop down list.
    /// </summary>
    public bool DisplayAllValue
    {
        get
        {
            return uniSelector.AllowAll;
        }
        set
        {
            uniSelector.AllowAll = value;
        }
    }


    /// <summary>
    /// Gets or sets whether (none) field is in drop down list.
    /// </summary>
    public bool DisplayNoneValue
    {
        get
        {
            return uniSelector.AllowEmpty;
        }
        set
        {
            uniSelector.AllowEmpty = value;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            EnsureChildControls();
            base.IsLiveSite = value;
            UniSelector.IsLiveSite = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    /// <param name="reloadUniSelector">If true, UniSelector is also reloaded</param>
    public void ReloadData(bool reloadUniSelector = false)
    {
        uniSelector.IsLiveSite = IsLiveSite;
        string whereCondition = String.Empty;

        if (OnlyDocumentTypes)
        {
            whereCondition = "ClassIsDocumentType = 1";
        }
        else if (OnlyCustomTables)
        {
            whereCondition = "ClassIsCustomTable = 1";
        }

        // Combine default where condition with external
        if (!String.IsNullOrEmpty(WhereCondition))
        {
            whereCondition = SqlHelper.AddWhereCondition(whereCondition, WhereCondition);
        }

        uniSelector.WhereCondition = whereCondition;

        if (reloadUniSelector)
        {
            uniSelector.Reload(true);
        }
    }
}