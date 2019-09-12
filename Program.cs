using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Text.RegularExpressions;

namespace SharpSniper
{
    class Program
    {
        static void Main(string[] args) 
        {
            if (args.Length < 3 || args.Length > 3)
            {
                System.Console.WriteLine("\r\n\r\nSniper: Find hostname and IP address of specific user (CEO etc) in Domain (requires Domain Admin Credentials or DC Event" +
                    "logs must be readable by your user.");
                System.Console.WriteLine("");
                System.Console.WriteLine("Usage: Sniper.exe TARGET_USERNAME DAUSER DAPASSWORD");
                System.Environment.Exit(1);
            }

            string targetusername = String.Empty;
            string dauser = String.Empty;
            string dapass = String.Empty;


            if (args.Length == 3)
            {
                targetusername = args[0];
                dauser = args[1];
                dapass = args[2];
            }

            string domain_name = DomainInformation.GetDomainOrWorkgroup();


            Domain domain = Domain.GetCurrentDomain();

            //Finds all of the discoverable domain controllers in this domain.
            List<string> dclist = new List<string>();
            foreach (DomainController dc in domain.FindAllDiscoverableDomainControllers())
            {
                dclist.Add(dc.Name);
            }
            // Loop through domain controllers and find hostname of user
            foreach (string dcfound in dclist)
            {
                string target_hostname = string.Empty;
                target_hostname = QueryDC.QueryRemoteComputer(targetusername, domain_name, dcfound, dauser, dapass);
                Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                MatchCollection result = ip.Matches(target_hostname);
                {
                    Console.WriteLine("User: " + targetusername + " - " + "IP Address: " + result[0]);
                }
                
            }


        }

    }
}
