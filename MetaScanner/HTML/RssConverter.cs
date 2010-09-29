////////////////////////////////////////////////////////////////
//
// Copyright (c) 2007-2010 MetaGeek, LLC
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//
//	http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
//
////////////////////////////////////////////////////////////////

using System.IO;
using System.Xml;
using System.Web.UI;

namespace inSSIDer.HTML
{
    class RssConverter
    {
        public void RssToHtml(string input, string output)
        {
            // write as UTF-8
            HtmlTextWriter writer = new HtmlTextWriter(new StreamWriter(output), "    ");

            XmlTextReader reader = new XmlTextReader(input)
                                       {
                                           XmlResolver = null,
                                           WhitespaceHandling = WhitespaceHandling.None
                                       };

            reader.MoveToContent();
            if (reader.Name == "rss")
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Html);
                while (reader.Read() &&
                  reader.NodeType != XmlNodeType.EndElement)
                {
                    switch (reader.LocalName)
                    {
                        case "channel":
                            ChannelToHtml(reader, writer);
                            break;
                        case "item":
                            //ItemToHtml(reader, writer);
                            break;
                        default: // ignore image and textinput.
                            break;
                    }
                }
                writer.RenderEndTag();
            }

            reader.Close();
            writer.Close();
        }

        void ChannelToHtml(XmlReader reader, HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Head);

            // scan header elements and pick out the title.
            reader.Read();
            while (reader.Name != "item" &&
              reader.NodeType != XmlNodeType.EndElement)
            {
                // SKIP title, etc. of the feed...
                reader.Skip();
            }

            // <link type="text/css" rel="stylesheet" media="all" href="./style.css" />
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
            writer.AddAttribute(HtmlTextWriterAttribute.Rel, "stylesheet");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "./news.css");
            writer.RenderBeginTag(HtmlTextWriterTag.Link);
            writer.RenderEndTag();

            
            writer.RenderEndTag();   // </head>

            writer.RenderBeginTag(HtmlTextWriterTag.Body);

            // transform the items.
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.Name == "item")
                {
                    ItemToHtml(reader, writer);
                }
                if (!reader.Read())
                    break;
            }

            writer.RenderEndTag();
        }

        void ItemToHtml(XmlReader reader, HtmlTextWriter writer)
        {
            string title = string.Empty;
            string link = string.Empty;
            string description = string.Empty;

            while (reader.Read() && ((reader.NodeType != XmlNodeType.EndElement) || reader.Name != "item"))
            {
                switch (reader.Name)
                {
                    case "title":
                        title = reader.ReadString();
                        break;
                    case "link":
                        link = reader.ReadString();
                        break;
                    case "description":
                        description = reader.ReadString();
                        break;

                    // Ignore tags we aren't using
                    case "pubDate":
                    case "comments":
                    case "guid":
                    case "category":
                        reader.ReadString();
                        break;
                }
            }

            // tweak img tags to have absolute path..
            description = description.Replace("<img src='/", "<img src='http://www.metageek.net/");
            description = description.Replace("<img src=\"/", "<img src=\"http://www.metageek.net/");

            // <div class="items">
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "items");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // <h1><a href=link>title</a></h1>
            writer.RenderBeginTag(HtmlTextWriterTag.H1);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, link);
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write(title);
            writer.RenderEndTag(); 
            writer.RenderEndTag(); 
            
            // <div class="description">description</div>
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "post-description");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.WriteLine(description);
            writer.RenderEndTag();

            // <div> for class="items"
            writer.RenderEndTag(); 

            writer.WriteLine();
        }

    }
}
