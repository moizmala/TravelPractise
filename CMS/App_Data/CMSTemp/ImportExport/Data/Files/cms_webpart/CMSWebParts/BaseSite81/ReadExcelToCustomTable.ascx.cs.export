using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.PortalControls;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.Membership;
using CMS.Base;
using CMS.CustomTables;
using CMS.Modules;
using CMS.EventLog;
using CMS.FormEngine;
using CMS.SiteProvider;
using Newtonsoft.Json;
using CMS.Search;

public partial class CMSWebParts_BaseSite81_ReadExcelToCustomTable : CMSAbstractWebPart
{
    string docName = string.Empty;
    string worksheetName = "TransactionLog";
    string className = "CustomerInformation.TransactionLog";
    string displayName = "TransactionLog";
    string errorMessage = null;

    //Declare variables to hold refernces to Excel objects.
    Workbook workBook;
    SharedStringTable sharedStrings;
    IEnumerable<Sheet> workSheets;
    WorksheetPart infoSheet;

    #region "Private properties"

    /// <summary>
    /// Edited DataClassInfo
    /// </summary>
    private DataClassInfo DataClassInfo
    {
        get;
        set;
    }

    /// <summary>
    /// Name of the new created class.
    /// </summary>
    private string ClassName
    {
        get
        {
            object obj = ViewState["ClassName"];
            return (obj == null) ? string.Empty : (string)obj;
        }

        set
        {
            ViewState["ClassName"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set controls of the first step
        if (!RequestHelper.IsPostBack())
        {
            lblDisplayName.Text = GetString("customtable.newwizzard.DisplayName");
            lblNamespaceName.Text = GetString("customtable.newwizzard.NamespaceName");
            lblCodeName.Text = GetString("customtable.newwizzard.CodeName");

            // Set validators' error messages
            rfvDisplayName.ErrorMessage = GetString("customtable.newwizzard.ErrorEmptyDisplayName");
            rfvCodeName.ErrorMessage = GetString("customtable.newwizzard.ErrorEmptyCodeName");
            rfvNamespaceName.ErrorMessage = GetString("customtable.newwizzard.ErrorEmptyNamespaceName");
            revNameSpaceName.ErrorMessage = GetString("customtable.newwizzard.NamespaceNameIdentifier");
            revCodeName.ErrorMessage = GetString("customtable.newwizzard.CodeNameIdentifier");

            txtNamespaceName.Text = "Customer";
            txtCodeName.Text = txtDisplayName.Text;

        }

        // Set regular expression for identifier validation
        revNameSpaceName.ValidationExpression = ValidationHelper.IdentifierRegExp.ToString();
        revCodeName.ValidationExpression = ValidationHelper.IdentifierRegExp.ToString();

        var objectType = GetObjectType();

        DataClassInfo = DataClassInfo.New(objectType);
        DataClassInfo.ClassFormDefinition = FormInfo.GetEmptyFormDocument().OuterXml;

        // Set edited object
        EditedObject = DataClassInfo;

    }

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }

    /// <summary>
    /// Initializes control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            MediaSelector1.DialogConfig.HideAttachments = true;
            MediaSelector1.DialogConfig.HideContent = true;
            MediaSelector1.ShowInformation("Please select the file from Media folder");
        }
    }

    /// <summary>
    /// Gets the object type of the created class
    /// </summary>
    private string GetObjectType()
    {
        string objectType = DataClassInfo.OBJECT_TYPE;

        objectType = DataClassInfo.OBJECT_TYPE_CUSTOMTABLE;
        
        return objectType;
    }

    /// <summary>
    /// Indicates if table has identity column defined
    /// </summary>
    /// <param name="tableName">Table name</param>
    /// <param name="columnName">Column name</param>
    /// <returns>Returns TRUE if table has identity column</returns>
    private static bool IsIdentityColumn(string tableName, string columnName)
    {
        const string queryText = @"SELECT COLUMNPROPERTY(OBJECT_ID(@tableName), @columnName, 'IsIdentity')";

        var queryData = new QueryDataParameters
        {
            { "tableName", tableName },
            { "columnName", columnName }
        };

        var result = ConnectionHelper.ExecuteScalar(queryText, queryData, QueryTypeEnum.SQLQuery);
        return ValidationHelper.GetBoolean(result, false);
    }

    /// <summary>
    /// Creates an empty form info for the new class
    /// </summary>
    private FormInfo CreateEmptyFormInfo()
    {
        // Create empty form definition
        var fi = new FormInfo();

        var ffiPK = new FormFieldInfo();

        // Fill FormInfo object
        ffiPK.Name = ValidationHelper.GetString(ViewState["PrimarykeyName"],string.Empty);
        ffiPK.SetPropertyValue(FormFieldPropertyEnum.FieldCaption, ValidationHelper.GetString(ViewState["PrimarykeyName"],string.Empty));
        ffiPK.DataType = FieldDataType.Integer;
        ffiPK.SetPropertyValue(FormFieldPropertyEnum.DefaultValue, string.Empty);
        ffiPK.SetPropertyValue(FormFieldPropertyEnum.FieldDescription, string.Empty);
        ffiPK.FieldType = FormFieldControlTypeEnum.CustomUserControl;
        ffiPK.Settings["controlname"] = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLowerCSafe();
        ffiPK.PrimaryKey = true;
        ffiPK.System = false;
        ffiPK.Visible = false;
        ffiPK.Size = 0;
        ffiPK.AllowEmpty = false;

        // Add field to form definition
        fi.AddFormItem(ffiPK);

        return fi;
    }

    /// <summary>
    /// Updates the inherited class fields if the class is inherited
    /// </summary>
    /// <param name="dci">DataClassInfo to update</param>
    private static void UpdateInheritedClass(DataClassInfo dci)
    {
        // Ensure inherited fields
        if (dci.ClassInheritsFromClassID > 0)
        {
            var parentCi = DataClassInfoProvider.GetDataClassInfo(dci.ClassInheritsFromClassID);
            if (parentCi != null)
            {
                FormHelper.UpdateInheritedClass(parentCi, dci);
            }
        }
    }

    /// <summary>
    /// Creates the GUID field
    /// </summary>
    private static FormFieldInfo CreateGuidField()
    {
        FormFieldInfo ffiGuid = new FormFieldInfo();

        // Fill FormInfo object
        ffiGuid.Name = "ItemGUID";
        ffiGuid.SetPropertyValue(FormFieldPropertyEnum.FieldCaption, "GUID");
        ffiGuid.DataType = FieldDataType.Guid;
        ffiGuid.SetPropertyValue(FormFieldPropertyEnum.DefaultValue, string.Empty);
        ffiGuid.SetPropertyValue(FormFieldPropertyEnum.FieldDescription, String.Empty);
        ffiGuid.FieldType = FormFieldControlTypeEnum.CustomUserControl;
        ffiGuid.Settings["controlname"] = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLowerCSafe();
        ffiGuid.PrimaryKey = false;
        ffiGuid.System = true;
        ffiGuid.Visible = false;
        ffiGuid.Size = 0;
        ffiGuid.AllowEmpty = false;

        return ffiGuid;
    }

    /// <summary>
    /// Creates the Custom field
    /// </summary>
    private static FormFieldInfo CreateCustomField(string FieldName, string FieldCaption, string FieldType)
    {
        FormFieldInfo ffiCustom = new FormFieldInfo();

        // Fill FormInfo object
        ffiCustom.Name = FieldName;
        ffiCustom.SetPropertyValue(FormFieldPropertyEnum.FieldCaption, FieldCaption);
         
        switch(FieldType)
        {
            case "guid":
                ffiCustom.DataType = FieldDataType.Guid;
                break;
            case "text":
                ffiCustom.DataType = FieldDataType.Text;
                break;
            case "datetime":
                ffiCustom.DataType = FieldDataType.DateTime;
                break;
            case "float":
                ffiCustom.DataType = FieldDataType.Decimal;
                break;
            case "integer":
                ffiCustom.DataType = FieldDataType.Integer;
                break;
        }

        ffiCustom.SetPropertyValue(FormFieldPropertyEnum.DefaultValue, string.Empty);
        ffiCustom.SetPropertyValue(FormFieldPropertyEnum.FieldDescription, String.Empty);
        ffiCustom.FieldType = FormFieldControlTypeEnum.CustomUserControl;
        ffiCustom.Settings["controlname"] = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLowerCSafe();
        ffiCustom.PrimaryKey = false;
        ffiCustom.System = false;
        ffiCustom.Visible = false;
        ffiCustom.Size = 0;
        ffiCustom.AllowEmpty = true;

        return ffiCustom;
    }

    /// <summary>
    /// Initializes the custom table
    /// </summary>
    /// <param name="dci">DataClassInfo of the custom table</param>
    /// <param name="fi">Form info</param>
    /// <param name="tm">Table manager</param>
    private void InitCustomTable(DataClassInfo dci, FormInfo fi, TableManager tm)
    {
        // Custom Fields 
        if (!fi.FieldExists("Number"))
        {
            var ffiCustom = CreateCustomField("Number", "Number", "integer");

            fi.AddFormItem(ffiCustom);
        }

        if (!fi.FieldExists("Date"))
        {
            var ffiCustom = CreateCustomField("Date", "Date", "datetime");

            fi.AddFormItem(ffiCustom);
        }

        if (!fi.FieldExists("Customer"))
        {
            var ffiCustom = CreateCustomField("Customer", "Customer", "text");

            fi.AddFormItem(ffiCustom);
        }

        if (!fi.FieldExists("Amount"))
        {
            var ffiCustom = CreateCustomField("Amount", "Amount", "float");

            fi.AddFormItem(ffiCustom);
        }

        if (!fi.FieldExists("Status"))
        {
            var ffiCustom = CreateCustomField("Status", "Status", "text");

            fi.AddFormItem(ffiCustom);
        }

        // Created by
        if (!fi.FieldExists("ItemCreatedBy"))
        {
            FormFieldInfo ffi = new FormFieldInfo();

            // Fill FormInfo object
            ffi.Name = "ItemCreatedBy";
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldCaption, "Created by");
            ffi.DataType = FieldDataType.Integer;
            ffi.SetPropertyValue(FormFieldPropertyEnum.DefaultValue, string.Empty);
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldDescription, string.Empty);
            ffi.FieldType = FormFieldControlTypeEnum.CustomUserControl;
            ffi.Settings["controlname"] = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLowerCSafe();
            ffi.PrimaryKey = false;
            ffi.System = true;
            ffi.Visible = false;
            ffi.Size = 0;
            ffi.AllowEmpty = true;

            fi.AddFormItem(ffi);
        }

        // Created when
        if (!fi.FieldExists("ItemCreatedWhen"))
        {
            FormFieldInfo ffi = new FormFieldInfo();

            // Fill FormInfo object
            ffi.Name = "ItemCreatedWhen";
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldCaption, "Created when");
            ffi.DataType = FieldDataType.DateTime;
            ffi.SetPropertyValue(FormFieldPropertyEnum.DefaultValue, string.Empty);
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldDescription, string.Empty);
            ffi.FieldType = FormFieldControlTypeEnum.CustomUserControl;
            ffi.Settings["controlname"] = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLowerCSafe();
            ffi.PrimaryKey = false;
            ffi.System = true;
            ffi.Visible = false;
            ffi.Size = 0;
            ffi.AllowEmpty = true;

            fi.AddFormItem(ffi);
        }

        // Modified by
        if (!fi.FieldExists("ItemModifiedBy"))
        {
            FormFieldInfo ffi = new FormFieldInfo();

            // Fill FormInfo object
            ffi.Name = "ItemModifiedBy";
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldCaption, "Modified by");
            ffi.DataType = FieldDataType.Integer;
            ffi.SetPropertyValue(FormFieldPropertyEnum.DefaultValue, string.Empty);
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldDescription, string.Empty);
            ffi.FieldType = FormFieldControlTypeEnum.CustomUserControl;
            ffi.Settings["controlname"] = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLowerCSafe();
            ffi.PrimaryKey = false;
            ffi.System = true;
            ffi.Visible = false;
            ffi.Size = 0;
            ffi.AllowEmpty = true;

            fi.AddFormItem(ffi);
        }

        // Modified when
        if (!fi.FieldExists("ItemModifiedWhen"))
        {
            FormFieldInfo ffi = new FormFieldInfo();

            // Fill FormInfo object
            ffi.Name = "ItemModifiedWhen";
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldCaption, "Modified when");
            ffi.DataType = FieldDataType.DateTime;
            ffi.SetPropertyValue(FormFieldPropertyEnum.DefaultValue, string.Empty);
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldDescription, string.Empty);
            ffi.FieldType = FormFieldControlTypeEnum.CustomUserControl;
            ffi.Settings["controlname"] = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLowerCSafe();
            ffi.PrimaryKey = false;
            ffi.System = true;
            ffi.Visible = false;
            ffi.Size = 0;
            ffi.AllowEmpty = true;

            fi.AddFormItem(ffi);
        }

        // Item order
        if (!fi.FieldExists("ItemOrder"))
        {
            FormFieldInfo ffi = new FormFieldInfo();

            // Fill FormInfo object
            ffi.Name = "ItemOrder";
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldCaption, "Order");
            ffi.DataType = FieldDataType.Integer;
            ffi.SetPropertyValue(FormFieldPropertyEnum.DefaultValue, string.Empty);
            ffi.SetPropertyValue(FormFieldPropertyEnum.FieldDescription, string.Empty);
            ffi.FieldType = FormFieldControlTypeEnum.CustomUserControl;
            ffi.Settings["controlname"] = Enum.GetName(typeof(FormFieldControlTypeEnum), FormFieldControlTypeEnum.LabelControl).ToLowerCSafe();
            ffi.PrimaryKey = false;
            ffi.System = true;
            ffi.Visible = false;
            ffi.Size = 0;
            ffi.AllowEmpty = true;

            fi.AddFormItem(ffi);
        }

        // GUID
        if (!fi.FieldExists("ItemGUID"))
        {
            var ffiGuid = CreateGuidField();

            fi.AddFormItem(ffiGuid);
        }

        // Update table structure - columns could be added
        bool old = TableManager.UpdateSystemFields;

        TableManager.UpdateSystemFields = true;

        string schema = fi.GetXmlDefinition();
        tm.UpdateTableByDefinition(dci.ClassTableName, schema);

        TableManager.UpdateSystemFields = old;

        // Update xml schema and form definition
        dci.ClassFormDefinition = schema;
        dci.ClassXmlSchema = tm.GetXmlSchema(dci.ClassTableName);

        using (CMSActionContext context = new CMSActionContext())
        {
            // Disable logging into event log
            context.LogEvents = false;

            DataClassInfoProvider.SetDataClassInfo(dci);
        }
    }

    protected void CreateCustomTable()
    {
        if (ViewState["UserInformation"] == null)
        {
            //plcMess.AddError("Customer Information is required.");
            plcMess.ShowMessage(CMS.ExtendedControls.MessageTypeEnum.Error, "Customer Information is required.");
            return;
        }

        className = txtNamespaceName.Text + "." + txtCodeName.Text;

        if (DataClassInfoProvider.GetDataClassInfo(className) != null)
        {
            errorMessage = GetString("sysdev.class_edit_gen.codenameunique");
            plcMessCustomTable.ShowMessage(CMS.ExtendedControls.MessageTypeEnum.Error, errorMessage);
        }
        else
        {

            var errorMsg = String.Empty;

            try
            {
                using (var tr = new CMSTransactionScope())
                {
                    // Set new class info
                    DataClassInfo.ClassDisplayName = txtDisplayName.Text;
                    DataClassInfo.ClassName = className;
                    DataClassInfo.ClassTableName = txtNamespaceName.Text.Trim() + "_" + txtCodeName.Text.Trim();

                    // Set class type according development mode setting
                    DataClassInfo.ClassShowAsSystemTable = false;
                    DataClassInfo.ClassShowTemplateSelection = false;
                    DataClassInfo.ClassIsDocumentType = false;
                    DataClassInfo.ClassCreateSKU = false;
                    DataClassInfo.ClassIsProduct = false;
                    DataClassInfo.ClassIsMenuItemType = false;
                    DataClassInfo.ClassUsesVersioning = false;
                    DataClassInfo.ClassUsePublishFromTo = false;
                    // Sets custom table
                    DataClassInfo.ClassIsCustomTable = true;

                    // Insert new class into DB
                    DataClassInfoProvider.SetDataClassInfo(DataClassInfo);

                    PermissionNameInfoProvider.CreateDefaultCustomTablePermissions(DataClassInfo.ClassID);

                    tr.Commit();
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                EventLogProvider.LogException("NewCustomTable", "CREATE", ex);
            }

            if (errorMsg == "")
            {
                // Save ClassName to viewstate to use in the next steps
                ClassName = DataClassInfo.ClassName;

                var dci = DataClassInfoProvider.GetDataClassInfo(ClassName);
                if (dci != null)
                {
                    var tm = new TableManager(null);

                    using (var tr = new CMSTransactionScope())
                    {
                        // Custom tables have always ItemID as primary key
                        string primarykeyName = TextHelper.FirstLetterToUpper(txtCodeName.Text.Trim() + "ID");
                        ViewState["PrimarykeyName"] = primarykeyName;

                        try
                        {
                            string tableName =  txtNamespaceName.Text.Trim() + "_" + txtCodeName.Text.Trim();
                            bool tableExists = tm.TableExists(tableName);
                            // Custom table from existing table - validate the table name
                            if (!tableExists)
                            {
                                tm.CreateTable(tableName, primarykeyName);
                            }

                            string owner = "";
                            owner = SqlHelper.GetDBSchema(SiteContext.CurrentSiteName);
                            if ((owner != "") && (owner.ToLowerCSafe() != "dbo"))
                            {
                                tm.ChangeDBObjectOwner(tableName, owner);
                                tableName = SqlHelper.GetSafeOwner(owner) + "." + tableName;
                            }

                            FormInfo fi;
                            // Create empty form info
                            fi = CreateEmptyFormInfo();

                            dci.ClassXmlSchema = tm.GetXmlSchema(tableName);

                            dci.ClassTableName = tableName;
                            dci.ClassFormDefinition = fi.GetXmlDefinition();
                            dci.ClassIsCoupledClass = true;

                            //dci.ClassInheritsFromClassID = ValidationHelper.GetInteger(selInherits.Value, 0);

                            // Update class in DB
                            using (var context = new CMSActionContext())
                            {
                                // Disable logging into event log
                                context.LogEvents = false;

                                DataClassInfoProvider.SetDataClassInfo(dci);

                                UpdateInheritedClass(dci);
                            }

                            try
                            {
                                InitCustomTable(dci, fi, tm);
                            }
                            catch (Exception ex)
                            {
                                EventLogProvider.LogException("NewClassWizard", "CREATE", ex);

                                string message = null;
                                if (ex is MissingSQLTypeException)
                                {
                                    var missingSqlType = (MissingSQLTypeException)ex;
                                    message = String.Format(GetString("customtable.sqltypenotsupported"), missingSqlType.UnsupportedType, missingSqlType.ColumnName, missingSqlType.RecommendedType);
                                }
                                else
                                {
                                    message = ex.Message;
                                }
                            }
                            tr.Commit();


                            //Assign to Site
                            ClassSiteInfoProvider.AddClassToSite(dci.ClassID, SiteContext.CurrentSiteID);

                            // Save default search settings
                            dci.ClassSearchEnabled = true;
                            dci.ClassSearchSettings = SearchHelper.GetDefaultSearchSettings(dci);
                            SearchHelper.SetDefaultClassSearchColumns(dci);
                            dci.Generalized.SetObject();

                        }
                        catch (Exception ex)
                        {
                            // Show error message if something caused unhandled exception
                            ShowError(ex.Message);
                        }
                    }
                }

                //Save Customer Information
                List<CustomerInformation> myDeserializedObjCustomerList;

                try
                {
                    myDeserializedObjCustomerList = (List<CustomerInformation>)Newtonsoft.Json.JsonConvert.DeserializeObject(ViewState["UserInformation"].ToString(), typeof(List<CustomerInformation>));

                    if (myDeserializedObjCustomerList.Count > 0)
                    {
                        SaveCustomerInformation(myDeserializedObjCustomerList);
                    }
                }
                catch (Exception ex)
                {
                    plcMess.ShowMessage(CMS.ExtendedControls.MessageTypeEnum.Error, ex.Message);
                }

            }
        }
    }

    private bool SaveCustomerInformation(List<CustomerInformation> customerInformationList)
    {
        // Check if Custom table 'Created table' exists
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(className);
        if (customTable != null)
        {
            foreach (CustomerInformation ci in customerInformationList)
            {
                // Create new custom table item 
                CustomTableItem newCustomTableItem = CustomTableItem.New(className);

                // Set the ItemText field value
                newCustomTableItem.SetValue("Number", ci.Number);
                newCustomTableItem.SetValue("Date", ci.Date);
                newCustomTableItem.SetValue("Customer", ci.Customer);
                newCustomTableItem.SetValue("Amount", ci.Amount);
                newCustomTableItem.SetValue("Status", ci.Status);

                // Insert the custom table item into database
                newCustomTableItem.Insert();
            }

            return true;
        }
        return false;
    }

    protected void btnReadCustomerInformation_Click(object sender, EventArgs e)
    {
        docName = MediaSelector1.Text.IndexOf("?") > -1 ? Server.MapPath(MediaSelector1.Text.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[0]) : string.Empty;
        if (docName.Length == 0)
        {
            MediaSelector1.ShowMessage(CMS.ExtendedControls.MessageTypeEnum.Error, "No file selected", "Please browse the file in Media folder by clicking select button", "No file selected", true);
            return;
        }

        //Declare helper variables.
        string customerID;
        List<CustomerInformation> customers;
        //Open the Excel workbook.
        using (SpreadsheetDocument document =
          SpreadsheetDocument.Open(docName, true))
        {
            //References to the workbook and Shared String Table.
            workBook = document.WorkbookPart.Workbook;
            workSheets = workBook.Descendants<Sheet>();
            sharedStrings =
              document.WorkbookPart.SharedStringTablePart.SharedStringTable;

            //Reference to Excel Worksheet with Customer data.
            customerID =
              workSheets.First(s => s.Name == worksheetName).Id;
            infoSheet =
              (WorksheetPart)document.WorkbookPart.GetPartById(customerID);

            //Load customer data to business object.
            customers =
              CustomerInformation.LoadCustomerList(infoSheet.Worksheet, sharedStrings);

            gridUserInformation.DataSource = customers;
            gridUserInformation.DataBind();

            ViewState["UserInformation"] = JsonConvert.SerializeObject(customers);
            //Check1
            //var user = UserInfoProvider.GetUserInfo("administrator");
            //using (new CMSActionContext(user))
            //{
            //    var item = CustomTableItem.New("CustomTables.CustomerInformation");
            //    item.SetValue("Number", "Sample text");
            //    item.Insert();
            //}

            //DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(customTableClassName); 
        }
    }

    /// <summary>
    /// Used to store customer information for analysis.
    /// </summary>
    public class CustomerInformation
    {
        //Properties.
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public float Amount { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// Helper method for creating a list of customers 
        /// from an Excel worksheet.
        /// </summary>
        public static List<CustomerInformation> LoadCustomerList(Worksheet worksheet,
          SharedStringTable sharedString)
        {
            //Initialize the customer list.
            List<CustomerInformation> result = new List<CustomerInformation>();

            //LINQ query to skip first row with column names.
            IEnumerable<Row> dataRows =
              from row in worksheet.Descendants<Row>()
              where row.RowIndex > 1
              select row;

            foreach (Row row in dataRows)
            {
                //LINQ query to return the row's cell values.
                //Where clause filters out any cells that do not contain a value.
                //Select returns the value of a cell unless the cell contains
                //  a Shared String.
                //If the cell contains a Shared String, its value will be a 
                //  reference id which will be used to look up the value in the 
                //  Shared String table.
                IEnumerable<String> textValues =
                  from cell in row.Descendants<Cell>()
                  where cell.CellValue != null
                  select
                    (cell.DataType != null
                      && cell.DataType.HasValue
                      && cell.DataType == CellValues.SharedString
                    ? sharedString.ChildElements[
                      int.Parse(cell.CellValue.InnerText)].InnerText
                    : cell.CellValue.InnerText)
                  ;

                //Check to verify the row contained data.
                if (textValues.Count() > 0)
                {
                    //Create a customer and add it to the list.
                    var textArray = textValues.ToArray();
                    CustomerInformation ci = new CustomerInformation();
                    ci.Number = ValidationHelper.GetInteger(textArray[0],0);
                    ci.Date = ValidationHelper.GetDateTime(DateTime.FromOADate(Convert.ToDouble(textArray[1])), DateTime.MinValue);
                    ci.Customer = ValidationHelper.GetString(textArray[2], string.Empty);
                    ci.Amount = ValidationHelper.GetFloat(textArray[3], 0);
                    ci.Status = ValidationHelper.GetString(textArray[4], string.Empty);
                    result.Add(ci);
                }
                else
                {
                    //If no cells, then you have reached the end of the table.
                    break;
                }
            }

            //Return populated list of customers.
            return result;
        }
    }
    protected void btnCreateCustomTable_Click(object sender, EventArgs e)
    {
        CreateCustomTable();
    }
}