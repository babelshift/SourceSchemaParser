using System;

namespace SourceSchemaParser.Dota2
{
    internal class DotaLeague
    {
        public string ItemDef { get; set; }
        public string Name { get; set; }
        public string ImageInventoryPath { get; set; }
        public string ImageBannerPath { get;set; }
        public string NameLocalized { get; set; }
        public string DescriptionLocalized { get; set; }
        public string TypeName { get; set; }
        public string TournamentUrl { get; set; }
        public int LeagueId { get; set; }
        public string Tier { get; set; }
        public string Location { get; set; }
        
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