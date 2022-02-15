using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using AtlassianCore.FieldManagement.Conditions;
using AtlassianCore.FieldManagement.Updaters;
using AtlassianCore.Models;
using Newtonsoft.Json;

namespace AtlassianCore.FieldManagement
{
    /// <summary>
    /// The rule processor.
    /// </summary>
    public class Processor
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The database of all issues.
        /// </summary>
        private JiraIssueDatabase mDatabase = new JiraIssueDatabase();

        /// <summary>
        /// Gets or sets the field rules.
        /// </summary>
        public ObservableCollection<FieldRules> Rules
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets the technical settings.
        /// </summary>
        public TechSettings TechSettings
        {
            get;
            set;
        }

        public string Initialize()
        {
            this.TechSettings = TechSettings.Build(@".\techsettings.json");

            string lFilename = @".\rules.json";

            // If the setting is null, create default one.
            if (File.Exists(lFilename))
            {
                try
                {
                    string lJsonValue = File.ReadAllText(@".\rules.json");

                    List<JsonConverter> lConverters = new List<JsonConverter> {new FieldConverter()};
                    this.Rules = JsonConvert.DeserializeObject<ObservableCollection<FieldRules>>(lJsonValue, lConverters.ToArray() );
                }
                catch (Exception lEx)
                {
                    msLogger.Error("Cannot load rules.json" + lEx);
                    // ignored
                }
            }
            else
            {
                msLogger.Error("Cannot find rules.json");
            }

            if (this.Rules == null)
            {
                this.Rules = new ObservableCollection<FieldRules>();

                {
                    FieldRules lRule = new FieldRules();
                    lRule.Description = "Update status according to leaf node";

                    lRule.ParentField = "Status";
                    lRule.ChildrenField = "Status";

                    IfAllChildrenEqualTo lCondition0 = new IfAllChildrenEqualTo { Value = "Done" };
                    SetValue lUpdater0 = new SetValue { Value = "Closed" };
                    lRule.Rules.Add(new FieldRule() { Condition = lCondition0, Updater = lUpdater0 });

                    IfAtLeastOneChildEqualsTo lCondition1 = new IfAtLeastOneChildEqualsTo { Value = "Todo" };
                    SetValue lUpdater1 = new SetValue { Value = "Open" };
                    lRule.Rules.Add(new FieldRule() { Condition = lCondition1, Updater = lUpdater1 });

                    IfAtLeastOneChildEqualsTo lCondition2 = new IfAtLeastOneChildEqualsTo { Value = "On going" };
                    SetValue lUpdater2 = new SetValue { Value = "Open" };
                    lRule.Rules.Add(new FieldRule() { Condition = lCondition2, Updater = lUpdater2 });

                    IfAtLeastOneChildEqualsTo lCondition3 = new IfAtLeastOneChildEqualsTo { Value = "Review" };
                    SetValue lUpdater3 = new SetValue { Value = "Open" };
                    lRule.Rules.Add(new FieldRule() { Condition = lCondition3, Updater = lUpdater3 });

                    NoCondition lCondition4 = new NoCondition();
                    SetValue lUpdater4 = new SetValue { Value = "Not defined" };
                    lRule.Rules.Add(new FieldRule() { Condition = lCondition4, Updater = lUpdater4 });
                    this.Rules.Add(lRule);
                }

                Formatting lIndented = Formatting.Indented;
                var lSerialized = JsonConvert.SerializeObject(this.Rules, lIndented);
                File.WriteAllText(@".\rules.json", lSerialized);
            }

            return lFilename;
        }

        /// <summary>
        /// Runs all rules.
        /// </summary>
        public void Run()
        {
            msLogger.Info("Start a process to apply rules on database");
            this.mDatabase.Initialize(this.TechSettings.EndPoint, this.TechSettings.Username, this.TechSettings.Password, this.TechSettings.ResponseDebugPath, this.TechSettings.ProjectKeys);
            
            foreach (var lRule in this.Rules)
            {
                msLogger.Info("Executing " + lRule.Description);
                lRule.Update(this.mDatabase);
            }
        }
    }
}
