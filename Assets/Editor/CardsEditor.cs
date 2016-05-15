using UnityEngine;
using System.Collections;
using UnityEditor;
using NetherWars.Data;

public class CardsEditor : EditorWindow{

    [SerializeField]
    private CardModel _selectedCard;

    private Vector2 _cardListScrollPosition = Vector2.zero;
    private Vector2 _cardEditScrollPosition = Vector2.zero;

    [MenuItem("Nether Wars/Cards Editor")]
    static void ShowWindow()
    {
        // Get existing open window or if none, make a new one:
        EditorWindow.GetWindow(typeof(CardsEditor));
    }

     
    void OnGUI()
    {

        EditorGUILayout.BeginHorizontal("Box");

        EditorGUILayout.LabelField("Cards Editor");

        if (GUILayout.Button("New Card", GUILayout.Width(150)))
        {
            _selectedCard = new CardModel();
        }

        if (_selectedCard == null)
        {
            GUI.enabled = false;
        }

        if (GUILayout.Button("Save Current Card", GUILayout.Width(150)))
        {
            CardsLoader.SaveCard(_selectedCard);
        }

        if (GUILayout.Button("Delete Current Card", GUILayout.Width(150)))
        {
            _selectedCard = null;
        }

        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();

        _cardListScrollPosition = EditorGUILayout.BeginScrollView(_cardListScrollPosition, "Box", GUILayout.Width(150));

        GUILayout.Button("Card");

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

            EditorGUILayout.BeginVertical("Box", GUILayout.Width(400));

            

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Powers");

            if (GUILayout.Button("+", GUILayout.Width(25)))
            {
                _selectedCard.Powers.Add("");
            }

            EditorGUILayout.EndHorizontal();

            for (int i=0; i< _selectedCard.Powers.Count; i++)
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

            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical("Box", GUILayout.Width(400));

            EditorGUILayout.LabelField("Description Text");

            _selectedCard.Description =  EditorGUILayout.TextArea(_selectedCard.Description, GUILayout.Height(50));

            EditorGUILayout.EndVertical();

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

}
