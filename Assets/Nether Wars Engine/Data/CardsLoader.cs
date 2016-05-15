using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace NetherWars.Data
{
    public class CardsLoader
    {
        public static string CARDS_RESOURCE_PATH = "Assets/Resources/Cards/";


        public static void SaveCard(CardModel card)
        {
            string jsonString = JsonUtility.ToJson(card);

            Debug.Log(jsonString);

            string path = CARDS_RESOURCE_PATH + card.CardId + ".json";

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.Write(jsonString);
                }
            }
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }


        public List<CardModel> LoadAllCards()
        {


            return null;
        }

        private CardModel LoadCardFile(string path)
        {
            return null;
        }


    }
}
