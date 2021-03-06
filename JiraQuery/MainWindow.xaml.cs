using AtlassianCore;
using AtlassianCore.Models;
using AtlassianCore.Utility;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using RestEase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace JiraQuery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Current NLog.
        /// </summary>
        private static readonly NLog.Logger msLogger = NLog.LogManager.GetCurrentClassLogger();

        // All loaded filters.
        public ObservableCollection<Filter> Filters
        {
            get;
            set;
        }

        // The technical settings.
        public TechSettings TechSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Set the window theme to Dark.Red
            ThemeManager.Current.ChangeTheme(this, "Light.Blue");

            this.TechSettings = TechSettings.Build(@".\techsettings.json");

            this.Filters = new ObservableCollection<Filter>();

            // Initialize all filters.
            string[] files = Directory.GetFiles(@".", "*.xml");

            foreach (string file in files)
            {
                XElement root = XElement.Load(file);
                Filter filter = new Filter();
                filter.Name = root.Attribute("name").Value;
                filter.Description = root.Element("Description")?.Value;


                foreach (XElement queryElement in root.Elements("Query"))
                {
                    Query query = new Query();
                    query.Name = queryElement.Attribute("name").Value;
                    query.Description = queryElement.Attribute("description").Value;
                    query.JQL = queryElement.Value;
                    filter.Queries.Add(query);
                }

                foreach (XElement parameterElement in root.Descendants("Parameter"))
                {
                    FilterParameter parameter = new FilterParameter();
                    parameter.Name = parameterElement.Attribute("name").Value;
                    parameter.Value = parameterElement.Attribute("defaultValue").Value;
                    filter.Parameters.Add(parameter);
                }

                this.Filters.Add(filter);
            }

            this.DataContext = this;

            this.CBFilters.SelectedIndex = 0;

            this.PGSelectedObject.SelectedObject = this.Filters[0];

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IJiraIssueApi api = null;
            if (string.IsNullOrWhiteSpace(this.TechSettings.ResponseDebugPath) == false)
            {
                if (Directory.Exists(this.TechSettings.ResponseDebugPath))
                {
                    api = new RestClient(this.TechSettings.EndPoint) { ResponseDeserializer = new DebugResponseDeserializer() { DebugPath = this.TechSettings.ResponseDebugPath }, RequestBodySerializer = new DebugRequestBodySerializer() { DebugPath = this.TechSettings.ResponseDebugPath } }.For<IJiraIssueApi>();
                }
                else
                {
                    msLogger.Warn("Directory for debug does not exist " + this.TechSettings.ResponseDebugPath);
                    api = RestClient.For<IJiraIssueApi>(this.TechSettings.EndPoint);
                }
            }
            else
            {
                api = RestClient.For<IJiraIssueApi>(this.TechSettings.EndPoint);
            }

            var value = Convert.ToBase64String(Encoding.ASCII.GetBytes(this.TechSettings.Username + ":" + this.TechSettings.Password));
            api.Authorization = new AuthenticationHeaderValue("Basic", value);

            RTBResult.Document.Blocks.Clear();

            // Look for the selected filter.
            Filter filter = this.Filters.FirstOrDefault(filterParam => filterParam.Name == this.CBFilters.SelectedValue.ToString());
            Dictionary<string, string> wheres = new Dictionary<string, string>();
            List<JiraIssueSmall> issues = new List<JiraIssueSmall>();
            string last = "";
            if (filter != null)
            {
                StringBuilder result = new StringBuilder();
                bool error = false;
                foreach (Query query in filter.Queries)
                {
                    issues.Clear();
                    if (error == false)
                    {
                        
                        string toExecute = query.JQL;
                        foreach (FilterParameter parameter in filter.Parameters)
                        {
                            toExecute = toExecute.Replace("$" + parameter.Name + "$", parameter.Value);
                        }
                        foreach (var where in wheres)
                        {
                            toExecute = toExecute.Replace("$" + where.Key + "$", where.Value);
                        }

                        int before = 0;
                        int after = 50;
                        int shift = 0;
                        result.AppendLine("Executing : " + toExecute);
                        while (before + 50 == after)
                        {
                            before = issues.Count;
                            try
                            {
                                issues.AddRange(api.ExecuteJqj(toExecute, shift).Result.Issues);
                            }
                            catch (Exception ex)
                            {
                                result.AppendLine("Error : " + ex.Message);
                                result.AppendLine("Stopping execution");
                                error = true;
                            }

                            shift += 50;
                            after = issues.Count;
                        }

                        if (wheres.ContainsKey(query.Name) == false)
                        {
                            wheres.Add(query.Name, string.Join(",", issues));
                        }
                        else
                        {
                            wheres[query.Name] = string.Join(",", issues);
                        }

                        result.AppendLine(query.Name + " Result Key in (" + wheres[query.Name] + ")");
                        last = query.Name;
                    }
                    
                }
                Clipboard.SetText("Key in (" + wheres[last] + ")");
                RTBResult.AppendText(result.ToString());
            }
        }

        private void CBFilters_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.PGSelectedObject.SelectedObject = this.Filters[this.CBFilters.SelectedIndex];
        }
    }
}
