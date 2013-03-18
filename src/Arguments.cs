using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public RemoteSubOptions()
        {
            AddVerb = new RemoteAddSubOptions { };
        }

        [VerbOption("add", HelpText = "Add remote crm 2011 connections")]
        public RemoteAddSubOptions AddVerb { get; set; }

    }

    public class RemoteAddSubOptions
    {
        [Option('n', "remote-name")]
        public string RemoteName { get; set; }

        [Option('s', "remote-solution")]
        public string SolutionName { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(new object { });
        }
    }
}
