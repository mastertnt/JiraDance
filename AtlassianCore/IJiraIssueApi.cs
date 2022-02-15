﻿using System.Net.Http.Headers;
using System.Threading.Tasks;
using AtlassianCore.Models;
using RestEase;

namespace AtlassianCore
{
    /// <summary>
    /// JIRA API to retrieve issues.
    /// </summary>
    [Header("Accept", "application/json")]
    [Header("Content-Type", "application/json")]
    public interface IJiraIssueApi
    {
        /// <summary>
        /// The authorization field put in the header.
        /// </summary>
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        /// <summary>
        /// Retrieves an issue with its id.
        /// </summary>
        /// <param name="issueId">The issue id</param>
        /// <returns></returns>
        [Get("agile/1.0/issue/{issueId}")]
        Task<JiraIssue> GetIssue([Path] string issueId);

        /// <summary>
        /// Finds all issues for a given project.
        /// </summary>
        /// <param name="projectName">The project name.</param>
        /// <param name="shift">The shift to apply.</param>
        /// <returns></returns>

        [Get("/api/3/search?jql=project={projectName}&startAt={shift}")]
        Task<JiraSearchResult> GetAllIssuesByProject([Path] string projectName, [Path] int shift);

        /// <summary>
        /// Creates a comment on the issue.
        /// </summary>
        /// <param name="issueId">The issue id.</param>
        /// <param name="comment">The comment to add</param>
        [Post("/api/2/issue/{issueId}/comment")]
        Task CreateComment([Path] string issueId, [Body] JiraComment comment);

        [Post("/api/2/issue/{issueId}/transitions")]
        Task ChangeStatus([Path] string issueId, [Body] string transition);
    }

}
