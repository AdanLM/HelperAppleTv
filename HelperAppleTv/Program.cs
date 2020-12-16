using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using Renci.SshNet;
using Renci.SshNet.Common;
using Windows.UI.Notifications;

namespace HelperAppleTv
{
    class Program
    {

        static void Main(string[] args)
        {
            var parseArgs = GetArgs(args);
            ConnectAppleTv(parseArgs["IP"], parseArgs["u"], parseArgs["p"]);
        }

        private static void ConnectAppleTv(string IP, string user, string pass)
        {
            using (var client = new SshClient(IP, user, pass)) {
                try
                {
                    client.Connect();
                    client.RunCommand("reboot");
                }
                catch (SocketException e)
                {
                    ShowToast("Error IP", "IP " + IP + " no valida.\nFavor de verificar.");
                }
                catch (SshAuthenticationException e)
                {
                    ShowToast("Error Auth", "Usuario o contraseña incorrecta.\nFavor de verificar.");
                }
                finally
                {
                    client.Disconnect();
                }
            }
        }

        private static Dictionary<string, string> GetArgs(string[] args)
        {
            Dictionary<string, string> argsMap = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                string[] getKeyValue = args[i].Split(':');
                argsMap.Add(getKeyValue[0], getKeyValue[1]);
            }
            return argsMap;
        }

        private static void ShowToast(string Title, string message)
        {
            var xml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            var text = xml.GetElementsByTagName("text");
            text[0].AppendChild(xml.CreateTextNode(Title));
            text[1].AppendChild(xml.CreateTextNode(message));

            ToastNotification toast = new ToastNotification(xml);
            ToastNotificationManager.CreateToastNotifier(Assembly.GetExecutingAssembly().GetName().Name).Show(toast);
        }
    }
}
