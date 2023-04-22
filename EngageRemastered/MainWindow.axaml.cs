using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Logging;

namespace EngageRemastered
{
    public partial class MainWindow : Window
    {

        /* important file locations */
        private string engageAssetsLocation = "C:\\Users\\darad\\Desktop\\\"Switch Hacking\"\\FEE\\EngageAssetsModified";
        private string engageXmlLocation = "C:\\Users\\darad\\Desktop\\\"Switch Hacking\"\\FEE\\EngageXML\\EngageXml.exe";

        /**
         * MainWindow: establish Avalonia debugging and initialize viewing screen
         */
        public MainWindow()
        {
            Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, "Avalonia Infrastructure");
            System.Diagnostics.Debug.WriteLine("System Diagnostics Debug");
            InitializeComponent();
            debugEnabled(true);
        }

        /**
         * debugEnabled: pre-establishes some variables for my own debugging purposes
         * args:
         *      enabled: whether or not to pre-establish variables
         */
        private void debugEnabled(bool enabled)
        {
            if (enabled)
            {
                engageAssetsLocation = "C:\\Users\\darad\\Desktop\\\"Switch Hacking\"\\FEE\\EngageAssetsModified";
                engageXmlLocation = "C:\\Users\\darad\\Desktop\\\"Switch Hacking\"\\FEE\\EngageXML\\EngageXml.exe";
            }
        }

        /**
         * executeCommands: executes a list of commands from the command prompt
         * args:
         *      commands    a list of string commands to execute from the command prompt
         */
        private void executeCommands(List<string> commands)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            foreach (string cmnd in commands)
            {
                cmd.StandardInput.WriteLine(cmnd);
                cmd.StandardInput.Flush();
            }

            cmd.StandardInput.Close();
            cmd.WaitForExit();
            System.Diagnostics.Debug.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        /**
         * btnRandomize_Click: executes whenever the Randomize button is pressed. Performs randomization
         * of all character base classes, starting inventories, and compartmentalizes them into .xml.bundle
         * files using EngageXml.exe
         */
        private void btnRandomize_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.Content = "You clicked me!";

            List<string> commands = new List<string>();
            commands.Add("cd " + engageAssetsLocation);
            commands.Add(engageXmlLocation + " calculator.xml");

            executeCommands(commands);

        }

        /**
         * btnEngageXmlPath_Click: Executes whenever the EngageXmlPath button is pressed. Obtains the
         * file location of EngageXml.exe and stores it in engageXmlLocation
         */
        private async void btnEngageXmlPath_Click(object sender, RoutedEventArgs e)
        {
            // open file window
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open EngageXml.exe";
            ofd.Filters = new List<FileDialogFilter>() { new FileDialogFilter() { Name = "All files", Extensions = new List<string>() { "exe" } } };
            ofd.AllowMultiple = false;
            // wait for user
            string[]? file = await ofd.ShowAsync(this);

            // get file path
            if (file == null)
            {
                return;
            }
            else
            {
                engageXmlLocation = file[0];
            }


        }

        /**
         * btnEngageXmlPath_Click: Executes whenever the EngageXmlPath button is pressed. Obtains the
         * file location of EngageXml.exe and stores it in engageXmlLocation
         */
        private async void btnEngageAssetsPath_Click(object sender, RoutedEventArgs e)
        {
            // open folder window
            OpenFolderDialog ofd = new OpenFolderDialog();
            ofd.Title = "Open Assets Folder";
            // wait for user
            string? file = await ofd.ShowAsync(this);

            // get folder path
            if (file == null)
            {
                return;
            }
            else
            {
                engageAssetsLocation = file;
            }
        }
    }
}