using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitWres.args;
using GitWres.config;

namespace GitWres.cmds
{
    public class Commands
    {
        public static void init(InitSubOptions options)
        {
            Configuration.Instance.InitializeConfig();

        }

        public static void push()
        {
        }

        public static void pull()
        {

        }

        public static void remote(RemoteSubOptions options)
        {
            //Configuration.AddRemoteCRMConnectionToConfig(options.RemoteName, options.url);
        }
    }
}