using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class FileDescriptionLineFactory3 : FileDescriptionLineFactory
    {
        protected override int FileTypeStartIndex => 14;

        protected override int FileFormatStartIndex => 18;

        protected override int DeviceStartIndex => 39;

        protected override string RecordLength(string line)
        {
            if(line.Length<27) return string.Empty;
            return line.Substring(23,4).TrimStart();
        }

    }
}
/*
プログラム記述（内部記述）
7-14	ファイル名	任意
15	ファイルタイプ	I：入力ファイル O：出力ファイル U：更新ファイル
16	ファイル指定	F：全手順ファイル P：プライマリーフィル
19	ファイル形式	F：プログラム記述
24-27	レコードの長さ	1-9999
29-30	キーフィールドの長さ	1-99
31	レコードアドレスタイプ	P：パック10進数 A：文字
32	ファイル編成	I：プログラム記述
35-38	キーフィールドの開始位置	1-99
40-46	装置名	DISK
66	ファイルに追加	A

印刷ファイル（PRTF）
7-14	ファイル名	任意
15	ファイルタイプ	O：出力ファイル
19	ファイル形式	E：外部記述
F：プログラム記述
24-27	レコードの長さ	1-9999
34-35	オーバーフロー標識	OA-OG、OV、01-99
39	拡張コード	L（L仕様書を加える場合）
40-46	装置名	PRINTER
*/