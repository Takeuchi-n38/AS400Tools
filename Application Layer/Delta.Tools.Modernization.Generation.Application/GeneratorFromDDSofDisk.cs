using Delta.AS400.DataTypes;
using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.Generator.DomainLayer;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.CSharp.Statements.Comments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.AS400.Libraries;
using Delta.Tools.AS400.DDSs.DiskFiles.LFs;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.Modernization;
using System.Reflection;
using Delta.Tools.Modernization.Generation;
using Delta.RelationalDatabases.Postgres;
using Delta.RelationalDatabases.Mssql;
using System.Diagnostics;

namespace Delta.Tools.AS400.Generator
{
    class GeneratorFromDDSofDisk
    {
        //static string Version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        Library mainLibrary => PathResolver.MainLibrary;
        PathResolverForTool PathResolver;
        DiskFileStructureBuilder DiskFileStructureBuilder;
        public GeneratorFromDDSofDisk(PathResolverForTool PathResolver, DiskFileStructureBuilder aDiskFileStructureBuilder)
        {
            this.PathResolver = PathResolver;
            this.DiskFileStructureBuilder = aDiskFileStructureBuilder;
        }

        public void Generate(ObjectID objectID, bool isForDB2)
        {
            var file = DiskFileStructureBuilder.Create(objectID);
            if (file is LFStructure lf)
            {
                GenerateForLF(lf, isForDB2);
            }
            else
            if (file is PFStructure pf)
            {
                GenerateForPF(pf, isForDB2);
            }
        }

        public void GenerateForPF(PFStructure pf, bool isForDB2)
        {
            var TypeDefinitions = DiskFileStructureBuilder.TypeDefinitionList(pf);

            var ScalarVariableListOfKey = ScalarVariableListFactory.OfKey(pf.KeysAndFields, TypeDefinitions);

            var KeyTuppleSpels = ScalarVariableListOfKey.TuppleSpellings();
            //var isForDB2 = LibrariesOfDB2foriRepository.Contains(pf.ObjectID.Library);

            var OriginalLineCommentLines = CommentFactory.OriginalLineCommentLines(pf.OriginalSource.OriginalLines);
            var originalLineCommentLines = OriginalLineCommentLines.Aggregate((all, cur) => $"{all}{Environment.NewLine}{cur}");
            if (isForDB2)
            {
                CreateDB2Entity(pf.ObjectID, TypeDefinitions, ScalarVariableListOfKey, originalLineCommentLines);
                CreateDB2IRepository(false,pf.ObjectID.Name.ToPublicModernName(), pf.ObjectID, ScalarVariableListOfKey, originalLineCommentLines);
                CreateDB2Repository(false,pf.ObjectID.Name.ToPublicModernName(), pf.ObjectID, TypeDefinitions, ScalarVariableListOfKey,originalLineCommentLines);
                CreateDB2EntityTestHelper(pf.ObjectID, pf.IsUnique, TypeDefinitions, ScalarVariableListOfKey, originalLineCommentLines);
            }
            else
            {
                CreateEntity(pf.ObjectID, TypeDefinitions, ScalarVariableListOfKey, originalLineCommentLines);
                CreateEntityExtension(pf.ObjectID, TypeDefinitions, ScalarVariableListOfKey, originalLineCommentLines);
                CreateIRepository(false, pf.ObjectID.Name.ToPublicModernName(), pf.ObjectID, ScalarVariableListOfKey, originalLineCommentLines);
                CreateDbContextRepository(false,pf.ObjectID.Name.ToPublicModernName(), pf.ObjectID, ScalarVariableListOfKey, originalLineCommentLines);
                CreatePostgresEntityTestHelper(pf.ObjectID, pf.IsUnique, TypeDefinitions, ScalarVariableListOfKey, originalLineCommentLines);
                //WritePostgresTableDDSSource(pf, TypeDefinitions, originalLineCommentLines);
                WriteMssqlTableDDSSource(pf, TypeDefinitions, originalLineCommentLines);
            }

        }

        public void GenerateForLF(LFStructure lf, bool isForDB2)
        {
            var TypeDefinitionsOfLF = DiskFileStructureBuilder.TypeDefinitionList(lf);

            var fields = VariableFactory.Of(TypeDefinitionsOfLF);
            var ScalarVariableListOfKey = ScalarVariableListFactory.OfKey(lf.KeysAndFields, TypeDefinitionsOfLF);

            var OriginalLineCommentLines = CommentFactory.OriginalLineCommentLines(lf.OriginalSource.OriginalLines);
            var originalLineCommentLines = OriginalLineCommentLines.Aggregate((all, cur) => $"{all}{Environment.NewLine}{cur}");

            if (isForDB2)
            {
                CreateDB2IRepository(true,lf.FileDifintion.ObjectID.Name.ToPublicModernName(), lf.ObjectID, ScalarVariableListOfKey, originalLineCommentLines);
                CreateDB2Repository(true,lf.FileDifintion.ObjectID.Name.ToPublicModernName(), lf.ObjectID, TypeDefinitionsOfLF, ScalarVariableListOfKey,originalLineCommentLines);
            }
            else
            {
                CreateIRepository(true,lf.FileDifintion.ObjectID.Name.ToPublicModernName(), lf.ObjectID, ScalarVariableListOfKey, originalLineCommentLines);
                var TypeDefinitionsOfPF = DiskFileStructureBuilder.TypeDefinitionList(lf);

                var ScalarVariableOfPF = ScalarVariableListFactory.OfKey(lf.FileDifintion.KeysAndFields, TypeDefinitionsOfPF).ScalarVariables;

                var keyListInLFonly = ScalarVariableListOfKey.ScalarVariables.Where(v => !ScalarVariableOfPF.Exists(p => p.Name == v.Name)).ToList();

                CreateLFEntityExtension(lf.ObjectID, lf.FileDifintion.ObjectID, keyListInLFonly, originalLineCommentLines);
                CreateDbContextRepository(true,lf.FileDifintion.ObjectID.Name.ToPublicModernName(), lf.ObjectID, ScalarVariableListOfKey, originalLineCommentLines);
            }
        }

        void CreateEntity(ObjectID objectID, IEnumerable<IDataTypeDefinition> ITypeDefinitions, ScalarVariableList keyList, string originalComments)
        {
            var contents = EntityGenerator.CreateContents(false, objectID, ITypeDefinitions, keyList);
            PathResolver.WriteEntitySource(objectID, contents, originalComments);
        }
        void CreateDB2Entity(ObjectID objectID, IEnumerable<IDataTypeDefinition> ITypeDefinitions, ScalarVariableList keyList, string originalComments)
        {
            var contents = EntityGenerator.CreateContents(true, objectID, ITypeDefinitions, keyList);
            PathResolver.WriteDB2EntitySource(objectID, contents, originalComments);
        }
        void CreateEntityExtension(ObjectID objectID, IEnumerable<IDataTypeDefinition> ITypeDefinitions, ScalarVariableList keyList, string originalComments)
        {
            var EntityName = objectID.Name.ToPublicModernName();
            string toFolderPath = Path.Combine(PathResolver.DomainFolderPath(objectID.Library), $"{EntityName}s");
            var contents = EntityGenerator.CreateExtensionContents(objectID, ITypeDefinitions, keyList);
            PathResolver.WriteAllText(toFolderPath, $"{EntityName}Extension.gen.cs", contents,originalComments);
        }

        void CreatePostgresEntityTestHelper(ObjectID objectID, bool isUnique, IEnumerable<IDataTypeDefinition> ITypeDefinitions, ScalarVariableList keyList, string originalComments)
        {
            var contents = EntityTestHelperGenerator.CreateContents(false, objectID, isUnique, ITypeDefinitions, keyList);
            PathResolver.WriteEntityTestHelper(objectID, contents,originalComments);
        }

        void CreateDB2EntityTestHelper(ObjectID objectID, bool isUnique, IEnumerable<IDataTypeDefinition> ITypeDefinitions, ScalarVariableList keyList, string originalComments)
        {
            var contents = EntityTestHelperGenerator.CreateContents(true, objectID, isUnique, ITypeDefinitions, keyList);
            PathResolver.WriteDB2EntityTestHelperSource(objectID, contents, originalComments);
        }

        void CreateIRepository(bool isLF,string entityName, ObjectID objectID, ScalarVariableList keyList, string originalComments)
        {
            var contents = RepoGenerator.CreateIRepositoryContents(false,isLF,entityName, objectID, keyList);
            PathResolver.WriteIRepositorySource(entityName,objectID, contents,originalComments);
        }
        void CreateDB2IRepository(bool isLF,string entityName, ObjectID objectID, ScalarVariableList keyList, string originalComments)
        {
            var contents = RepoGenerator.CreateIRepositoryContents(true, isLF, entityName, objectID, keyList);
            PathResolver.WriteDB2IRepositorySource(entityName, objectID, contents, originalComments);
        }

        void CreateDbContextRepository(bool isLF, string entityName, ObjectID objectID, ScalarVariableList keyList, string originalComments)
        {
            var contents = RepoGenerator.CreateDbContextRepositoryContents(isLF,entityName, objectID,keyList);
            PathResolver.WriteDbContextRepositorySource(entityName,objectID, contents, originalComments);
        }

        void CreateDB2Repository(bool isLF, string entityName, ObjectID objectID, List<IDataTypeDefinition> dataTypeDefinitions, ScalarVariableList keyList, string originalComments)
        {
            var contents = RepoGenerator.CreateDB2RepositoryContents(isLF, entityName, objectID, dataTypeDefinitions, keyList);
            PathResolver.WriteDB2RepositorySource(entityName, objectID, contents, originalComments);
        }

        void CreateLFEntityExtension(ObjectID objectIDOfLF, ObjectID objectIDOfPF, List<Variable> keyListInLFonly, string originalComments)
        {
            string toFolderPath = Path.Combine(PathResolver.DomainFolderPath(objectIDOfPF.Library), $"{objectIDOfPF.Name.ToPublicModernName()}s");
            var contents = EntityGenerator.CreateContentsForLF(objectIDOfPF, keyListInLFonly);
            PathResolver.WriteAllText(toFolderPath, $"{objectIDOfLF.Name.ToPublicModernName()}Extension.gen.cs", contents, originalComments);
        }

        void WritePostgresTableDDSSource(PFStructure pf, IEnumerable<IDataTypeDefinition> ITypeDefinitions, string originalComments)
        {

            ObjectID objectID = pf.ObjectID;

            var table = new PostgresTable(RelationalDatabases.Schema.Of(objectID.Library.Name.ToLower()), objectID.Name.ToLower(),pf.Summary);
            
            var idCol = PostgresColumn.Id;

            if (!pf.IsUnique)
            {
                table.AddColumn(idCol);
            }

            var KeyNames = pf.KeysAndFields.RecordFormatKeys.Select(k => k.KeyName.ToLower()).ToList();

            ITypeDefinitions.ToList().ForEach(t =>
            {
                var col= new PostgresColumn(t.Name.ToLower(), PostgresColumnDataTypeOf(t), t.Summary);
                table.AddColumn(col);
                if(KeyNames.Any(k=>k==t.Name.ToLower())) {
                    table.AddColumn(col.Of4s);
                }
            });

            if (pf.IsUnique)
            {
                table.KeyNames = KeyNames;
            }
            else
            {
                table.KeyNames = new List<string> { idCol.Name };
            }

            var contents = table.Ddl();
            PathResolver.WritePostgresTableDDSSource(objectID, contents, originalComments);

        }

        void WriteMssqlTableDDSSource(PFStructure pf, IEnumerable<IDataTypeDefinition> ITypeDefinitions, string originalComments)
        {

            ObjectID objectID = pf.ObjectID;

            var table = new MssqlTable(RelationalDatabases.Schema.Of(objectID.Library.Name.ToLower()), objectID.Name.ToLower(), pf.Summary);

            var idCol = MssqlColumn.Id;

            if (!pf.IsUnique)
            {
                table.AddColumn(idCol);
            }

            var KeyNames = pf.KeysAndFields.RecordFormatKeys.Select(k => k.KeyName.ToLower()).ToList();

            ITypeDefinitions.ToList().ForEach(t =>
            {
                var col = new MssqlColumn(t.Name.ToLower(), MssqlColumnDataTypeOf(t), t.Summary);
                table.AddColumn(col);
                if (KeyNames.Any(k => k == t.Name.ToLower()))
                {
                    table.AddColumn(col.Of4s);
                }
            });

            if (pf.IsUnique)
            {
                table.KeyNames = KeyNames;
            }
            else
            {
                table.KeyNames = new List<string> { idCol.Name };
            }

            var contents = table.Ddl();
            PathResolver.WriteMssqlTableDDSSource(objectID, contents, originalComments);

        }

        static PostgresColumnDataType PostgresColumnDataTypeOf(IDataTypeDefinition typeDefinition)
        {

            if (typeDefinition.IsExplicitCharacter) return PostgresColumnDataType.CharacterVarying(typeDefinition.LengthToInt);

            if (typeDefinition.IsBinary) throw new NotImplementedException();// return TypeOfVariable.OfByte(typeDefinition.Length);

            if (typeDefinition.IsUnknownDatetime) throw new NotImplementedException();//return TypeOfVariable.OfDateTime(typeDefinition.Length);

            if (typeDefinition.IsExplicitNumeric)
            {
                var length = typeDefinition.LengthToInt;//typeDefinition.IsPackedDecimal ? typeDefinition.LengthToInt * 2 - 1 : typeDefinition.LengthToInt
                return PostgresColumnDataType.OfNumeric(length, typeDefinition.DecimalPositionsToInt);
            }

            if (typeDefinition.InternalDataType == string.Empty)
            {
                string Length = typeDefinition.Length;

                if (typeDefinition.DecimalPositions == string.Empty)
                {
                    if (typeDefinition.Length == string.Empty)
                    {
                        throw new NotImplementedException();//return TypeOfVariable.OfUnknown;
                    }
                    else
                    {
                        return PostgresColumnDataType.CharacterVarying(typeDefinition.LengthToInt);
                    }
                }
                else
                {
                    return PostgresColumnDataType.OfNumeric(typeDefinition.LengthToInt, typeDefinition.DecimalPositionsToInt);
                }
            }
            throw new NotImplementedException();//return TypeOfVariable.OfUnknown;
        }
        static MssqlColumnDataType MssqlColumnDataTypeOf(IDataTypeDefinition typeDefinition)
        {

            if (typeDefinition.IsSingleByteCharacter) return MssqlColumnDataType.Bpchar(typeDefinition.LengthToInt);
            if (typeDefinition.IsDoubleByteCharacter) return MssqlColumnDataType.CharacterVarying(typeDefinition.LengthToInt);

            if (typeDefinition.IsBinary)
            {
                Debug.WriteLine($"binaryType {typeDefinition.Name}");
                return MssqlColumnDataType.Bpchar(typeDefinition.LengthToInt);
            }
            if (typeDefinition.IsUnknownDatetime) throw new NotImplementedException();//return TypeOfVariable.OfDateTime(typeDefinition.Length);

            if (typeDefinition.IsExplicitNumeric)
            {
                var length = typeDefinition.LengthToInt;//typeDefinition.IsPackedDecimal ? typeDefinition.LengthToInt * 2 - 1 : typeDefinition.LengthToInt
                return MssqlColumnDataType.OfNumeric(length, typeDefinition.DecimalPositionsToInt);
            }

            if (typeDefinition.InternalDataType == string.Empty)
            {
                string Length = typeDefinition.Length;

                if (typeDefinition.DecimalPositions == string.Empty)
                {
                    if (typeDefinition.Length == string.Empty)
                    {
                        throw new NotImplementedException();//return TypeOfVariable.OfUnknown;
                    }
                    else
                    {
                        return MssqlColumnDataType.CharacterVarying(typeDefinition.LengthToInt);
                    }
                }
                else
                {
                    return MssqlColumnDataType.OfNumeric(typeDefinition.LengthToInt, typeDefinition.DecimalPositionsToInt);
                }
            }
            throw new NotImplementedException();//return TypeOfVariable.OfUnknown;
        }
        /*
            P
            Packed decimal
            S
            Zoned decimal
            B
            Binary
            F
            Floating-point
            A
            Character
            H
            Hexadecimal
            L
            Date
            T
            Time
            Z
            Timestamp
            5
            Binary character
         */
        /*
            Entry keyboard shifts	Meaning	Data type permitted
            Blank	Default	 
            X	Alphabetic only	Character
            A	Alphanumeric shift	Character
            N	Numeric shift	Character or numeric
            S	Signed numeric	Numeric
            Y	Numeric only	Numeric
            W	Katakana (for Japan only)	Character
            I	Inhibit keyboard entry	Character or numeric
            D	Digits only	Character or numeric
            M	Numeric only character	Character
            Data type (see note)	 	 
            F	Floating point	Numeric
            L	Date	 
            T	Time	 
            Z	Timestamp
         */

    }
}

