using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace me.joshbennett.git_wres.args
{
    public class Arguments
    {

        #region singleton
        
        private static Arguments _instance;

        private Arguments(){
            // Since we create this instance the parser will not overwrite it
            InitVerb = new InitSubOptions { };
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

        [VerbOption("push", HelpText = "Update remote refs along with associated objects.")]
        public PushSubOptions AddVerb { get; set; }

        #endregion

        [HelpOption]
        public string GetUsage()
        {
            // this without using CommandLine.Text
            //  or using HelpText.AutoBuild
            var usage = new StringBuilder();
            usage.AppendLine("Quickstart Application 1.0");
            usage.AppendLine("Read user manual for usage instructions...");
            return usage.ToString();
        }
    }

    public class InitSubOptions
    {
        
    }

    class PushSubOptions
    {
        
    }

    class PullSubOptions
    {
        // Remainder omitted 
    }
}
