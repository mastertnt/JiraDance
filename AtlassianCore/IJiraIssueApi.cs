using System.Net.Http.Headers;
using AtlassianCore.Models;
using RestEase;

namespace AtlassianCore
{
    /// <summary>
    /// JIRA API to retrieve issues.
    /// </summary>
    public interface IJiraIssueApi
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        [Get("agile/1.0/issue/{issueId}")]
        Task<JiraIssue> GetIssue([Path] string issueId);

        [Get("/api/3/search?jql=project={projectName}&startAt={shift}")]
        Task<JiraSearchResult> GetAllIssuesByProject([Path] string projectName, [Path] int shift);
    }

}
