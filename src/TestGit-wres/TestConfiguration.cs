using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GitWres;

namespace TestGit_wres
{
    [TestClass]
    public class TestConfiguration
    {
        [TestMethod]
        public void TestInitializeConfig()
        {
            // Delete the config director to start fresh
            if (
                Directory.Exists(Configuration.DIR_GIT) && 
                Directory.Exists(Configuration.DIR_WRES))
            {
                Directory.Delete(Configuration.DIR_WRES, true);
            }

            // Initialize directory
            Configuration.Instance.InitializeConfig();

            //Make sure both the config and snapshot files have been created
            Assert.IsTrue(File.Exists(Configuration.FILE_CONFIG));
            Assert.IsTrue(File.Exists(Configuration.FILE_SNAPSHOT));
        }

        [TestMethod]
        public void TestAddRemoteCRMConnectionToConfig()
        {
            Configuration.Instance.AddRemoteCRMConnectionToConfig("test1", "http://crmdev/example");
        }
    }
}
