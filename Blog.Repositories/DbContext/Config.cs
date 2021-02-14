using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Repositories.DbContext
{
    internal static class Config
    {
        public const string Entities = "Entities";

        public static string ToTableName(string name)
        {
            return name.Replace(Entities, string.Empty);
        }
    }
}
