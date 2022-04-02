using System.Threading.Tasks;
using GitHubAPIDemo.Configuration;
using GitHubAPIDemo.Extensions;
using GitHubAPIDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GitHubAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private readonly GitHubService _gitHubService;

        public GitHubController(IOptions<GitHubConfiguration> gitHubConfiguration)
        {
            _gitHubService = new GitHubService(gitHubConfiguration.Value);
        }

        [HttpGet("lastcommit")]
        public async Task<IActionResult> GetLastCommit()
        {
            var result = await _gitHubService.GetLastCommitFilesContent(directory:"BlogPosts");

            return Ok(result);
        }



        [HttpGet("commit")]
        public async Task<IActionResult> GetCommit(
            string commitHash= "d92ce2adf403add16c2c426d6e62a56c84f08eba"
            , string directory="")
        {
            if (commitHash.IsEmptyString())
            {
                return BadRequest();
            }

            var result = await _gitHubService.GetCommitFilesContent(commitHash, directory);

            return Ok(result);
        }
    }
}