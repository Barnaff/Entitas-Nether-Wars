using System;
using System.Collections.Generic;
using Entitas;
using NetherWars.Data;
using NetherWars.Powers;

namespace NetherWars
{
    public class InitializaeGameSystem : ISetPool, IInitializeSystem
    {
        private Pool _pool;

        Random _random;

        private Dictionary<string, CardModel> _cardsData;


        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            Logger.LogMessage("Initialize match");

            LoadCardsList();

            // generate random seed
            _random = new Random();

            // create players and deal cards
            List<Entity> players = new List<Entity>();
            players.Add(CreatePlayer(1, "player 1", new string[20] { "a01", "a02", "a03", "a04", "a05", "a01", "a02", "a03", "a04", "a05", "a01", "a02", "a03", "a04", "a05", "a01", "a02", "a03", "a04", "a05" }));
            players.Add(CreatePlayer(2, "player 2", new string[20] { "a01", "a02", "a03", "a04", "a05", "a01", "a02", "a03", "a04", "a05", "a01", "a02", "a03", "a04", "a05", "a01", "a02", "a03", "a04", "a05" }));

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
                CreateCard(cardsInDeck[i], playerId);
            }

            // add mana pool to the player
            player.AddManaPool(0, 0);

            // make sure he can play a resource card
            player.isPlayedResource = false;

            // make the player draw 5 cards
            player.AddDraw(5);

            return player;
        }


        private void LoadCardsList()
        {
            _cardsData = new Dictionary<string, CardModel>();
            List<CardModel> cardsList = CardsLoader.LoadAllCards();
            foreach (CardModel cardModel in cardsList)
            {
                _cardsData.Add(cardModel.CardId, cardModel);
            }
        }

        private Entity CreateCard(string cardId, int controllerId)
        {
            CardModel cardModel = _cardsData[cardId];

            Entity card = _pool.CreateEntity();

            card.AddCard(cardId, cardModel.CardName);

            card.AddController(controllerId);

            card.AddManaCost(cardModel.ConvertedManaCost);

            card.IsDeck(true);

            switch (cardModel.CardType)
            {
                case eCardType.Creature:
                    {
                        card.AddStrength(cardModel.Strength);
                        card.AddHealth(cardModel.Health);
                        break;
                    }
                case eCardType.Artifact:
                case eCardType.Spell:
                    {
                        break;
                    }
            }


			if (cardModel.Powers != null)
			{
				foreach (Power power in cardModel.Powers)
				{
					if (power.Triggers != null)
					{
						foreach (TriggerAbstract trigger in power.Triggers)
						{
							if (trigger is ChangedZoneTrigger)
							{
								card.AddChangedZoneTrigger(trigger as ChangedZoneTrigger);
							}
							else if (trigger is DealDamageTrigger)
							{
								card.AddDealDamageTrigger(trigger as DealDamageTrigger);
							}
						}
					}
				}
			}

            return card;
        }
    }

}
