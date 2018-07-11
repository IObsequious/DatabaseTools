// -----------------------------------------------------------------------
// <copyright file="ICodeGenerator.cs" company="Ollon, LLC">
//     Copyright © 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Microsoft.SqlServer.Dac.CodeGeneration.TypedModel
{
    internal static class DacUtilities
    {
        public static Model CodeGenerationModel = XmlModelParser.ParseModelString(CodeGenerationResources.ModelMetadata);
        public const string ClassNamePrefix = "Sql";
        public const string NamespaceName = "Microsoft.SqlServer.Dac.Extensions.Prototype";
        public const string DefaultReturnType = "TSqlModelElementReference";

        public const string BaseModelInterfaceName = "ISqlModelElement";
        public const string BaseModelInterfaceReferenceName = "ISqlModelElementReference";

        public const string BaseModelInterfaceFullName = "Microsoft.SqlServer.Dac.Extensions.Prototype.ISqlModelElement";
        public static SqlServerVersion[] GetSqlServerVersions()
        {
            var values = Enum.GetValues(typeof(SqlServerVersion));
            SqlServerVersion[] array = new SqlServerVersion[values.Length];
            values.CopyTo(array, 0);
            return array;
        }

        public static Element FindElement(string name) => CodeGenerationModel.FindElement(name);

        public static bool SupportsVersion(TSqlPlatforms platform, SqlServerVersion version)
        {
            switch (version)
            {
                case SqlServerVersion.Sql90:
                    return (platform & TSqlPlatforms.Sql90) == TSqlPlatforms.Sql90;
                case SqlServerVersion.Sql100:
                    return (platform & TSqlPlatforms.Sql100) == TSqlPlatforms.Sql100;
                case SqlServerVersion.Sql110:
                    return (platform & TSqlPlatforms.Sql110) == TSqlPlatforms.Sql110;
                case SqlServerVersion.Sql120:
                    return (platform & TSqlPlatforms.Sql120) == TSqlPlatforms.Sql120;
                case SqlServerVersion.Sql130:
                    return (platform & TSqlPlatforms.Sql130) == TSqlPlatforms.Sql130;
                case SqlServerVersion.Sql140:
                    return (platform & TSqlPlatforms.Sql140) == TSqlPlatforms.Sql140;
                case SqlServerVersion.SqlAzure:
                    return (platform & TSqlPlatforms.SqlAzure) == TSqlPlatforms.SqlAzure;
                default:
                    return false;
            }
        }
    }
}
