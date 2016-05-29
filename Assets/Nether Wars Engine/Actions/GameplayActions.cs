using Entitas;
using NetherWars.Powers;
using UnityEngine;

namespace NetherWars
{
    public class GameplayActions
    {

        public static bool MatchTarget(Target target, Entity entity, Entity matchedEntity)
        {
            eTargetType targetType = eTargetType.None;
            if (entity == matchedEntity)
            {
                return CheckTargetFlags(target.ValidTargets, eTargetType.This);
            }
            else if (entity.hasCard)
            {
                if (matchedEntity.hasCard)
                {
                    if (entity.controller.Id == matchedEntity.controller.Id)
                    {
                        return CheckTargetFlags(target.ValidTargets, eTargetType.FriendlyCreature);
                    }
                    else
                    {
                        return CheckTargetFlags(target.ValidTargets, eTargetType.EnemyCreature);
                    }
                }
                else if (matchedEntity.hasPlayer)
                {
                    if (entity.controller.Id == matchedEntity.player.Id)
                    {
                        return CheckTargetFlags(target.ValidTargets, eTargetType.ThisController);
                    }
                    else
                    {
                        return CheckTargetFlags(target.ValidTargets, eTargetType.EnemyPlayer);
                    }
                }
            }
            else if (entity.hasPlayer)
            {
                if (matchedEntity.hasCard)
                {
                    if (entity.player.Id == matchedEntity.controller.Id)
                    {
                        return CheckTargetFlags(target.ValidTargets, eTargetType.FriendlyCreature);
                    }
                    else
                    {
                        return CheckTargetFlags(target.ValidTargets, eTargetType.EnemyCreature);
                    }
                }
                else if (matchedEntity.hasPlayer)
                {
                    if (entity.player.Id == matchedEntity.player.Id)
                    {
                        return CheckTargetFlags(target.ValidTargets, eTargetType.ThisController);
                    }
                    else
                    {
                        return CheckTargetFlags(target.ValidTargets, eTargetType.EnemyPlayer);
                    }
                }
            }

            return false;
        }

        private static bool CheckTargetFlags(eTargetType flags, eTargetType target)
        {
            Debug.Log("Check target: " + flags + " , target:  " + target + " => " + (flags & target) + " === " + ((flags & target) == eTargetType.None));
            return (flags & target) == eTargetType.None;
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