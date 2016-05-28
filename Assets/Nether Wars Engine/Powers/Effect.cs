

namespace NetherWars.Powers
{
    [System.Serializable]
    public abstract class EffectAbstract
    {
        public string EffectDisplayname = "";


    }


    public enum eEffectType
    {
        None,
        DrawCards,
        DealDamage,
    }

    public class DrawCardEffect : EffectAbstract
    {
        public Variable CardsToDraw = new Variable("cardsToDraw", eVaribaleReturnType.Number);

        public Variable TargetPlayer = new Variable("targetPlayer", eVaribaleReturnType.Player); 
    }

    public class DealDamageEffect : EffectAbstract
    {
        public Variable DamageAmount = new Variable("damageAmount", eVaribaleReturnType.Number);

        public Variable TargetPlayer = new Variable("targetPlayer", eVaribaleReturnType.Target);

    }

}
