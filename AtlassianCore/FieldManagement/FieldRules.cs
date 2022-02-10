using System.Collections.ObjectModel;
using AtlassianCore.Models;

namespace AtlassianCore.FieldManagement
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
        /// Parent field.
        /// </summary>
        public string ParentField
        {
            get;
            set;
        }

        /// <summary>
        /// ChildrenField field.
        /// </summary>frank
        public string ChildrenField
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
        public void Update(JiraIssueDatabase database)
        {
            


            msLogger.Info("---->Applying rules.");
            List<int> lUpdatedArtifacts = new List<int>();
            List<IJiraIssue> roots = database.Issues.Where(issue => issue.Parent == null).ToList();
            foreach (var rootIssue in roots)
            {
                ApplyRule(rootIssue as JiraIssue);

            }
            msLogger.Debug("Rules applied.");
        }

        private void ApplyRule(JiraIssue parent)
        {
            msLogger.Info("---->Check rule for " + parent.Key);
            // Apply rules on children.
            foreach (var child in parent.Children)
            {
                ApplyRule(child as JiraIssue);
            }

            // Then apply rules on parent.
            bool hasBeenRaised = false;
            foreach (var rule in this.Rules)
            {
                if (rule.Condition.IsRaised(parent.Children, this.ChildrenField) && hasBeenRaised == false)
                {
                    hasBeenRaised = true;
                    rule.Updater.Update(parent, this.ParentField, "");
                }
            }
        }
    }
}
