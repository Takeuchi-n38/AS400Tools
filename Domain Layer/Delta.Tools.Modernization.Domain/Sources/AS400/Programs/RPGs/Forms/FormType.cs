using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms
{
    public enum FormType
    {
        Control = 0,
        FileDescription = 1,
        L = 2,
        Extension = 3,
        Definition = 4,
        Input = 5,
        Calculation = 6,
        Output = 7,
        ProgramData = 8,
    }
    //RPG3 H F E L I   C O
    //RPG4 H F D,I D,I C O P
    public static partial class FormTypeExtend
    {
        //public static bool IsCaluculationLine(this FormType formType) => formType == FormType.Calculation;

        public static bool IsCaluculationLine(string rpgLine) => rpgLine.Length >= 6 && rpgLine.Substring(5, 1) == "C";

        static FormType? Of(string rpgLine)
        {
            if (rpgLine.StartsWith("** ")) return FormType.ProgramData;

            if (rpgLine.Length < 6) return FormType.ProgramData;

            //if (rpgLine.Length < 6) return FormType.ProgramData;

            var sixthChar = rpgLine.Substring(5, 1);

            switch (sixthChar)
            {
                case "H"://Control
                    return FormType.Control;
                case "F"://File Description
                    return FormType.FileDescription;
                case "L"://
                    return FormType.L;
                case "E"://Definition on RPG3
                    return FormType.Extension;
                case "D"://Definition
                    return FormType.Definition;
                case "I"://Input
                    return FormType.Input;
                case "C"://Calculation
                    return FormType.Calculation;
                case "O"://Output
                    return FormType.Output;
                default:
                    return null;//FormType.ProgramData;
            }

        }

        public static FormType OfNextLine(this FormType currentFormType, string[] OriginalLines,int nextLineIndex)
        {
            if(currentFormType==FormType.ProgramData) return FormType.ProgramData;

            FormType? nextFormType = Of(OriginalLines[nextLineIndex]);

            if (nextFormType.HasValue)
            {
                if(currentFormType == nextFormType.Value)
                {
                    return nextFormType.Value;
                }
                else
                if(nextFormType.Value == FormType.ProgramData)
                {
                    return FormType.ProgramData;
                }
            }

            //FormType? afterNextFormType = (nextLineIndex + 1) < OriginalLines.Length ? Of(OriginalLines[nextLineIndex + 1]):null;

            //if (afterNextFormType.HasValue && nextFormType.HasValue && (nextFormType.Value == afterNextFormType.Value)) return nextFormType.Value;

            for (var originalLinesIndex = nextLineIndex+1; originalLinesIndex < OriginalLines.Length; originalLinesIndex++)
            {
                var afterNextFormType = Of(OriginalLines[originalLinesIndex]);
                if (nextFormType.HasValue && afterNextFormType.HasValue)
                {
                    if(nextFormType.Value == afterNextFormType.Value)
                    {
                        return nextFormType.Value;
                    }
                    else
                    if (currentFormType == afterNextFormType.Value)
                    {
                        return currentFormType;
                    }
                    else
                    {
                        //if (nextRPGLine.StartsWith("** ") && afterNextFormType == FormType.ProgramData) return FormType.ProgramData;

                        if (currentFormType.CompareTo(nextFormType.Value) <= 0 && nextFormType.Value.CompareTo(afterNextFormType.Value) <= 0) return nextFormType.Value;

                        //if (afterNextFormType == FormType.ProgramData) return FormType.ProgramData;

                    }
                }
                
                nextFormType=afterNextFormType;

            }

            return currentFormType;

        }

    }

}
