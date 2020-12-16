using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace HelperAppleTv
{
    class Program
    {
        static void Main(string[] args)
        {
            var parseArgs = getArgs(args);
            connectAppleTv(parseArgs["IP"], parseArgs["u"], parseArgs["p"]);
        }

        private static void connectAppleTv(string IP, string user, string pass)
        {
            using (var client = new SshClient(IP, user, pass)) {
                try
                {
                    client.Connect();
                    client.RunCommand("reboot");
                }
                catch (SocketException e)
                {
                    //TODO - Notificacion IP no valida
                }
                catch (SshAuthenticationException e)
                {
                    //TODO - Notificacion usuario y/o password erroneas
                }
                finally
                {
                    client.Disconnect();
                }
            }
        }

        private static Dictionary<string, string> getArgs(string[] args)
        {
            Dictionary<string, string> argsMap = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                string[] getKeyValue = args[i].Split(':');
                argsMap.Add(getKeyValue[0], getKeyValue[1]);
            }
            return argsMap;
        }
    }
}
