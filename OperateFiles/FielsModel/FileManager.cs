using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FilesModel.EquipmentConfig;
using System.IO;
using System.Configuration;

namespace FilesModel
{
    /// <summary>
    /// 文件主要操作
    /// </summary>
    public class FileManager
    {
        #region  属性

        /// <summary>
        /// 源数据存放位置
        /// </summary>
        private static String SourcePath
        {
            get { return ConfigurationManager.AppSettings["sourcePath"].ToString(); }
        }

        /// <summary>
        /// 类似 【01 zm Desc】 之间的分隔符
        /// </summary>
        private static Char NameSplit
        {
            get { return ConfigurationManager.AppSettings["nameSplit"] == null ? ' ' : Convert.ToChar(ConfigurationManager.AppSettings["nameSplit"]); }
        }

        /// <summary>
        /// 数据段内容分隔符
        /// </summary>
        private static Char DataSplit
        {
            get { return ConfigurationManager.AppSettings["dataSplit"] == null ? '|' : Convert.ToChar(ConfigurationManager.AppSettings["dataSplit"]); }
        }
        /// <summary>
        /// 导出数据时，每个字段对应的长度
        /// </summary>
        private static Int32 FieldLength
        {
            get { return ConfigurationManager.AppSettings["fieldLength"] == null ? 15 : Convert.ToInt32(ConfigurationManager.AppSettings["fieldLength"]); }
        }

        private static List<FileDataDetail> fileDataDetailList;
        /// <summary>
        /// 读取的文件内容
        /// </summary>
        public static List<FileDataDetail> FileDataDetailList
        {
            get
            {
                if (fileDataDetailList == null)
                {
                    fileDataDetailList = GetFileDataDetailList(SourcePath);
                }
                return fileDataDetailList;
            }
        }
        #endregion


        /// <summary>
        /// 读取文本文件的内容
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static List<FileDataDetail> GetFileDataDetailList(string sourcePath)
        {
            // 结果数据
            List<FileDataDetail> fileDataDetailList = new List<FileDataDetail>();

            bool isNewNode = true;              // 是否为一个新的 <> 的开始
            FileDataDetail fileDataDetail = null;   // 一个<> 开始跟结束段，为一个数据详细信息
            SerialPortConfigInfo serialPortConfigInfo = null; //串品配置信息
            EquipmentInfo equipmentInfo = null;         //设备信息

            using (StreamReader sr = new StreamReader(sourcePath, Encoding.Default))
            {
                string line = string.Empty;
                while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                {
                    #region 处理节点配置 <> 信息

                    if (line.StartsWith("<"))
                    {
                        //1、去掉 "<>" 截取字符串 本行主要内容 
                        //记录带<>行的数据内容
                        string lineContent = line.Substring(1, line.Length - 2);

                        // 2、读取形如：<02 zm Desc>
                        if (lineContent.Contains(NameSplit))
                        {
                            //设备信息
                            equipmentInfo = new EquipmentInfo();
                            equipmentInfo.Name = lineContent;
                            fileDataDetail.EquipmentList.Add(equipmentInfo);
                            continue;
                        }

                        // 2、读取形如：<串口配置> 
                        //第一次加载时，直接实例化一个FileDataDetail
                        if (isNewNode)
                        {
                            fileDataDetail = new FileDataDetail();
                            fileDataDetail.Name = lineContent;
                            fileDataDetailList.Add(fileDataDetail);
                            isNewNode = false;
                            continue;
                        }
                        // 3、读取形如：<串口配置结束> 
                        if (lineContent == (fileDataDetail.Name + "结束"))
                        {
                            isNewNode = true;
                            continue;
                        }
                    }
                    #endregion

                    #region  读取数据段的内容

                    // 拆分行内容
                    string[] contents = line.Split(DataSplit);

                    // 串口配置信息
                    if (fileDataDetail.Name == "串口配置")
                    {
                        serialPortConfigInfo = new SerialPortConfigInfo();
                        serialPortConfigInfo.DDC_ID = Convert.ToInt32(contents[0]);
                        serialPortConfigInfo.Comm = Convert.ToInt32(contents[1]);
                        serialPortConfigInfo.Baud = Convert.ToInt32(contents[2]);
                        serialPortConfigInfo.DataBit = Convert.ToInt32(contents[3]);
                        serialPortConfigInfo.StopBit = Convert.ToInt32(contents[4]);
                        serialPortConfigInfo.Parity = contents[5];
                        serialPortConfigInfo.CNT_Resend = Convert.ToInt32(contents[6]);

                        fileDataDetail.SerialPortList.Add(serialPortConfigInfo);
                    }
                    else // 如：大厅照明 的详细信息
                    {
                        EquipmentConfigInfo equipmentConfigInfo = new EquipmentConfigInfo();
                        equipmentConfigInfo.DDC_ID = Convert.ToInt32(contents[0]);
                        equipmentConfigInfo.VarType = contents[1];
                        equipmentConfigInfo.Func = contents[2];
                        equipmentConfigInfo.Offset = contents[3];
                        equipmentConfigInfo.Desc = contents[4];
                        equipmentConfigInfo.Unit = contents[5];
                        equipmentConfigInfo.Point = contents[6];
                        equipmentConfigInfo.Param1 = contents[7];
                        equipmentConfigInfo.Param2 = contents[8];
                        equipmentInfo.EquipmentConfigList.Add(equipmentConfigInfo);
                    }
                    #endregion
                }
            }
            return fileDataDetailList;
        }

        /// <summary>
        /// 导出文本文件
        /// </summary>
        /// <param name="fileDataDetailList"></param>
        public static void ResponseData(string savePath)
        {
            if (fileDataDetailList == null || fileDataDetailList.Count <= 0)
            {
                throw new Exception("数据集为空！");
            }

            StringBuilder dataContent = new StringBuilder();

            byte[] dataField = null;
            using (FileStream fs = new FileStream(savePath, FileMode.Append))
            {
                foreach (FileDataDetail fileDataDetail in fileDataDetailList)
                {
                    dataContent.Remove(0, dataContent.Length);
                    dataContent.Append("<");
                    dataContent.Append(fileDataDetail.Name);
                    dataContent.Append(">\r\n");
                    if (fileDataDetail.Name == "串口配置")
                    {
                        dataContent = SerialPortString(dataContent, fileDataDetail.SerialPortList);
                    }
                    else
                    {
                        dataContent = EquipmentStriing(dataContent, fileDataDetail.EquipmentList);
                    }
                    dataContent.Append("<");
                    dataContent.Append(fileDataDetail.Name);
                    dataContent.Append("结束>\r\n");

                    //数据读完后，开始写该数据段内容
                    dataField = System.Text.Encoding.Default.GetBytes(dataContent.ToString());
                    fs.Write(dataField, 0, dataField.Length);
                    fs.Flush();
                }
            }
        }

        private static StringBuilder EquipmentStriing(StringBuilder dataContent, List<EquipmentInfo> equipmentList)
        {
            foreach (EquipmentInfo equipmentInfo in equipmentList)
            {
                dataContent.Append("<");
                dataContent.Append(equipmentInfo.Name);
                dataContent.Append(">\r\n");
                dataContent.Append("DDC_ID".PadRight(FieldLength, ' '));
                dataContent.Append("VARTYPE".PadRight(FieldLength, ' '));
                dataContent.Append("FUNC".PadRight(FieldLength, ' '));
                dataContent.Append("OFFSET".PadRight(FieldLength, ' '));
                dataContent.Append("DESC".PadRight(FieldLength, ' '));
                dataContent.Append("UNIT".PadRight(FieldLength, ' '));
                dataContent.Append("POINT".PadRight(FieldLength, ' '));
                dataContent.Append("0".PadRight(FieldLength, ' '));
                dataContent.Append("1\r\n");
                foreach (EquipmentConfigInfo equipmentConfigInfo in equipmentInfo.EquipmentConfigList)
                {
                    dataContent.Append(equipmentConfigInfo.DDC_ID.ToString().PadRight(FieldLength, ' '));
                    dataContent.Append(equipmentConfigInfo.VarType.PadRight(FieldLength, ' '));
                    dataContent.Append(equipmentConfigInfo.Func.PadRight(FieldLength, ' '));
                    dataContent.Append(equipmentConfigInfo.Offset.PadRight(FieldLength, ' '));
                    dataContent.Append(equipmentConfigInfo.Desc.PadRight(FieldLength, ' '));
                    dataContent.Append(equipmentConfigInfo.Unit.PadRight(FieldLength, ' '));
                    dataContent.Append(equipmentConfigInfo.Point.PadRight(FieldLength, ' '));
                    dataContent.Append(equipmentConfigInfo.Param1.PadRight(FieldLength, ' '));
                    dataContent.Append(equipmentConfigInfo.Param2);
                    dataContent.Append("\r\n");
                }
            }
            return dataContent;
        }

        private static StringBuilder SerialPortString(StringBuilder dataContent, List<SerialPortConfigInfo> serialPortList)
        {
            dataContent.Append("DDC_ID".PadRight(FieldLength, ' '));
            dataContent.Append("COMM".PadRight(FieldLength, ' '));
            dataContent.Append("BAUD".PadRight(FieldLength, ' '));
            dataContent.Append("DATABIT".PadRight(FieldLength, ' '));
            dataContent.Append("STOPBIT".PadRight(FieldLength, ' '));
            dataContent.Append("PARITY".PadRight(FieldLength, ' '));
            dataContent.Append("CNT_RESEND\r\n");

            foreach (SerialPortConfigInfo serialPortConfigInfo in serialPortList)
            {
                dataContent.Append(serialPortConfigInfo.DDC_ID.ToString().PadRight(FieldLength, ' '));
                dataContent.Append(serialPortConfigInfo.Comm.ToString().PadRight(FieldLength, ' '));
                dataContent.Append(serialPortConfigInfo.Baud.ToString().PadRight(FieldLength, ' '));
                dataContent.Append(serialPortConfigInfo.DataBit.ToString().PadRight(FieldLength, ' '));
                dataContent.Append(serialPortConfigInfo.StopBit.ToString().PadRight(FieldLength, ' '));
                dataContent.Append(serialPortConfigInfo.Parity.PadRight(FieldLength, ' '));
                dataContent.Append(serialPortConfigInfo.CNT_Resend.ToString());
                dataContent.Append("\r\n");
            }
            return dataContent;
        }

    }
}
