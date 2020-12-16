using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment;

namespace DataParser
{
    public partial class MainForm : Form
    {
        List<string> fieldNames;
        List<string> fieldDataTypes;
        List<string> fieldDescriptions;
        List<string> headers;
        XmlDocument xDoc;
        Document document;
        string inputFilePath, dataTableName;
        frmTableName tableNameForm;
        DataTypeConverter dtConverter;
        XmlElement xRoot;
        bool fileChosen = false;

        public MainForm()
        {
            InitializeComponent();
            fieldNames = new List<string>();
            fieldDataTypes = new List<string>();
            fieldDescriptions = new List<string>();
            headers = new List<string> { "Поле", "Тип данных", "Описание" };
        }

        private void getTableName(object sender, EventArgs e)
        {
            if (CheckTableExists(tableNameForm.tableName.Text))
            {
                dataTableName = tableNameForm.tableName.Text;
                tableNameForm.Close();
            }
        }

        private bool CheckTableExists(string dataTableName)
        {
            XmlNode table = xRoot.SelectSingleNode($"//*[@Name='{dataTableName}']");

            if (table == null)
            {
                MessageBox.Show($"Table with such name can not be found");
                return false;
            }

            return true;
        }

        private void parseBtn_Click(object sender, EventArgs e)
        {
            CheckFileChosen(Parse);
        }

        private void Parse()
        {
            bool readSucceded = ReadData();
            if (readSucceded)
            {
                dtConverter.InitializeTypes();
                SelectDataTypesFormat();
                CreateAndWriteTable();
                SaveDocument();
            }
        }

        private void SelectDataTypesFormat()
        {
            for (int i = 0; i < fieldDataTypes.Count; i++)
            {
                if (defaultDataType.Checked)
                    fieldDataTypes[i] = dtConverter.dataTypes.Find(t => t.DefaultType == fieldDataTypes[i]).DefaultType;

                if (sqlDataType.Checked)
                    fieldDataTypes[i] = dtConverter.dataTypes.Find(t => t.DefaultType == fieldDataTypes[i]).SqlType;
            }
        }

        private void OpenFrmTableName()
        {
            tableNameForm = new frmTableName();
            tableNameForm.OKbtn.Click += new EventHandler(this.getTableName);
            tableNameForm.Show();
            tableNameForm.TopMost = true;
        }

        private void OpenDocument()
        {
            OpenFileDialog OD = new OpenFileDialog();
            OD.Title = "Open File";
            OD.Filter = "XML Files|*.xml";
            DialogResult result = OD.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    inputFilePath = OD.FileName;
                    xDoc = new XmlDocument();
                    xDoc.Load(inputFilePath);
                    xRoot = xDoc.DocumentElement;
                    fileChosen = true;
                    OpenFrmTableName();
                }
                catch
                {
                    MessageBox.Show("File is not supported");
                }
            }
            else
                fileChosen = false;
        }

        private bool ReadData()
        {
            if (String.IsNullOrEmpty(dataTableName))
            {
                MessageBox.Show("You should select a table");
                return false;
            }
            
            XmlNode table = xRoot.SelectSingleNode($"//*[@Name='{dataTableName}']");
            
            XmlNode documentAttributes = table?.SelectSingleNode("Attributes");

            if (documentAttributes == null)
            {
                MessageBox.Show("Table format is not supported");
                return false;
            }

            else
            {
                foreach (XmlNode n in documentAttributes)
                {
                    fieldNames.Add(n.SelectSingleNode("@Name").Value);
                    fieldDataTypes.Add(n.SelectSingleNode("@DataType").Value);
                    fieldDescriptions.Add(n.SelectSingleNode("@HintText").Value);
                }

                XmlNode dataTypes = xRoot.SelectSingleNode("DataTypes");
                dtConverter = new DataTypeConverter(dataTypes);
                return true;
            }
        }

        private void CreateAndWriteTable()
        {
            document = new Document();
            Section sec = document.AddSection();
            Table table = sec.AddTable();

            table.ResetCells(fieldNames.Count + 1, headers.Count);

            TableRow headerRow = table.Rows[0];
            headerRow.Height = 23;

            TextRange text = new TextRange(document);
            Paragraph paragraph;

            for (int i = 0; i < headers.Count; i++)
            {
                paragraph = headerRow.Cells[i].AddParagraph();
                headerRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                paragraph.Format.HorizontalAlignment = HorizontalAlignment.Center;
                text = paragraph.AppendText(headers[i]);
                text.CharacterFormat.FontName = "Times New Roman";
                text.CharacterFormat.FontSize = 14;
                text.CharacterFormat.TextColor = Color.Black;
                text.CharacterFormat.Bold = true;
            }

            TableRow DataRow;

            for (int r = 0; r < fieldNames.Count; r++)
            {
                DataRow = table.Rows[r + 1];
                DataRow.Height = 35;

                for (int c = 0; c < headers.Count; c++)
                {
                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    paragraph = DataRow.Cells[c].AddParagraph();

                    if (c == 0)
                    {
                        DataRow.Cells[c].Width = 250;
                        text = paragraph.AppendText($"[{fieldNames[r]}]");
                    }
                    if (c == 1)
                    {
                        DataRow.Cells[c].Width = 150;
                        text = paragraph.AppendText($"[{fieldDataTypes[r]}]");
                    }
                    if (c == 2)
                    {
                        DataRow.Cells[c].Width = 300;
                        text = paragraph.AppendText(fieldDescriptions[r]);
                    }
                    paragraph.Format.HorizontalAlignment = HorizontalAlignment.Left;
                    text.CharacterFormat.FontName = "Times New Roman";
                    text.CharacterFormat.FontSize = 14;
                    text.CharacterFormat.TextColor = Color.Black;
                }
            }

            table.TableFormat.Borders.BorderType = Spire.Doc.Documents.BorderStyle.Single;
        }

        private void CheckFileChosen(Action callback)
        {
            if (fileChosen)
                callback();
            else
            {
                MessageBox.Show("File is not selected!");
                OpenDocument();
            }
        }

        private void selectTableBtn_Click(object sender, EventArgs e)
        {
            CheckFileChosen(OpenFrmTableName);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void SaveDocument()
        {
            SaveFileDialog SD = new SaveFileDialog();

            SD.Filter = "Word File |*.docx";
            SD.Title = "Save File";
            DialogResult result = SD.ShowDialog();

            if (result == DialogResult.OK)
                try
                {
                    document.SaveToFile(SD.FileName, FileFormat.Docx);
                    MessageBox.Show($"File is saved!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"File is in use: {ex.Message}");
                }

            fieldNames.Clear();
            fieldDataTypes.Clear();
            fieldDescriptions.Clear();
        }
    }
}
