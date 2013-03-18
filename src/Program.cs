using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using me.joshbennett.git_wres.args;
using me.joshbennett.git_wres.config;

namespace me.joshbennett.git_wres
{
    class Program
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string invokedVerb;
        public static object invokedVerbInstance;
        
        private static Arguments options;
        private static Configuration config; 

        static void Main(string[] args)
        {
            config = Configuration.Instance;
            options = Arguments.Instance;

            ;

            if (!CommandLine.Parser.Default.ParseArguments(args, options,
                (verb, subOptions) => {
                    // if parsing succeeds the verb name and correct instance
                    // will be passed to onVerbCommand delegate (string,object)
                    invokedVerb = verb;
                    invokedVerbInstance = subOptions;
                })
              )
            {
              //Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            switch (invokedVerb)
            {
                case "init":
                    log.Info("Running init");
                    Commands.init(options.InitVerb);
                    break;
                case "pull":
                    log.Info("running pull");
                    break;
                case "push":
                    log.Info("running push");
                    break;
                case "remote":
                    log.Info("running remote");
                    Commands.remote(options.RemoteVerb);
                    break;
                default:
                    log.Info(String.Format("Default: verb is {0}",invokedVerb));
                    break;
            }

            Console.WriteLine("Press enter to continue....");
            Console.ReadKey();
        }
    }
}
