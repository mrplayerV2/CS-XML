using System.Collections.Generic;
using System.IO;
using System.Xml;

public class XMLTool
{
	public string xmlDocToString(XmlDocument xmlDoc, bool withXMLDeclaration)
	{
		if (!withXMLDeclaration)
		{
			return xmlDoc.FirstChild.OuterXml;
		}
		using (StringWriter stringWriter = new StringWriter())
		{
			using (XmlWriter xmlTextWriter = XmlWriter.Create(stringWriter))
			{
				xmlDoc.WriteTo(xmlTextWriter);
				xmlTextWriter.Flush();
				return stringWriter.GetStringBuilder().ToString();
			}
		}
	}

	public void appendElementWithNodeValue(string nodeName, string nodeValue, ref XmlDocument xmlDoc, ref XmlElement targetNode)
	{
		XmlElement newElement = xmlDoc.CreateElement(nodeName);
		XmlCDataSection newText = xmlDoc.CreateCDataSection(nodeValue);
		newElement.AppendChild(newText);
		targetNode.AppendChild(newElement);
	}

	public static void setXMLNodeAttribute(ref XmlDocument doc, ref XmlNode node, string name, string value)
	{
		XmlAttribute attr = doc.CreateAttribute(name);
		attr.Value = value;
		node.Attributes.SetNamedItem(attr);
	}

	public static string getXMLNodeAttribute(XmlNode node, string attributeName)
	{
		string attributeValue = null;
		try
		{
			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (node.Attributes[i].Name.ToString().Equals(attributeName))
				{
					attributeValue = node.Attributes[i].Value.ToString();
					return attributeValue;
				}
			}
			return attributeValue;
		}
		catch
		{
			return attributeValue;
		}
	}

	public static Dictionary<string, string> getXMLNodeAttributes(XmlNode node)
	{
		Dictionary<string, string> attributes = new Dictionary<string, string>();
		for (int i = 0; i < node.Attributes.Count; i++)
		{
			attributes.Add(node.Attributes[i].Name.ToString(), node.Attributes[i].Value.ToString());
		}
		return attributes;
	}

	public void appendChildToXML(string nodeName, string nodeValue, XmlDocument targetDoc)
	{
		XmlElement elem = targetDoc.CreateElement(nodeName);
		XmlText text = targetDoc.CreateTextNode(nodeValue);
		targetDoc.DocumentElement.AppendChild(elem);
		targetDoc.DocumentElement.LastChild.AppendChild(text);
	}

	public void appendCDATAChildToXML(string nodeName, string nodeValue, XmlDocument targetDoc)
	{
		XmlNode elem = targetDoc.CreateElement(nodeName);
		targetDoc.DocumentElement.AppendChild(elem);
		targetDoc.DocumentElement.LastChild.AppendChild(targetDoc.CreateCDataSection(nodeValue));
	}
}