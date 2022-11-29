using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public interface IRPGDefinitionLine3 : IRPGDefinitionLine
    {

        //     E                    MSG     1   1 43      
        //Position 6 (Form Type)
        //６桁 仕様書コードＥを指定する。
        //２７～３２桁 配列名を指定する。

        string IRPGDefinitionLine.Name => Value.Substring(26, 6).TrimEnd();

        //３７～３９桁 配列の要素の数を指定する。
        string LengthOfArray => Value.Substring(36, 3).TrimStart();

        //４０～４２桁 要素の長さを指定する。
        string IRPGDefinitionLine.ToPosition_Length => Value.Substring(39, 3).TrimStart();

        //４５桁 ゾーン１０進数の場合小数点以下の桁数を指定する。
        string IRPGDefinitionLine.DecimalPositions => Value.Substring(44, 1).Trim();

        //３３～３５桁	1レコードあたりの項目数を指定する。
        //配列が入力または演算仕様でロードされる場合には，このフィールドはブランクのままにする。
        string IRPGDefinitionLine.Perrcd => LengthOfArray;//Value.Substring(32, 3);

        //５４桁～	注釈を指定する。


    }
}
