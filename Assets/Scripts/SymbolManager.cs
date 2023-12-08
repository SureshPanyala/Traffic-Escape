using UnityEngine;
using System.Collections.Generic;

public static class SymbolManager
{
    public enum SymbolsEnum
    {
        Right,
        Left,
        Straight,
        UturnLeft,
        UTurnRight
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
            { SymbolsEnum.Straight, Resources.Load<Sprite>("Symbols/Straight") },
            { SymbolsEnum.Left, Resources.Load<Sprite>("Symbols/Left") },
            { SymbolsEnum.UturnLeft, Resources.Load<Sprite>("Symbols/UturnLeft") },
            { SymbolsEnum.Right, Resources.Load<Sprite>("Symbols/Right") },
            { SymbolsEnum.UTurnRight, Resources.Load<Sprite>("Symbols/UTurnRight") }
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