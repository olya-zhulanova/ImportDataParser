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
        WordDocCreator creator;

        public MainForm()
        {
            InitializeComponent();
            fieldNames = new List<string>();
            fieldDataTypes = new List<string>();
            fieldDescriptions = new List<string>();
            headers = new List<string> { "Поле", "Тип данных", "Описание" };
            creator = new WordDocCreator();
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

            creator.CreateTable(document, headers, new List<List<string>> { fieldNames, fieldDataTypes, fieldDescriptions });
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
