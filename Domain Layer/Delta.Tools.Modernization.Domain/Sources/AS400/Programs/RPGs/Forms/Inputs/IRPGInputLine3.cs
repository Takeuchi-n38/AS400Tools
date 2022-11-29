using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs
{
    public interface IRPGInputLine3: IRPGInputLine
    {

        string IRPGInputLine.FileName => Value.Substring(6, 8).TrimEnd();


        //フィールド名 44-47桁目	開始位置	フィールドの桁位置の開始位置
        string IRPGInputLine.FromLocation => Value.Substring(43, 4).TrimStart();

        //フィールド名 48-51桁目	終了位置	フィールドの桁位置の終了位置
        string IRPGInputLine.ToLocation => Value.Substring(47, 4).Trim();

        //フィールド名 52桁目	小数部桁数	数値フィールドの場合に小数部桁数を指定
        string IRPGInputLine.DecimalPositions => Value.Substring(51, 1).Trim();
        
        //フィールド名　53-58桁目	フィールド名	内部記述フィールド名
        string IRPGInputLine.FieldName => Value.Substring(52, 6).Trim();


    }
}


//ファイル名
//7 - 14桁目 ファイル名	
//15-16桁目	順序	AA、BBなど適当な文字
//19-20桁目	レコード識別標識	読取時にONになる標識番号
//無くてもOK
//フィールド名
//43桁目	P/B/L/R	パック10進数の場合にはPを入れる
//44-47桁目	開始位置	フィールドの桁位置の開始位置
//48-51桁目	終了位置	フィールドの桁位置の終了位置
//52桁目	小数部桁数	数値フィールドの場合に小数部桁数を指定
//53-58桁目	フィールド名	内部記述フィールド名
//65-70	結果標識	結果標識の受取

//TODO RPG3 //0016      I            DS                                                  
//TODO RPG3 //0017      I                                        1 230 TCARY             
//TODO RPG3 //0018      I                                        1 230 ARC             