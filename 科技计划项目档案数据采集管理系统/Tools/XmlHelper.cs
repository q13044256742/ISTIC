using System.Xml;
using System.Data;
using System;

namespace DotNet.Utilities
{
    /// <summary>
    /// Xml的操作公共类
    /// </summary>    
    public class XmlHelper
    {
        /// <summary>
        /// 创建XML文档
        /// </summary>
        /// <param name="name">根节点名称</param>
        /// <param name="type">根节点的一个属性值</param>
        /// <returns>XmlDocument对象</returns>     
        public static XmlDocument CreateXmlDocument(string name, string type)
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.LoadXml("<" + name + "/>");
                var rootEle = doc.DocumentElement;
                rootEle?.SetAttribute("type", type);
            }
            catch(Exception e)
            {

            }
            return doc;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        public static string Read(string path, string node, string attribute)
        {
            var value = "";
            try
            {
                var doc = new XmlDocument();
                doc.Load(path);
                var xn = doc.SelectSingleNode(node);
                if(xn != null && xn.Attributes != null)
                    value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch(Exception e)
            {
                
            }
            return value;
        }
    }
}