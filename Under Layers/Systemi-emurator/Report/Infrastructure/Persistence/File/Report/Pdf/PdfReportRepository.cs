using System;
using System.Xml;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public abstract class PdfReportRepository
    {

        public void Output(String xsltPath, String pdfPath)
        {
            PdfTransformer.Transform(CreateDocument(), xsltPath, pdfPath);
        }

        protected abstract void CreateDocumentBody();

        private XmlDocument document;

        private void CreateDocumentWithAddRootNode()
        {
            document = new XmlDocument();
            XmlElement root = CreateElement("root");
            document.AppendChild(root);
        }

        protected XmlElement CreateElement(String tagName)
        {
            return document.CreateElement(tagName);
        }

        private XmlText CreateTextNode(String data)
        {
            return document.CreateTextNode(data);
        }

        private XmlNode RootNode()
        {
            return document.DocumentElement.FirstChild;
        }

        protected XmlDocument CreateDocument()
        {

            CreateDocumentWithAddRootNode();

            CreateDocumentBody();

            return document;

        }

        protected XmlNode AppendChildToRoot(XmlNode newChild)
        {
            return RootNode().AppendChild(newChild);
        }

        protected XmlAttribute CreateAttribute(String name, String value)
        {
            XmlAttribute attr = document.CreateAttribute(name);
            attr.Value = value;
            return attr;
        }

        protected XmlElement SetAttributeNodeWithCreating(XmlElement element, String nameOfAttr, String valueOfAttr)
        {
            XmlAttribute attr = CreateAttribute(nameOfAttr, valueOfAttr);
            element.SetAttributeNode(attr);
            return element;
        }

        protected XmlNode AppendElementWithCreateingWithAppendTextChild(XmlNode element, String tagNameOfElement, String textOfChild)
        {
            var grandChild = CreateTextNode(textOfChild);
            var child = CreateElement(tagNameOfElement);
            child.AppendChild(grandChild);
            element.AppendChild(child);
            return element;
        }

        protected XmlNode AppendChildToNode(XmlNode root, XmlNode newChild)
        {
            return root.AppendChild(newChild);
        }
    }
}