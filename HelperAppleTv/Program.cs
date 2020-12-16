using System;
using System.Net.Sockets;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace HelperAppleTv
{
    class Program
    {
        static void Main(string[] args)
        {
            
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
    }
}
