using System;

namespace SourceSchemaParser
{
    public class DotaLeague
    {
        public string Name { get; private set; }
        public string ImageInventoryPath { get; private set; }
        public string ImageBannerPath { get; private set; }
        public string NameLocalized { get; private set; }
        public string DescriptionLocalized { get; private set; }
        public string TypeName { get; private set; }
        public string TournamentUrl { get; private set; }
        public int LeagueId { get; private set; }
        public string Tier { get; private set; }
        public string Location { get; private set; }

        public DotaLeague(DotaSchemaItemLeague schema)
        {
            if (schema == null)
            {
                throw new ArgumentNullException("schema");
            }

            Name = schema.Name;
            ImageInventoryPath = schema.ImageInventoryPath;
            ImageBannerPath = schema.ImageBannerPath;
            NameLocalized = schema.NameLocalized;
            DescriptionLocalized = schema.DescriptionLocalized;
            TypeName = schema.TypeName;
            TournamentUrl = schema.TournamentUrl;
            if (schema.Tool != null && schema.Tool.Usage != null)
            {
                Tier = schema.Tool.Usage.Tier;
                Location = schema.Tool.Usage.Location;
                LeagueId = schema.Tool.Usage.LeagueId;
            }
        }

        public override string ToString()
        {
            string result = String.Format("Name: {0}", NameLocalized);
            if (!String.IsNullOrEmpty(Tier))
            {
                result += String.Format(", Tier: {0}", Tier);
            }
            if (!String.IsNullOrEmpty(Location))
            {
                result += String.Format(", Location: {0}", Location);
            }
            return result;
        }
    }
}