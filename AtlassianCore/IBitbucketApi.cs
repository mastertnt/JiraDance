using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AtlassianCore.Models.Bitbucket;
using RestEase;

namespace AtlassianCore
{
    [Header("Accept", "application/json")]
    [Header("Content-Type", "application/json")]
    public interface IBitbucketApi
    {
        /// <summary>
        /// The authorization field put in the header.
        /// </summary>
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }

        [Get("/2.0/repositories/{workspace}/{repository}/pullrequests")]
        Task<List<PullRequest>> GetAllPullRequests([Path] string workspace, [Path] string repository);
    }
}
