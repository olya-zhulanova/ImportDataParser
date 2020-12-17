using System.Collections.Generic;
using System.Drawing;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment;

namespace DataParser
{
    class WordDocCreator
    {
        TextRange text;
        Paragraph paragraph;
        Document document;

        private void DrawHeader(Table table, List<string> header)
        {
            TableRow headerRow = table.Rows[0];
            headerRow.Height = 23;

            for (int i = 0; i < header.Count; i++)
            {
                paragraph = headerRow.Cells[i].AddParagraph();
                headerRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
                text = paragraph.AppendText(header[i]);
                text.CharacterFormat.FontName = "Times New Roman";
                text.CharacterFormat.FontSize = 14;
                text.CharacterFormat.TextColor = Color.Black;
                text.CharacterFormat.Bold = true;
            }
        }

        private void DrawDataRows(Table table, List<List<string>> data)
        {
            TableRow DataRow;

            for (int r = 0; r < data[0].Count; r++)
            {
                DataRow = table.Rows[r + 1];
                DataRow.Height = 35;

                for (int c = 0; c < data.Count; c++)
                {
                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    paragraph = DataRow.Cells[c].AddParagraph();

                    if (c == 0)
                    {
                        DataRow.Cells[c].Width = 250;
                        text = paragraph.AppendText($"[{data[0][r]}]");
                    }
                    if (c == 1)
                    {
                        DataRow.Cells[c].Width = 150;
                        text = paragraph.AppendText($"[{data[1][r]}]");
                    }
                    if (c == 2)
                    {
                        DataRow.Cells[c].Width = 300;
                        text = paragraph.AppendText($"{data[2][r]}");
                    }
                    paragraph.Format.HorizontalAlignment = HorizontalAlignment.Left;
                    text.CharacterFormat.FontName = "Times New Roman";
                    text.CharacterFormat.FontSize = 14;
                    text.CharacterFormat.TextColor = Color.Black;
                }
            }
        }

        public Document CreateTable(List<string> header, List<List<string>> data)
        {
            document = new Document();
            Section sec = document.AddSection();
            Table table = sec.AddTable();

            table.ResetCells(data[0].Count + 1, header.Count);

            DrawHeader(table, header);
            DrawDataRows(table, data);

            table.TableFormat.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Single;

            return document;
        }
    }
}
