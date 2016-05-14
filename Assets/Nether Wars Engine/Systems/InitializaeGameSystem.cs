﻿using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class InitializaeGameSystem : ISetPool, IInitializeSystem
    {
        private Pool _pool;

        Random _random;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            Logger.LogMessage("Initialize match");

            // generate random seed
            _random = new Random();

            // create players and deal cards
            List<Entity> players = new List<Entity>();
            players.Add(CreatePlayer(1, "player 1", new string[20] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10" , "01", "02", "03", "04", "05", "06", "07", "08", "09", "10" }));
            players.Add(CreatePlayer(2, "player 2", new string[20] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10" }));

            // pick the first player
            int randomIndex = _random.Next(players.Count);
            Entity randomPlayer = players[randomIndex];
            randomPlayer.isActivePlayer = true;

            Logger.LogAction("seelct starting player: " + randomPlayer.player.Id);

            // set the turn phase
            _pool.CreateEntity().AddTurnPhase(TurnPhase.eTurnPhase.Upkeep).AddTurnCount(0);

            Logger.LogAction("start the match");
        }


        private Entity CreatePlayer(int playerId, string playerName, string[] cardsInDeck)
        {
            Logger.LogMessage("Create Player " + playerId);

            Entity player = _pool.CreateEntity().AddPlayer(playerId, playerName).AddHealth(20);
  
            

            // put all the player's cards in his deck and mark them as his.
            for (int i=0; i< cardsInDeck.Length; i++)
            {

                _pool.CreateEntity()
                    .AddCard(cardsInDeck[i])
                    .AddController(playerId)
                    .IsDeck(true)
                    .AddManaCost(_random.Next(3) + 1)
                    .AddStrength(_random.Next(2) + 1)
                    .AddHealth(_random.Next(2) + 1);
            }

            // add mana pool to the player
            player.AddManaPool(0, 0);

            // make sure he can play a resource card
            player.isPlayedResource = false;

            // make the player draw 5 cards
            player.AddDraw(5);

            return player;
        }
    }

}
