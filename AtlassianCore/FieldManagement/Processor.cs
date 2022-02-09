using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using JiraDance.FieldManagement.Conditions;
using JiraDance.FieldManagement.Updaters;
using JiraDance.Models;
using Newtonsoft.Json;
using RestEase;

namespace JiraDance.FieldManagement
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
            // If the setting is null, create default one.
            if (File.Exists(@".\techsettings.json"))
            {
                try
                {
                    string lJsonValue = File.ReadAllText(@".\techsettings.json");
                    this.TechSettings = JsonConvert.DeserializeObject<TechSettings>(lJsonValue);
                }
                catch (Exception lEx)
                {
                    msLogger.Error("Cannot load techsettings.json" + lEx);
                    // ignored
                }
            }
            else
            {
                msLogger.Error("Cannot find settings.json");
            }

            if (this.TechSettings == null)
            {
                this.TechSettings = new TechSettings()
                {
                    EndPoint = "https://tuleap.diginext.local/api/",
                    Username= "USER_TO_CHANGE",
                    Password= "PASSWORD_TO_CHANGE",
                    LogOnlyAction= true,
                    ProjectKeys = new List<string>() { "SDCNGF, TDCWP31" },
                    ResponseDebugPath = ""
                };

                Formatting lIndented = Formatting.Indented;
                var lSerialized = JsonConvert.SerializeObject(this.TechSettings, lIndented);
                File.WriteAllText(@".\techsettings.json", lSerialized);
            }

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
                    lRule.Description = "ATMC DD update process according to tasks";

                    lRule.TargetField = new Field { TrackerId = 344, FieldName = "Status" };
                    Field lSourceField = new Field { TrackerId = 342, FieldName = "Status" };
                    lRule.SourceFields.Add(lSourceField);

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

                {
                    FieldRules lRule = new FieldRules();
                    lRule.Description = "SOCLE GLOBAL DD update process according to tasks";

                    lRule.TargetField = new Field { TrackerId = 489, FieldName = "Status" };
                    Field lSourceField = new Field { TrackerId = 156, FieldName = "Status" };
                    Field lSourceField1 = new Field { TrackerId = 108, FieldName = "Status" };
                    lRule.SourceFields.Add(lSourceField);
                    lRule.SourceFields.Add(lSourceField1);

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
