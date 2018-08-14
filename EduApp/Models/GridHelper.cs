using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.ASPxThemes;
using DevExpress.Web.Mvc;
using DevExpress.Web.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace EduApp.Helpers
{
    public class GridHelper
    {
        private HtmlHelper Html;
        public GridHelper(HtmlHelper htmlHelper)
        {
            Html = htmlHelper;
        }


        public void FitPagedGridToScreenSize(GridViewSettings settings, int adjustSize, int rowHeight = 27, int minimumRows = 10, int defaultRowCount = 15)
        {
            //if(Html.Paltrack().BrowserHeight > 0)
            //{
            //	settings.SettingsPager.PageSize = Math.Max(((@Html.Paltrack().BrowserHeight - adjustSize) / rowHeight), minimumRows);
            //}
            //else
            //{
            //	settings.SettingsPager.PageSize = defaultRowCount;
            //}
        }

        public void GridDefaultBehavior(GridViewSettings settings, string gridName, bool hideSearchBar = false,
          bool showSelectCheckColumn = false,
          bool trandformHeadersMultiSelection = true,
          bool override100Width = false,
          int newWidthForOverride = 0)
        {
            settings.Name = gridName;
            var layoutName = settings.Name + "Layout";
            #region current add again

            // settings.SettingsBehavior.AutoFilterRowInputDelay = 0;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.Settings.ShowHeaderFilterButton = true;
            settings.SettingsBehavior.AllowSelectByRowClick = false;
            //settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowEllipsisInText = true;
            settings.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Control;
            settings.SettingsBehavior.AllowDragDrop = true;
            settings.Width = Unit.Percentage(100);
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;
            settings.SettingsPager.AllButton.Visible = true;
            //settings.Settings.ShowFooter = true;
            settings.Settings.ShowFilterRowMenu = true;
            settings.SettingsBehavior.EnableCustomizationWindow = true;
            settings.SettingsPager.PageSize = 25;
            settings.SettingsPager.NumericButtonCount = 6;
            settings.Styles.Cell.Wrap = DefaultBoolean.False;
            settings.SettingsBehavior.AllowFocusedRow = true;
            #endregion

            // adaptability
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.Off;
            settings.SettingsAdaptivity.AdaptiveColumnPosition = GridViewAdaptiveColumnPosition.Right;
            settings.SettingsAdaptivity.AdaptiveDetailColumnCount = 1;
            settings.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = true;
            settings.SettingsAdaptivity.HideDataCellsAtWindowInnerWidth = 0;

            if (showSelectCheckColumn == true)
            {
                settings.CommandColumn.Visible = true;
                settings.CommandColumn.ShowSelectCheckbox = true;
                settings.CommandColumn.Width = 40;
            }

            if (trandformHeadersMultiSelection == true)
            {
                //transform header filter to check list for multiple selection
                settings.Settings.ShowHeaderFilterButton = true;
                settings.SettingsPopup.HeaderFilter.Height = 250;
                var headerFilterMode = GridHeaderFilterMode.CheckedList;
                foreach (GridViewDataColumn column in settings.Columns)
                    column.SettingsHeaderFilter.Mode = headerFilterMode;
            }

            settings.ClientSideEvents.ColumnResized = "HighlightEllipsis";
            settings.SettingsBehavior.AllowEllipsisInText = true;

            settings.SettingsPager.PageSizeItemSettings.Visible = true;
            settings.SettingsPager.AlwaysShowPager = true;

            settings.ControlStyle.Wrap = DefaultBoolean.False;

            if (override100Width == true && newWidthForOverride > 0)
            {
                settings.Width = newWidthForOverride;
            }
            else
            {
                settings.Width = Unit.Percentage(100);
            }


#if !DEBUG
            settings.ClientLayout = (s, e) =>
            {

                switch (e.LayoutMode)
                {
                    case ClientLayoutMode.Loading:
                        //Load Last ClientLayout Via First Load
                        if (Html.ViewContext.TempData[layoutName] != null)
                        {
                            e.LayoutData = Html.ViewContext.TempData[layoutName].ToString();
                        }
                        break;
                    case ClientLayoutMode.Saving:
                        //Save Last ClientLayout Automatically
                        Html.ViewContext.TempData[layoutName] = e.LayoutData;
                        break;
                }
            };
#endif
            // GRID EXPORTS
            settings.SettingsExport.EnableClientSideExportAPI = true;
            settings.SettingsExport.ExcelExportMode = DevExpress.Export.ExportType.WYSIWYG;
            settings.SettingsExport.RenderBrick = (sender, e) => {
                if (e.RowType == GridViewRowType.Data && e.VisibleIndex % 2 == 0)
                    e.BrickStyle.BackColor = System.Drawing.Color.FromArgb(0xEE, 0xEE, 0xEE);
            };
            settings.Toolbars.Add(toolbar =>
            {

                toolbar.EnableAdaptivity = true;
                toolbar.Items.Add(GridViewToolbarCommand.Refresh);
                toolbar.Items.Add(GridViewToolbarCommand.ClearFilter);
                toolbar.Items.Add(GridViewToolbarCommand.ClearGrouping);
                toolbar.Items.Add(GridViewToolbarCommand.ShowSearchPanel);

                if (gridName != "gvFruitSpecStock")
                {
                    // toolbar.Items.Add(GridViewToolbarCommand.ExportToPdf); // removed for now
                    toolbar.Items.Add(GridViewToolbarCommand.ExportToXls);
                    // toolbar.Items.Add(GridViewToolbarCommand.ExportToXlsx);
                    //toolbar.Items.Add(GridViewToolbarCommand.ExportToDocx);// removed for now
                    // toolbar.Items.Add(GridViewToolbarCommand.ExportToRtf);// removed for now
                    // toolbar.Items.Add(GridViewToolbarCommand.ExportToCsv);// removed for now
                }

            });

        }

        public GridViewSettings GetGridDefaultBehavior(GridViewSettings settings, string gridName, bool hideSearchBar = false,
        bool showSelectCheckColumn = false,
        bool trandformHeadersMultiSelection = true,
        bool override100Width = false,
        int newWidthForOverride = 0)
        {
            settings.Name = gridName;
            var layoutName = settings.Name + "Layout";
            #region current add again

            // settings.SettingsBehavior.AutoFilterRowInputDelay = 0;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.Settings.ShowHeaderFilterButton = true;
            settings.SettingsBehavior.AllowSelectByRowClick = false;
            //settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowEllipsisInText = true;
            settings.SettingsResizing.ColumnResizeMode = ColumnResizeMode.Disabled;
            settings.SettingsBehavior.AllowDragDrop = true;
            settings.Width = Unit.Percentage(100);
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;
            settings.SettingsPager.AllButton.Visible = true;
            //settings.Settings.ShowFooter = true;
            settings.Settings.ShowFilterRowMenu = true;
            //settings.ClientSideEvents.ToolbarItemClick = "OnToolbarItemClick";
            //settings.SettingsSearchPanel.CustomEditorName = "search" + gridName;
            settings.SettingsBehavior.EnableCustomizationWindow = true;
            //settings.CommandColumn.ShowClearFilterButton = true;
            settings.SettingsPager.PageSize = 25;
            settings.SettingsPager.NumericButtonCount = 6;
            settings.Styles.Cell.Wrap = DefaultBoolean.False;
            #endregion

            // adaptability
            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.Off;
            settings.SettingsAdaptivity.AdaptiveColumnPosition = GridViewAdaptiveColumnPosition.Right;
            settings.SettingsAdaptivity.AdaptiveDetailColumnCount = 1;
            settings.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = true;
            settings.SettingsAdaptivity.HideDataCellsAtWindowInnerWidth = 0;

            if (showSelectCheckColumn == true)
            {
                settings.CommandColumn.Visible = true;
                settings.CommandColumn.ShowSelectCheckbox = true;
                settings.CommandColumn.Width = 40;
            }

            if (trandformHeadersMultiSelection == true)
            {
                //transform header filter to check list for multiple selection
                settings.Settings.ShowHeaderFilterButton = true;
                settings.SettingsPopup.HeaderFilter.Height = 250;
                var headerFilterMode = GridHeaderFilterMode.CheckedList;
                foreach (GridViewDataColumn column in settings.Columns)
                    column.SettingsHeaderFilter.Mode = headerFilterMode;
            }

            settings.ClientSideEvents.ColumnResized = "HighlightEllipsis";
            settings.SettingsBehavior.AllowEllipsisInText = true;

            settings.SettingsPager.PageSizeItemSettings.Visible = true;
            settings.SettingsPager.AlwaysShowPager = true;

            settings.ControlStyle.Wrap = DefaultBoolean.False;

            if (override100Width == true && newWidthForOverride > 0)
            {
                settings.Width = newWidthForOverride;
            }
            else
            {
                settings.Width = Unit.Percentage(100);
            }


#if !DEBUG
            settings.ClientLayout = (s, e) =>
            {

                switch (e.LayoutMode)
                {
                    case ClientLayoutMode.Loading:
                        //Load Last ClientLayout Via First Load
                        if (Html.ViewContext.TempData[layoutName] != null)
                        {
                            e.LayoutData = Html.ViewContext.TempData[layoutName].ToString();
                        }
                        break;
                    case ClientLayoutMode.Saving:
                        //Save Last ClientLayout Automatically
                        Html.ViewContext.TempData[layoutName] = e.LayoutData;
                        break;
                }
            };
#endif
            // GRID EXPORTS
            settings.SettingsExport.EnableClientSideExportAPI = true;
            settings.SettingsExport.ExcelExportMode = DevExpress.Export.ExportType.WYSIWYG;
            settings.SettingsExport.RenderBrick = (sender, e) => {
                if (e.RowType == GridViewRowType.Data && e.VisibleIndex % 2 == 0)
                    e.BrickStyle.BackColor = System.Drawing.Color.FromArgb(0xEE, 0xEE, 0xEE);
            };
            settings.Toolbars.Add(toolbar =>
            {
                //toolbar.Enabled = true;
                //toolbar.Position = GridToolbarPosition.Top;
                //toolbar.ItemAlign = GridToolbarItemAlign.Left;
                //toolbar.Name = gridName + "Refresh";

                //if(gridName != "gvLoadoutHeader" || gridName != "gvFruitSpecStock")
                //   toolbar.Items.Add(GridViewToolbarCommand.Refresh, true);

                toolbar.EnableAdaptivity = true;
                toolbar.Items.Add(GridViewToolbarCommand.Refresh);
                toolbar.Items.Add(GridViewToolbarCommand.ClearFilter);
                toolbar.Items.Add(GridViewToolbarCommand.ClearGrouping);
                toolbar.Items.Add(GridViewToolbarCommand.ShowSearchPanel);

                //if (gridName != "gvFruitSpecStock")
                //{
                //    // toolbar.Items.Add(GridViewToolbarCommand.ExportToPdf); // removed for now
                //    toolbar.Items.Add(GridViewToolbarCommand.ExportToXls);
                //    toolbar.Items.Add(GridViewToolbarCommand.ExportToXlsx);
                //    //toolbar.Items.Add(GridViewToolbarCommand.ExportToDocx);// removed for now
                //    // toolbar.Items.Add(GridViewToolbarCommand.ExportToRtf);// removed for now
                //    // toolbar.Items.Add(GridViewToolbarCommand.ExportToCsv);// removed for now
                //}

            });

            return settings;

        }


    }

    public static class Helper
    {
        public static string ToAbsoluteUrl(this string relativeUrl) //Use absolute URL instead of adding phycal path for CSS, JS and Images     
        {
            if (string.IsNullOrEmpty(relativeUrl)) return relativeUrl;
            if (HttpContext.Current == null) return relativeUrl;
            if (relativeUrl.StartsWith("/")) relativeUrl = relativeUrl.Insert(0, "~");
            if (!relativeUrl.StartsWith("~/")) relativeUrl = relativeUrl.Insert(0, "~/");
            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;
            return String.Format("{0}://{1}{2}{3}", url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }
        public static string EncryptString(string source, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Encoding.UTF8.GetBytes(source);

            string encoded =
                Convert.ToBase64String(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));

            return encoded;
        }

        public static string DecryptString(string encodedText, string key)
        {
            TripleDESCryptoServiceProvider desCryptoProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider();

            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Convert.FromBase64String(encodedText);
            byte[] inputBuffer = byteBuff;
            int inputOffset = 0;
            int inputCount = byteBuff.Length;
            var s = desCryptoProvider.CreateDecryptor().TransformFinalBlock(inputBuffer, inputOffset, inputCount);
            string plaintext = Encoding.UTF8.GetString(s);
            return plaintext;
        }

    }
}