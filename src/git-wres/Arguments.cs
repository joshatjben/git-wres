using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CommandLine;
using CommandLine.Text;
using log4net;

namespace me.joshbennett.git_wres.args
{
    public class Arguments
    {
        
        #region singleton
        
        private static Arguments _instance;

        private Arguments(){
            // Since we create this instance the parser will not overwrite it
            InitVerb = new InitSubOptions { };
            PushVerb = new PushSubOptions { };
            PullVerb = new PullSubOptions { };
            RemoteVerb = new RemoteSubOptions { };
        }

        public static Arguments Instance {
            get
            {
                if (_instance == null)
                {
                    _instance = new Arguments();
                }

                return _instance;
            }
        }

        #endregion

        #region verb options

        [VerbOption("init", HelpText = "initalize the repository.")]
        public InitSubOptions InitVerb { get; set; }

        [VerbOption("push", HelpText = "Push the changes to crm 2011")]
        public PushSubOptions PushVerb { get; set; }

        [VerbOption("pull", HelpText = "Pull the most recent changes from crm 2011")]
        public PullSubOptions PullVerb { get; set; }

        [VerbOption("remote", HelpText = "Manage remote crm 2011 connections")]
        public RemoteSubOptions RemoteVerb { get; set; }

        #endregion

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(new object { });
        }
    }

    public class InitSubOptions
    {
        
    }

    public class PushSubOptions
    {
        
    }

    public class PullSubOptions
    {
        
    }

    public class RemoteSubOptions
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string _url;

        [ValueOption(0)]
        public string action { get; set; }

        [ValueOption(1)]
        public string url
        {
            get
            {
                return _url;
            }
            set
            {
                // Test to make sure that the url is in the form of http(s)://domain/organization/
                Regex matchUrl = new Regex("^https?://[^/]+/[^/\n$]+/?$", RegexOptions.IgnoreCase);
                
                if (!matchUrl.IsMatch(value))
                {
                    string errorMessage = "Error: the url must be in the form of http(s)://domain/organization";
                    Console.WriteLine("Failed");
                    Console.WriteLine(errorMessage);
                    log.Error(errorMessage);
                    Console.ReadKey();
                    Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
                }

                _url = value;
            }
        }

        [Option('n', "remote-name"
            , MutuallyExclusiveSet="add")]
        public string RemoteName { get; set; }

        [Option('s', "remote-solution"
            , MutuallyExclusiveSet ="add")]
        public string SolutionName { get; set; }
    }

}
