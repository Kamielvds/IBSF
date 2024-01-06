using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace DataProcessing.commands

{
    public class UserSettings
    {
        public static void FilterCommand(string[] command)
        {
            switch (command[1])
            {
                case "path":
                case "--p":
                    UpdatePath(command[2]);
                    break;
            }
        }

        private static void UpdatePath(string xmlContent)
        {
            
        }
    }
}