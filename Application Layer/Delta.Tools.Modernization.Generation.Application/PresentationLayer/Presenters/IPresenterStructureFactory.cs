using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using Delta.Tools.CSharp.Statements.Comments;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using System;
using System.Linq;

namespace Delta.Tools.AS400.Generator.PresentationLayer.Presenters
{
    public class IPresenterStructureFactory 
    {

        internal static InterfaceStructure Create(PathResolver PathResolver, DisplayFileStructure dspf) 
        {
            var dspfObjectID = dspf.OriginalSource.ObjectID;

            var iPresenter = new InterfaceStructure(
                NamespaceItemFactory.DeltaOf(dspfObjectID), 
                $"I{dspfObjectID.Name.ToPublicModernName()}Presenter",
                ""
                );

            iPresenter.AddUsingNamespace(NamespaceItemFactory.System);

            dspf.RecordFormatHeaderList.ToList().ForEach(RecordFormatHeader =>
            {
                var formattedRecordName = RecordFormatHeader.PublicModernName;

                if (RecordFormatHeader is SubFileControlRecordFormatHeader)
                {
                    iPresenter.AddContentLine($"(bool notFound,{((SubFileControlRecordFormatHeader)RecordFormatHeader).SubFileRecordName} value) Chain(int recordNumber);");
                }

                if (RecordFormatHeader is SubFileRecordFormatHeader)
                {
                    iPresenter.AddContentLine($"void Write({formattedRecordName} recordFormat, int recordNumber);");
                }
                else
                {
                    iPresenter.AddContentLine($"void Write({formattedRecordName} recordFormat);");
                }

                iPresenter.AddContentLine($"{formattedRecordName} Read{formattedRecordName}();");

            }
            );

            iPresenter.AddAppendLinesOfEndOfFile(CommentFactory.OriginalLineCommentLines(dspf.OriginalSource.OriginalLines));

            return iPresenter;
        }
    }
}
