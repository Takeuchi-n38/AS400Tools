using Delta.AS400.DataTypes.Characters;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.AS400.Generator.PresentationLayer.Presenters
{
    class RecordFormatXamlFactory
    {
        internal static XamlStructure Create(PathResolver PathResolver, DisplayFileStructure dspf, RecordFormatHeader RecordFormatHeader)
        {
            var dspfObjectID = dspf.OriginalSource.ObjectID;

            var XamlStructure = new XamlStructure(
                NamespaceItemFactory.DeltaOf(dspfObjectID),
                $"{RecordFormatHeader.PublicModernName}View",
                "",
                false
                );

            XamlStructure.DesignWidth = 1280;
            XamlStructure.DesignHeight = RecordFormatHeader.LineSpan * 32;

            XamlStructure.AddContentLine("<Grid>");

            XamlStructure.AddContentLine($"{Indent.Single}<Grid.ColumnDefinitions>");
            Enumerable.Range(0, 80).ToList().ForEach(i => XamlStructure.AddContentLine($"{Indent.Couple}<ColumnDefinition Width=\"*\"/>"));
            XamlStructure.AddContentLine($"{Indent.Single}</Grid.ColumnDefinitions>");

            XamlStructure.AddContentLine($"{Indent.Single}<Grid.RowDefinitions>");
            Enumerable.Range(0, RecordFormatHeader.LineSpan).ToList().ForEach(i => XamlStructure.AddContentLine($"{Indent.Couple}<RowDefinition Height=\"*\"/>"));
            XamlStructure.AddContentLine($"{Indent.Single}</Grid.RowDefinitions>");

            /*COLOR_WHT
                    <TextBlock Foreground="{StaticResource COLOR_YLW}" Grid.Row="0" Grid.Column="1" Grid.ColumnSpan="79" Text ="{Binding Pfkey.Value}"></TextBlock>
                    <TextBlock Foreground="{StaticResource COLOR_RED}" Grid.Row="2" Grid.Column="1" Grid.ColumnSpan="79" Text ="{Binding Errmsg.Value}"></TextBlock>
             */

            var firstLine = RecordFormatHeader.RecordFormatFields.FirstLine;
            RecordFormatHeader.RecordFormatFields.OutputFields.ToList().ForEach(f =>
            {
                
                var column = f.Position - 1;
                var row = f.Line - firstLine;
                var visibility = string.Empty;
                if (f is RecordFormatOutputField recordFormatOutputField && recordFormatOutputField.HasIndicator) visibility = $"Visibility=\"{{Binding In{recordFormatOutputField.IndicatorValueString}.Value, Converter={{StaticResource BooleanToVisibilityConverter}}}}\"";

                if (f is RecordFormatConstField recordFormatConstField)
                {
                    var val = recordFormatConstField.JoinedValue;
                    var columnSpan=string.Empty;
                    var text = string.Empty;
                    if (val.StartsWith('\'')&& val.EndsWith('\'')){
                        val = val.Replace("\'","");
                        var PaddedShiftInOut = PadShiftInOut(val);
                        columnSpan= PaddedShiftInOut.length.ToString();
                        text = PaddedShiftInOut.padded;
                    }
                    else
                    {
                        if (val.Equals("DATE"))
                        {
                            columnSpan = "8";
                            text = "{Binding Source={x:Static sys:DateTime.Now}, StringFormat=yy/MM/dd}";
                        }//mm:hh:ss}
                        else
                        if(val.Equals("TIME"))
                        {
                            columnSpan = "8";
                            text = "{Binding Source={x:Static sys:DateTime.Now}, StringFormat=mm:hh:ss}";
                        }
                        else
                        {
                            columnSpan = val.Length.ToString();
                            text = val;
                        }

                    }

                    var foreGround = $"COLOR_{((f.Color??string.Empty)==string.Empty?"WHT":f.Color)}";
                    
                    XamlStructure.AddContentLine($"{Indent.Single}<TextBlock Foreground=\"{{StaticResource {foreGround}}}\" Grid.Row=\"{row}\" Grid.Column=\"{column}\" Grid.ColumnSpan=\"{columnSpan}\" Text =\"{text}\" {visibility}/>");

                    //Visibility="{Binding In70.Value, Converter={StaticResource BooleanToVisibilityConverter}}"
                }
                else if (f is RecordFormatBindField)
                {
                    if (((RecordFormatBindField)f).IsReadOnly)
                    {
                        var td = ((RecordFormatBindField)f).TypeDefinition;
                        var val = td.Name.ToPublicModernName();
                        var columnSpan = td.Length;
                        var foreGround = "COLOR_WHT";
                        XamlStructure.AddContentLine($"{Indent.Single}<TextBlock Foreground=\"{{StaticResource {foreGround}}}\" Grid.Row=\"{row}\" Grid.Column=\"{column}\" Grid.ColumnSpan=\"{columnSpan}\" Text =\"{{Binding {val}.Value}}\" {visibility}/>");
                    }
                    else
                    {
                        var td = ((RecordFormatBindField)f).TypeDefinition;
                        var val = td.Name.ToPublicModernName();
                        var columnSpan = td.Length;
                        XamlStructure.AddContentLine($"{Indent.Single}<TextBox Foreground=\"{{StaticResource COLOR_GRN}}\" CaretBrush=\"{{StaticResource COLOR_GRN}}\" Background=\"Transparent\" Grid.Row=\"{row}\" Grid.Column=\"{column}\" Grid.ColumnSpan=\"{columnSpan}\" MaxLength=\"{columnSpan}\"  Width=\"{(int.Parse(columnSpan)*16)}\" Text =\"{{Binding {val}.Value,UpdateSourceTrigger=PropertyChanged}}\" {visibility}/>");
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }

            });

            if (RecordFormatHeader is SubFileControlRecordFormatHeader)
            {
                XamlStructure.AddContentLine($"{Indent.Single}<ListBox Grid.Column=\"0\" Grid.ColumnSpan=\"80\" Grid.Row=\"2\" Grid.RowSpan=\"17\"");//TODO:Grid.Row
                XamlStructure.AddContentLine($"{Indent.Single}Background=\"Black\" BorderBrush=\"Transparent\"");
                XamlStructure.AddContentLine($"{Indent.Single}ItemsSource=\"{{Binding SubFileItemViewModels}}\"  SelectedItem=\"{{Binding SelectedSubFileItemViewModel}}\"");
                XamlStructure.AddContentLine($"{Indent.Single}>");

                XamlStructure.AddContentLine($"{Indent.Couple}<ListBox.ItemTemplate>");
                XamlStructure.AddContentLine($"{Indent.Triple}<DataTemplate>");
                XamlStructure.AddContentLine($"{Indent.Quadruple}<local:{((SubFileControlRecordFormatHeader)RecordFormatHeader).SubFileRecordName}View DataContext=\"{{Binding}}\" Height=\"32px\"/>");
                XamlStructure.AddContentLine($"{Indent.Triple}</DataTemplate>");
                XamlStructure.AddContentLine($"{Indent.Couple}</ListBox.ItemTemplate>");

                XamlStructure.AddContentLine($"{Indent.Couple}<ListBox.Template>");
                XamlStructure.AddContentLine($"{Indent.Triple}<ControlTemplate TargetType=\"ListBox\">");
                XamlStructure.AddContentLine($"{Indent.Quadruple}<ScrollViewer Margin=\"0\" Focusable=\"false\" HorizontalScrollBarVisibility=\"Hidden\" VerticalScrollBarVisibility=\"Hidden\">");
                XamlStructure.AddContentLine($"{Indent.Quintuple}<StackPanel Margin=\"0\" IsItemsHost=\"True\" />");
                XamlStructure.AddContentLine($"{Indent.Quadruple}</ScrollViewer>");
                XamlStructure.AddContentLine($"{Indent.Triple}</ControlTemplate>");
                XamlStructure.AddContentLine($"{Indent.Couple}</ListBox.Template>");

                XamlStructure.AddContentLine($"{Indent.Couple}<ListBox.ItemContainerStyle>");
                XamlStructure.AddContentLine($"{Indent.Triple}<Style TargetType=\"{{x:Type ListBoxItem}}\">");
                XamlStructure.AddContentLine($"{Indent.Quadruple}<Setter Property=\"Template\">");

                XamlStructure.AddContentLine($"{Indent.Quintuple}<Setter.Value>");
                XamlStructure.AddContentLine($"{Indent.Sextuple}<ControlTemplate TargetType=\"{{x:Type ListBoxItem}}\">");
                XamlStructure.AddContentLine($"{Indent.Septuple}<ContentPresenter  HorizontalAlignment=\"Stretch\"/>");
                XamlStructure.AddContentLine($"{Indent.Sextuple}</ControlTemplate>");
                XamlStructure.AddContentLine($"{Indent.Quintuple}</Setter.Value>");

                XamlStructure.AddContentLine($"{Indent.Quadruple}</Setter>");

                XamlStructure.AddContentLine($"{Indent.Quadruple}<!--<Setter Property=\"wrkstn:AutoSelectWhenAnyChildGetsFocus.Enabled\" Value=\"True\"></Setter>-->");

                XamlStructure.AddContentLine($"{Indent.Triple}</Style>");
                XamlStructure.AddContentLine($"{Indent.Couple}</ListBox.ItemContainerStyle>");

                XamlStructure.AddContentLine($"{Indent.Single}</ListBox>");

            }

            XamlStructure.AddContentLine("</Grid>");

            return XamlStructure;
        }

        public static (string padded, int length) PadShiftInOut(string utf16Strings)
        {
            if (CodePage290.Encodable(utf16Strings)) return (utf16Strings, utf16Strings.Length);

            int lastByteLength = 1;
            int curByteLength = 1;
            var paddedStrings = new StringBuilder();
            int sum = 0;
            foreach (char c in utf16Strings.ToCharArray())
            {

                curByteLength = CodePage290.TryParse(c, out byte cp290byte) ? 1 : 2;

                if (lastByteLength == 1 && curByteLength == 2)//shiftIn
                {
                    paddedStrings.Append(" ");
                    sum++;
                }

                //paddedStrings.Append(v.Character.ToString());
                paddedStrings.Append(c);

                sum += curByteLength;

                if (lastByteLength == 2 && curByteLength == 1)//shiftOut
                {
                    paddedStrings.Append(" ");
                    sum++;
                }

                lastByteLength = curByteLength;
            }
            return (paddedStrings.ToString(), sum);
        }

    }
}
/*
//0039      A          R PNLMSG
//0040      A                                      OVERLAY PROTECT
//0041      A            PFKEY         70   O 23  2DSPATR(HI)
//0042      A            ERRMSG        70   O 24  2COLOR(RED)
 */
