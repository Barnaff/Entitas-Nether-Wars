using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using NetherWars.Powers;
using NetherWars.Data;
using System.Text.RegularExpressions;

public class PowerEditor
{
   
    public static bool DrawPowerPanel(Power power, CardModel card)
    {
        PowerEditor powerEditor = new PowerEditor();
        return powerEditor.DrawPowerPanelInternal(power, card); ;
    }


    private bool DrawPowerPanelInternal(Power power, CardModel card)
    {
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.BeginHorizontal();

        power.PowerDescription = EditorGUILayout.TextField("Power Description", power.PowerDescription);

        if (GUILayout.Button("X", GUILayout.Width(25)))
        {
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            return true;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Create Veribal"))
        {

        }

        if (GUILayout.Button("Add Trigger"))
        {
            if (power.Triggers == null)
            {
                power.Triggers = new List<TriggerAbstract>();
            }

            power.Triggers.Add(null);
        }



        EditorGUILayout.EndHorizontal();

        if (power.Triggers != null)
        {
            for (int i = 0; i < power.Triggers.Count; i++)
            {
                EditorGUILayout.BeginVertical("Box");
                TriggerAbstract trigger = power.Triggers[i];
                eTriggerType triggerType = eTriggerType.None;
                if (trigger == null)
                {
                    EditorGUILayout.BeginHorizontal();
                    triggerType = (eTriggerType)EditorGUILayout.EnumPopup("Trigger Type", triggerType);
                    switch (triggerType)
                    {
                        case eTriggerType.ChangedZone:
                            {
                                power.Triggers[i] = new ChangedZoneTrigger();
                                break;
                            }
                        case eTriggerType.DealDamage:
                            {
                                power.Triggers[i] = new DealDamageTrigger();
                                break;
                            }
                    }
                    if (power.Triggers[i] != null)
                    {
                        power.Triggers[i].Effects = new List<EffectAbstract>();

                        DrawCardEffect drawCardEffect = new DrawCardEffect();

                        power.Triggers[i].Effects.Add(drawCardEffect);
                    }


                    if (GUILayout.Button("X", GUILayout.Width(25)))
                    {
                        power.Triggers.RemoveAt(i);
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.EndVertical();
                        return false;
                    }

                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    EditorGUILayout.BeginHorizontal("Box");
                    EditorGUILayout.LabelField(CamelCaseString(trigger.GetType().Name), EditorStyles.boldLabel);
                    if (GUILayout.Button("X", GUILayout.Width(25)))
                    {
                        power.Triggers[i] = null;
                        triggerType = eTriggerType.None;
                        Debug.Log("delete trigger");

                    }
                    EditorGUILayout.EndHorizontal();
                    if (trigger != null)
                    {
                        DrawTrigger(trigger);


                        EditorGUILayout.BeginVertical("Box");

                        EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

                        if (GUILayout.Button("Add Effect"))
                        {
                            if (trigger.Effects == null)
                            {
                                trigger.Effects = new List<EffectAbstract>();
                            }
                            trigger.Effects.Add(null);
                        }

                        if (trigger.Effects != null)
                        {
                            bool deletedEffect = false;
                            for (int j = 0; j < trigger.Effects.Count; j++)
                            {
                                EffectAbstract effect = trigger.Effects[j];

                                if (effect == null)
                                {
                                    EditorGUILayout.BeginHorizontal();

                                    eEffectType effectType = eEffectType.None;
                                    effectType = (eEffectType)EditorGUILayout.EnumPopup("Effect Type", effectType);

                                    if (GUILayout.Button("X", GUILayout.Width(25)))
                                    {
                                        trigger.Effects.RemoveAt(j);
                                        break;
                                    }
                                    EditorGUILayout.EndHorizontal();

                                    switch (effectType)
                                    {
                                        case eEffectType.DrawCards:
                                            {
                                                trigger.Effects[j] = new DrawCardEffect();
                                                break;
                                            }
                                        case eEffectType.DealDamage:
                                            {
                                                trigger.Effects[j] = new DealDamageEffect();
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    deletedEffect = DrawEffect(effect, trigger);
                                }

                                if (deletedEffect)
                                {
                                    trigger.Effects[j] = null;
                                    EditorGUILayout.EndVertical();
                                    break;
                                }


                            }
                        }
                       

                        EditorGUILayout.EndVertical();

                        
                    }
                }

                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();
            }
        }

        EditorGUILayout.EndVertical();

        return false;
    }

    #region Triggers Inspectoes 

    private void DrawTrigger(TriggerAbstract trigger)
    {
        if (trigger is ChangedZoneTrigger)
        {
            DrawChangedZoneTrigger(trigger as ChangedZoneTrigger);

        }
        else if (trigger is DealDamageTrigger)
        {
            DrawDealDamageTrigger(trigger as DealDamageTrigger);
        }
    }


    private void DrawChangedZoneTrigger(ChangedZoneTrigger chnagedZoneTrigger)
    {
        EditorGUILayout.BeginHorizontal();
        chnagedZoneTrigger.FromZone = (eZoneType)EditorGUILayout.EnumPopup("From Zone", chnagedZoneTrigger.FromZone);
        chnagedZoneTrigger.ToZone = (eZoneType)EditorGUILayout.EnumPopup("To Zone", chnagedZoneTrigger.ToZone);
        EditorGUILayout.EndHorizontal();
        chnagedZoneTrigger.ValidTarget = DrawTargetField("Valid Target", chnagedZoneTrigger.ValidTarget);
    }

    private void DrawDealDamageTrigger(DealDamageTrigger dealDamageTrigger)
    {
        EditorGUILayout.BeginVertical();
        dealDamageTrigger.ValidAttacker = DrawTargetField("Valid Attacker", dealDamageTrigger.ValidAttacker);
        dealDamageTrigger.ValidTarget = DrawTargetField("Valid Target", dealDamageTrigger.ValidTarget);
        EditorGUILayout.BeginHorizontal();
        dealDamageTrigger.ValidZone = (eZoneType)EditorGUILayout.EnumPopup("Valid Zone", dealDamageTrigger.ValidZone);
        dealDamageTrigger.RequireCombatDamage = EditorGUILayout.Toggle("Require Combat Damage", dealDamageTrigger.RequireCombatDamage);
        dealDamageTrigger.MinRequireDamage = EditorGUILayout.IntField("Min Damage", dealDamageTrigger.MinRequireDamage);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    #endregion


    #region Effects Inspectors

    private bool DrawEffect(EffectAbstract effect, TriggerAbstract trigger)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(CamelCaseString(effect.GetType().Name), EditorStyles.boldLabel);

        if (GUILayout.Button("X", GUILayout.Width(25)))
        {
            EditorGUILayout.EndHorizontal();
            return true;
        }

        EditorGUILayout.EndHorizontal();

        if (effect is DrawCardEffect)
        {
            DrwaDrawEffect(effect as DrawCardEffect, trigger);
        }
        else if (effect is DealDamageEffect)
        {
            DrawDealDamageEffect(effect as DealDamageEffect, trigger);
        }

        return false;
    }

    private void DrwaDrawEffect(DrawCardEffect drawCardEffect, TriggerAbstract trigger)
    {
        EditorGUILayout.BeginVertical();

        List<string> avalablePointers = new List<string>();
        foreach (string key in trigger.Fields.Keys)
        {
            avalablePointers.Add(key);
        }

        drawCardEffect.CardsToDraw = DrawVaribalField(drawCardEffect.CardsToDraw, avalablePointers);

        drawCardEffect.TargetPlayer = DrawVaribalField(drawCardEffect.TargetPlayer, avalablePointers);

        EditorGUILayout.EndVertical();
    }

    private void DrawDealDamageEffect(DealDamageEffect dealDamageEffect, TriggerAbstract trigger)
    {
        EditorGUILayout.BeginVertical();

        List<string> avalablePointers = new List<string>();
        foreach (string key in trigger.Fields.Keys)
        {
            avalablePointers.Add(key);
        }

        dealDamageEffect.DamageAmount = DrawVaribalField(dealDamageEffect.DamageAmount, avalablePointers);

        dealDamageEffect.TargetPlayer = DrawVaribalField(dealDamageEffect.TargetPlayer, avalablePointers);

        EditorGUILayout.EndVertical();
    }

    #endregion

    private Target DrawTargetField(string title, Target target)
    {
        EditorGUILayout.BeginVertical("Box");

        if (target == null)
        {
            if (GUILayout.Button("Set Target"))
            {
                target = new Target();
            }
        }
        else
        {
            EditorGUILayout.BeginHorizontal();

            target.ValidTargets = (eTargetType)EditorGUILayout.EnumMaskField(title, target.ValidTargets);

            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                target = null;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        return target;
    }

    private Target DrawTargetField(Target target)
    {
        return DrawTargetField("", target);
    }

    private Variable DrawVaribalField(Variable varibal, List<string> avalablePointers = null)
    {
        EditorGUILayout.BeginHorizontal("Box");

        EditorGUILayout.LabelField(CamelCaseString(varibal.Name), EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Type: " + varibal.ReturnType.ToString(), EditorStyles.boldLabel);
       
        varibal.Type = (eVaribalType)EditorGUILayout.EnumPopup("Value Type", varibal.Type);
        EditorGUILayout.EndHorizontal();

        int selectedIndex = 0;

        switch (varibal.Type)
        {
            case eVaribalType.Number:
                {
                    varibal.Value = EditorGUILayout.IntField("Value", varibal.Value);
                    break;
                }
            case eVaribalType.Pointer:
                {
                    if (avalablePointers != null && avalablePointers.Count > 0)
                    {
                        selectedIndex = EditorGUILayout.Popup("Pointer Name", selectedIndex, avalablePointers.ToArray());

                        varibal.Operation = (eVaribalOperation)EditorGUILayout.EnumPopup("Pointer Operation", varibal.Operation);
                    }
                    else
                    {
                        EditorGUILayout.LabelField("No Avalable Pointers", EditorStyles.boldLabel);
                    }
                    break;
                }
            case eVaribalType.String:
                {
                    varibal.PointerTarget = EditorGUILayout.TextField("Value", varibal.PointerTarget);
                    break;
                }
            default:
                {
                   
                    break;
                }
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        return varibal;
    }

    private string CamelCaseString(string s)
    {
        return Regex.Replace(s, "(\\B[A-Z])", " $1");
    }
}

