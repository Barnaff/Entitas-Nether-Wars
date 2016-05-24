

namespace NetherWars.Powers
{
    [System.Serializable]
    public abstract class EffectAbstract
    {
        public string EffectDisplayname = "";


    }


    public class DrawCardEffect : EffectAbstract
    {
        public int CardsToDraw;

        public Target ValidTarget;
    }

    public class DealDamage : EffectAbstract
    {
        public int DamageAmount;

        public Target ValidTarget;

    }

}
