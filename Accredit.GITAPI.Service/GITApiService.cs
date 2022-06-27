using Accredit.GITAPI.Service.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Accredit.GITAPI.Service
{
    public class GITAPIService : IGITAPIService
    {

        private readonly HttpClient _httpClient = new HttpClient();

        #region Public Methods

        public async Task<string> GetUser(string username)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["GetUserUrl"];
                string getUserUrl = String.Format(url, username);

                User user = await getuserDetails(getUserUrl);
                if (user != null && !string.IsNullOrEmpty(user.ReposUrl))
                {
                    user.ReposList = await getUserRepoDetails(user.ReposUrl);
                }

                return JsonConvert.SerializeObject(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Private functions

        private async Task<User> getuserDetails(string getUserUrl)
        {
            var userResult = await getDetails(getUserUrl);
            if (userResult != null)
            {
                var user = JsonConvert.DeserializeObject<User>(userResult);
                return user;
            }
            return null;
        }

        private async Task<IEnumerable<Repo>> getUserRepoDetails(string reposUrl)
        {
            var repoResult = await getDetails(reposUrl);
            if (repoResult != null)
            {
                var repoList = JsonConvert.DeserializeObject<List<Repo>>(repoResult).OrderByDescending(x => x.StargazersCount);
                return repoList.Count() > 5 ? repoList.Take(5) : repoList;
            }
            return null;
        }

        private async Task<string> getDetails(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    throw new UriFormatException("Invalid service URI.");
                }

                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                var stringTask = await _httpClient.GetStringAsync(url);

                return stringTask;
            }
            catch (HttpRequestException httpex) when (httpex.Message.Contains("40"))
            {
                throw new HttpRequestException("User not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong, please try again.");
            }

        }

        #endregion
    }
}
