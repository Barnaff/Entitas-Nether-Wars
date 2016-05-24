

namespace NetherWars.Powers
{
    [System.Serializable]
    public abstract class EffectAbstract
    {
        public string EffectDisplayname = "";


    }


    public class EffectDrawCard : EffectAbstract
    {
        public int CardsToDraw;

        public Target ValidTarget;
    }

}
