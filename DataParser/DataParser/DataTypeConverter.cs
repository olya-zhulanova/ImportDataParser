using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataParser
{
    class DataTypeConverter
    {
        XmlNode dataTypesInXML;
        public List<DataType> dataTypes;

        public DataTypeConverter(XmlNode dataTypesNode)
        {
            dataTypesInXML = dataTypesNode;
            dataTypes = new List<DataType>();
        }

        public void InitializeTypes()
        {
            DataType dt;
            XmlNodeList xmlTypes = dataTypesInXML.ChildNodes;
            foreach (XmlNode n in xmlTypes)
            {
                dt = new DataType();
                dt.DefaultType = n.SelectSingleNode("@Name")?.Value;
                dt.SqlType = n.SelectSingleNode("@SqlType")?.Value;
                dt.CType = n.SelectSingleNode("@CType")?.Value;
                dataTypes.Add(dt);
            }
        }
    }
}
