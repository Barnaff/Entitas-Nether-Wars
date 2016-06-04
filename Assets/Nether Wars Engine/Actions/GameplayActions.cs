using Entitas;
using NetherWars.Powers;
using UnityEngine;
using System.Collections.Generic;
using NetherWars.Powers;
using System.Collections;

namespace NetherWars
{
    public class GameplayActions
    {

        public static void  RunTargetTest()
        {
            System.Array values = System.Enum.GetValues(typeof(eTargetType));
            for (int i=0; i< values.Length; i++)
            {
                for (int j=0; j < values.Length; j++)
                {
                    bool res = ((eTargetType)values.GetValue(i) & (eTargetType)values.GetValue(j)) != 0;
                }
                eTargetType b = (eTargetType)13;
                bool res2 = ((eTargetType)values.GetValue(i) & b) != 0;

                Debug.Log(" +++ checking : " + (eTargetType)values.GetValue(i) + " and " + b + " >>> " + res2);
            }
        }

        public static int GetHealth(Entity entity)
        {
            int health = 0;
            if (entity.hasHealth)
            {
                health = entity.health.Value;
            }

            if (entity.hasDamage)
            {
                health -= entity.damage.Value;
            }

            return health;
        }

        public static List<Entity> GeValidTargetsForAttacking(Entity attacker)
        {
            int controllerId = -1;
            if (attacker.hasController)
            {
                controllerId = attacker.controller.Id;
            }
            else if (attacker.hasPlayer)
            {
                controllerId = attacker.player.Id;
            }

            List<Entity> validTargets = new List<Entity>();
            List<Entity> valitTauntTargets = new List<Entity>();

            Entity[] entities = Pools.pool.GetGroup(Matcher.AnyOf(Matcher.Battlefield, Matcher.Player)).GetEntities();

            foreach (Entity entity in entities)
            {
                if (entity.hasCard && entity.controller.Id != controllerId)
                {
                    validTargets.Add(entity);
                    if (entity.isTaunt)
                    {
                        valitTauntTargets.Add(entity);
                    }
                }
                else if (entity.hasPlayer && entity.player.Id != controllerId)
                {
                    validTargets.Add(entity);
                }
            }

            if (valitTauntTargets.Count > 0)
            {
                return valitTauntTargets;
            }

            return validTargets;
        }

        public static void ExecuteEffect(EffectAbstract effect, Dictionary<string, object> pointers)
        {
            Logger.LogEvent("Execute Event : " + effect);
            if (effect is DrawCardEffect)
            {
                DrawCardEffect drawCardEffect = effect as DrawCardEffect;

                Entity player = GetVaribalValue<Entity>(drawCardEffect.TargetPlayer, pointers);

                int cardsToDraw = (int)GetVaribalValue<object>(drawCardEffect.CardsToDraw, pointers);

                if (cardsToDraw > 0)
                {
                    player.AddDraw(cardsToDraw);
                }
               
            }
            else if (effect is DealDamageEffect)
            {
                DealDamageEffect dealDamageEffect = effect as DealDamageEffect;

                Entity target = GetVaribalValue<Entity>(dealDamageEffect.Target, pointers);

                Entity attacker = null;
                if (pointers.ContainsKey("attacker"))
                {
                    attacker = pointers["attacker"] as Entity;
                }
               
                int damageAmount = (int)GetVaribalValue<object>(dealDamageEffect.DamageAmount, pointers);

                if (damageAmount > 0)
                {
                    if (target.hasDealDamage)
                    {
                        target.ReplaceDealDamage(damageAmount, false, attacker);
                    }
                    else
                    {
                        target.AddDealDamage(damageAmount, false, attacker);
                    }
                }
            }
        }

  
        private static T GetVaribalValue<T>(Variable varibal, Dictionary<string, object> pointers) where T : class
        {
            object value = null;
            if (varibal.Type == eVaribalType.Number)
            {
                return varibal.Value as T;
            }

            foreach (string pointerName in pointers.Keys)
            {
                if (pointerName == varibal.PointerTarget)
                {
                   
                    value = pointers[pointerName];
                    break;
                }
            }

            switch (varibal.Operation)
            {
                case eVaribalOperation.GetController:
                    {
                        if (value is Entity)
                        {
                            if ((value as Entity).hasCard)
                            {
                                int controllerId = (value as Entity).controller.Id;

                                Entity[] players = Pools.pool.GetGroup(Matcher.Player).GetEntities();
                                foreach (Entity entity in players)
                                {
                                    if (entity.player.Id == controllerId)
                                    {
                                        value = entity;
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    }
                case eVaribalOperation.GetHealth:
                    {
                        break;
                    }
                case eVaribalOperation.GetPower:
                    {
                        break;
                    }
                case eVaribalOperation.None:
                default:
                    {
                        break;
                    }
                    
            }

            return value as T;
        }

        public static bool MatchTarget(Target target, Entity entity, Entity matchedEntity)
        {
            eTargetType targetType = 0;
            if (entity == matchedEntity)
            {
                return CheckTargetFlags(target.ValidTargets, eTargetType.This);
            }


            int controllerId = -1;
            if (matchedEntity.hasPlayer)
            {
                controllerId = matchedEntity.player.Id;
            }
            else if (matchedEntity.hasController)
            {
                controllerId =  matchedEntity.controller.Id;
            }


            if (entity.hasPlayer)
            {
                // is player
                if (entity.player.Id == controllerId)
                {
                    return CheckTargetFlags(target.ValidTargets, eTargetType.ThisController);
                }
                else
                {
                    return CheckTargetFlags(target.ValidTargets, eTargetType.EnemyPlayer);
                }
            }
            else if (entity.hasController)
            {
                // is creature
                if (entity.controller.Id == controllerId)
                {
                    return CheckTargetFlags(target.ValidTargets, eTargetType.FriendlyCreature);
                }
                else
                {
                    return CheckTargetFlags(target.ValidTargets, eTargetType.EnemyCreature);
                }
            }

            return false;
        }

        private static bool CheckTargetFlags(eTargetType flags, eTargetType target)
        {
            Debug.Log("Match: flags: " + flags + " >>>> target: " + target);

            return (flags & target) != 0;
        }

        public static eZoneType GetCardZone(Entity card)
        {
            eZoneType cardZone = eZoneType.None;

            if (card.isBattlefield)
            {
                cardZone = eZoneType.Battlefield;
            }
            else if (card.isHand)
            {
                cardZone = eZoneType.Hand;
            }
            else if (card.isDeck)
            {
                cardZone = eZoneType.Deck;
            }
            else if (card.isGraveyard)
            {
                cardZone = eZoneType.Graveyard;
            }
            else if (card.isResource)
            {
                cardZone = eZoneType.ResourcesPool;
            }

            return cardZone;
        }

        public static void ChangeCardZone(Entity card, eZoneType newZone)
        {
            eZoneType previusZone = GetCardZone(card);

            switch (previusZone)
            {
                case eZoneType.Battlefield:
                    {
                        card.isBattlefield = false;
                        break;
                    }
                case eZoneType.Deck:
                    {
                        card.isDeck = false;
                        break;
                    }
                case eZoneType.Exile:
                    {
                        break;
                    }
                case eZoneType.Graveyard:
                    {
                        card.isGraveyard = false;
                        break;
                    }
                case eZoneType.Hand:
                    {
                        card.isHand = false;
                        break;
                    }
                case eZoneType.ResourcesPool:
                    {
                        card.isResource = false;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }


            if (card.hasPreviusZone)
            {
                card.ReplacePreviusZone(previusZone);
            }
            else
            {
                card.AddPreviusZone(previusZone);
            }

            switch (newZone)
            {
                case eZoneType.Battlefield:
                    {
                        card.isBattlefield = true;
                        break;
                    }
                case eZoneType.Deck:
                    {
                        card.isDeck = true;
                        break;
                    }
                case eZoneType.Exile:
                    {
                        break;
                    }
                case eZoneType.Graveyard:
                    {
                        card.isGraveyard = true;
                        break;
                    }
                case eZoneType.Hand:
                    {
                        card.isHand = true;
                        break;
                    }
                case eZoneType.ResourcesPool:
                    {
                        card.isResource = true;
                        break;
                    }
                case eZoneType.None:
                default:
                    {
                        Logger.LogError("Error - invalid zone to move card");
                        break;
                    }
            }

        }

    }
}