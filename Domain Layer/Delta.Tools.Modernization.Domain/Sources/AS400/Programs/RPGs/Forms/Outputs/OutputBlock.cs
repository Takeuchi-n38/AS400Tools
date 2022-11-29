using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Outputs
{
    public class OutputBlock : IBlockStatement<IStatement>//, ICSharpGeneratable
    {

        public void Reform(IEnumerable<(string FileName, int LineLength)> InternalFileNameAndLineLengths)
        {

            for (var i = 0; i < Statements.Count; i++)
            {
                if(Statements[i] is RPGCommentLine) continue;

                var outputLine = (IRPGOutputLine)Statements[i];
                if (outputLine.IsLineNameLine)
                {

                    OutputRowBlock block = createOutputRowBlock(InternalFileNameAndLineLengths,outputLine);
                    Statements.RemoveAt(i);
                    while (i < Statements.Count)
                    {
                        if (Statements[i] is RPGCommentLine commentLine)
                        {
                            block.Add(commentLine);
                            Statements.RemoveAt(i);
                            if (i == Statements.Count)
                            {
                                Statements.Insert(i, block);
                                break;
                            }
                        }
                        else
                        {
                            var nextLine = (IRPGOutputLine)Statements[i];
                            if (nextLine.IsLineNameLine)
                            {
                                Statements.Insert(i, block);
                                break;
                            }
                            else
                            if (nextLine.IsLineItemLine || nextLine.IsStaticValueLine)
                            {
                                block.Add(nextLine);
                                Statements.RemoveAt(i);
                                if (i == Statements.Count)
                                {
                                    Statements.Insert(i, block);
                                    break;
                                }
                            }
                            else
                            {
                                //throw new InvalidOperationException();TODO D T
                                block.Add(nextLine);
                                Statements.RemoveAt(i);
                                if (i == Statements.Count)
                                {
                                    Statements.Insert(i, block);
                                    break;
                                }

                            }
                        }
                        
                    }

                }
                else
                {
                    if (outputLine.IsLineItemLine || outputLine.IsStaticValueLine)
                    {
                    }
                    else
                    {
                        //rpg3 cycle d
                        OutputRowBlock block = createOutputRowBlock(InternalFileNameAndLineLengths, outputLine);
                        Statements.RemoveAt(i);
                        while (i < Statements.Count)
                        {
                            if (Statements[i] is RPGCommentLine commentLine)
                            {
                                block.Add(commentLine);
                                Statements.RemoveAt(i);
                                if (i == Statements.Count)
                                {
                                    Statements.Insert(i, block);
                                    break;
                                }
                            }
                            else
                            {
                                var nextLine = (IRPGOutputLine)Statements[i];
                                if (nextLine.IsLineNameLine)
                                {
                                    Statements.Insert(i, block);
                                    break;
                                }
                                else
                                if (nextLine.IsLineItemLine || nextLine.IsStaticValueLine)
                                {
                                    block.Add(nextLine);
                                    Statements.RemoveAt(i);
                                    if (i == Statements.Count)
                                    {
                                        Statements.Insert(i, block);
                                        break;
                                    }
                                }
                                else
                                {
                                    //throw new InvalidOperationException();TODO D T
                                    block.Add(nextLine);
                                    Statements.RemoveAt(i);
                                    if (i == Statements.Count)
                                    {
                                        Statements.Insert(i, block);
                                        break;
                                    }

                                }
                            }

                           
                        }
                    }
                }
            }

            var fileName = string.Empty;
            for (var i = 0; i < Statements.Count; i++)
            {

                var rpgLine = Statements[i];

                if (!(rpgLine is OutputRowBlock)) continue;

                OutputRowsContainerBlock block = new OutputRowsContainerBlock((OutputRowBlock)rpgLine);
                if (((OutputRowBlock)rpgLine).IsFileNameLine) fileName = ((OutputRowBlock)rpgLine).FileName;

                block.FileName = fileName;

                Statements.RemoveAt(i);
                if (i == Statements.Count)
                {
                    Statements.Insert(i, block);
                    break;
                }
                while (i < Statements.Count)
                {
                    var nextLine = (OutputRowBlock)Statements[i];
                    if (nextLine.Name != ((OutputRowBlock)rpgLine).Name)
                    {
                        Statements.Insert(i, block);
                        break;
                    }
                    else
                    {
                        block.Add(nextLine);
                        Statements.RemoveAt(i);
                        if (i == Statements.Count)
                        {
                            Statements.Insert(i, block);
                            break;
                        }
                    }
                }


            }
        }

        OutputRowBlock createOutputRowBlock(IEnumerable<(string FileName, int LineLength)> InternalFileNameAndLineLengths,IRPGOutputLine outputLine)
        {
            var recordLength = string.Empty;
            if (InternalFileNameAndLineLengths.Any(i => i.FileName == outputLine.FileName))
            {
                recordLength = InternalFileNameAndLineLengths.Where(i => i.FileName == outputLine.FileName).First().LineLength.ToString();
            }
            return new OutputRowBlock(outputLine, recordLength);
        }

    }
}