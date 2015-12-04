using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceSchemaParser.Dota2
{
    public static class SchemaStringExtensions
    {
        public static string ToSlashSeparatedString(this string value)
        {
            if(!String.IsNullOrEmpty(value))
            {
                value = value.Replace(" ", " / ");
                return value;
            }

            return String.Empty;
        }
    }
}
