﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeDevPnP.Core.Tests;
namespace Microsoft.SharePoint.Client.Tests
{
    [TestClass()]
    public class FeatureExtensionsTests
    {
        const string TEST_CATEGORY = "Feature Extensions";
        private ClientContext clientContext;
        private Guid sp2007WorkflowSiteFeatureId = new Guid("c845ed8d-9ce5-448c-bd3e-ea71350ce45b");
        private Guid contentOrganizerWebFeatureId = new Guid("7ad5272a-2694-4349-953e-ea5ef290e97c");

        [TestInitialize()]
        public void Initialize()
        {
            clientContext = TestCommon.CreateClientContext();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            clientContext.Dispose();
        }

        [TestCategory(TEST_CATEGORY)]
        [TestMethod()]
        public void ActivateFeatureTest()
        {
            // Test
            clientContext.Site.ActivateFeature(sp2007WorkflowSiteFeatureId);

            Assert.IsTrue(clientContext.Site.IsFeatureActive(sp2007WorkflowSiteFeatureId));

            clientContext.Site.DeactivateFeature(sp2007WorkflowSiteFeatureId);
            
            Assert.IsFalse(clientContext.Site.IsFeatureActive(sp2007WorkflowSiteFeatureId));
        }

        [TestCategory(TEST_CATEGORY)]
        [TestMethod()]
        public void ActivateFeatureTest1()
        {
            // Test
            clientContext.Web.ActivateFeature(contentOrganizerWebFeatureId);

            Assert.IsTrue(clientContext.Web.IsFeatureActive(contentOrganizerWebFeatureId));

            clientContext.Web.DeactivateFeature(contentOrganizerWebFeatureId);

            Assert.IsFalse(clientContext.Web.IsFeatureActive(contentOrganizerWebFeatureId));
        }

        [TestCategory(TEST_CATEGORY)]
        [TestMethod()]
        public void DeactivateFeatureTest()
        {
            // Setup
            clientContext.Site.ActivateFeature(sp2007WorkflowSiteFeatureId);

            // Test
            clientContext.Site.DeactivateFeature(sp2007WorkflowSiteFeatureId);
            Assert.IsFalse(clientContext.Site.IsFeatureActive(sp2007WorkflowSiteFeatureId));
        }

        [TestCategory(TEST_CATEGORY)]
        [TestMethod()]
        public void DeactivateFeatureTest1()
        {
            // Setup
            clientContext.Web.ActivateFeature(contentOrganizerWebFeatureId);

            // Test
            clientContext.Web.DeactivateFeature(contentOrganizerWebFeatureId);
            Assert.IsFalse(clientContext.Web.IsFeatureActive(contentOrganizerWebFeatureId));
        }

        [TestCategory(TEST_CATEGORY)]
        [TestMethod()]
        public void IsFeatureActiveTest()
        {
            // Setup
            try
            {
                clientContext.Site.DeactivateFeature(sp2007WorkflowSiteFeatureId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ignoring exception: {0}", ex.Message);
            }

            // Test
            Assert.IsFalse(clientContext.Site.IsFeatureActive(sp2007WorkflowSiteFeatureId));
        }

        [TestMethod()]
        public void IsFeatureActiveTest1()
        {
            // Setup
            try
            { 
                clientContext.Web.DeactivateFeature(contentOrganizerWebFeatureId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ignoring exception: {0}", ex.Message);
            }

            // Test
            Assert.IsFalse(clientContext.Web.IsFeatureActive(contentOrganizerWebFeatureId));
        }
    }
}
