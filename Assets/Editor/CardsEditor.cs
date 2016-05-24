using UnityEngine;
using System.Collections;
using UnityEditor;
using NetherWars.Data;
using NetherWars.Powers;
using System.Collections.Generic;

public class CardsEditor : EditorWindow{

    [SerializeField]
    private CardModel _selectedCard;

    [SerializeField]
    private List<CardModel> _cardsList;

    private Vector2 _cardListScrollPosition = Vector2.zero;
    private Vector2 _cardEditScrollPosition = Vector2.zero;

    [MenuItem("Nether Wars/Cards Editor")]
    static void ShowWindow()
    {
        // Get existing open window or if none, make a new one:
        EditorWindow.GetWindow(typeof(CardsEditor));
    }


    void OnEnable()
    {
        ReloadCardsList();
    }

     
    void OnGUI()
    {

        EditorGUILayout.BeginHorizontal("Box");

        EditorGUILayout.LabelField("Cards Editor");

        if (GUILayout.Button("New Card", GUILayout.Width(150)))
        {
            _selectedCard = new CardModel();
            ReloadCardsList();
        }

        if (_selectedCard == null)
        {
            GUI.enabled = false;
        }

        if (GUILayout.Button("Save Current Card", GUILayout.Width(150)))
        {
            CardsLoader.SaveCard(_selectedCard);
            ReloadCardsList();
        }

        if (GUILayout.Button("Delete Current Card", GUILayout.Width(150)))
        {
            _selectedCard = null;
            ReloadCardsList();
        }

        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();

        _cardListScrollPosition = EditorGUILayout.BeginScrollView(_cardListScrollPosition, "Box", GUILayout.Width(150));

        if (_cardsList != null)
        {
            for (int i = 0; i < _cardsList.Count; i++)
            {
                if (GUILayout.Button(_cardsList[i].CardName + " [" +_cardsList[i].CardId + "]"))
                {
                    _selectedCard = _cardsList[i];
                }
            }
        }
  

        EditorGUILayout.EndScrollView();



        _cardEditScrollPosition = EditorGUILayout.BeginScrollView(_cardEditScrollPosition, "Box");

        if (_selectedCard != null)
        {
             _selectedCard.CardId = EditorGUILayout.TextField("Card ID", _selectedCard.CardId, GUILayout.Width(350));

            _selectedCard.CardName = EditorGUILayout.TextField("Card Name", _selectedCard.CardName, GUILayout.Width(350));

            _selectedCard.ManaCost = EditorGUILayout.TextField("Mana Cost", _selectedCard.ManaCost, GUILayout.Width(250));

            _selectedCard.ConvertedManaCost = EditorGUILayout.IntField("Converted Mana Cost", _selectedCard.ConvertedManaCost, GUILayout.Width(200));

            _selectedCard.CardType = (eCardType)EditorGUILayout.EnumPopup("Card Type", _selectedCard.CardType, GUILayout.Width(350));

            switch (_selectedCard.CardType)
            {
                case eCardType.Creature:
                    {
                        EditorGUILayout.BeginHorizontal("Box");

                        _selectedCard.Strength = EditorGUILayout.IntField("Strength", _selectedCard.Strength, GUILayout.Width(200));

                        _selectedCard.Health = EditorGUILayout.IntField("Health", _selectedCard.Health, GUILayout.Width(200));

                        EditorGUILayout.EndHorizontal();

                        break;
                    }
                case eCardType.Spell:
                case eCardType.Artifact:
                    {
                        break;
                    }
            }


            /*
            EditorGUILayout.BeginVertical("Box", GUILayout.Width(400));

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Powers");

            if (GUILayout.Button("+", GUILayout.Width(25)))
            {
                _selectedCard.Powers.Add("");
            }

            EditorGUILayout.EndHorizontal();

            if (_selectedCard.Powers != null)
            {
                for (int i = 0; i < _selectedCard.Powers.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    _selectedCard.Powers[i] = EditorGUILayout.TextArea(_selectedCard.Powers[i], GUILayout.Height(50));
                    if (GUILayout.Button("X", GUILayout.Width(25), GUILayout.Height(50)))
                    {
                        _selectedCard.Powers.RemoveAt(i);
                        EditorGUILayout.EndHorizontal();
                        break;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
           

            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical("Box", GUILayout.Width(400));

            EditorGUILayout.LabelField("Description Text");

            _selectedCard.Description =  EditorGUILayout.TextArea(_selectedCard.Description, GUILayout.Height(50));

            EditorGUILayout.EndVertical();
            */

            PowersListPanel(_selectedCard);

        }
        else
        {
            if (GUILayout.Button("Create New Card"))
            {
                _selectedCard = new CardModel();
            }
        }

        EditorGUILayout.EndScrollView();


        EditorGUILayout.EndHorizontal();

    }


    private void ReloadCardsList()
    {
        _cardsList = CardsLoader.LoadAllCards();
    }


    private void PowersListPanel(CardModel card)
    {
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(400));



        EditorGUILayout.LabelField("Powers");

        if (card.Powers != null)
        {
            if (GUILayout.Button("Add Power"))
            {
               

                card.Powers.Add(new Power());
            }

            foreach (Power power in card.Powers)
            {
                PowerPanel(power);
            }
        }
        else
        {
            card.Powers = new List<Power>();

        }
       

       

        EditorGUILayout.EndVertical();
    }


    private void PowerPanel(Power power)
    {

        GUILayout.BeginVertical("Box");

        GUILayout.BeginHorizontal("Box");

        if (GUILayout.Button("Create Veribal"))
        {

        }

        if (GUILayout.Button("Add Trigger"))
        {
            if (power.Triggers == null)
            {
                power.Triggers = new List<TriggerAbstract>();
            }

            ChangedZoneTrigger t = new ChangedZoneTrigger();
            t.FromZone = eZoneType.Hand;
            t.ToZone = eZoneType.Battlefield;

            power.Triggers.Add(t);
        }

        if (GUILayout.Button("Add Effect"))
        {
            if (power.Effects == null)
            {
                power.Effects = new List<EffectAbstract>();
            }

            EffectDrawCard e = new EffectDrawCard();
            e.CardsToDraw = 2;
            power.Effects.Add(e);
        }

       

        GUILayout.EndHorizontal();


        GUILayout.EndVertical();

    }
}
