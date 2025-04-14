using Microsoft.AspNetCore.Diagnostics;
using Octokit;
using System.Text;

namespace RMalekarAPI.Services
{
    public class GitHubJsonUpdater
    {
        private readonly HttpClient _httpClient;
        private readonly string _owner = "owner";
        private readonly string _repo = "repo";
        private readonly string _pat = "pat";
        private readonly string _branch = "branch";
        private readonly ILogger<GitHubJsonUpdater> _logger;

        public GitHubJsonUpdater(ILogger<GitHubJsonUpdater> logger)
        {
            _httpClient = new();
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("App", "1.0"));
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _pat);
            _logger = logger;

            //   gitHubClient = new GitHubClient(new ProductHeaderValue("RMalekarAPI"));
            // gitHubClient.Credentials = new(_pat);
        }

        /*  public async Task UpdateJsonFileAsync(string newContent, string filePath) {
              var fileDetail = await gitHubClient.Repository.Content.GetAllContentsByRef(_owner, _repo, filePath);
              if (fileDetail != null)
              {
                  //Encode new content with Base64
                 /* var newContentBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(newContent));
                  var updateJson = JsonSerializer.Serialize(newContentBase64);
                  var content = new StringContent(updateJson, Encoding.UTF8, "application/json");

                  var updateJsonFile = await gitHubClient.Repository.Content.UpdateFile(
                                  _owner, _repo, filePath,
                                  new UpdateFileRequest(
                                          "File Updated using API background service", newContent, fileDetail.FirstOrDefault()?.Sha, true
                                      )
                                  );
              }
              else
              {
                  var createJsonFile = await gitHubClient.Repository.Content.CreateFile(_owner, _repo, filePath,
                          new CreateFileRequest($"FIrst commit for {filePath}", newContent, true));
              }
          }*/

         public async Task UpdateJsonFileAsync(string newContent, string filePath)
         {
            try
            {
                //Get the sha of current file
                var getUrl = $"https://api.github.com/repos/{_owner}/{_repo}/contents/{filePath}";
                var getResponse = await _httpClient.GetAsync(getUrl);

                string sha = null;

                if (getResponse.IsSuccessStatusCode)
                {
                    var getContent = await getResponse.Content.ReadAsStringAsync();
                    var fileInfo = JsonSerializer.Deserialize<GitHubFileIinfo>(getContent);

                    sha = fileInfo?.sha;
                }


                //Encode new content with Base64
                var newContentBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(newContent));

                //Build payload

                var payload = new
                {
                    message = "Automated Updated JSON file",
                    content = newContentBase64,
                    sha = sha, // if null, it will create a new file
                    branch = _branch
                };

                var updateJson = JsonSerializer.Serialize(payload);
                var content = new StringContent(updateJson, Encoding.UTF8, "application/json");

                //Send Put Request
                var putUrl = $"https://api.github.com/repos/{_owner}/{_repo}/contents/{filePath}";
                var putResponse = await _httpClient.PutAsync(putUrl, content);

                if (putResponse.IsSuccessStatusCode)
                {
                    _logger.LogInformation("JSON file updated successfully on GitHub!");
                }
                else
                {
                    var errorContent = await putResponse.Content.ReadAsStringAsync();
                    _logger.LogError($"Failed to update JSON file. Status Code: {putResponse.StatusCode}, Error: {errorContent}");
                }
            } catch (Exception ex)
            {
                _logger.LogError($"Failed to update JSON file. Status Code: {ex.Message}, Error: {ex}");
            }
            
         }

         public class GitHubFileIinfo
         {
             public string name { get; set; }
             public string path { get; set; }
             public string sha { get; set; }
             public int size { get; set; }
             public string url { get; set; }
             public string html_url { get; set; }
             public string git_url { get; set; }
             public string download_url { get; set; }
             public string type { get; set; }
            public string content { get; set; }

            public string encoding {  get; set; }

            public links _link { get; set; }
            public class links
            {
                public string self;
                public string git;
                public string html;

            }
            
        }
        
    }
}
