using Delta.AS400.DataTypes;
using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Modernization.Statements.Items.Namespaces;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.Modernization.Generation
{
    public class RepoGenerator
    {
        static string forSorting = "4s";

        public static string CreateIRepositoryContents(bool isForDB2, bool isLF,string EntityName, ObjectID objectID, ScalarVariableList keyList)
        {

            string FindAllMethodName= (!isForDB2 && isLF) ? "FindLFAll": "FindAll";

            var FileName = objectID.Name.ToCSharpOperand();
            var LibraryName = objectID.Library.Name.ToPublicModernName();

            var indent = new Indent();
            var contents = new StringBuilder();

            contents.AppendLine(NamespaceItemFactory.System.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemCollectionsGeneric.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemLinq.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemLinqExpressions.ToUsingLine);

            contents.AppendLine(NamespaceItemFactory.DeltaOf(objectID.Library, EntityName).ToNamespaceLine);

            contents.AppendLine("{");
            contents.AppendLine($"{Indent.Single}public partial interface I{FileName}Repository");
            contents.AppendLine($"{Indent.Single}{{");
            contents.AppendLine($"{Indent.Couple}");
            contents.AppendLine($"{Indent.Couple}int Insert({EntityName} item);");
            contents.AppendLine($"{Indent.Couple}");
            contents.AppendLine($"{Indent.Couple}int Update({EntityName} item);");
            contents.AppendLine($"{Indent.Couple}");
            contents.AppendLine($"{Indent.Couple}int Delete({EntityName} item);");
            contents.AppendLine($"{Indent.Couple}");

            if (!isForDB2)
            {
                if (isLF)
                {
                    contents.AppendLine($"{Indent.Couple}IQueryable<{EntityName}> {FindAllMethodName}();");
                }

                contents.AppendLine($"{Indent.Couple}IQueryable<{EntityName}> FindAll();");
                contents.AppendLine($"{Indent.Couple}");

                contents.AppendLine($"{Indent.Couple}int Count();");
                contents.AppendLine($"{Indent.Couple}");

                contents.AppendLine($"{Indent.Couple}int Count(Expression<Func<{EntityName}, bool>> predicate);");
                contents.AppendLine($"{Indent.Couple}");

                contents.AppendLine($"{Indent.Couple}int DeleteWhere(Expression<Func<{EntityName}, bool>> predicate);");
                contents.AppendLine($"{Indent.Couple}");

                contents.AppendLine($"{Indent.Couple}int ExecuteSqlRaw(string rawSqlString, params object[] parameters);");
                contents.AppendLine($"{Indent.Couple}");

                contents.AppendLine($"{Indent.Couple}void Truncate(string objectName);");
                contents.AppendLine($"{Indent.Couple}");

            }

            var p = keyList.ScalarVariables.Select(s => $"{s.TypeSpelling} {s.CamelCaseName}").Aggregate((all, cur) => $"{all}, {cur}");

            if (isForDB2)
            {
                contents.AppendLine($"{Indent.Couple}{EntityName} Find({p});");
                contents.AppendLine($"{Indent.Couple}");
                if (!isLF)
                {
                    contents.AppendLine($"{Indent.Couple}int Delete({p});");
                }
            }
            else
            {
                contents.AppendLine($"{Indent.Couple}{EntityName} Find({p});");
                contents.AppendLine($"{Indent.Couple}");
                if (!isLF)
                {
                    contents.AppendLine($"{Indent.Couple}int Delete({p});");
                }
            }
            contents.AppendLine($"{Indent.Couple}");


            var tuppleSpels = keyList.TuppleSpellings();

            var limitKeyType = tuppleSpels.Last();

            var lineLimitFuncAll = keyList.ScalarVariables.Select(v => $"item.{v.Name}{(v.Name == "Id" ? string.Empty : forSorting)}").Aggregate((tempAll, added) => $"{tempAll} + {added}");

            for (int equalKeyCount = 0; equalKeyCount <= keyList.ItemCount; equalKeyCount++)
            {
                var lineLimitFunc = string.Empty;
                if (equalKeyCount < keyList.ItemCount) lineLimitFunc = keyList.ScalarVariables.Skip(equalKeyCount).Select(v => $"item.{v.Name}{(v.Name == "Id" ? string.Empty : forSorting)}").Aggregate((tempAll, added) => $"{tempAll} + {added}");

                var equals = string.Empty;
                if (equalKeyCount == 0)
                {
                    equals = string.Empty;
                }
                else
                if (equalKeyCount == 1)
                {
                    equals = $"item.{keyList.ScalarVariables[0].Name}==equalKey";
                }
                else
                {
                    equals = keyList.ScalarVariables.Take(equalKeyCount).Select(v => $"item.{v.Name}==equalKey.{v.Name.ToLower()}").Aggregate((tempAll, added) => $"{tempAll} && {added}");
                }

                var curKeyTypeOfEqualKey = string.Empty;
                if (equalKeyCount == 0)
                {
                    curKeyTypeOfEqualKey = string.Empty;
                }
                else
                {
                    curKeyTypeOfEqualKey = $"{tuppleSpels[equalKeyCount - 1]} equalKey";
                }
                
                for (int limitKeyCount = 1; limitKeyCount <= keyList.ScalarVariables.Count; limitKeyCount++)
                {

                    var lk = limitKeyCount == 1 ? $"{keyList.ScalarVariables[0].TypeSpelling} {keyList.ScalarVariables[0].CamelCaseName}" : $"{keyList.TuppleSpellings()[limitKeyCount - 1]} limitKey";

                    var limitLineValue = string.Empty;
                    var limitLineJoiner = string.Empty;
                    if (limitKeyCount == 1)
                    {
                        if (keyList.ScalarVariables[0].OfTypeIsNumeric)
                        {
                            limitLineValue = keyList.ScalarVariables[0].Name.ToLower();
                            limitLineJoiner = $"item.{keyList.ScalarVariables[0].Name}";
                        }
                        else
                        {
                            limitLineValue = $"{EntityName}.{keyList.ScalarVariables[0].Name}To4s({keyList.ScalarVariables[0].CamelCaseName})";
                            limitLineJoiner = $"item.{keyList.ScalarVariables[0].Name}4s";
                        }
                    }
                    else
                    {
                        limitLineValue = keyList.ScalarVariables.Take(limitKeyCount)
                            .Select(v => $"{EntityName}.{v.Name}To4s(limitKey.{v.CamelCaseName})")
                            .Aggregate((tempAll, added) => $"{tempAll} + {added}");
                        limitLineJoiner = keyList.ScalarVariables.Take(limitKeyCount)
                            .Select(v => $"item.{v.Name}4s")
                            .Aggregate((tempAll, added) => $"{tempAll} + {added}");
                    }

                    if (isForDB2)
                    {
                        if (equalKeyCount == 0)
                        {
                            contents.AppendLine($"{Indent.Couple}List<{EntityName}> Read(bool isSetLLnotGT, {lk});");
                            contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadPrior(bool isSetLLnotGT, {lk});");
                        }
                        else
                        {
                            contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadEqual(bool isSetLLnotGT, {lk}, {curKeyTypeOfEqualKey});");
                            contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadPriorEqual(bool isSetLLnotGT, {lk}, {curKeyTypeOfEqualKey});");
                        }
                    }
                    else
                    {
                        CreateEqualMethodInIRepo(contents, EntityName, keyList, equalKeyCount, curKeyTypeOfEqualKey, equals, lk, limitLineJoiner, limitLineValue, lineLimitFunc, false, isForDB2, FindAllMethodName);
                        CreateEqualMethodInIRepo(contents, EntityName, keyList, equalKeyCount, curKeyTypeOfEqualKey, equals, lk, limitLineJoiner, limitLineValue, lineLimitFunc, true, isForDB2, FindAllMethodName);
                    }
                }

                var curKeyTypeOfNonLimit = string.Empty;
                if (equalKeyCount == 0)
                {
                    curKeyTypeOfNonLimit = string.Empty;
                }
                else
                if (equalKeyCount == 1)
                {
                    curKeyTypeOfNonLimit = $"{keyList.ScalarVariables[0].TypeSpelling} {keyList.ScalarVariables[0].Name.ToLower()}";
                }
                else
                {
                    curKeyTypeOfNonLimit = $"{tuppleSpels[equalKeyCount - 1]} equalKey";
                }

                if (isForDB2)
                {
                    if (equalKeyCount == 0)
                    {
                        contents.AppendLine($"{Indent.Couple}List<{EntityName}> Read();");
                    }
                    else
                    {
                        contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadEqual({curKeyTypeOfNonLimit});");
                    }
                }
                else
                {
                    if (equalKeyCount == 0)
                    {
                        contents.AppendLine($"{Indent.Couple}List<{EntityName}> Read()");
                    }
                    else
                    {
                        contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadEqual({curKeyTypeOfNonLimit})");
                    }
                    contents.AppendLine($"{Indent.Couple}{{");
                    contents.AppendLine($"{Indent.Triple}return {FindAllMethodName}()");
                    if (equalKeyCount == 1)
                    {
                        contents.AppendLine($"{Indent.Triple}.Where(item => item.{keyList.ScalarVariables[0].Name}=={keyList.ScalarVariables[0].Name.ToLower()})");
                    }
                    else
                    if (equalKeyCount >= 2)
                    {
                        contents.AppendLine($"{Indent.Triple}.Where(item => {equals})");
                    }

                    var vars = keyList.ScalarVariables.Skip(equalKeyCount).ToList();
                    for (int x = 0; x < vars.Count; x++)
                    {
                        var command = $"{(x == 0 ? "Order" : "Then")}By";
                        var v = vars[x];
                        var name = $"{v.Name}{(((v.Name == "Id") || v.OfTypeIsNumeric) ? string.Empty : forSorting)}";
                        contents.AppendLine($"{Indent.Triple}.{command}(item => item.{name})");
                    }
                    contents.AppendLine($"{Indent.Triple}.ToList();");
                    contents.AppendLine($"{Indent.Couple}}}");
                }

                if (isForDB2)
                {
                    //if (keyList.ItemCount - equalKeyCount <= 1)
                    {
                        if (equalKeyCount == 0)
                        {
                            contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadPrior();");
                        }
                        else
                        {
                            contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadPriorEqual({curKeyTypeOfNonLimit});");
                        }
                    }
                    //else
                    //{
                    //    if (equalKeyCount == 0)
                    //    {
                    //        contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadPrior() => throw new NotImplementedException();");
                    //    }
                    //    else
                    //    {
                    //        contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadPriorEqual({curKeyTypeOfNonLimit}) => throw new NotImplementedException();");
                    //    }
                    //}
                }
                else
                {
                    if (equalKeyCount == 0)
                    {
                        contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadPrior()");
                    }
                    else
                    {
                        contents.AppendLine($"{Indent.Couple}List<{EntityName}> ReadPriorEqual({curKeyTypeOfNonLimit})");
                    }
                    contents.AppendLine($"{Indent.Couple}{{");
                    contents.AppendLine($"{Indent.Triple}return {FindAllMethodName}()");
                    if (equalKeyCount == 1)
                    {
                        contents.AppendLine($"{Indent.Triple}.Where(item => item.{keyList.ScalarVariables[0].Name}=={keyList.ScalarVariables[0].Name.ToLower()})");
                    }
                    else
                    if (equalKeyCount >= 2)
                    {
                        contents.AppendLine($"{Indent.Triple}.Where(item => {equals})");
                    }

                    if (equalKeyCount < keyList.ItemCount)
                    {
                        contents.AppendLine($"{Indent.Triple}.OrderByDescending(item => {lineLimitFunc})");
                    }
                    contents.AppendLine($"{Indent.Triple}.ToList();");
                    contents.AppendLine($"{Indent.Couple}}}");
                }

            }

            for (int i = 0; i <= keyList.TuppleSpellings().Count; i++)
            {
                var param = i == 0 ? string.Empty : $"{keyList.TuppleSpellings()[i - 1]} key";
                contents.AppendLine($"{Indent.Couple}{limitKeyType} LowLimit({param});");
            }
            for (int i = 0; i <= keyList.TuppleSpellings().Count; i++)
            {
                var param = i == 0 ? string.Empty : $"{keyList.TuppleSpellings()[i - 1]} key";
                contents.AppendLine($"{Indent.Couple}{limitKeyType} HighLimit({param});");
            }

            contents.AppendLine($"{Indent.Single}}}");
            contents.AppendLine("}");
            return contents.ToString();
        }

        static void CreateEqualMethodInIRepo(StringBuilder contents, string EntityName, ScalarVariableList keyList,
            int equalKeyCount, string curKeyTypeOfEqualKey, string equals,
            string lk, string limitLineJoiner, string limitLineValue,
            string lineLimitFunc,
            bool isP, bool isForDB2, string FindAllMethodName)
        {


            if (isForDB2)
            {
                if (equalKeyCount == 0)
                {
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> Read{(isP ? "Prior" : string.Empty)}(bool isSetLLnotGT, {lk});");
                }
                else
                {
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> Read{(isP ? "Prior" : string.Empty)}Equal(bool isSetLLnotGT, {lk}, {curKeyTypeOfEqualKey});");
                }
            }
            else
            {
                if (equalKeyCount == 0)
                {
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> Read{(isP ? "Prior" : string.Empty)}(bool isSetLLnotGT, {lk})");
                }
                else
                {
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> Read{(isP ? "Prior" : string.Empty)}Equal(bool isSetLLnotGT, {lk}, {curKeyTypeOfEqualKey})");
                }

                contents.AppendLine($"{Indent.Couple}{{");
                contents.AppendLine($"{Indent.Triple}var limitLine = {limitLineValue};");
                contents.AppendLine($"{Indent.Triple}IQueryable<{EntityName}> limitQuery;");
                contents.AppendLine($"{Indent.Triple}if (isSetLLnotGT)");
                contents.AppendLine($"{Indent.Triple}{{");
                contents.AppendLine($"{Indent.Quadruple}limitQuery = {FindAllMethodName}().Where(item => ({limitLineJoiner}).CompareTo(limitLine) {(isP ? "<" : ">=")} 0);");
                contents.AppendLine($"{Indent.Triple}}}");
                contents.AppendLine($"{Indent.Triple}else");
                contents.AppendLine($"{Indent.Triple}{{");
                contents.AppendLine($"{Indent.Quadruple}limitQuery = {FindAllMethodName}().Where(item => ({limitLineJoiner}).CompareTo(limitLine) {(isP ? "<=" : ">")} 0);");
                contents.AppendLine($"{Indent.Triple}}}");

                contents.AppendLine($"{Indent.Triple}return limitQuery");
                if (equalKeyCount > 0)
                {
                    contents.AppendLine($"{Indent.Triple}.Where(item => {equals})");
                }
                if (isP)
                {
                    if (equalKeyCount < keyList.ItemCount)
                    {
                        contents.AppendLine($"{Indent.Triple}.OrderByDescending(item => {lineLimitFunc})");
                    }
                }
                else
                {
                    var vars = keyList.ScalarVariables.Skip(equalKeyCount).ToList();
                    for (int x = 0; x < vars.Count; x++)
                    {
                        var command = $"{(x == 0 ? "Order" : "Then")}By";
                        var v = vars[x];
                        var name = $"{v.Name}{(((v.Name == "Id") || v.OfTypeIsNumeric) ? string.Empty : forSorting)}";
                        contents.AppendLine($"{Indent.Triple}.{command}(item => item.{name})");
                    }
                }

                contents.AppendLine($"{Indent.Triple}.ToList();");
                contents.AppendLine($"{Indent.Couple}}}");
            }


        }


        public static string CreateDB2RepositoryContents(bool isLF,string EntityName, ObjectID objectID, List<IDataTypeDefinition> dataTypeDefinitions, ScalarVariableList keyList)
        {

            var FileName = objectID.Name.ToCSharpOperand();
            var RepositoryName = $"{FileName}DB2Repository";
            var IRepositoryName = $"I{FileName}Repository";

            var contents = new StringBuilder();

            contents.AppendLine(NamespaceItemFactory.DeltaRelationalDatabasesDb2fori.ToUsingLine);
            //contents.AppendLine(NamespaceItemFactory.DeltaRelationalDatabases.ToUsingLine);

            contents.AppendLine(NamespaceItemFactory.System.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemCollectionsGeneric.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemDataCommon.ToUsingLine);

            //contents.AppendLine(NamespaceItemFactory.SystemLinq.ToUsingLine);
            //contents.AppendLine(NamespaceItemFactory.SystemLinqExpressions.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemText.ToUsingLine);

            contents.AppendLine();
            contents.AppendLine(NamespaceItemFactory.DeltaOf(objectID.Library, EntityName).ToNamespaceLine);
            contents.AppendLine("{");
            contents.AppendLine($"{Indent.Single}public class {RepositoryName} : Db2foriFileRepository, {IRepositoryName}");
            contents.AppendLine($"{Indent.Single}{{");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}public static {RepositoryName} Of(string aIP) => Of(aIP, \"{objectID.Library.Name}\");");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}public static {RepositoryName} Of(string aIP, string aLibraryName)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}return new {RepositoryName}(aIP, aLibraryName);");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}{RepositoryName}(string aIP, string aLibraryName) : base(aIP, aLibraryName, \"{FileName.ToUpper()}\")");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Insert({EntityName} item)");
            contents.AppendLine($"{Indent.Couple}{{");
            if (!isLF)
            {
                contents.AppendLine($"{Indent.Triple}var query = new StringBuilder();");
                contents.AppendLine($"{Indent.Triple}query.Append($\"insert into {{ObjectName}} \");");
                var fieldNames = dataTypeDefinitions.Select(item => item.Name.ToUpper()).Aggregate((all, cur) => $"{all}, {cur}");
                contents.AppendLine($"{Indent.Triple}query.Append($\"({fieldNames})\");");
                contents.AppendLine($"{Indent.Triple}query.Append($\" values \");");
                var fieldValues = dataTypeDefinitions.Select(item => item.IsExplicitCharacter ? $"'{{item.{item.Name.ToPascalCase()}}}'" : $"{{item.{item.Name.ToPascalCase()}}}").Aggregate((all, cur) => $"{all}, {cur}");
                contents.AppendLine($"{Indent.Triple}query.Append($\"({fieldValues})\");");
                contents.AppendLine($"{Indent.Triple}return ExecuteNonQuery(query.ToString());");
            }
            else
            {
                contents.AppendLine($"{Indent.Triple}throw new NotImplementedException();");
            }
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Update({EntityName} item)");
            contents.AppendLine($"{Indent.Couple}{{");
            if (!isLF)
            {
                contents.AppendLine($"{Indent.Triple}var query = new StringBuilder();");
                contents.AppendLine($"{Indent.Triple}query.Append($\"update {{ObjectName}} set \");");
                var notKeyFields = dataTypeDefinitions.Where(item=> !keyList.Names.Any(k=>k.ToUpper()==item.Name.ToUpper())).ToList();
                for (int i = 0; i < notKeyFields.Count; i++)
                {
                    var dtd = notKeyFields[i];
                    var de = dtd.IsExplicitCharacter ? "'" : string.Empty;
                    contents.AppendLine($"{Indent.Triple}query.Append(\" {dtd.Name} \").Append('=').Append($\" {de}{{item.{dtd.Name.ToPascalCase()}}}{de} \"){(i == notKeyFields.Count - 1 ? string.Empty : ".Append(\" , \")")};");
                }
                contents.AppendLine($"{Indent.Triple}query.Append(\" where \");");
                for (int i = 0; i < keyList.Names.Count; i++)
                {
                    var keyName = keyList.Names[i];
                    var de = keyList.ScalarVariables[i].OfTypeIsString ? "'" : string.Empty;
                    contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} \").Append('=').Append($\" {de}{{item.{keyName.ToPascalCase()}}}{de} \"){(i == keyList.Names.Count - 1 ? string.Empty : ".Append(\" and \")")};");
                }
                contents.AppendLine($"{Indent.Triple}return ExecuteNonQuery(query.ToString());");
            }
            else
            {
                contents.AppendLine($"{Indent.Triple}throw new NotImplementedException();");
            }

            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Delete({EntityName} item)");
            contents.AppendLine($"{Indent.Couple}{{");
            if (!isLF)
            {
                contents.AppendLine($"{Indent.Triple}var query = new StringBuilder();");
                contents.AppendLine($"{Indent.Triple}query.Append($\"delete from {{ObjectName}} where \");");
                for (int i = 0; i < keyList.Names.Count; i++)
                {
                    var keyName = keyList.Names[i];
                    var de = keyList.ScalarVariables[i].OfTypeIsString ? "'" : string.Empty;
                    contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} \").Append('=').Append($\" {de}{{item.{keyName.ToPascalCase()}}}{de} \"){(i == keyList.Names.Count - 1 ? string.Empty : ".Append(\" and \")")};");
                }
                contents.AppendLine($"{Indent.Triple}return ExecuteNonQuery(query.ToString());");
            }
            else
            {
                contents.AppendLine($"{Indent.Triple}throw new NotImplementedException();");
            }
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            var p = keyList.ScalarVariables.Select(s => $"{s.TypeSpelling} {s.CamelCaseName}").Aggregate((all, cur) => $"{all}, {cur}");

            if (!isLF)
            {
                contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Delete({p})");
                contents.AppendLine($"{Indent.Couple}{{");
                contents.AppendLine($"{Indent.Triple}var query = new StringBuilder();");
                contents.AppendLine($"{Indent.Triple}query.Append($\"delete from {{ObjectName}} where \");");
                for (int i = 0; i < keyList.Names.Count; i++)
                {
                    var keyName = keyList.Names[i];
                    var de = keyList.ScalarVariables[i].OfTypeIsString ? "'" : string.Empty;
                    contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} \").Append('=').Append($\" {de}{{{keyName.ToLower()}}}{de} \"){(i == keyList.Names.Count - 1 ? string.Empty : ".Append(\" and \")")};");
                }
                contents.AppendLine($"{Indent.Triple}return ExecuteNonQuery(query.ToString());");
                contents.AppendLine($"{Indent.Couple}}}");
                contents.AppendLine();
            }

            var cond = keyList.ScalarVariables.Select(s => $"item.{s.Name}=={s.CamelCaseName}").Aggregate((all, cur) => $"{all} && {cur}");
            contents.AppendLine($"{Indent.Couple}{EntityName} {IRepositoryName}.Find({p})");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}var query = new StringBuilder();");
            contents.AppendLine($"{Indent.Triple}query.Append($\"select * from {{ObjectName}} where \");");
            for (int i = 0; i < keyList.Names.Count; i++)
            {
                var keyName = keyList.Names[i];
                var de = keyList.ScalarVariables[i].OfTypeIsString ? "'" : string.Empty;
                contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} \").Append('=').Append($\" {de}{{{keyName.ToLower()}}}{de} \"){(i == keyList.Names.Count - 1 ? string.Empty : ".Append(\" and \")")};");
            }
            contents.AppendLine($"{Indent.Triple}return Find(query.ToString(), Mapping);");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();



            contents.AppendLine($"{Indent.Couple}{EntityName} Mapping(DbDataReader reader)");
            contents.AppendLine($"{Indent.Couple}{{");

            contents.AppendLine($"{Indent.Triple}return new {EntityName}()");
            contents.AppendLine($"{Indent.Triple}{{");
            for (int i = 0; i < dataTypeDefinitions.Count; i++)
            {
                var dtd = dataTypeDefinitions[i];
                var getter = "reader.GetString";
                if (dtd.IsExplicitCharacter)
                {
                    getter = "reader.GetString";
                }
                else
                if (dtd.IsExplicitNumeric)
                {
                    getter = "reader.GetDecimal";

                    if (dtd.IsExplicitInteger)
                    {
                        if (dtd.LengthToInt > 9)
                        {
                            getter = $"(long){getter}";
                        }
                        else
                        if (dtd.LengthToInt > 4)
                        {
                            getter = $"(int){getter}";
                        }
                        else
                        {
                            getter = $"(short){getter}";
                        }
                    }
                }
                else
                if (dtd.IsBinary)
                {
                    getter = "reader.GetByte";
                }
                else
                {
                    throw new NotImplementedException();
                    //getter = "String";
                }
                contents.AppendLine($"{Indent.Triple}{dtd.Name.ToPublicModernName()} = {getter}({i}),");
            }
            contents.AppendLine($"{Indent.Triple}}};");

            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}void ForCording({keyList.JoinedTypes()}){{");
            contents.AppendLine($"{Indent.Triple}var query = new StringBuilder();");
            contents.AppendLine($"{Indent.Triple}query.Append(\"select \");");
            for (int i = 0; i < dataTypeDefinitions.Count; i++)
            {
                var dtd = dataTypeDefinitions[i];
                contents.AppendLine($"{Indent.Triple}query.Append(\" {dtd.Name} \"){(i == dataTypeDefinitions.Count - 1 ? string.Empty : ".Append(',')")};");
            }
            contents.AppendLine($"{Indent.Triple}query.Append(\"from\");");
            contents.AppendLine($"{Indent.Triple}query.Append($\" {{ObjectName}} \");");
            contents.AppendLine($"{Indent.Triple}query.Append(\"where\");");
            for (int i = 0; i < keyList.Names.Count; i++)
            {
                var keyName = keyList.Names[i];
                var de = keyList.ScalarVariables[i].OfTypeIsString ? "'" : string.Empty;
                contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} \").Append('=').Append($\" {de}{{{keyName.ToLower()}}}{de} \"){(i == keyList.Names.Count - 1 ? string.Empty : ".Append(\" and \")")};");
            }
            contents.AppendLine($"{Indent.Triple}query.Append(\"order by\");");
            for (int i = 0; i < keyList.Names.Count; i++)
            {
                var keyName = keyList.Names[i];
                contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} \"){(i == keyList.Names.Count - 1 ? string.Empty : ".Append(',')")};");
            }

            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}");
            
            var tuppleSpels = keyList.TuppleSpellings();

            var limitKeyType = tuppleSpels.Last();

            var lineLimitFuncAll = keyList.ScalarVariables.Select(v => $"item.{v.Name}{(v.Name == "Id" ? string.Empty : forSorting)}").Aggregate((tempAll, added) => $"{tempAll} + {added}");

            for (int equalKeyCount = 0; equalKeyCount <= keyList.ItemCount; equalKeyCount++)
            {
                var lineLimitFunc = string.Empty;
                if (equalKeyCount < keyList.ItemCount) lineLimitFunc = keyList.ScalarVariables.Skip(equalKeyCount).Select(v => $"item.{v.Name}{(v.Name == "Id" ? string.Empty : forSorting)}").Aggregate((tempAll, added) => $"{tempAll} + {added}");

                var equals = string.Empty;
                if (equalKeyCount == 0)
                {
                    equals = string.Empty;
                }
                else
                if (equalKeyCount == 1)
                {
                    equals = $"item.{keyList.ScalarVariables[0].Name}==equalKey";
                }
                else
                {
                    equals = keyList.ScalarVariables.Take(equalKeyCount).Select(v => $"item.{v.Name}==equalKey.{v.Name.ToLower()}").Aggregate((tempAll, added) => $"{tempAll} && {added}");
                }

                var curEqualKeyType = string.Empty;
                if (equalKeyCount == 0)
                {
                    curEqualKeyType = string.Empty;
                }
                else
                if (equalKeyCount == 1)
                {
                    curEqualKeyType = $"{keyList.ScalarVariables[0].TypeSpelling} {keyList.ScalarVariables[0].Name.ToLower()}" ;
                }
                else
                {
                    curEqualKeyType = $"{tuppleSpels[equalKeyCount - 1]} equalKey";
                }
                
                for (int limitKeyCount = 1; limitKeyCount <= keyList.ScalarVariables.Count; limitKeyCount++)
                {

                    var lk = limitKeyCount == 1 ? $"{keyList.ScalarVariables[0].TypeSpelling} {keyList.ScalarVariables[0].CamelCaseName}" : $"{keyList.TuppleSpellings()[limitKeyCount - 1]} limitKey";

                    var limitLineValue = string.Empty;
                    var limitLineJoiner = string.Empty;
                    if (limitKeyCount == 1)
                    {
                        if (keyList.ScalarVariables[0].OfTypeIsNumeric)
                        {
                            limitLineValue = keyList.ScalarVariables[0].CamelCaseName;
                            limitLineJoiner = $"item.{keyList.ScalarVariables[0].Name}";
                        }
                        else
                        {
                            limitLineValue = $"{EntityName}.{keyList.ScalarVariables[0].Name}To4s({keyList.ScalarVariables[0].CamelCaseName})";
                            limitLineJoiner = $"item.{keyList.ScalarVariables[0].Name}4s";
                        }
                    }
                    else
                    {
                        limitLineValue = keyList.ScalarVariables.Take(limitKeyCount)
                            .Select(v => $"{EntityName}.{v.Name}To4s(limitKey.{v.CamelCaseName})")
                            .Aggregate((tempAll, added) => $"{tempAll} + {added}");
                        limitLineJoiner = keyList.ScalarVariables.Take(limitKeyCount)
                            .Select(v => $"item.{v.Name}4s")
                            .Aggregate((tempAll, added) => $"{tempAll} + {added}");
                    }

                    //CreateEqualMethod(contents, EntityName, keyList, equalKeyCount, curKeyType, equals, lk, limitLineJoiner, limitLineValue, lineLimitFunc, false);
                    //CreateEqualMethod(contents, EntityName, keyList, equalKeyCount, curKeyType, equals, lk, limitLineJoiner, limitLineValue, lineLimitFunc, true);

                }

                if (equalKeyCount == 0)
                {
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> {IRepositoryName}.Read()");
                }
                else
                {
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> {IRepositoryName}.ReadEqual({curEqualKeyType})");
                }
                contents.AppendLine($"{Indent.Couple}{{");

                contents.AppendLine($"{Indent.Triple}var query = new StringBuilder();");
                contents.AppendLine($"{Indent.Triple}query.Append($\"select * from {{ObjectName}} \");");
                if (equalKeyCount > 0)
                {
                    contents.AppendLine($"{Indent.Triple}query.Append($\" where \");");
                    for (int i = 0; i < equalKeyCount; i++)
                    {
                        var keyName = keyList.Names[i];
                        var de = keyList.ScalarVariables[i].OfTypeIsString ? "'" : string.Empty;
                        contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} \").Append('=').Append($\" {de}{{{(equalKeyCount <= 1?string.Empty: "equalKey.")}{keyName.ToLower()}}}{de} \"){(i == equalKeyCount - 1 ? string.Empty : ".Append(\" and \")")};");
                    }
                }
                if (keyList.ItemCount - equalKeyCount > 0)
                {
                    contents.AppendLine($"{Indent.Triple}query.Append(\"order by\");");
                    for (int i = equalKeyCount; i < keyList.ItemCount; i++)
                    {
                        var keyName = keyList.Names[i];
                        contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} \"){(i == keyList.ItemCount - 1 ? string.Empty : ".Append(',')")};");
                    }
                }
                contents.AppendLine($"{Indent.Triple}return ExecuteReader(query.ToString(),Mapping);");

                contents.AppendLine($"{Indent.Couple}}}");

                if (equalKeyCount == 0)
                {
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> {IRepositoryName}.ReadPrior()");
                }
                else
                {
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> {IRepositoryName}.ReadPriorEqual({curEqualKeyType})");
                }
                contents.AppendLine($"{Indent.Couple}{{");
                if(keyList.ItemCount - equalKeyCount <= 1)
                {

                    contents.AppendLine($"{Indent.Triple}var query = new StringBuilder();");
                    contents.AppendLine($"{Indent.Triple}query.Append($\"select * from {{ObjectName}} \");");
                    if (equalKeyCount > 0)
                    {
                        contents.AppendLine($"{Indent.Triple}query.Append($\" where \");");
                        for (int i = 0; i < equalKeyCount; i++)
                        {
                            var keyName = keyList.Names[i];
                            var de = keyList.ScalarVariables[i].OfTypeIsString ? "'" : string.Empty;
                            contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} \").Append('=').Append($\" {de}{{{(equalKeyCount <= 1 ? string.Empty : "equalKey.")}{keyName.ToLower()}}}{de} \"){(i == equalKeyCount - 1 ? string.Empty : ".Append(\" and \")")};");
                        }
                    }
                    if (keyList.ItemCount - equalKeyCount ==1)
                    {
                        contents.AppendLine($"{Indent.Triple}query.Append(\"order by\");");
                        var keyName = keyList.Names[equalKeyCount];
                        contents.AppendLine($"{Indent.Triple}query.Append(\" {keyName.ToUpper()} desc\");");
                    }
                    contents.AppendLine($"{Indent.Triple}return ExecuteReader(query.ToString(),Mapping);");
                }
                else
                {
                    contents.AppendLine($"{Indent.Triple}throw new NotImplementedException();");
                }
                contents.AppendLine($"{Indent.Couple}}}");
               

                for (int limitKeyCount = 1; limitKeyCount <= keyList.ScalarVariables.Count; limitKeyCount++)
                {
                    var lk = limitKeyCount == 1 ? $"{keyList.ScalarVariables[0].TypeSpelling} limitKey" : $"{keyList.TuppleSpellings()[limitKeyCount - 1]} limitKey";
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> {IRepositoryName}.Read{(equalKeyCount == 0 ? string.Empty : "Equal")}(bool isSetLLnotGT, {lk}{(equalKeyCount==0?string.Empty:", ")}{curEqualKeyType})");
                    contents.AppendLine($"{Indent.Couple}{{");
                    contents.AppendLine($"{Indent.Triple}throw new NotImplementedException();");
                    contents.AppendLine($"{Indent.Couple}}}");
                    contents.AppendLine($"{Indent.Couple}List<{EntityName}> {IRepositoryName}.ReadPrior{(equalKeyCount == 0 ? string.Empty : "Equal")}(bool isSetLLnotGT, {lk}{(equalKeyCount == 0 ? string.Empty : ", ")}{curEqualKeyType})");
                    contents.AppendLine($"{Indent.Couple}{{");
                    contents.AppendLine($"{Indent.Triple}throw new NotImplementedException();");
                    contents.AppendLine($"{Indent.Couple}}}");

                }
                

            }

            for (int i = 0; i <= keyList.TuppleSpellings().Count; i++)
            {
                var param = i == 0 ? string.Empty : $"{keyList.TuppleSpellings()[i - 1]} key";
                contents.AppendLine($"{Indent.Couple}{limitKeyType} {IRepositoryName}.LowLimit({param})");
                contents.AppendLine($"{Indent.Couple}{{");
                contents.AppendLine($"{Indent.Triple}throw new NotImplementedException();");
                contents.AppendLine($"{Indent.Couple}}}");
            }

            for (int i = 0; i <= keyList.TuppleSpellings().Count; i++)
            {
                var param = i == 0 ? string.Empty : $"{keyList.TuppleSpellings()[i - 1]} key";
                contents.AppendLine($"{Indent.Couple}{limitKeyType} {IRepositoryName}.HighLimit({param})");
                contents.AppendLine($"{Indent.Couple}{{");
                contents.AppendLine($"{Indent.Triple}throw new NotImplementedException();");
                contents.AppendLine($"{Indent.Couple}}}");
            }

            contents.AppendLine($"{Indent.Single}}}");
            contents.AppendLine("}");
            return contents.ToString();

        }
        
        public static string CreateDbContextRepositoryContents(bool isLF,string entityName, ObjectID objectID, ScalarVariableList keyList)
        {

            var FileName = objectID.Name.ToCSharpOperand();
            var RepositoryName = $"{FileName}DbContextRepository";
            var IRepositoryName = $"I{FileName}Repository";

            var contents = new StringBuilder();

            contents.AppendLine(NamespaceItemFactory.DeltaEntities.ToUsingLine);

            contents.AppendLine(NamespaceItemFactory.MicrosoftEntityFrameworkCore.ToUsingLine);

            contents.AppendLine(NamespaceItemFactory.System.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemCollectionsGeneric.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemLinq.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemLinqExpressions.ToUsingLine);

            contents.AppendLine();
            contents.AppendLine(NamespaceItemFactory.DeltaOf(objectID.Library, entityName).ToNamespaceLine);
            contents.AppendLine("{");
            contents.AppendLine($"{Indent.Single}public class {RepositoryName} : {IRepositoryName}");
            contents.AppendLine($"{Indent.Single}{{");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}readonly DbContext dbContext;");
            contents.AppendLine($"{Indent.Couple}public {RepositoryName}(DbContext dbContext)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}this.dbContext = dbContext;");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.ExecuteSqlRaw(string rawSqlString, params object[] parameters)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}return dbContext.Database.ExecuteSqlRaw(rawSqlString, parameters);");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}IQueryable<{entityName}> {IRepositoryName}.FindAll()");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}return dbContext.Set<{entityName}>().AsNoTracking();");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            string FindAllMethodName =  "FindAll";
            if (isLF)
            {
                FindAllMethodName = "FindLFAll" ;
                contents.AppendLine($"{Indent.Couple}IQueryable<{entityName}> {IRepositoryName}.{FindAllMethodName}()");
                contents.AppendLine($"{Indent.Couple}{{");
                contents.AppendLine($"{Indent.Triple}return (({IRepositoryName})this).FindAll();//TODO: .Where(item => item.Custkb == string.Empty && (item.Claikb == \"1\" || item.Claikb == string.Empty));");
                contents.AppendLine($"{Indent.Couple}}}");
                contents.AppendLine($"{Indent.Couple}");
            }

            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Count()");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}return (({IRepositoryName})this).{FindAllMethodName}().Count();");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine($"{Indent.Couple}");

            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Count(Expression<Func<{entityName}, bool>> predicate)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}return (({IRepositoryName})this).{FindAllMethodName}().Where(predicate).Count();");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine($"{Indent.Couple}");

            contents.AppendLine($"{Indent.Couple}void {IRepositoryName}.Truncate(string objectName)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}(({IRepositoryName})this).ExecuteSqlRaw($\"TRUNCATE TABLE {{objectName}}\");");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine($"{Indent.Couple}");

            var p = keyList.ScalarVariables.Select(s => $"{s.TypeSpelling} {s.CamelCaseName}").Aggregate((all, cur) => $"{all}, {cur}");
            var cond = keyList.ScalarVariables.Select(s => $"item.{s.Name}=={s.CamelCaseName}").Aggregate((all, cur) => $"{all} && {cur}");

            contents.AppendLine($"{Indent.Couple}{entityName} {IRepositoryName}.Find({p})");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}return (({IRepositoryName})this).{FindAllMethodName}().Where(item=>{cond}).FirstOrDefault();");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine($"{Indent.Couple}");
            if (!isLF)
            {
                contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Delete({p})");
                contents.AppendLine($"{Indent.Couple}{{");
                contents.AppendLine($"{Indent.Triple}return (({IRepositoryName})this).DeleteWhere(item=>{cond});");
                contents.AppendLine($"{Indent.Couple}}}");
            }


            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Insert({entityName} entity)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}if(entity is ISortaleBy4s sortaleBy4S) sortaleBy4S.Update4sValues();");
            contents.AppendLine($"{Indent.Triple}dbContext.Add(entity);");
            contents.AppendLine($"{Indent.Triple}return dbContext.SaveChanges();");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Update({entityName} entity)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}if(entity is ISortaleBy4s sortaleBy4S) sortaleBy4S.Update4sValues();");
            contents.AppendLine($"{Indent.Triple}dbContext.Update(entity);");
            contents.AppendLine($"{Indent.Triple}return dbContext.SaveChanges();");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.Delete({entityName} entity)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}dbContext.Remove(entity);");
            contents.AppendLine($"{Indent.Triple}return dbContext.SaveChanges();");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            contents.AppendLine($"{Indent.Couple}int {IRepositoryName}.DeleteWhere(Expression<Func<{entityName}, bool>> predicate)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}var dbSet = dbContext.Set<{entityName}>();");
            contents.AppendLine($"{Indent.Triple}dbContext.Set<{entityName}>().RemoveRange(dbSet.Where(predicate));");
            contents.AppendLine($"{Indent.Triple}return dbContext.SaveChanges();");
            contents.AppendLine($"{Indent.Couple}}}");
            contents.AppendLine();

            var limitKeyType = keyList.TuppleSpellings().Last();

            for (int i = 0; i <= keyList.TuppleSpellings().Count; i++)
            {
                var param = i == 0 ? string.Empty : $"{keyList.TuppleSpellings()[i - 1]} key";

                var lowLimitSpels = keyList.LowLimitSpels();

                contents.AppendLine($"{Indent.Couple}{limitKeyType} {IRepositoryName}.LowLimit({param})");
                contents.AppendLine($"{Indent.Couple}{{");
                contents.AppendLine($"{Indent.Triple}return {lowLimitSpels[i]};");
                contents.AppendLine($"{Indent.Couple}}}");

            }
            for (int i = 0; i <= keyList.TuppleSpellings().Count; i++)
            {
                var param = i == 0 ? string.Empty : $"{keyList.TuppleSpellings()[i - 1]} key";
                var hiLimitSpels = keyList.HiLimitSpels();
                contents.AppendLine($"{Indent.Couple}{limitKeyType} {IRepositoryName}.HighLimit({param})");
                contents.AppendLine($"{Indent.Couple}{{");
                contents.AppendLine($"{Indent.Triple}return {hiLimitSpels[i]};");
                contents.AppendLine($"{Indent.Couple}}}");
            }

            contents.AppendLine($"{Indent.Single}}}");
            contents.AppendLine("}");
            return contents.ToString();

        }

    }
}
