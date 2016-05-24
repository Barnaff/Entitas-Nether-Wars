using System.Collections;
using Entitas;

namespace NetherWars.Events
{
    public class EventsDelegates
    {
        public delegate void OnThisEnterBattlefield(Entity card);

        public delegate void OnCardEnterBattlefield(Entity card);

        public delegate void OnDamageDealt(Entity source, Entity target, int damage);

        public delegate void OnPlayerDraw(Entity player);

        public delegate void OnCastCard(Entity card);

        public delegate void OnPlayAsResource(Entity card);

        public delegate void OnCardMovedToGraveyard(Entity card);

        public delegate void OnUpkeepPhase();

        public delegate void OnControllerUpkeepPhase();

        public delegate void OnEndTurnPhase();

        public delegate void OnControllerEndTurnPhase();

    }

}
