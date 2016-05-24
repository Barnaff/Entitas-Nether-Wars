using System.Collections.Generic;

namespace NetherWars.Powers
{
    [System.Serializable]
    public class Power 
    {
        public List<TriggerAbstract> Triggers;

        public List<EffectAbstract> Effects;
    }

}
