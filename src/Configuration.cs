using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Xml;

namespace me.joshbennett.git_wres.config
{
    public class Configuration
    {
        #region private props

        private bool _enableLogging = false;
        private string _initLogLevel = LogManager.GetRepository().Threshold.DisplayName;

        #endregion

        #region fields

        public bool EnalbeLogging
        {
            get
            {
                return _enableLogging;
            }
            set
            {
                if (value == _enableLogging)
                {
                    return;
                }
                
                _enableLogging = value;
                
                if(value){
                    
                }
            }
        }

        #endregion

        #region instance

        private static Configuration _instance;

        private Configuration()
        {
            EnalbeLogging = IsLoggingEnabledByDefault();
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

        #region writeToConfig

        public static void AddRemoteCRMToConfig(string name, string url)
        {
        }

        #endregion

        #region logging config functions

        /// <summary>
        /// Disable log4net logging
        /// </summary>
        public static void DisableLogging()
        {
            SetLogging("OFF");
        }

        /// <summary>
        /// Set the log4net logging level
        /// </summary>
        /// <param name="level"></param>
        public static void SetLogging(string level)
        {
            LogManager.GetRepository().Threshold = LogManager.GetRepository().LevelMap[level];
        }

        /// <summary>
        /// Look to the app settings to see if logging is enabled by default.
        /// </summary>
        /// <returns></returns>
        private static bool IsLoggingEnabledByDefault()
        {
            return ConfigurationManager.AppSettings["enableLogging"].ToLower() == "true";
        }

        #endregion
    }

    public class Remotes
    {
        public string Name{get; set;}
        public string Url { get; set; }
    }

}
