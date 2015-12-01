﻿using Newtonsoft.Json;

namespace SourceSchemaParser.Dota2
{
    public class DotaSchemaItem : SchemaItem
    {
        public DotaSchemaItemTool Tool { get; set; }

        [JsonProperty("tournament_url")]
        public string TournamentUrl { get; set; }

        [JsonProperty("image_banner")]
        public string ImageBannerPath { get; set; }
    }
}