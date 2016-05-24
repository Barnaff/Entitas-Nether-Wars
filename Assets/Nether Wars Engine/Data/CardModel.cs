using System.Collections.Generic;
using NetherWars.Powers;

namespace NetherWars.Data
{
    [System.Serializable]
    public enum eCardType
    {
        Creature,
        Spell,
        Artifact,
    }

    [System.Serializable]
    public class CardModel
    {
        public string CardId;

        public string CardName;

        public string ManaCost;

        public int ConvertedManaCost;

        public eCardType CardType;

        public int Strength;

        public int Health;

        //public System.Collections.Generic.List<string> Powers;

        public string Description;

        public List<Power> Powers;
       
    }
}