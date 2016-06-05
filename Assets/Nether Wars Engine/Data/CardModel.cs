using System.Collections.Generic;
using NetherWars.Powers;

namespace NetherWars.Data
{


    [System.Serializable]
    public class CardModel
    {
        public string CardId;

        public string CardName;

        public eColorType ColorIdentity;

        public eColorType ColorsGeneration;

        public Dictionary<eColorType, int> Thrashold;

        public int ManaCost;

        public int ResourceGeneration;

        public eCardType CardType;

        public int Strength;

        public int Health;

        public string Description;

        public List<eKeywardType> Keywords;

        public List<Power> Powers;
       
    }
}