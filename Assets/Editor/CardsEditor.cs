using UnityEngine;
using System.Collections;
using UnityEditor;
using NetherWars;
using NetherWars.Data;
using NetherWars.Powers;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

            EditorGUILayout.BeginVertical("Box", GUILayout.Width(600));

            EditorGUILayout.LabelField("Mana and Colors", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();

            _selectedCard.ManaCost = EditorGUILayout.IntField("Mana Cost", _selectedCard.ManaCost, GUILayout.Width(250));

            _selectedCard.ColorIdentity = (eColorType)EditorGUILayout.EnumMaskField("Colors", _selectedCard.ColorIdentity);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical("Box");

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Thrashold", EditorStyles.boldLabel);

            if (GUILayout.Button("+", GUILayout.Width(25)))
            {
                if (_selectedCard.Thrashold == null)
                {
                    _selectedCard.Thrashold = new Dictionary<eColorType, int>();
                }

                eColorType newColor = eColorType.Black;

                System.Array colorValues = System.Enum.GetValues(typeof(eColorType));
                for (int i = 0; i < colorValues.Length; i++)
                {
                    newColor = (eColorType)colorValues.GetValue(i);

                    if (!_selectedCard.Thrashold.ContainsKey(newColor))
                    {
                        break;
                    }
                }

                if (!_selectedCard.Thrashold.ContainsKey(newColor))
                {
                    _selectedCard.Thrashold.Add(newColor, 0);
                }            
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();

            if (_selectedCard.Thrashold != null)
            {
                List<eColorType> keys = new List<eColorType>();
                foreach (eColorType colorKey in _selectedCard.Thrashold.Keys)
                {
                    keys.Add(colorKey);
                }

                foreach (eColorType colorKey in keys)
                {
                    EditorGUILayout.BeginHorizontal("Box");
 
                    eColorType color = colorKey;
                    color = (eColorType)EditorGUILayout.EnumPopup("Color", color);

                    int amount = _selectedCard.Thrashold[colorKey];
                    amount = EditorGUILayout.IntField("Amount", amount);

                    _selectedCard.Thrashold[colorKey] = amount;

                    if (GUILayout.Button("X", GUILayout.Width(25)))
                    {
                        _selectedCard.Thrashold.Remove(colorKey);
                        EditorGUILayout.EndHorizontal();
                        break;
                    }

                    if (color != colorKey)
                    {
                        if (_selectedCard.Thrashold.ContainsKey(color))
                        {
                            _selectedCard.Thrashold[color] = _selectedCard.Thrashold[colorKey];
                        }
                        else
                        {
                            _selectedCard.Thrashold.Add(color, _selectedCard.Thrashold[colorKey]);
                        }
                        _selectedCard.Thrashold.Remove(colorKey);
                        EditorGUILayout.EndHorizontal();
                        break;
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();
            

         


            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginHorizontal("Box");

            _selectedCard.ResourceGeneration = EditorGUILayout.IntField("Resource Generation", _selectedCard.ResourceGeneration, GUILayout.Width(250));

            _selectedCard.ColorsGeneration = (eColorType)EditorGUILayout.EnumMaskField("Colors Generation", _selectedCard.ColorsGeneration);

            EditorGUILayout.EndHorizontal();


            EditorGUILayout.EndVertical();


           

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


            EditorGUILayout.BeginVertical("Box", GUILayout.Width(600));

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Key Words", EditorStyles.boldLabel);

            if (GUILayout.Button("+", GUILayout.Width(25)))
            {
                if (_selectedCard.Keywords == null)
                {
                    _selectedCard.Keywords = new List<eKeywardType>();
                }

                _selectedCard.Keywords.Add(eKeywardType.None);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (_selectedCard.Keywords != null)
            {
                for (int i = 0; i < _selectedCard.Keywords.Count; i++)
                {
                    if (i % 3 == 0)
                    {
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                    }

                    EditorGUILayout.BeginHorizontal("Box", GUILayout.Width(175));
                    _selectedCard.Keywords[i] = (eKeywardType)EditorGUILayout.EnumPopup(_selectedCard.Keywords[i]);
                    if (GUILayout.Button("X", GUILayout.Width(25)))
                    {
                        _selectedCard.Keywords.RemoveAt(i);
                        EditorGUILayout.EndHorizontal();

                        break;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
           

            EditorGUILayout.EndHorizontal();


            EditorGUILayout.EndVertical();


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
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(600));
        EditorGUILayout.LabelField("Powers");
        if (card.Powers != null)
        {
            if (GUILayout.Button("Add Power"))
            {
                card.Powers.Add(new Power());
            }
			for (int i=0; i< card.Powers.Count; i++)
			{
				Power power = card.Powers[i];

				bool delete = PowerEditor.DrawPowerPanel(power, _selectedCard);

                if (delete)
				{
					card.Powers.RemoveAt(i);
					EditorGUILayout.EndVertical();
					return;
				}
			}
        }
        else
        {
            card.Powers = new List<Power>();
        }
        EditorGUILayout.EndVertical();
    }






   

    
}
