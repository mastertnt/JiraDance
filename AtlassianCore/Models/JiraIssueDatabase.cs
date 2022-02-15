using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using AtlassianCore.Utility;
using OfficeOpenXml;
using RestEase;

namespace AtlassianCore.Models
{
    /// <summary>
    /// This class is responsible to store all issues.
    /// </summary>
    public class JiraIssueDatabase
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets all issues.
        /// </summary>
        public ObservableRangeCollection<IJiraIssue> Issues
        {
            get;
            set;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public JiraIssueDatabase()
        {
            this.Issues = new ObservableRangeCollection<IJiraIssue>();
            this.Issues.CollectionChanged += this.Issues_CollectionChanged;
        }

        public int Initialize(string endPoint, string username, string password, string responseDebugPath, List<string> projectKeys)
        {
            msLogger.Debug("---->Retrieving all issues from projects " + String.Join("/ ", projectKeys.ToArray()));
            IJiraIssueApi api = null;
            if (string.IsNullOrWhiteSpace(responseDebugPath) == false)
            {
                if (Directory.Exists(responseDebugPath))
                {
                    api = RestClient.For<IJiraIssueApi>(endPoint, new DebugResponseDeserializer() { DebugPath = responseDebugPath });
                }
                else
                {
                    msLogger.Warn("Directory for debug does not exist " + responseDebugPath);
                    api = RestClient.For<IJiraIssueApi>(endPoint);
                }
            }
            else
            {
                api = RestClient.For<IJiraIssueApi>(endPoint);
            }

            var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            api.Authorization = new AuthenticationHeaderValue("Basic", value);


            foreach (var projectKey in projectKeys)
            {
                int before = 0;
                int after = 50;
                int shift = 0;
                while (before + 50 == after)
                {
                    before = this.Issues.Count;
                    this.Issues.AddRange(api.GetAllIssuesByProject(projectKey, shift).Result.Issues);
                    shift += 50;
                    after = this.Issues.Count;
                }
            }

            msLogger.Debug("----> Issue retrieved " + this.Issues.Count());

            return this.Issues.Count;
        }


        public void ExportToExcel(string filename)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            File.Delete(filename);
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filename)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventory");
                int currentRow = 1;
                IEnumerable<IJiraIssue> roots = this.Issues.Where(issue => issue.Parent == null);
                foreach (var root in roots)
                {
                    this.ExportToExcel(root, worksheet, ref currentRow, 1);
                    currentRow++;
                }
                package.Save();
                
            }
        }

        private void ExportToExcel(IJiraIssue issue, ExcelWorksheet worksheet, ref int currentRow, int offset)
        {
            JiraIssue typedIssue = issue as JiraIssue;
            if (typedIssue != null)
            {
                worksheet.Cells[currentRow, offset + 0].Value = typedIssue.Key;
                worksheet.Cells[currentRow, offset + 1].Value = typedIssue.Summary;
                worksheet.Cells[currentRow, offset + 2].Value = typedIssue.Status;
                worksheet.Cells[currentRow, offset + 3].Value = typedIssue.Type;
                worksheet.Cells[currentRow, offset + 4].Value = typedIssue.ParentId;
                worksheet.Cells[currentRow, offset + 5].Value = typedIssue.Id;
                foreach (var child in issue.Children)
                {
                    currentRow++;
                    this.ExportToExcel(child, worksheet, ref currentRow, offset + 1);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();

            return base.ToString();
        }

        /// <summary>
        /// Delegate called when an issue is added or removed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Issues_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.NewItems!= null)
                        {
                            foreach (var issue in e.NewItems)
                            {
                                IJiraIssue issueItem = issue as IJiraIssue;
                                issueItem.Database = this;
                            }
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.OldItems != null)
                        {
                            foreach (var issue in e.OldItems)
                            {
                                (issue as IJiraIssue).Database = null;
                            }
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.OldItems != null)
                        {
                            foreach (var issue in e.OldItems)
                            {
                                (issue as IJiraIssue).Database = null;
                            }
                        }

                        if (e.NewItems != null)
                        {
                            foreach (var issue in e.NewItems)
                            {
                                (issue as IJiraIssue).Database = this;
                            }
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        if (e.NewItems != null)
                        {
                            foreach (var issue in e.NewItems)
                            {
                                (issue as IJiraIssue).Database = this;
                            }
                        }
                        if (e.OldItems != null)
                        {
                            foreach (var issue in e.OldItems)
                            {
                                (issue as IJiraIssue).Database = null;
                            }
                        }
                    }
                    break;
            }
        }
    }
}
