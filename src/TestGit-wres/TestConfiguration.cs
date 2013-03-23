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
            Configuration.InitializeConfig();
            
            
        }
    }
}
