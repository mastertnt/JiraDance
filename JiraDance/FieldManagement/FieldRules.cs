using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
using JiraDance.FieldManagement.Conditions;
using JiraDance.Models;
using RestEase;

namespace JiraDance.FieldManagement
{
    /// <summary>
    /// Field rules.
    /// </summary>
    public class FieldRules
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FieldRules()
        {
            this.Rules = new ObservableCollection<FieldRule>();
            this.SourceFields = new ObservableCollection<Field>();
        }

        /// <summary>
        /// Description of the rules.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Target field.
        /// </summary>
        public Field TargetField
        {
            get;
            set;
        }

        /// <summary>
        /// List of source fields.
        /// </summary>
        public ObservableCollection<Field> SourceFields
        {
            get;
            set;
        }

        /// <summary>
        /// List of rules.
        /// </summary>
        public ObservableCollection<FieldRule> Rules
        {
            get;
            set;
        }

        /// <summary>
        /// Updates the rules.
        /// </summary>
        public void Update(JiraIssueDatabase issues)
        {
            //IJiraIssueApi api = null;
            //if (string.IsNullOrWhiteSpace(settings.ResponseDebugPath) == false)
            //{
            //    if (Directory.Exists(settings.ResponseDebugPath))
            //    {
            //        api = RestClient.For<IJiraIssueApi>(settings.EndPoint, new DebugResponseDeserializer() { DebugPath = settings.ResponseDebugPath });
            //    }
            //    else
            //    {
            //        msLogger.Warn("Directory for debug does not exist " + settings.ResponseDebugPath);
            //        api = RestClient.For<IJiraIssueApi>(settings.EndPoint);
            //    }
            //}
            //else
            //{
            //    api = RestClient.For<IJiraIssueApi>(settings.EndPoint);
            //}

            //var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(settings.Username + ":" + settings.Password));
            //api.Authorization = new AuthenticationHeaderValue("Basic", value);


            //foreach (var projectKey in settings.ProjectKeys)
            //{
            //    this.mSourceIssues.AddRange(api.GetAllIssuesByProject(projectKey).Result.Issues);
            //}
            
            //Console.WriteLine(this.mSourceIssues.Count);


            //Connection lConnection = new  Connection(pSettings.Uri, pSettings.Key);
            //this.mTargetArtifacts.Clear();
            //this.mSourceArtifacts.Clear();

            //// Retrieve all field values.
            //lConnection.TrackerStructures.Clear();

            //// Target tracker.
            //msLogger.Debug("---->Retrieving target artifacts.");
            //TrackerStructure lTargetStructure = lConnection.AddTrackerStructure(this.TargetField.TrackerId);
            //Tracker<Artifact> lTargetTracker = new Tracker<Artifact>(lTargetStructure);
            //lTargetTracker.PreviewRequest(lConnection);
            //lTargetTracker.Request(lConnection);
            //this.mTargetArtifacts.AddRange(lTargetTracker.Artifacts);
            //msLogger.Debug("---->Target artifacts retrieved : " + this.mTargetArtifacts.Count);

            //msLogger.Debug("---->Retrieving source artifacts.");
            //// Source trackers.
            //foreach (var lSourceField in this.SourceFields)
            //{
            //    TrackerStructure lSourceStructure = lConnection.AddTrackerStructure(lSourceField.TrackerId);
            //    Tracker<Artifact> lSourceTracker = new Tracker<Artifact>(lSourceStructure);
            //    lSourceTracker.PreviewRequest(lConnection);
            //    lSourceTracker.Request(lConnection);
            //    this.mSourceArtifacts.AddRange(lSourceTracker.Artifacts);
            //}
            //msLogger.Debug("---->Source artifacts retrieved : " + this.mSourceArtifacts.Count);

            //msLogger.Debug("---->Retrieving source values.");
            //Dictionary<int, List<object>> lSourceValues = new Dictionary<int, List<object>>();
            //foreach (var lArtifact in this.mSourceArtifacts)
            //{
            //    Field lField = this.SourceFields.FirstOrDefault(pField => pField.TrackerId == lArtifact.TrackerId);
            //    if (lField != null)
            //    {
            //        object lFieldValue = lArtifact.GetFieldValue(lField.FieldName);
            //        if (lArtifact.Links != null)
            //        {
            //            foreach (var lLink in lArtifact.Links)
            //            {
            //                if (lSourceValues.ContainsKey(lLink.Id) == false)
            //                {
            //                    lSourceValues.Add(lLink.Id, new List<object>());
            //                }
            //                lSourceValues[lLink.Id].Add(lFieldValue);
            //            }
            //        }
            //        else
            //        {
            //            msLogger.Error("No links found");
            //        }
            //    }
            //    else
            //    {
            //        msLogger.Error("No field found");
            //    }
            //}

            //foreach (var lSourceValue in lSourceValues)
            //{
            //    msLogger.Trace(lSourceValue.Key +  " = " + string.Join(";", lSourceValue.Value));
            //}
            //msLogger.Debug("---->Source values retrieved : " + lSourceValues.Count);

            //msLogger.Debug("---->Applying rules.");
            //List<int> lUpdatedArtifacts = new List<int>();
            //foreach (var lTargetArtifact in this.mTargetArtifacts)
            //{
            //    if (pSettings.TargetArtifactId == 0 || pSettings.TargetArtifactId == lTargetArtifact.Id)
            //    {
            //        bool lConditionRaised = false;

            //        List<object> lAttachedValues;
            //        if (lSourceValues.TryGetValue(lTargetArtifact.Id, out lAttachedValues))
            //        {
            //            for (int lIndex = 0; lIndex < this.Rules.Count && lConditionRaised == false; lIndex++)
            //            {
            //                if (this.Rules[lIndex].Condition.IsRaised(lAttachedValues))
            //                {
            //                    lConditionRaised = true;
            //                    if (string.IsNullOrEmpty(this.Rules[lIndex].Updater.Update(lConnection, lTargetArtifact, this.TargetField, lAttachedValues)) == false)
            //                    {
            //                        lUpdatedArtifacts.Add(lTargetArtifact.Id);
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            FieldRule lRule = this.Rules.FirstOrDefault(pRule => pRule.Condition.GetType() == typeof(NoChild));
            //            if (lRule != null)
            //            {
            //                if (string.IsNullOrEmpty(lRule.Updater.Update(lConnection, lTargetArtifact, this.TargetField, new List<object>())) == false)
            //                {
            //                    lUpdatedArtifacts.Add(lTargetArtifact.Id);
            //                }
            //            }
            //        }
            //    }
            //}
            //msLogger.Info("---->Artifact updated (" + lUpdatedArtifacts.Count + ")  = " + string.Join(";", lUpdatedArtifacts));
            msLogger.Debug("Rules applied.");
        }
    }
}
