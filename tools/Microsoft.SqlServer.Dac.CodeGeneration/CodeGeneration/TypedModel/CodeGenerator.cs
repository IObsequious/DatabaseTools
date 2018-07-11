using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dac.Extensions;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Microsoft.SqlServer.Dac.CodeGeneration.TypedModel
{
    public static class CodeGenerator
    {
        private const string TSqlScriptName = "Microsoft.SqlServer.TransactSql.ScriptDom.TSqlScript";

        private const string ModelNamespace = "Microsoft.SqlServer.Dac.Model.";

        private const string IEnumerableName = "System.Collections.Generic.IEnumerable";

        private const string ObjectIdentifierName = ModelNamespace + "ObjectIdentifier";

        private const string TSqlObjectName = ModelNamespace + "TSqlObject";

        private const string DacExternalQueryScopesName = ModelNamespace + "DacExternalQueryScopes";

        private const string DacQueryScopesName = ModelNamespace + "DacQueryScopes";

        private const string ModelMetadataPropertyName = ModelNamespace + "MetadataPropertyClass";

        private const string ModelMetadataClassName = ModelNamespace + "ModelMetadataClass";

        private const string ModelPropertyClassName = ModelNamespace + "ModelPropertyClass";

        private const string ModelTypeClassName = ModelNamespace + "ModelTypeClass";

        private const string ModelRelationshipClassName = ModelNamespace + "ModelRelationshipClass";

        private const string ModelRelationshipInstanceName = ModelNamespace + "ModelRelationshipInstance";

        public static void GenerateInterfaces(FormattedTextWriter writer)
        {
            writer.WriteFileHeader();
            writer.WriteLine($"namespace {DacUtilities.NamespaceName}");
            using (writer.StartBlock())
            {
                using (writer.BeginRegion(DacUtilities.BaseModelInterfaceName))
                {
                    writer.WriteLine($"public interface {DacUtilities.BaseModelInterfaceName}");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine($"{ObjectIdentifierName} Name {{ get; }}");
                        writer.WriteLine($"{TSqlObjectName} Element {{ get; }}");
                        writer.WriteLine($"{TSqlScriptName} GetAst();");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetChildren();");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetChildren({DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"object GetMetadata({ModelMetadataClassName} metadata);");
                        writer.WriteLine($"TMetadata GetMetadata<TMetadata>({ModelMetadataClassName} metadata);");
                        writer.WriteLine($"{TSqlObjectName} GetParent();");
                        writer.WriteLine($"{TSqlObjectName} GetParent({DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"object GetProperty({ModelPropertyClassName} property);");
                        writer.WriteLine($"TProperty GetProperty<TProperty>({ModelPropertyClassName} property);");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetReferenced();");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetReferenced({DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetReferenced({ModelRelationshipClassName} relationshipType);");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetReferenced({ModelRelationshipClassName} relationshipType, {DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencedRelationshipInstances();");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencedRelationshipInstances({DacExternalQueryScopesName} queryScopes);");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencedRelationshipInstances({DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencedRelationshipInstances({ModelRelationshipClassName} relationshipType);");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencedRelationshipInstances({ModelRelationshipClassName} relationshipType, {DacExternalQueryScopesName} queryScopes);");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencedRelationshipInstances({ModelRelationshipClassName} relationshipType, {DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetReferencing();");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetReferencing({DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetReferencing({ModelRelationshipClassName} relationshipType);");
                        writer.WriteLine($"{IEnumerableName}<{TSqlObjectName}> GetReferencing({ModelRelationshipClassName} relationshipType, {DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencingRelationshipInstances();");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencingRelationshipInstances({DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencingRelationshipInstances({ModelRelationshipClassName} relationshipType);");
                        writer.WriteLine($"{IEnumerableName}<{ModelRelationshipInstanceName}> GetReferencingRelationshipInstances({ModelRelationshipClassName} relationshipType, {DacQueryScopesName} queryScopes);");
                        writer.WriteLine($"string GetScript();");
                        writer.WriteLine($"{ModelTypeClassName} ObjectType {{ get; }}");
                        writer.WriteLine($"object this[{ModelPropertyClassName} property] {{ get; }}");
                        writer.WriteLine($"bool TryGetAst(out {TSqlScriptName} objectAst);");
                        writer.WriteLine($"bool TryGetScript(out string objectScript);");
                    }
                }

                writer.WriteLine();

                writer.WriteLine($"public interface {DacUtilities.BaseModelInterfaceReferenceName} : {DacUtilities.BaseModelInterfaceName}");
                using (writer.StartBlock())
                {
                    writer.WriteLine($"TMetadataProperty GetMetadataProperty<TMetadataProperty>({ModelMetadataPropertyName} property);");
                }

                writer.WriteLine();

                WriteSimpleInterface(writer, "ISqlSecurityPrincipal", DacUtilities.BaseModelInterfaceName);
                WriteSimpleInterface(writer, "IServerSecurityPrincipal", "ISqlSecurityPrincipal");
                WriteSimpleInterface(writer, "ISqlDatabaseSecurityPrincipal", "ISqlSecurityPrincipal", "ISqlObjectAuthorizer");

                writer.WriteLine("");
                writer.WriteLine("public sealed class UnresolvedISqlDatabaseSecurityPrincipalElement : TSqlModelElementReference, ISqlDatabaseSecurityPrincipal");
                using (writer.StartBlock())
                {
                    writer.WriteLine($"public UnresolvedISqlDatabaseSecurityPrincipalElement({ModelRelationshipInstanceName} relationshipReference) : base(relationshipReference)");
                    using (writer.StartBlock())
                    {

                    }
                }

                WriteSimpleInterface(writer, "ISqlSecurable", DacUtilities.BaseModelInterfaceName);

                writer.WriteLine();
                writer.WriteLine("public sealed class UnresolvedISqlSecurableElement : TSqlModelElementReference, ISqlSecurable");
                using (writer.StartBlock())
                {
                    writer.WriteLine($"public UnresolvedISqlSecurableElement({ModelRelationshipInstanceName} relationshipReference):base(relationshipReference)");
                    using (writer.StartBlock())
                    {
                    }
                }

                writer.WriteLine("    }");
                writer.WriteLine("");
                writer.WriteLine($"public interface ISpecifiesIndex : {DacUtilities.BaseModelInterfaceName}");
                using (writer.StartBlock())
                {
                    writer.WriteLine($"{IEnumerableName}<ISqlIndex> Indexes {{ get; }}");
                }

                writer.WriteLine();

                writer.WriteLine($"public sealed class UnresolvedISpecifiesIndexElement: TSqlModelElementReference, ISpecifiesIndex");
                using (writer.StartBlock())
                {
                    writer.WriteLine($"public UnresolvedISpecifiesIndexElement({ModelRelationshipInstanceName} instance) : base(instance)");
                    using (writer.StartBlock())
                    {
                    }

                    writer.WriteLine();
                    writer.WriteLine($"public {IEnumerableName}<ISqlIndex> Indexes => throw new System.InvalidOperationException();");
                }

                WriteAdvancedInterface(writer, "TSqlDmlTrigger", "Triggers", "ISpecifiesDmlTrigger", DacUtilities.BaseModelInterfaceName);
                WriteAdvancedInterface(writer, "ISqlColumn", "Columns", "ISqlColumnSource", DacUtilities.BaseModelInterfaceName);

                WriteSimpleInterface(writer, "ISqlPromotedNodePath", DacUtilities.BaseModelInterfaceName);

                writer.WriteLine("");
                writer.WriteLine("public sealed class UnresolvedISqlPromotedNodePathElement : TSqlModelElementReference, ISqlPromotedNodePath");
                using (writer.StartBlock())
                {
                    writer.WriteLine($"public UnresolvedISqlPromotedNodePathElement({ModelRelationshipInstanceName} relationshipReference) : base(relationshipReference)");
                    using (writer.StartBlock())
                    {
                    }
                }

                WriteSimpleInterface(writer, "ISqlIndex", DacUtilities.BaseModelInterfaceName);
                WriteSimpleInterface(writer, "ITableTypeConstraint", DacUtilities.BaseModelInterfaceName);
                WriteSimpleInterface(writer, "IProtocolSpecifier", DacUtilities.BaseModelInterfaceName);
                WriteSimpleInterface(writer, "IEndpointLanguageSpecifier", DacUtilities.BaseModelInterfaceName);
                WriteSimpleInterface(writer, "IExtendedPropertyHost", DacUtilities.BaseModelInterfaceName);
                WriteSimpleInterface(writer, "ISqlIndex", DacUtilities.BaseModelInterfaceName);
                WriteSimpleInterface(writer, "ISqlObjectAuthorizer", DacUtilities.BaseModelInterfaceName);

                writer.WriteLine("");
                writer.WriteLine("public sealed class UnresolvedISqlObjectAuthorizerElement : TSqlModelElementReference, ISqlObjectAuthorizer");
                using (writer.StartBlock())
                {
                    writer.WriteLine($"public UnresolvedISqlObjectAuthorizerElement({ModelRelationshipInstanceName} relationshipReference) : base(relationshipReference)");
                    using (writer.StartBlock())
                    {
                    }
                }

                writer.WriteLine("");
                writer.WriteLine($"public interface ISpecifiesStorage : {DacUtilities.BaseModelInterfaceName}");
                using (writer.StartBlock())
                {
                    writer.WriteLine($"{IEnumerableName}<TSqlDataCompressionOption> DataCompressionOptions {{ get; }}");
                }

                writer.WriteLine($"public interface ISqlDataType: {DacUtilities.BaseModelInterfaceName}");
                using (writer.StartBlock())
                {
                }
                writer.WriteLine();
                writer.WriteLine("public sealed class UnresolvedISqlDataTypeElement : TSqlModelElementReference, ISqlDataType");
                using (writer.StartBlock())
                {
                    writer.WriteLine($"public UnresolvedISqlDataTypeElement({ModelRelationshipInstanceName} relationshipReference) : base(relationshipReference)");
                    using (writer.StartBlock())
                    {
                    }
                }

                foreach (SqlServerVersion version in DacUtilities.GetSqlServerVersions())
                {
                    string interfacePrefix = GetInterfacePrefix(version);

                    foreach (ModelTypeClass type in ModelSchema.SchemaInstance.AllTypes)
                    {
                        string interfaceName = GetBaseInterfaceName(type, version);
                        string referenceInterfaceName = GetReferenceInterfaceName(type, version);
                        Element element = DacUtilities.FindElement(type.Name);
                        if (element != null)
                        {
                            WriteInterfaceDeclaration(writer, version, type, interfaceName, element);
                        }
                    }
                }
            }
        }

        public static void GenerateModelExtensions(FormattedTextWriter writer)
        {
            writer.WriteFileHeader();
            writer.WriteLine();
            writer.WriteLine($"namespace {DacUtilities.NamespaceName}");
            using (writer.StartBlock())
            {
                writer.WriteLine(@"public partial class TSqlTable");
                using (writer.StartBlock())
                {
                    writer.WriteLine(@"public IEnumerable<ISqlIndex> Indexes");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get ");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(Index.IndexedObject))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (ISqlIndex)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlForeignKeyConstraint> ForeignKeyConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(ForeignKeyConstraint.Host))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (TSqlForeignKeyConstraint)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlPrimaryKeyConstraint> PrimaryKeyConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get ");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(PrimaryKeyConstraint.Host))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (TSqlPrimaryKeyConstraint)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlDefaultConstraint> DefaultConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(DefaultConstraint.Host))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (TSqlDefaultConstraint)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlCheckConstraint> CheckConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(CheckConstraint.Host))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (TSqlCheckConstraint)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlUniqueConstraint> UniqueConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(UniqueConstraint.Host))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (TSqlUniqueConstraint)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"/// <summary>");
                    writer.WriteLine(@"/// Returns all constraints for the table");
                    writer.WriteLine(@"/// </summary>");
                    writer.WriteLine(@"public IEnumerable<ISqlModelElement> AllConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach(var constraint in ForeignKeyConstraints)");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return constraint;");
                            }

                            writer.WriteLine();
                            writer.WriteLine(@"foreach (var constraint in PrimaryKeyConstraints)");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return constraint;");
                            }

                            writer.WriteLine();
                            writer.WriteLine(@"foreach(var constraint in UniqueConstraints)");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return constraint;");
                            }

                            writer.WriteLine(@"foreach (var constraint in CheckConstraints)");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return constraint;");
                            }

                            writer.WriteLine(@"foreach (var constraint in DefaultConstraints)");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return constraint;");
                            }
                        }
                    }

                    writer.WriteLine(@"public IEnumerable<TSqlDmlTrigger> Triggers");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach(var element in Element.GetReferencing(DmlTrigger.TriggerObject))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (TSqlDmlTrigger)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }
                }

                writer.WriteLine();
                writer.WriteLine(@"public partial class TSqlFileTable");
                using (writer.StartBlock())
                {
                    writer.WriteLine(@"public IEnumerable<ISqlIndex> Indexes");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(Index.IndexedObject))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (ISqlIndex)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlDmlTrigger> Triggers");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(DmlTrigger.TriggerObject))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (TSqlDmlTrigger)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }
                }

                writer.WriteLine();
                writer.WriteLine(@"public partial class TSqlTableValuedFunction");
                using (writer.StartBlock())
                {
                    writer.WriteLine(@"public IEnumerable<ISqlIndex> Indexes");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(Index.IndexedObject))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (ISqlIndex)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }
                }

                writer.WriteLine();
                writer.WriteLine(@"public partial class TSqlView");
                using (writer.StartBlock())
                {
                    writer.WriteLine(@"public IEnumerable<ISqlIndex> Indexes");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(Index.IndexedObject))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (ISqlIndex)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlDmlTrigger> Triggers");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Element.GetReferencing(DmlTrigger.TriggerObject))");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return (TSqlDmlTrigger)TSqlModelElement.AdaptInstance(element);");
                            }
                        }
                    }
                }

                writer.WriteLine();
                writer.WriteLine(@"public partial class TSqlTableType");
                using (writer.StartBlock())
                {
                    writer.WriteLine(@"public IEnumerable<TSqlTableTypePrimaryKeyConstraint> PrimaryKeyConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine();
                            writer.WriteLine(@"foreach (var element in Constraints.OfType<TSqlTableTypePrimaryKeyConstraint>())");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return element;");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlTableTypeDefaultConstraint> DefaultConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Constraints.OfType<TSqlTableTypeDefaultConstraint>())");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return element;");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlTableTypeCheckConstraint> CheckConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in Constraints.OfType<TSqlTableTypeCheckConstraint>())");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return element;");
                            }
                        }
                    }

                    writer.WriteLine();
                    writer.WriteLine(@"public IEnumerable<TSqlTableTypeUniqueConstraint> UniqueConstraints");
                    using (writer.StartBlock())
                    {
                        writer.WriteLine(@"get");
                        using (writer.StartBlock())
                        {
                            writer.WriteLine(@"foreach (var element in this.Constraints.OfType<TSqlTableTypeUniqueConstraint>())");
                            using (writer.StartBlock())
                            {
                                writer.WriteLine(@"yield return element;");
                            }
                        }
                    }
                }
            }
        }

        private static void WriteAdvancedInterface(FormattedTextWriter writer, string enumerableTypeName, string enumerablePropertyName, string interfaceName, string baseInterfaceName)
        {
            writer.WriteLine("");
            writer.WriteLine($"public interface {interfaceName} : {baseInterfaceName} ");
            using (writer.StartBlock())
            {
                writer.WriteLine($"{IEnumerableName}<{enumerableTypeName}> {enumerablePropertyName} {{ get; }}");
            }
        }

        private static void WriteSimpleInterface(FormattedTextWriter writer, string interfaceName, params string[] baseInterfaceNames)
        {
            if (baseInterfaceNames == null)
            {
                writer.WriteLine();
                writer.WriteLine($"public interface {interfaceName}");
                using (writer.StartBlock())
                {
                }
            }
            else if (baseInterfaceNames.Length == 1)
            {
                writer.WriteLine();
                writer.WriteLine($"public interface {interfaceName} : {baseInterfaceNames[0]}");
                using (writer.StartBlock())
                {
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < baseInterfaceNames.Length; i++)
                {
                    if (i == 0)
                    {
                        sb.Append(baseInterfaceNames[i]);
                    }
                    else
                    {
                        sb.Append(", ").Append(baseInterfaceNames[i]);
                    }
                }

                writer.WriteLine();
                writer.WriteLine($"public interface {interfaceName} : {sb}");
                using (writer.StartBlock())
                {
                }
            }

        }

        private static void WriteInterfaceDeclaration(FormattedTextWriter writer, SqlServerVersion version, ModelTypeClass type, string interfaceName, Element element)
        {
            writer.WriteLine();
            writer.WriteLine($"public interface {interfaceName} : {DacUtilities.BaseModelInterfaceFullName}");
            using (writer.StartBlock())
            {
                foreach (ModelPropertyClass property in GetProperties(type))
                {
                    if (!DacUtilities.SupportsVersion(property.SupportedPlatforms, version))
                    {
                        continue;
                    }
                    bool useGenericGetter;
                    string typeName = GetPropertyTypeName(property.DataType, out useGenericGetter);
                    string propertyName = GetPropertyName(property, element);
                    writer.WriteLine();
                    writer.WriteLine($"{typeName} {propertyName} {{ get; }}");
                }
            }
        }

        public static void GenerateVersionSpecificClasses(FormattedTextWriter writer)
        {
            writer.WriteFileHeader();
            writer.WriteLine($"namespace {DacUtilities.NamespaceName}");
            using (writer.StartBlock())
            {
                foreach (SqlServerVersion version in DacUtilities.GetSqlServerVersions())
                {
                    string interfacePrefix = GetInterfacePrefix(version);

                    foreach (ModelTypeClass type in ModelSchema.SchemaInstance.AllTypes)
                    {
                        string interfaceName = GetBaseInterfaceName(type, version);
                        string referenceInterfaceName = GetReferenceInterfaceName(type, version);
                        Element element = DacUtilities.FindElement(type.Name);
                        if (element != null)
                        {
                            writer.WriteLine();
                            writer.WriteLine("/// <summary>");
                            writer.WriteLine($"/// Explicit implementation of <see cref=\"{interfaceName}\"/>.");
                            writer.WriteLine("/// </summary>");
                            writer.WriteLine($"public partial class {DacUtilities.ClassNamePrefix + type.Name} : {interfaceName}");
                            using (writer.StartBlock())
                            {
                                foreach (ModelPropertyClass property in GetProperties(type))
                                {
                                    if (!DacUtilities.SupportsVersion(property.SupportedPlatforms, version))
                                    {
                                        continue;
                                    }
                                    bool useGenericGetter;
                                    string typeName = GetPropertyTypeName(property.DataType, out useGenericGetter);
                                    string propertyName = GetPropertyName(property, element);
                                    writer.WriteLine();
                                    writer.WriteLine($"{typeName} {interfaceName}.{propertyName} => {propertyName};");
                                }
                                foreach (var relationship in type.Relationships.OrderBy(r => r.Name))
                                {
                                    if (!!DacUtilities.SupportsVersion(relationship.SupportedPlatforms, version))
                                    {
                                        continue;
                                    }
                                    string returnType = DacUtilities.DefaultReturnType;
                                    string castExpression = "";
                                    if (element != null)
                                    {
                                        Relationship localoverride;
                                        if (element.Children.OfType<Relationship>().ToDictionary(r => r.Name, r => r).TryGetValue(relationship.Name, out localoverride))
                                        {
                                            if (localoverride.Specialize)
                                            {
                                                returnType = $"{localoverride.ReturnTypeNamespace}.{interfacePrefix}{localoverride.ReturnType}";
                                                castExpression = $".Cast<{returnType}>()";
                                            }
                                            else
                                            {
                                                returnType = $"{localoverride.ReturnTypeNamespace}.{localoverride.ReturnType}";
                                            }
                                        }
                                    }
                                    writer.WriteLine();
                                    writer.WriteLine($"// {relationship.Type} relationship");
                                    writer.WriteLine($"{IEnumerableName}<{returnType}> {interfaceName}.{relationship.Name} => {relationship.Name + castExpression};");
                                }
                            }
                        }
                    }
                }
            }
        }

        private static string GetPropertyTypeName(Type dataType, out bool useGenericGetter)
        {
            useGenericGetter = true;
            string typeName;
            if (dataType.IsGenericType)
            {
                Type[] genericTypes = dataType.GetGenericArguments();
                typeName = genericTypes[0].Name + "?";
            }
            else if (dataType.Name == "SqlScriptProperty")
            {
                typeName = "String";
                useGenericGetter = false;
            }
            else
            {
                typeName = dataType.Name;
            }
            switch (typeName)
            {
                case "Boolean?": return "bool?";
                case "Boolean": return "bool";
                case "String?": return "string?";
                case "String": return "string";
                case "Int16?": return "short?";
                case "Int16": return "short";
                case "Int32?": return "int?";
                case "Int32": return "int";
                case "Int64?": return "long?";
                case "Int64": return "long";
                case "Decimal?": return "decimal?";
                case "Decimal": return "decimal";
                default: return typeName;
            }
        }

        private static string GetPropertyName(ModelPropertyClass property, Element element)
        {
            string propertyName = null;
            if (element != null && element.Children.OfType<Property>().ToDictionary(x => x.Name, x => x.OverrideName).TryGetValue(property.Name, out propertyName))
            {
                return propertyName;
            }

            return property.Name;
        }

        private static List<ModelPropertyClass> GetProperties(ModelTypeClass type) =>
            new List<ModelPropertyClass>(type.Properties.OrderBy(t => t.Name));

        private static string GetInterfacePrefix(SqlServerVersion version) => $"I{version}";

        private static string GetBaseInterfaceName(ModelTypeClass type, SqlServerVersion version)
        {
            return GetInterfacePrefix(version) + DacUtilities.ClassNamePrefix + type.Name;
        }

        private static string GetReferenceInterfaceName(ModelTypeClass type, SqlServerVersion version)
        {
            return $"{GetBaseInterfaceName(type, version)}Reference";
        }
    }
}
