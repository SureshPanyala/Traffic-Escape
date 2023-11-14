 
using UnityEngine;
using UnityEngine.UI;
 
[CreateAssetMenu(fileName ="SymbolContainer", menuName = "container")]
public class SymbolsContainer : ScriptableObject
{
    public Sprite leftTurn;
    public Sprite rightTurn;
    public Sprite straight;
    public Sprite lowLeft;
    public Sprite lowRight;
    public Sprite hardLeft;
    public Sprite hardRight;
    public Sprite rightUTurn;
    public Sprite leftUTurn;


    public Sprite[] symbols;
}
