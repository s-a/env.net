

namespace env.net
{
    public class WebConfig
    {
        public enum Environments { development, test, staging, production }

        Environments GetEnvironment() {
            Environments result = Environments.development;

            string environmentString = System.Environment.GetEnvironmentVariable("ASP_ENV");
            try
            {
                Environments environmentValue = (Environments)System.Enum.Parse(typeof(Environments), environmentString);
                if (System.Enum.IsDefined(typeof(Environments), environmentValue))
                {
                    result = environmentValue;
                }
            }
            catch (System.ArgumentException)
            {
                return Environments.development;
            }

            return result;
        }

        public bool Patch() {
            string home = System.Web.HttpRuntime.AppDomainAppPath;
            System.IO.DirectoryInfo folder = new System.IO.DirectoryInfo(home);
            if (folder.Exists) 
            {
                string wildcard = "config." + this.GetEnvironment().ToString() + ".*.xml";
                System.IO.FileInfo[] files = folder.GetFiles(wildcard);
                System.Collections.Generic.List<string> section = new System.Collections.Generic.List<string>();

                string webConfigFilename = System.IO.Path.Combine(home, "Web.config");
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(webConfigFilename);

                bool changed = false;

                foreach (System.IO.FileInfo file in files)
                {
                    string webConfigSectionFilename = System.IO.Path.Combine(home, file.Name);

                    string[] filename = file.Name.Split('.');
                    for (int i = 2; i < filename.Length-1; i++)
                    {
                        section.Add(filename[i]);
                    }

                    System.Xml.XmlDocument xmlDocSection = new System.Xml.XmlDocument();

                    xmlDocSection.Load(webConfigSectionFilename);

                    string xPath = System.String.Join("/", section);
                    System.Xml.XmlNode node = xmlDoc.SelectSingleNode(xPath);

                    if (node != null && node.InnerXml != xmlDocSection.ChildNodes[1].InnerXml) {
                        node.InnerXml = xmlDocSection.ChildNodes[1].InnerXml;
                        changed = true;
                    }
                }
                if (changed)
                {
                    xmlDoc.Save(webConfigFilename);
                }
            }
            return true;
        }


        public WebConfig() {
            
        }
    }
}
