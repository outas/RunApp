/* 
 * RunApp
 * by outas,
 *
 * Created: Feb 2, 2021
 *
 * Register a Custom URL Protocol Handler
 * 
 */

#region Namespace Inclusions
using System;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;
#endregion

namespace RunApp
{
	class Program
	{
		static void Main(string[] args)
		{
			// The name of this app for user messages
			string title = "错误";

			// Verify the command line arguments
			if (args.Length == 0)
			{ MessageBox.Show("缺少启动参数！", title); return; }

			// Obtain the part of the protocol we're interested in
			string regex = Regex.Match(args[0], @"(?<=://).+?(?=:|/|\Z)").Value;
			
			string[] split = regex.Split(';') ;

			// Path to the configuration file
			string file = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "RegisteredApps.xml");

			// Verify the config file exists
			if (!File.Exists(file))
			{ MessageBox.Show("找不到配置文件：\n" + file, title, MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

			// Load the config file
			XmlDocument xml = new XmlDocument();
			try { xml.Load(file); }
			catch (XmlException e) 
			{ MessageBox.Show(String.Format("XML配置文件错误，请检查后重试！\n{0}\n{1}", file, e.Message), title, MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

			// Locate the app to run
			XmlNode node = xml.SelectSingleNode(String.Format("/RunApp/App[@key='{0}']", split[0]));

			// If the app is not found, let the user know
			if (node == null)
			{ MessageBox.Show("未配置的APP KEY：" + split[0] + "\n请检查配置文件！\n" + file , title); return; }
	
			// Resolve the target app name
			string target = Environment.ExpandEnvironmentVariables(node.SelectSingleNode("@target").Value);

			string[] args2 = new string[split.Length];
			for (int i = 1; i < split.Length; i++)
            {
               args2[i-1] = split[i];
            }
			// Pull the command line args for the target app if they exist
			string procargs = Environment.ExpandEnvironmentVariables(string.Join(" ", args2));

			// Start the application
			Process.Start(target, procargs);
		}
	}
}
