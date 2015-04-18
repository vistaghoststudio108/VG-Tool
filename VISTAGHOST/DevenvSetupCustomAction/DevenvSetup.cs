using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;


namespace DevenvSetupCustomAction
{
    [RunInstaller(true)]
    public partial class DevenvSetup : Installer
    {
        public DevenvSetup()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            using (RegistryKey setupKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\9.0\Setup\VS"))
            {
                if (setupKey != null)
                {
                    string devenv = setupKey.GetValue("EnvironmentPath").ToString();
                    if (!string.IsNullOrEmpty(devenv))
                    {
                        Process.Start(devenv, "/setup /nosetupvstemplates").WaitForExit();
                    }
                }
            }
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            using (RegistryKey setupKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\9.0\Packages\{8b00b194-7670-422b-9ff2-40f0b2527890}"))
            {
                string path = setupKey.GetValue("CodeBase").ToString();

                if(!String.IsNullOrEmpty(path))
                {
                    string rootDir = Directory.GetDirectoryRoot(path);

                    if(Directory.Exists(rootDir))
                        Directory.Delete(rootDir, true);
                }
            }
        }
    }
}
