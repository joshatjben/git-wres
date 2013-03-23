using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Xml;

namespace GitWres
{
    public class Configuration
    {
        #region private props

        private bool _enableLogging;
        private static string _initLogLevel = LogManager.GetRepository().Threshold.DisplayName;
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region public const
        
        public static readonly string DIR_GIT = ".git";
        public static readonly string DIR_WRES = ".git\\git-wres";
        public static readonly string FILE_CONFIG = DIR_WRES + "\\config";
        public static readonly string FILE_SNAPSHOT = DIR_WRES + "\\LAST_SNAPSHOT";

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
                if (value)
                {
                    ResetLogging();
                }
                else
                {
                    DisableLogging();
                }

                _enableLogging = value;
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

        public static void InitializeConfig()
        {
            
            // Check to see if 
            if (
                Directory.Exists(DIR_GIT)
                && Directory.Exists(DIR_WRES) 
                && File.Exists(FILE_CONFIG)
                && File.Exists(FILE_SNAPSHOT)
                )
            {
                log.Warn("Nothing to do, directory has already been initialized by git-wres.");
                Console.WriteLine("Nothing to do.");
                Console.WriteLine("This directory has already been initialized by git-wres.");
                return;
            }

            // Check if there is a .git folder in the working directory
            if (!Directory.Exists(DIR_GIT))
            {
                System.Diagnostics.Process.Start("git", "init");
            }

            // Check that git-wres directory exists
            if (!Directory.Exists(DIR_WRES))
            {
                Directory.CreateDirectory(DIR_WRES);
            }

            // Check that config file exists
            if (!File.Exists(FILE_CONFIG))
            {
                FileStream file = File.Create(FILE_CONFIG);
                file.Close();
            }

            if (!File.Exists(FILE_SNAPSHOT))
            {
                FileStream file = File.Create(FILE_SNAPSHOT);
                file.Close();
            }

            log.Info("directory has been initialized by git-wres.");

            Console.WriteLine("Success. This directory has been initialized by git-wres.");
        }

        public static void AddRemoteCRMConnectionToConfig(string name, string url)
        {
            FileStream configFile = File.Open(FILE_CONFIG, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            
            if (!configFile.CanWrite)
            {
                log.Error("Can not write to file " + FILE_CONFIG + ".");
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
                return;
            }

            string successMessage = String.Format("A Remote connection \"{0}\" has been added with url \"{1}\".", name, url);
            
            log.Info(successMessage);
            Console.WriteLine("Success. " + successMessage);

            configFile.Close();
        }

        #endregion

        #region read from config

        /*public static Remotes[] ReadRemoteConnectionsFromConfig()
        {
            //FileStream configFile = File.Open(FILE_CONFIG
            return new Remotes[1];
        }*/

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
        /// Reset logging to what appears in the app.config
        /// </summary>
        public static void ResetLogging()
        {
            SetLogging(_initLogLevel);
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
