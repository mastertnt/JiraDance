using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AtlassianCore
{
    /// <summary>
    /// Stores the technical settings.
    /// </summary>
    public class TechSettings
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TechSettings()
        {
            this.ProjectKeys = new List<string>();
        }

        /// <summary>
        /// API username.
        /// </summary>
        public string Username
        {
            get;
            set;
        }

        /// <summary>
        /// API password.
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// URL endpoint e.g : https://xxx.atlassian.net/rest
        /// </summary>
        public string EndPoint
        {
            get;
            set;
        }

        /// <summary>
        /// Debug response path. If null, no debug is done.
        /// </summary>
        public string ResponseDebugPath
        {
            get;
            set;
        }

        /// <summary>
        /// This flag is used to log only the actions (nothing is done on the server).
        /// </summary>
        public bool LogOnlyAction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the project keys where to retrieve the issues.
        /// </summary>
        public List<string> ProjectKeys
        {
            get;
            set;
        }

        /// <summary>
        /// Build the technical settings.
        /// </summary>
        /// <param name="filename">filename of the technical settings.</param>
        /// <returns>The technical settings.</returns>
        public static TechSettings Build(string filename)
        {
            TechSettings lSettings = null;

            // If the setting is null, create default one.
            if (File.Exists(filename))
            {
                try
                {
                    string lJsonValue = File.ReadAllText(filename);
                    lSettings = JsonConvert.DeserializeObject<TechSettings>(lJsonValue);
                }
                catch (Exception lEx)
                {
                    msLogger.Error("Cannot load " + filename + " / " + lEx);
                    // ignored
                }
            }
            else
            {
                msLogger.Error("Cannot find " + filename);
            }

            if (lSettings == null)
            {
                lSettings = new TechSettings()
                {
                    EndPoint = "https://tuleap.diginext.local/api/",
                    Username = "USER_TO_CHANGE",
                    Password = "PASSWORD_TO_CHANGE",
                    LogOnlyAction = true,
                    ProjectKeys = new List<string>() { "SDCNGF, TDCWP31" },
                    ResponseDebugPath = ""
                };

                Formatting lIndented = Formatting.Indented;
                var lSerialized = JsonConvert.SerializeObject(lSettings, lIndented);
                File.WriteAllText(filename, lSerialized);
            }

            return lSettings;
        }
    }
}
