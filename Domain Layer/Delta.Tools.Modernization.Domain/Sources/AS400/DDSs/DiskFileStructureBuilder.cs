using Delta.AS400.DataTypes;
using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs.DiskFiles;
using Delta.Tools.AS400.DDSs.DiskFiles.LFs;
using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using Delta.Tools.AS400.DDSs.FMTs;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Libraries;
using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Programs.CLs.Lines;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using Delta.Tools.AS400.TXTs;
using Delta.Tools.Modernization.Sources.AS400.Programs.CLs.Lines;
using Delta.Tools.Sources.Statements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs
{
    public class DiskFileStructureBuilder
    {
        public readonly ObjectIDFactory ObjectIDFactory;
        LibraryFactory LibraryFactory => ObjectIDFactory.LibraryFactory;

        StructureBuilder[] diskFileStructureBuilders;
        ReCreateTXTList ReCreateTXTList;
        StructureBuilder PFStructureBuilder;
        StructureBuilder DSPStructureBuilder;
        StructureBuilder FMTStructureBuilder;
        DiskFileStructureBuilder(ISourceFactory DSPSourceFileReader, ISourceFactory FMTSourceFileReader, ISourceFactory PFSourceFileReader, ISourceFactory LFSourceFileReader, 
            ISourceFactory TXTSourceFileReader, ReCreateTXTList ReCreateTXTList,
            ObjectIDFactory ObjectIDFactory)
        {
            this.ObjectIDFactory = ObjectIDFactory;
            this.ReCreateTXTList = ReCreateTXTList;
            PFStructureBuilder = new StructureBuilder(LibraryFactory, PFSourceFileReader, PFStructureFactory.Of());
            DSPStructureBuilder = new StructureBuilder(LibraryFactory, DSPSourceFileReader, DSPStructureFactory.Of());
            FMTStructureBuilder = new StructureBuilder(LibraryFactory, FMTSourceFileReader, FMTStructureFactory.Of());

            diskFileStructureBuilders = new StructureBuilder[] {
                PFStructureBuilder,
                new StructureBuilder(LibraryFactory,LFSourceFileReader, LFStructureFactory.Of(CreatePFStructure)),
                new StructureBuilder(LibraryFactory,TXTSourceFileReader, TXTStructureFactory.Of()),
                FMTStructureBuilder,
            };

        }
        public static DiskFileStructureBuilder Of(ObjectIDFactory ObjectIDFactory, SourceFactoryBuilder sourceFactoryBuilder, List<ObjectID> ReCreateFileObjectIDs)
        {
            var DSPSourceFileReader = sourceFactoryBuilder.DSPSourceFileReader();
            var FMTSourceFileReader = sourceFactoryBuilder.FMTSourceFileReader();

            var PFSourceFileReader = sourceFactoryBuilder.PFSourceFileReader();
            var LFSourceFileReader = sourceFactoryBuilder.LFSourceFileReader();
            var reCreateTXTList = new ReCreateTXTList(ReCreateFileObjectIDs);
            var TXTSourceFileReader = sourceFactoryBuilder.TXTSourceFileReader(reCreateTXTList);

            return new DiskFileStructureBuilder(DSPSourceFileReader, FMTSourceFileReader, PFSourceFileReader, LFSourceFileReader, TXTSourceFileReader, reCreateTXTList, ObjectIDFactory);

        }

        IStructure Create(Library library, string name)
        {
            var targetObjectID= ObjectIDFactory.Create(library, name);
            foreach (var factory in diskFileStructureBuilders)
            {
                var fileStructure = factory.Create(targetObjectID);
                if (!fileStructure.OriginalSource.IsNotFound) return fileStructure;
            }

            return NotFoundSourceStructure.Of(targetObjectID);

        }

        public IStructure Create(ObjectID objectID)
        {
            return Create(objectID.Library, objectID.Name);
        }

        public List<IStructure> CreateFilesOperateStatement(IStatement FilesOperateStatement)
        {
            if (FilesOperateStatement is ChkobjLine ChkobjLine) return new List<IStructure>() { Create(ChkobjLine) };

            if (FilesOperateStatement is ClrpfLine ClrpfLine) return new List<IStructure>() { Create(ClrpfLine) };

            if (FilesOperateStatement is ClrsavfLine ClrsavfLine) return new List<IStructure>() { Create(ClrsavfLine) };

            if (FilesOperateStatement is CpytoimpfLine cpytoimpfLine)
            {
                var fromToStructure = Create(cpytoimpfLine);
                return new List<IStructure>() { fromToStructure.FromFileStructure, fromToStructure.ToFileStructure };
            }
            if (FilesOperateStatement is CrtddmfLine CrtddmfLine)
            {
                var fromToStructure = Create(CrtddmfLine);
                return new List<IStructure>() { fromToStructure.FromFileStructure, fromToStructure.ToFileStructure };
            }
            if (FilesOperateStatement is CrtlfLine CrtlfLine)
            {
                var fromToStructure = Create(CrtlfLine);
                return new List<IStructure>() { fromToStructure.FromFileStructure, fromToStructure.ToFileStructure };
            }

            if (FilesOperateStatement is CrtpfLine CrtpfLine) return new List<IStructure>() { Create(CrtpfLine) };

            if (FilesOperateStatement is CrtsavfLine crtsavfLine) return new List<IStructure>() { Create(crtsavfLine) };

            if (FilesOperateStatement is CpyfLine CpyfLine)
            {
                var fromToStructure = Create(CpyfLine);
                return new List<IStructure>() { fromToStructure.FromFileStructure, fromToStructure.ToFileStructure };
            }

            if (FilesOperateStatement is DclfLine DclfLine) return new List<IStructure>() { Create(DclfLine) };

            if (FilesOperateStatement is DltfLine DltfLine) return new List<IStructure>() { Create(DltfLine) };

            if (FilesOperateStatement is FmtdtaLine FmtdtaLine)
            {
                var fromToStructure = Create(FmtdtaLine);
                var structres = new List<IStructure>(fromToStructure.InFileStructures);
                structres.Add(fromToStructure.OutFileStructure);
                structres.Add(fromToStructure.SrcStructure);
                return structres;
            }

            if (FilesOperateStatement is MvsrcLine MvsrcLine)
            {
                var fromToStructure = Create(MvsrcLine);
                return new List<IStructure>() { fromToStructure.FromFileStructure, fromToStructure.ToFileStructure };
            }

            if (FilesOperateStatement is OvrdbfLine OvrdbfLine)
            {
                var fromToStructure = Create(OvrdbfLine);
                return new List<IStructure>() { fromToStructure.FromFileStructure, fromToStructure.ToFileStructure };
            }

            if (FilesOperateStatement is RgzpfmLine RgzpfmLine) return new List<IStructure>() { Create(RgzpfmLine) };

            if (FilesOperateStatement is RstobjLine RstobjLine) return new List<IStructure>() { Create(RstobjLine) };

            if (FilesOperateStatement is RtvmbrdLine RtvmbrdLine) return new List<IStructure>() { Create(RtvmbrdLine) };

            if (FilesOperateStatement is SavobjLine SavobjLine) return new List<IStructure>() { Create(SavobjLine) };


            return new List<IStructure>();
        }

        public IStructure Create(ChkobjLine ChkobjLine)
        {
            return CreateDiskAndTXT(ChkobjLine.FileObjectID);
        }

        public IStructure Create(ClrpfLine ClrpfLine)
        {
            return Create(ClrpfLine.FileObjectID);
        }

        public IStructure Create(ClrsavfLine aClrsavfLine)
        {
            return Create(aClrsavfLine.FileObjectID);
        }
        public (IStructure FromFileStructure, IStructure ToFileStructure) Create(CpytoimpfLine cpytoimpfLine)
        {
            return (Create(cpytoimpfLine.FromFileObjectID), Create(cpytoimpfLine.TargetFileObjectID));
        }
        public (IStructure FromFileStructure, IStructure ToFileStructure) Create(CrtddmfLine CrtddmfLine)
        {
            return (CreateTXT(CrtddmfLine.FromFileObjectID), Create(CrtddmfLine.ToFileObjectID));
        }
        public (IStructure FromFileStructure, IStructure ToFileStructure) Create(CrtlfLine CrtlfLine)
        {
            return (Create(CrtlfLine.FromFileObjectID), Create(CrtlfLine.ToFileObjectID));
        }
        public (IStructure FromFileStructure, IStructure ToFileStructure) Create(CpyfLine CpyfLine)
        {
            return (Create(CpyfLine.FromFileObjectID), CreateTXT(CpyfLine.ToFileObjectID));
        }
        public IStructure Create(CrtpfLine CrtpfLine)
        {
            return Create(CrtpfLine.FileObjectID);
        }
        public IStructure Create(CrtsavfLine CrtsavfLine)
        {
            return Create(CrtsavfLine.FileObjectID);
        }
        public IStructure Create(DclfLine DclfLine)
        {
            return DSPStructureBuilder.Create(DclfLine.FileObjectID);
        }
        public IStructure Create(DltfLine DltfLine)
        {
            return Create(DltfLine.FileObjectID);
        }

        public (List<IStructure> InFileStructures, IStructure OutFileStructure, IStructure SrcStructure) Create(FmtdtaLine FmtdtaLine)
        {

            var InFiles = new List<IStructure>();
            foreach (var InFileObjectID in FmtdtaLine.InFileObjectIDs)
            {
                InFiles.Add(Create(InFileObjectID));
            }
            var outfile = Create(FmtdtaLine.OutFileObjectID);

            var srcStrcture = FMTStructureBuilder.Create(FmtdtaLine.SrcObjectID);

            return (InFiles, outfile, srcStrcture);
        }

        public (IStructure FromFileStructure, IStructure ToFileStructure) Create(OvrdbfLine OvrdbfLine)
        {
            return (Create(OvrdbfLine.FromFileObjectID), Create(OvrdbfLine.ToFileObjectID));
        }

        public IStructure Create(RgzpfmLine RgzpfmLine)
        {
            return Create(RgzpfmLine.FileObjectID);
        }

        public (IStructure FromFileStructure, IStructure ToFileStructure) Create(MvsrcLine MvsrcLine)
        {
            return (Create(MvsrcLine.FromFileObjectID), Create(MvsrcLine.ToFileObjectID));
        }

        public IStructure Create(RstobjLine aRstobjLine)
        {
            return Create(aRstobjLine.SavfObjectID);
        }

        public IStructure Create(RtvmbrdLine RtvmbrdLine)
        {
            return Create(RtvmbrdLine.FileObjectID);
        }

        public IStructure Create(SavobjLine aSavobjLine)
        {
            return Create(aSavobjLine.SavfObjectID);
        }

        private IStructure CreateDiskAndTXT(ObjectID fileObjectID)
        {

            var file = Create(fileObjectID);
            if (file.OriginalSource.IsNotFound)
            {
                file = CreateTXT(fileObjectID);
            }
            return file;

        }
        private IStructure CreateTXT(ObjectID fileObjectID)
        {
            return ReCreateTXTList.ReCreate(fileObjectID);
        }

        public IDataTypeDefinition CreateTypeDefinition(IDDSLine r, ObjectID? RecordFormatRefObjectID)
        {

            if (r is RecordFormatRefFieldLine recordFormatRefFieldLine)
            {
                var RefObjectID = recordFormatRefFieldLine.RefObjectID ?? RecordFormatRefObjectID;
                var Reffld = recordFormatRefFieldLine.Reffld;
                var referDisk = CreatePFStructure(RefObjectID);
                var typeDefLine = CreateTypeDefinition(referDisk.RecordFormatFieldBy(Reffld), referDisk.RefObjectID);
                //var typeDefLine = referDisk.FindTypeDefOfFieldBy(Reffld);

                if (typeDefLine == null) throw new NotImplementedException();
                return DataTypeDefinition.Of(((IDDSLine)recordFormatRefFieldLine).Name, typeDefLine.Length, typeDefLine.InternalDataType, typeDefLine.DecimalPositions, typeDefLine.Summary);
            }
            else
            if (r is RecordFormatFieldLine)
            {
                return (IDataTypeDefinition)r;
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        public List<IDataTypeDefinition> TypeDefinitionList(ExternalFormatFileStructure pf)
        {
            var RefObjectID = pf.RefObjectID;
            if (pf.FileDifintion.IsFormat)
            {
#pragma warning disable CS8604 // Null 参照引数の可能性があります。
                var referDisk = CreatePFStructure(RefObjectID);
#pragma warning restore CS8604 // Null 参照引数の可能性があります。
                return referDisk.RecordFormatFields.Select(r=>r.ToTypeDefinition).ToList();
            }
            else
            {
                return TypeDefinitionList(pf.FileDifintion.RecordFormatHeaderLine.RecordFormatFields, RefObjectID);
            }
        }

        public List<IDataTypeDefinition> TypeDefinitionList(List<IDDSLine> RecordFormatFields, ObjectID? RecordFormatRefObjectID)
        {
            var ITypeDefinitions = new List<IDataTypeDefinition>();
            RecordFormatFields.Where(f => !f.IsCommentLine).ToList().ForEach(r =>
            {
                ITypeDefinitions.Add(CreateTypeDefinition(r, RecordFormatRefObjectID));
            });
            return ITypeDefinitions;
        }

        public PFStructure CreatePFStructure(ObjectID objectID)
        {
            return (PFStructure)PFStructureBuilder.Create(objectID);
        }
    }
}
