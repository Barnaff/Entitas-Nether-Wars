using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class DrawSystem : ISetPool,  IExecuteSystem
    {
        private Pool _pool;
        private Group _group;

        public void Execute()
        {
            if (_group != null)
            {
                foreach (Entity drawEntity in _group.GetEntities())
                {
                    PerformDraw(drawEntity);
                }
            }
        }

     
        public void SetPool(Pool pool)
        {
            _pool = pool;

            _group = _pool.GetGroup(Matcher.Draw);
        }


        private void PerformDraw(Entity drawEntity)
        {
            Player player = drawEntity.player;

            Entity[] cardsInDeck = _pool.GetEntities(Matcher.AllOf(Matcher.Deck, Matcher.Controller));

            List<Entity> playerCards = new List<Entity>();

            for (int i=0; i < cardsInDeck.Length; i++)
            {
                if (cardsInDeck[i].controller.Id == player.Id)
                {
                    playerCards.Add(cardsInDeck[i]);
                }
            }

            for (int i=0; i< drawEntity.draw.CardsToDraw; i++)
            {
                if (playerCards.Count > 0)
                {
                    playerCards[0].isDeck = false;
                    playerCards[0].isHand = true;
                    playerCards.RemoveAt(0);
                }
            }

            drawEntity.RemoveDraw();
        }
    }

}

