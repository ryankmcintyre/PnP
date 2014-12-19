﻿using System;
using System.IO;
using System.Management.Automation;
using System.Linq;
using System.Xml.Linq;

namespace OfficeDevPnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "SPOConfiguration")]
    public class GetConfiguration : PSCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Key;

        protected override void ProcessRecord()
        {
            string path = null;
            XDocument document = null;

            // check for existing configuration, if not existing, create it
            string appDataFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configFolder = System.IO.Path.Combine(appDataFolder, "OfficeDevPnP.PowerShell");
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }
            path = System.IO.Path.Combine(configFolder, "configuration.xml");


            if (!File.Exists(path))
            {
                document = new XDocument(new XDeclaration("1.0", "UTF-8", string.Empty));
                var configElement = new XElement("items");
                document.Add(configElement);
                document.Save(path);
            }
            else
            {
                document = XDocument.Load(path);
            }

            if (Key != null)
            {
                var configItems = from item in document.Descendants("item") where item.Attribute("key").Value == Key select new { Key = item.Attribute("key").Value, Value = item.Value };
                foreach (var configItem in configItems)
                {
                    WriteObject(configItem);
                }
            }
            else
            {
                var configItems = from item in document.Descendants("item") select new { Key = item.Attribute("key").Value, Value = item.Value };
                foreach (var configItem in configItems)
                {
                    WriteObject(configItem);
                }
            }
        }


    }
}
