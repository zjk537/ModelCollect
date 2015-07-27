using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConfigModel.ConfigXML;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using CommonHelper.Results;
using System.Xml.Linq;

namespace ConfigModel
{
    public class XMLManager
    {
        private static XmlCommendService commandService = null;
        /// <summary>
        /// 从XML文档中读数据，并转换成XmlCommend
        /// </summary>
        /// <param name="path">xml文档存放的路径</param>
        /// <returns></returns>
        public static XmlCommendService ReadXmlToObject(string path)
        {
            XmlCommendService xmlCommendService = null;

            XmlReader xmlReader = null;

            try
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(streamReader.ReadToEnd());
                    streamReader.Close();

                    xmlReader = XmlReader.Create(new MemoryStream(buffer), null);

                    //数据有效性验证
                    while (xmlReader.Read())
                    {
                    }

                    //序列化之后再反序列化
                    XmlSerializer serializer = new XmlSerializer(typeof(XmlCommendService));
                    commandService = xmlCommendService = (XmlCommendService)serializer.Deserialize(new MemoryStream(buffer));
                    
                    return xmlCommendService;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
            }
        }

        /// <summary>
        /// 获取推荐业务列表
        /// </summary>
        /// <param name="businessTypeName">业务种类名称</param>
        /// <returns></returns>
        public List<CommendService> GetCommendServiceList(string businessTypeName)
        {
            List<CommendService> commendService = new List<CommendService>();

            foreach (BusinessType businessType in commandService.BusinessTypes)
            {
                if (businessType.Name.ToLower().Equals(businessTypeName.ToLower()))
                {
                    commendService = businessType.CommentService;
                    break;
                }
            }

            return commendService;
        }

        /// <summary>
        /// 向指定的xml文档中插入记录
        /// </summary>
        /// <param name="path"></param>
        /// <param name="businessType"></param>
        public static ResultInfo InserRecordIntoXml(string path, BusinessType businessType)
        {
            ResultInfo resultInfo = null;
            FileInfo xmlFile = new FileInfo(path);

            //如果文件不存在
            if (!xmlFile.Exists)
            {
                resultInfo = CreateXmlFile(path, businessType);
            }
            else
            {
                //如果文件存在，判断是加到那个Businesstype下
                XElement xElementRoot = XElement.Load(path);
                XmlOperation xmlOperation = XmlOperation.None;

                //遍历判断业务名是否存在
                foreach (XElement xElementType in xElementRoot.Element("BusinessTypes").Elements("BusinessType"))
                {
                    if (xElementType.Attribute("Name").Value.ToString() == businessType.Name)
                    {
                        xmlOperation = XmlOperation.BusinessTypeExist;

                        //遍历判断服务名是否存在
                        foreach (XElement xElementService in xElementType.Element("CommendServices").Elements("CommendService"))
                        {
                            if (xElementService.Attribute("Name").Value.ToString() == businessType.CommentService[0].Name)
                            {
                                //服务名已经存在
                                xmlOperation = XmlOperation.CommendServiceExist;
                                resultInfo = ResultInfoFactory<ResultInfo>.Create(false, "ServiceExit", "CommendService名称已存在！");
                                break;
                            }
                            else
                                xmlOperation = XmlOperation.CommendService;
                        }

                        //如果服务不存在，则创建
                        if (xmlOperation == XmlOperation.CommendService)
                            resultInfo = CreateCommendServiceNode(xElementType, businessType.CommentService[0]);
                        break;
                    }
                    else
                        xmlOperation = XmlOperation.BusinessType;
                }

                //如果业务不存在，则创建节点
                if (xmlOperation == XmlOperation.BusinessType)
                {
                    resultInfo = CreateBusinessTypeNode(xElementRoot, businessType);
                }

                try
                {
                    xElementRoot.Save(path);
                }
                catch (Exception ex)
                {
                    resultInfo = ResultInfoFactory<ResultInfo>.Create(true, "SaveException", "保存信息时发生异常：\r\t {0}", ex.Message);
                }
            }
            return resultInfo;
        }


        /// <summary>
        /// 新建xml文档
        /// </summary>
        /// <param name="path">文件保存的路径</param>
        /// <param name="businessType">业务实体</param>
        private static ResultInfo CreateXmlFile(string path, BusinessType businessType)
        {
            ResultInfo resultInfo = null;
            try
            {
                XDocument xmlDoc = new XDocument(
                              new XDeclaration("1.0", "utf-8", "no"),
                              new XElement("XmlCommendService",
                                  new XElement("BusinessTypes",
                                      new XElement("BusinessType",
                                          new XAttribute("Name", businessType.Name),
                                          new XElement("CommendServices",
                                              new XElement("CommendService",
                                                  new XAttribute("Guid", businessType.CommentService[0].Guid),
                                                  new XAttribute("Name", businessType.CommentService[0].Name),
                                                  new XAttribute("Enable", businessType.CommentService[0].Enable.ToString()),
                                                  new XAttribute("Description", businessType.CommentService[0].Description),
                                                  new XAttribute("LogoUrl", businessType.CommentService[0].LogoUrl)
                                                  ))))));
                xmlDoc.Save(path);
                resultInfo = ResultInfoFactory<ResultInfo>.Create(true, "XDocumentSuccessed", "Xml创建成功");
            }
            catch (Exception ex)
            {
                resultInfo = ResultInfoFactory<ResultInfo>.Create(false, "XDocumentException", "Xml创建时异常:\r\t{1}", ex.Message);
            }
            return resultInfo;

        }

        /// <summary>
        /// 创建业务节点
        /// </summary>
        /// <param name="xElementParent">该业务在父节点</param>
        /// <param name="businessType">业务实体</param>
        private static ResultInfo CreateBusinessTypeNode(XElement xElementParent, BusinessType businessType)
        {
            ResultInfo resultInfo = null;
            try
            {
                XElement newElement = new XElement("BusinessType",
                                           new XAttribute("Name", businessType.Name),
                                           new XElement("CommendServices",
                                               new XElement("CommendService",
                                                   new XAttribute("Guid", businessType.CommentService[0].Guid),
                                                       new XAttribute("Name", businessType.CommentService[0].Name),
                                                       new XAttribute("Enable", businessType.CommentService[0].Enable.ToString().ToLower()),
                                                       new XAttribute("Description", businessType.CommentService[0].Description),
                                                       new XAttribute("LogoUrl", businessType.CommentService[0].LogoUrl)
                                                       )));
                xElementParent.Element("BusinessTypes").Add(newElement);
                resultInfo = ResultInfoFactory<ResultInfo>.Create(true, "BusinessSuccessed", "业务节点 {0} 创建成功", businessType.Name);
            }
            catch (Exception ex)
            {
                resultInfo = ResultInfoFactory<ResultInfo>.Create(false, "BusinessException", "业务节点 {0} 创建时异常:\r\t{1}", businessType.Name, ex.Message);
            }
            return resultInfo;


        }

        /// <summary>
        /// 创建服务节点
        /// </summary>
        /// <param name="xElementParent">该服务在父节点</param>
        /// <param name="commendService">服务实体</param>
        private static ResultInfo CreateCommendServiceNode(XElement xElementParent, CommendService commendService)
        {
            ResultInfo resultInfo = null;
            try
            {
                XElement newElement = new XElement("CommendService",
                                                          new XAttribute("Guid", commendService.Guid),
                                                          new XAttribute("Name", commendService.Name),
                                                          new XAttribute("Enable", commendService.Enable.ToString()),
                                                          new XAttribute("Description", commendService.Description),
                                                          new XAttribute("LogoUrl", commendService.LogoUrl)
                                                          );
                xElementParent.Element("CommendServices").Add(newElement);
                resultInfo = ResultInfoFactory<ResultInfo>.Create(true, "ServiceSuccessed", "服务节点 {0} 创建成功", commendService.Name);
            }
            catch (Exception ex)
            {
                resultInfo = ResultInfoFactory<ResultInfo>.Create(false, "ServiceException", "服务节点 {0} 创建时异常:\r\t{1}", commendService.Name, ex.Message);
            }
            return resultInfo;

        }



        #region  学习用代码，没有用到


        /// <summary>
        /// 获取指定业务节点的名字
        /// </summary>
        /// <param name="xElement">xml文档元素</param>
        /// <param name="businessName">业务节点名字</param>
        /// <returns></returns>
        private List<CommendService> GetCommendService(XElement xElement, string businessName)
        {
            //Descendants 可遍历某节点或文档下的所有子节点 
            //Elements 则是遍历当前节点或文档下一级的子节点
            var quaryXml = from q in xElement.Descendants("CommendService")
                           where q.Parent.Parent.Attribute("Name").Value.ToString() == businessName
                           select new CommendService
                           {
                               Guid = q.Attribute("Guid").Value.ToString(),
                               Name = q.Attribute("Name").Value.ToString(),
                               Enable = Convert.ToBoolean(q.Attribute("Enable").Value),
                               Description = q.Attribute("Description").Value.ToString(),
                               LogoUrl = q.Attribute("LogoUrl").Value.ToString()
                           };
            return quaryXml.ToList();
        }

        /// <summary>
        /// 获得业务信息
        /// </summary>
        /// <param name="xElement">xml文档元素</param>
        /// <param name="xElementName">xml文档元素中节点的某个属性</param>
        /// <param name="searchName">匹配值</param>
        /// <returns></returns>
        private int GetBusinessTypes(XElement xElement, string xElementName, string searchName)
        {
            //Linq形式
            var quaryXml = from q in xElement.Descendants(xElementName)
                           where q.Attribute("Name").Value.ToString() == searchName
                           select q;
            //Lambda表达式
            var quary = xElement.Descendants(xElementName)
                .Where(e => (e.Element("Name").Value.ToString() == searchName))
                .Select(e => new { Name = e.Element("Guid").Value.ToString() });
            return quaryXml.Count();


        }

        #endregion
    }
}
