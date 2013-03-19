using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using me.joshbennett.git_wres.args;
using me.joshbennett.git_wres.config;

namespace me.joshbennett.git_wres
{
    public class Commands
    {
        public static void init(InitSubOptions options)
        {
            Configuration.InitializeConfig();

        }

        public static void push()
        {
        }

        public static void pull()
        {

        }

        public static void remote(RemoteSubOptions options)
        {
            Configuration.AddRemoteCRMConnectionToConfig(options.RemoteName, options.url);
        }
    }
}