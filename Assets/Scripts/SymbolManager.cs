using UnityEngine;
using System.Collections.Generic;

public static class SymbolManager
{
    public enum SymbolsEnum
    {
        leftTurn,
        rightTurn,
        straight,
        lowLeft,
        lowRight,
        hardLeft,
        hardRight,
        rightUTurn,
        leftUTurn,
        whiteStraight,
        whiteLeft,
        whiteUturnLeft,
    }

    private static Dictionary<SymbolsEnum, Sprite> symbolDictionary;

    static SymbolManager()
    {
        InitializeSymbolDictionary();
    }

    private static void InitializeSymbolDictionary()
    {
        symbolDictionary = new Dictionary<SymbolsEnum, Sprite>
        {
            { SymbolsEnum.leftTurn, Resources.Load<Sprite>("Symbols/leftTurn") },
            { SymbolsEnum.rightTurn, Resources.Load<Sprite>("Symbols/rightTurn") },
            { SymbolsEnum.straight, Resources.Load<Sprite>("Symbols/straight") },
            { SymbolsEnum.lowLeft, Resources.Load<Sprite>("Symbols/lowLeft") },
            { SymbolsEnum.lowRight, Resources.Load<Sprite>("Symbols/lowRight") },
            { SymbolsEnum.hardLeft, Resources.Load<Sprite>("Symbols/hardLeft") },
            { SymbolsEnum.hardRight, Resources.Load<Sprite>("Symbols/hardRight") },
            { SymbolsEnum.rightUTurn, Resources.Load<Sprite>("Symbols/rightUTurn") },
            { SymbolsEnum.leftUTurn, Resources.Load<Sprite>("Symbols/leftUTurn") },
            { SymbolsEnum.whiteStraight, Resources.Load<Sprite>("Symbols/whiteStraight") },
            { SymbolsEnum.whiteLeft, Resources.Load<Sprite>("Symbols/whiteLeft") },
            { SymbolsEnum.whiteUturnLeft, Resources.Load<Sprite>("Symbols/whiteUturnLeft") }
        };
    }

    public static Sprite GetSymbol(SymbolsEnum symbol)
    {
        if (symbolDictionary.TryGetValue(symbol, out Sprite result))
        {
            return result;
        }
        else
        {
            Debug.LogError($"Symbol not found for {symbol} in SymbolManager.");
            return null;
        }
    }
}