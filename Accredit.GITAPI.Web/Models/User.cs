using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accredit.GITAPI.Web.Models
{
    public class User
    {
        [Required]
        [StringLength(38)]
        [RegularExpression(@"^[a-z\d](?:[a-z\d]|-(?=[a-z\d])){1,38}$", ErrorMessage = "Please provide a valid username.")]
        [JsonProperty("login")]
        public string Username { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("repos_url")]
        public string ReposUrl { get; set; }

        public IEnumerable<Repo> ReposList { get; set; }
    }

    public class Repo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("html_url")]
        public string URL { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }
    }
}