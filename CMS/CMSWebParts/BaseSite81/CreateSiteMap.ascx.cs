using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.PortalControls;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using CMS.Membership;
using CMS.DocumentEngine;
using System.Data;
using CMS.SiteProvider;
using CMS.Helpers;
using System.IO;

public partial class CMSWebParts_BaseSite_CreateSiteMap : CMSAbstractWebPart
{
    string docName = string.Empty;
    string worksheetName = "Definition";

    //Declare variables to hold refernces to Excel objects.
    Workbook workBook;
    SharedStringTable sharedStrings;
    IEnumerable<Sheet> workSheets;
    WorksheetPart menusSheet;

    protected void Page_Load(object sender, EventArgs e)
    {

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

    private bool GetDocuments(string NodePath)
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser);

        // Fill dataset with documents
        DataSet documents = tree.SelectNodes(SiteContext.CurrentSiteName, NodePath, CMS.DocumentEngine.DocumentContext.CurrentDocumentCulture.CultureCode, false, "CMS.MenuItem");

        if (!DataHelper.DataSourceIsEmpty(documents))
            return true;
        else
            return false;
    }

    private void CreateDocument(string DocumentPath, string DocumentName)
    {
        // Create new instance of the Tree provider
        TreeProvider tree = new TreeProvider(MembershipContext.AuthenticatedUser);

        // Get parent node
        CMS.DocumentEngine.TreeNode parentNode = tree.SelectSingleNode(SiteContext.CurrentSiteName, DocumentPath, CMS.DocumentEngine.DocumentContext.CurrentDocumentCulture.CultureCode);
        if (parentNode != null)
        {
            //Check if document already created
            string currentDocumentPath = DocumentPath + "/" + DocumentName.Replace(" ", "-").Replace("&", "").Replace("--", "-").Replace("//", "/");
            currentDocumentPath = currentDocumentPath.StartsWith("//") ? currentDocumentPath.Replace("//", "/") : currentDocumentPath;
            CMS.DocumentEngine.TreeNode currentNode = tree.SelectSingleNode(SiteContext.CurrentSiteName, currentDocumentPath, CMS.DocumentEngine.DocumentContext.CurrentDocumentCulture.CultureCode);
            if (currentNode == null)
            {
                CMS.DocumentEngine.TreeNode newNode = CMS.DocumentEngine.TreeNode.New("CMS.MenuItem", tree);
                newNode.DocumentName = DocumentName;
                newNode.DocumentCulture = CMS.DocumentEngine.DocumentContext.CurrentDocumentCulture.CultureCode;
                newNode.Insert(parentNode);
            }
        }
    }

    /// <summary>
    /// Used to store customer information for analysis.
    /// </summary>
    public class MenuDefinition
    {
        //Properties.
        public string Number { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string LinkedWith { get; set; }

        /// <summary>
        /// Helper method for creating a list of customers 
        /// from an Excel worksheet.
        /// </summary>
        public static List<MenuDefinition> LoadMenus(Worksheet worksheet,
          SharedStringTable sharedString)
        {
            //Initialize the customer list.
            List<MenuDefinition> result = new List<MenuDefinition>();

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
                    MenuDefinition menu = new MenuDefinition();
                    menu.Number = textArray[0];
                    menu.Name = textArray[1];
                    menu.Level = textArray[2] == null ? "" : textArray[2];
                    menu.LinkedWith = textArray[3] == null ? "" : textArray[3];
                    result.Add(menu);
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



    protected void btnStartMapping_Click(object sender, EventArgs e)
    {
        docName = MediaSelector1.Text.IndexOf("?") > -1 ? Server.MapPath(MediaSelector1.Text.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[0]) : string.Empty;
        if (docName.Length == 0)
        {
            MediaSelector1.ShowMessage(CMS.ExtendedControls.MessageTypeEnum.Error, "No file selected", "Please browse the file in Media folder by clicking select button", "No file selected", true);
            return;
        }

        //Declare helper variables.
        string menuID;
        List<MenuDefinition> menus;
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
            menuID =
              workSheets.First(s => s.Name == worksheetName).Id;
            menusSheet =
              (WorksheetPart)document.WorkbookPart.GetPartById(menuID);

            //Load customer data to business object.
            menus =
              MenuDefinition.LoadMenus(menusSheet.Worksheet, sharedStrings);

            //LINQ Query for base Menu.
            IEnumerable<MenuDefinition> allMenus =
                from menulist in menus
                where menulist.Level == "0"
                select menulist;

            string BasePath = "/";
            if (allMenus.Count() > 0)
            {
                foreach (MenuDefinition md in allMenus)
                {
                    CreateDocument(BasePath, md.Name);
                }
            }

            //LINQ Query for Menu >> Level 1.
            IEnumerable<MenuDefinition> allMenusLevel1 =
                from menulist in menus
                where menulist.Level == "1"
                select menulist;
            if (allMenusLevel1.Count() > 0)
            {
                foreach (MenuDefinition md in allMenusLevel1)
                {
                    IEnumerable<MenuDefinition> ParentLevel0 =
                       from menulist in menus
                       where menulist.Level == "0" && int.Parse(menulist.Number) == int.Parse(md.LinkedWith)
                       select menulist;

                    string Level1Path = string.Empty;
                    foreach (MenuDefinition mdParent in ParentLevel0)
                    {
                        Level1Path = BasePath + mdParent.Name.Replace(" ", "-");
                        break;
                    }

                    CreateDocument(Level1Path, md.Name);
                }
            }


            //LINQ Query for Menu >> Level 2.
            IEnumerable<MenuDefinition> allMenusLevel2 =
                from menulist in menus
                where menulist.Level == "2"
                select menulist;

            if (allMenusLevel2.Count() > 0)
            {
                foreach (MenuDefinition md in allMenusLevel2)
                {
                    //Fetch Level 2 Path
                    IEnumerable<MenuDefinition> ParentLevel2 =
                        from menulist in menus
                        where menulist.Level == "1" && int.Parse(menulist.Number) == int.Parse(md.LinkedWith)
                        select menulist;

                     string Level2Path = string.Empty;
                     foreach (MenuDefinition mdParent in ParentLevel2)
                     {
                         //Fetch Level 2 Path's Parent
                         IEnumerable<MenuDefinition> ParentLevel1 =
                               from menulist1 in menus
                               where menulist1.Level == "0" && int.Parse(menulist1.Number) == int.Parse(mdParent.LinkedWith)
                               select menulist1;

                         string Level1Path = string.Empty;
                         foreach (MenuDefinition mdParent1 in ParentLevel1)
                         {
                             Level1Path = mdParent1.Name.Replace(" ", "-");
                             break;
                         }

                         Level2Path = BasePath + Level1Path + "/" + mdParent.Name.Replace(" ", "-");

                         break;
                     }

                     CreateDocument(Level2Path, md.Name);

                     //LINQ Query for Menu >> Level 3.
                     IEnumerable<MenuDefinition> allMenusLevel3 =
                         from menulist in menus
                         where menulist.Level == "3" && int.Parse(menulist.LinkedWith) == int.Parse(md.Number)
                         select menulist;

                     if (allMenusLevel3.Count() > 0)
                     {
                         foreach (MenuDefinition mdLevel3 in allMenusLevel3)
                         {
                             string Level3Path = Level2Path + "/" + md.Name.Replace(" ", "-");

                             CreateDocument(Level3Path, mdLevel3.Name);
                         }
                     }
                }
            }

            

        } //end using
    }
}