using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialize
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtFilePath.Text = AppDomain.CurrentDomain.BaseDirectory + "SerializeResult\\";
        }

        protected void btnSerialize_Click(object sender, EventArgs e)
        {
            switch (this.ddlFileType.SelectedValue)
            {
                case "bat":
                    this.BinarySerialize();
                    break;
                case "xml":
                    this.XMLSerialize();
                    break;
            }
        }


        protected void btnDeSerialize_Click(object sender, EventArgs e)
        {
            switch (this.ddlFileType.SelectedValue)
            {
                case "bat":
                    this.BinaryDeserialize();
                    break;
                case "xml":
                    this.XMLDeserialize();
                    break;
            }

        }

        /// <summary>
        /// 二进制序列化
        /// </summary>
        public void BinarySerialize()
        {
            ClassToSerialize c = new ClassToSerialize();
            FileStream fileStream = new FileStream(this.txtFilePath.Text + "Serialize", FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fileStream, c);
            fileStream.Close();
        }

        /// <summary>
        /// XML序列化
        /// </summary>
        public void XMLSerialize()
        {
            ClassToSerialize c = new ClassToSerialize();
            XmlSerializer mySerializer = new XmlSerializer(typeof(ClassToSerialize));
            StreamWriter myWriter = new StreamWriter(this.txtFilePath.Text + "Serialize.xml");
            mySerializer.Serialize(myWriter, c);
            myWriter.Close();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void BinaryDeserialize()
        {
            ClassToSerialize c = new ClassToSerialize();
            c.sex = "女";
            c.MoblieNumber = "15818526539";
            FileStream fileStream = new FileStream(this.txtFilePath.Text + "Serialize", FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter b = new BinaryFormatter();
            c = b.Deserialize(fileStream) as ClassToSerialize;
            this.lbResult.Text = "【Name】" + c.name + " 【Sex】" + c.sex + " 【MoblieNumber】" + c.MoblieNumber;
            fileStream.Close();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void XMLDeserialize()
        {
            ClassToSerialize c = new ClassToSerialize();
            c.sex = "man";
            c.MoblieNumber = "15818526539";
            XmlSerializer mySerializer = new XmlSerializer(typeof(ClassToSerialize));
            FileStream myFileStream = new FileStream(this.txtFilePath.Text + "Serialize.xml", FileMode.Open);
            c = mySerializer.Deserialize(myFileStream) as ClassToSerialize;
            this.lbResult.Text = "【Name】" + c.name + " 【Sex】" + c.sex + " 【MoblieNumber】" + c.MoblieNumber;
            myFileStream.Close();
        }
    }
}
