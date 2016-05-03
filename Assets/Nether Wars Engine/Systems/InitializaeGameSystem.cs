using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class InitializaeGameSystem : ISetPool, IInitializeSystem
    {
        private Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            // create players and deal cards
            List<Entity> players = new List<Entity>();
            players.Add(CreatePlayer(1, "player 1", new string[10] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10" }));
            players.Add(CreatePlayer(2, "player 2", new string[10] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10" }));

            // pick the first player
            Random random = new Random();
            int randomIndex = random.Next(players.Count);
            Entity randomPlayer = players[randomIndex];
            randomPlayer.isActivePlayer = true;

            // set the turn phase
            _pool.CreateEntity().AddTurnPhase(TurnPhase.eTurnPhase.Upkeep).AddTurnCount(0);
        }


        private Entity CreatePlayer(int playerId, string playerName, string[] cardsInDeck)
        {
            Entity player = _pool.CreateEntity().AddPlayer(playerId, playerName).AddHealth(20, 20);
  
            // put all the player's cards in his deck and mark them as his.
            for (int i=0; i< cardsInDeck.Length; i++)
            {
                _pool.CreateEntity().AddCard(cardsInDeck[i]).AddController(playerId).IsDeck(true);
            }

            // make the player draw 5 cards
            player.AddDraw(5);

            return player;
        }
    }

}
