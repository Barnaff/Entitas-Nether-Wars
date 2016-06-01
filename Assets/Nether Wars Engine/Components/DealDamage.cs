using Entitas;

namespace NetherWars
{
    public class DealDamage : IComponent
    {
        public int Amount;

        public bool IsCombatDamage;

        public Entity Dealer;
    }
}

