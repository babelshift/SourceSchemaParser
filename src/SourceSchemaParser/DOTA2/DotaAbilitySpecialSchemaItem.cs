namespace SourceSchemaParser.DOTA2
{
    internal class DotaAbilitySpecialSchemaItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string VarType { get; set; }
        public string LinkedSpecialBonus { get; set; }
        public bool RequiresScepter { get; set; }
    }
}