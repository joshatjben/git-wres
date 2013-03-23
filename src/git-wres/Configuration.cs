using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Xml;
using YamlDotNet.RepresentationModel;
using YamlDotNet.RepresentationModel.Serialization;


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

        public ConfigSettings Config{get; set;}

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

        /// <summary>
        /// Initialize the configuration files
        /// </summary>
        public void InitializeConfig()
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

        /// <summary>
        /// Add a remote CRM connection to the configuration file
        /// </summary>
        /// <param name="name">Name of the connection</param>
        /// <param name="url">http(s)://domain/organization</param>
        public void AddRemoteCRMConnectionToConfig(string name, string url)
        {
            Remote crmRemote = new Remote{
                Name = name,
                Url = url
            };

            List<Remote> remoteList = Config.RemoteConnections.ToList();
            remoteList.Add(crmRemote);

            Config.RemoteConnections = remoteList.ToArray();

            WriteToConfigSettingsToFile();
        }

        /// <summary>
        /// Write the config object to the config file
        /// </summary>
        public void WriteToConfigSettingsToFile()
        {
            StreamWriter configFile = new StreamWriter(FILE_CONFIG, false);

            Serializer s = new Serializer();
            s.Serialize(configFile, Config);
        }

        #endregion

        #region read from config

        public void ReadConfig()
        {
            if (!File.Exists(FILE_CONFIG))
            {
                Config = new ConfigSettings();
                return;
            }

            StreamReader input = new StreamReader(FILE_CONFIG);
            
            // Load the stream
            YamlStream yaml = new YamlStream();
            yaml.Load(input);

            // Examine the stream
            YamlMappingNode mapping = (YamlMappingNode)yaml.Documents[0].RootNode;


            // TODO put config into object
        }

        public static Remote[] ReadRemoteConnectionsFromConfig()
        {
            return new Remote[1];
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
            return ConfigurationManager.AppSettings["enableLogging"] != null ? ConfigurationManager.AppSettings["enableLogging"].ToLower() == "true" : false;
        }

        #endregion
    }

    public class Remote
    {
        public string Name{get; set;}
        public string Url { get; set; }
    }

    public class ConfigSettings
    {
        public Remote[] RemoteConnections{get; set;}
    }

}
