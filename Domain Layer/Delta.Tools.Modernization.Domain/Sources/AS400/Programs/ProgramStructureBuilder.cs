using Delta.AS400.Objects;
using Delta.Tools.AS400.Analyzer.Programs.COBOLs;
using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Programs.CLs;
using Delta.Tools.AS400.Programs.RPGs;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using System;
using System.Diagnostics;

namespace Delta.Tools.AS400.Programs
{
    public class ProgramStructureBuilder
    {
        public readonly StructureBuilder CLStructureBuilder;
        public readonly StructureBuilder RPG3StructureBuilder;
        public readonly StructureBuilder RPG4StructureBuilder;
        public readonly StructureBuilder COBOLStructureBuilder;

        StructureBuilder[] StructureBuilders;
        ProgramStructureBuilder(ObjectIDFactory ObjectIDFactory, SourceFactoryBuilder sourceFactoryBuilder)
        {
            var LibraryFactory = ObjectIDFactory.LibraryFactory;

            CLStructureBuilder = new StructureBuilder(LibraryFactory, sourceFactoryBuilder.CLSourceFileReader(), CLStructureFactory.Of(ObjectIDFactory));
            RPG3StructureBuilder = new StructureBuilder(LibraryFactory, sourceFactoryBuilder.RPG3SourceFileReader(), RPGStructureFactory3.Of());
            RPG4StructureBuilder = new StructureBuilder(LibraryFactory, sourceFactoryBuilder.RPG4SourceFileReader(), RPGStructureFactory4.Of());
            COBOLStructureBuilder = new StructureBuilder(LibraryFactory, sourceFactoryBuilder.COBOLSourceFileReader(), COBOLStructureFactory.Of());

            var StructureFactories = new StructureBuilder[] {
                CLStructureBuilder,
                RPG3StructureBuilder,
                RPG4StructureBuilder,
                COBOLStructureBuilder,
                new StructureBuilder(LibraryFactory,sourceFactoryBuilder.ILECOBOLSourceFileReader(), COBOLStructureFactory.Of()),
                };
            this.StructureBuilders = StructureFactories;
        }

        public IStructure Create(ObjectID objectID)
        {
            try
            {
                foreach (var pgmLoader in StructureBuilders)
                {
                    var pgm = pgmLoader.Create(objectID);
                    if (pgm.OriginalSource.IsNotFound) continue;
                    return pgm;
                }

                return NotFoundSourceStructure.Of(objectID);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("error at " + string.Join('.', objectID.ToClassification()));
                throw;
            }

        }

        public static ProgramStructureBuilder Of(ObjectIDFactory ObjectIDFactory, SourceFactoryBuilder sourceFactoryBuilder)
        {
            return new ProgramStructureBuilder(ObjectIDFactory, sourceFactoryBuilder);
        }

    }
}
