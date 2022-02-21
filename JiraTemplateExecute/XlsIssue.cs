using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraTemplateExecute
{
    class XlsIssue
    {
        /// <summary>
        /// Internal Xls Id.
        /// </summary>
        [Name("XLS ID")]
        public string XlsId
        {
            get;
            set;
        }

        /// <summary>
        /// Jira Id.
        /// </summary>
        [Name("ID")]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Project key.
        /// </summary>
        [Name("PROJECT")]
        public string Project
        {
            get;
            set;
        }

        /// <summary>
        /// Summary of the issue.
        /// </summary>
        [Name("PARENT ID")]
        public string ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// Type of issue.
        /// </summary>
        [Name("TYPE")]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Summary of issue.
        /// </summary>
        [Name("SUMMARY")]
        public string Summary
        {
            get;
            set;
        }

        /// <summary>
        /// Description of issue.
        /// </summary>
        [Name("DESCRIPTION")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Acceptance criteria of issue.
        /// </summary>
        [Name("ACCEPTANCE CRITERIA")]
        public string AcceptanceCriteria
        {
            get;
            set;
        }

        /// <summary>
        /// Priority of issue.
        /// </summary>
        [Name("PRIORITY")]
        public string Priority
        {
            get;
            set;
        }

        /// <summary>
        /// Key or XLS ID of issue.
        /// </summary>
        [Name("IS BLOCKED BY")]
        public string IsBlockedBy
        {
            get;
            set;
        }

        /// <summary>
        /// Comment to put on the issue.
        /// </summary>
        [Name("COMMENT")]
        public string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// Sprint of the issue.
        /// </summary>
        [Name("SPRINT")]
        public string Sprint
        {
            get;
            set;
        }

        /// <summary>
        /// PI of the issue.
        /// </summary>
        [Name("PI")]
        public string PI
        {
            get;
            set;
        }

        /// <summary>
        /// Duration of the issue.
        /// </summary>
        [Name("DURATION")]
        public string Duration
        {
            get;
            set;
        }

        /// <summary>
        /// Checks if the issue is valid or not.
        /// </summary>
        [Ignore]
        public bool IsValid
        {
            get;
            set;
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(Id) &&
                        string.IsNullOrEmpty(Project) &&
                        string.IsNullOrEmpty(ParentId) &&
                        string.IsNullOrEmpty(Type) &&
                        string.IsNullOrEmpty(Summary) &&
                        string.IsNullOrEmpty(Description) &&
                        string.IsNullOrEmpty(AcceptanceCriteria) &&
                        string.IsNullOrEmpty(Priority) &&
                        string.IsNullOrEmpty(IsBlockedBy) &&
                        string.IsNullOrEmpty(Comment) &&
                        string.IsNullOrEmpty(Sprint) &&
                        string.IsNullOrEmpty(PI) &&
                        string.IsNullOrEmpty(Duration);
            }
        }

        /// <summary>
        /// Get or sets the errors
        /// </summary>
        [Ignore]
        public string Errors
        {
            get;
            set;
        }
    }
}
