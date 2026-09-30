using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Helper
{
    /// <summary>Exports or prints whatever report is currently shown in a DataGridView.</summary>
    public static class ReportExporter
    {
        public static bool ExportToCsv(DataGridView grid, string defaultFileName)
        {
            if (grid.Rows.Count == 0 || grid.Columns.Count == 0)
            {
                MessageHelper.ShowWarning("There is nothing to export. Generate a report first.", "Export");
                return false;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV file (*.csv)|*.csv";
                dialog.FileName = defaultFileName;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                List<DataGridViewColumn> columns = VisibleColumns(grid);
                StringBuilder csv = new StringBuilder();
                csv.AppendLine(string.Join(",", columns.Select(c => Escape(c.HeaderText))));

                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }
                    csv.AppendLine(string.Join(",", columns.Select(c => Escape(Convert.ToString(row.Cells[c.Index].FormattedValue)))));
                }

                // UTF-8 with a byte-order mark so Excel shows the peso sign correctly.
                File.WriteAllText(dialog.FileName, csv.ToString(), new UTF8Encoding(true));
                MessageHelper.ShowInfo("Report exported to:\n" + dialog.FileName, "Export");
                return true;
            }
        }

        public static void Print(DataGridView grid, string title)
        {
            if (grid.Rows.Count == 0 || grid.Columns.Count == 0)
            {
                MessageHelper.ShowWarning("There is nothing to print. Generate a report first.", "Print");
                return;
            }

            List<DataGridViewColumn> columns = VisibleColumns(grid);
            int nextRow = 0;

            PrintDocument document = new PrintDocument();
            document.DocumentName = title;
            document.BeginPrint += (s, e) => { nextRow = 0; };
            document.PrintPage += (s, e) =>
            {
                using (Font titleFont = new Font("Segoe UI", 14F, FontStyle.Bold))
                using (Font headerFont = new Font("Segoe UI", 9F, FontStyle.Bold))
                using (Font bodyFont = new Font("Segoe UI", 9F))
                using (StringFormat format = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap })
                {
                    float x = e.MarginBounds.Left;
                    float y = e.MarginBounds.Top;
                    float columnWidth = (float)e.MarginBounds.Width / columns.Count;
                    float lineHeight = bodyFont.GetHeight(e.Graphics) + 6;

                    e.Graphics.DrawString(title, titleFont, Brushes.Black, x, y);
                    y += titleFont.GetHeight(e.Graphics) + 12;

                    for (int c = 0; c < columns.Count; c++)
                    {
                        e.Graphics.DrawString(columns[c].HeaderText, headerFont, Brushes.Black,
                            new RectangleF(x + c * columnWidth, y, columnWidth - 6, lineHeight), format);
                    }
                    y += lineHeight;
                    e.Graphics.DrawLine(Pens.Black, x, y, x + e.MarginBounds.Width, y);
                    y += 4;

                    while (nextRow < grid.Rows.Count)
                    {
                        DataGridViewRow row = grid.Rows[nextRow];
                        if (!row.IsNewRow)
                        {
                            if (y + lineHeight > e.MarginBounds.Bottom)
                            {
                                e.HasMorePages = true;
                                return;
                            }

                            for (int c = 0; c < columns.Count; c++)
                            {
                                string text = Convert.ToString(row.Cells[columns[c].Index].FormattedValue);
                                e.Graphics.DrawString(text, bodyFont, Brushes.Black,
                                    new RectangleF(x + c * columnWidth, y, columnWidth - 6, lineHeight), format);
                            }
                            y += lineHeight;
                        }
                        nextRow++;
                    }

                    e.HasMorePages = false;
                }
            };

            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = document;
                preview.Width = 900;
                preview.Height = 700;
                preview.ShowDialog();
            }
        }

        private static List<DataGridViewColumn> VisibleColumns(DataGridView grid)
        {
            return grid.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();
        }

        private static string Escape(string value)
        {
            value = value ?? "";
            if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0)
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }
    }
}
