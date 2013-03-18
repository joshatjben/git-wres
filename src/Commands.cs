using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using me.joshbennett.git_wres.args;

namespace me.joshbennett.git_wres
{
    public class Commands
    {
        public static void init(InitSubOptions options)
        {
            string dirGit = ".git";
            string dirWres = ".git\\git-wres";
            string fileConfig = dirWres + "\\config";

            if ( Directory.Exists(dirGit) && Directory.Exists(dirWres) && File.Exists(fileConfig))
            {
                Console.Write("This directory has already been initialized by git-wres");
                return;
            }

            if (!Directory.Exists(dirGit))
            {
                System.Diagnostics.Process.Start("git", "init");
            }

            if (!Directory.Exists(dirWres))
            {
                Directory.CreateDirectory(dirWres);
            }

            if(!File.Exists(fileConfig))
            {
                StreamWriter configStream = File.CreateText(".git\\git-wres\\config");
                configStream.WriteLine("[core]");
                configStream.WriteLine("\tlogging = false");
                configStream.Close();
            }

        }

        public static void push()
        {
        }

        public static void pull()
        {
   
        }

        public static void remote()
        {
        }
    }
}
