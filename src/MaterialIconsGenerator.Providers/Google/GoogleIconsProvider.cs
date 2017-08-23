﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaterialIconsGenerator.Core;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace MaterialIconsGenerator.Providers.Google
{
    public class GoogleIconsProvider : IIconProvider
    {
        public string Name => "Google Material Icons";

        public string Reference => "https://github.com/google/material-design-icons";

        public bool IsAndroidSupported => true;

        public bool IsiOSSupported => true;

        public IEnumerable<string> GetAndroidSizes()
        {
            return new List<string>()
            {
                "18dp",
                "24dp",
                "36dp",
                "48dp"
            };
        }

        public IEnumerable<string> GetiOSSizes()
        {
            return new List<string>()
            {
                "",
                "18pt",
                "36pt",
                "48pt"
            };
        }

        public IEnumerable<string> GetAndroidDensities()
        {
            return new List<string>()
            {
                "mdpi",
                "hdpi",
                "xhdpi",
                "xxhdpi",
                "xxxhdpi"
            };
        }

        public IEnumerable<string> GetiOSDensities()
        {
            return new List<string>()
            {
                "",
                "2X",
                "3X"
            };
        }

        public async Task<IEnumerable<IIcon>> Get()
        {
            var client = new RestClient("https://material.io");
            var request = new RestRequest("/icons/data/grid.json", Method.GET);
            request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36");
            request.AddHeader("cache-control", "max-age=0");
            var response = await client.ExecuteTaskAsync(request);
            var json = JObject.Parse(response.Content);

            var groups = json["groups"].Select(item => new GoogleIconCategory()
            {
                Id = item["data"]["id"].Value<string>(),
                Name = item["data"]["name"].Value<string>()
            }).ToList();

            return json["icons"].Select(item => new GoogleIcon()
            {
                Id = item["id"].Value<string>(),
                Name = item["name"].Value<string>(),
                Category = groups.FirstOrDefault(x => x.Id == item["group_id"].Value<string>()),
                Keywords = item["keywords"].Values<string>().ToList()
            }).ToList();
        }

        public IProjectIcon CreateProjectIcon(IIcon icon, IIconColor color, string size, string density)
        {
            return new GoogleProjectIcon(icon, color, size, density);
        }
    }
}
