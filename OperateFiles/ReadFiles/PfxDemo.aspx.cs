using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ReadFiles
{
    public partial class PfxDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 将证书从证书存储区导出，并存储为pfx文件，同时为pfx文件指定打开的密码
        /// 本函数同时也演示如何用公钥进行加密，私钥进行解密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnToPfx_Click(object sender, EventArgs e)
        {
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2Collection storeCollection = (X509Certificate2Collection)store.Certificates;

            foreach (X509Certificate2 x509 in storeCollection)
            {
                if (x509.Subject == "CN=lunminji")
                {
                    Debug.Print(string.Format("cerfificate name:{0}", x509.Subject));
                    byte[] pfxByte=x509.Export(X509ContentType.Pfx,"123");
                    using (FileStream fileStream = new FileStream("luminji.pfx", FileMode.Create))
                    {
                        for (int i = 0; i < pfxByte.Length; i++)
                        {
                            fileStream.WriteByte(pfxByte[i]);
                            fileStream.Seek(0, SeekOrigin.Begin);
                        }
                        for (int i = 0; i < fileStream.Length; i++)
                        {
                            if (pfxByte[i] != fileStream.ReadByte())
                            {
                                Debug.Print("Error Writing Data.");
                                return;
                            }
                        }
                    }
                    //fileStream.Close();
                    Debug.Print("The Data was Written to {0} and Verified", fileStream.Name);
                    string myName = "我是zjk！";
                    string enStr = this.RsAEncrypt(x509.PublicKey.Key.ToXmlString(false), myName);
                    Response.Write("密文是：" + enStr);
                    string deStr = this.RSADecrypt(x509.PrivateKey.ToXmlString(true), enStr);
                    Response.Write("明文是：" + deStr);
                }
            }
            store.Close();
            store = null;
            storeCollection = null;
        }

        protected void btnCreatePfx_Click(object sender, EventArgs e)
        {
            string makeCert = @"C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin\makecert.ext";
            string x509Name = "CN=luminje";
            string param = "-pe -ss my -n\"" + x509Name + "\"" ;
            Process p = Process.Start(makeCert, param);
            p.WaitForExit();
            p.Close();
            Response.Write("over");
        }

        protected void btnReadPfxFile_Click(object sender, EventArgs e)
        {
            X509Certificate2 pc = new X509Certificate2("luminli.pfx","123");
            Response.Write("name:" + pc.SubjectName.Name);
            Response.Write("public:"+pc.PublicKey.ToString());
            Response.Write("private:"+pc.PrivateKey.ToString());
            pc = null;
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        private string RSADecrypt(string xmlPrivateKey, string decryptString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xmlPrivateKey);
            byte[] rgb = Convert.FromBase64String(decryptString);
            byte[] bytes = provider.Decrypt(rgb, false);
            return new UnicodeEncoding().GetString(bytes);
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="p"></param>
        /// <param name="myName"></param>
        /// <returns></returns>
        private string RsAEncrypt(string xmlPublicKey, string encryptString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xmlPublicKey);
            byte[] bytes = new UnicodeEncoding().GetBytes(encryptString);
            return Convert.ToBase64String(provider.Encrypt(bytes, false));
        }


    }
}
