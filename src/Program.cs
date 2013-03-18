using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using me.joshbennett.git_wres.args;

namespace me.joshbennett.git_wres
{
    class Program
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string invokedVerb;
        public static object invokedVerbInstance;
        private static Arguments argumnets;

        static void Main(string[] args)
        {
            argumnets = Arguments.Instance;

            if (!CommandLine.Parser.Default.ParseArguments(args, argumnets,
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
                    log.Info("Verb is init");
                    Commands.init(argumnets.InitVerb);
                    break;
                case "pull":
                    log.Info("Verb is pull");
                    break;
                case "push":
                    log.Info("Verb is push");
                    break;
                case "remote":
                    log.Info("Verb is remote");
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
