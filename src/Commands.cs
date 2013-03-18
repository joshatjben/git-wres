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
        void init(InitSubOptions options)
        {
            if (Directory.Exists(".git"))
            {
                Console.WriteLine("Initializing in ");
            }
        }

        void push()
        {
        }

        void pull()
        {
   
        }

        void remote()
        {
        }
    }
}
