using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace me.joshbennett.git_wres.config
{
    public class Configuration
    {

        #region instance
        
        private static Configuration _instance;

        private Configuration()
        {

        }

        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Configuration();
                }

                return _instance;
            }
        }

        #endregion

        public static void AddRemoteCRM(string name, string url)
        {
        }
    }

    public class Remotes
    {
        public string Name{get; set;}
        public string Url { get; set; }
    }

}
