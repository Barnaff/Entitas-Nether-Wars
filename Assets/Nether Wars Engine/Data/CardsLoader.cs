using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace NetherWars.Data
{
    public class CardsLoader
    {
        public static string CARDS_RESOURCE_PATH = "/Resources/Cards/";
        public static string CARD_FILE_EXTENSION = "json";

        public static void SaveCard(CardModel card)
        {

            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto };

            string jsonString =  Newtonsoft.Json.JsonConvert.SerializeObject(card, settings);

           // string jsonString = JsonUtility.ToJson(card);

            Debug.Log(jsonString);

            string path = Application.dataPath + CARDS_RESOURCE_PATH + card.CardId + "." + CARD_FILE_EXTENSION;

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


        public static string[] GetAllCardsFilesPaths()
        {
            string[] filePaths = Directory.GetFiles(Application.dataPath + CARDS_RESOURCE_PATH, "*" + CARD_FILE_EXTENSION);
            return filePaths;
        }


        public static List<CardModel> LoadAllCards()
        {
            string[] cardsFilesPaths = GetAllCardsFilesPaths();
            List<CardModel> cards = new List<CardModel>();
            foreach (string cardFilePath in cardsFilesPaths)
            {
                
                CardModel card = CardsLoader.LoadCardFile(cardFilePath);
                if (card != null)
                {
                    cards.Add(card);
                }
            }
            return cards;
        }

        private static CardModel LoadCardFile(string path)
        {
            string filePath = Path.GetFileName(path).Replace("." + CARD_FILE_EXTENSION, "");

            TextAsset targetFile = Resources.Load<TextAsset>("Cards/" + filePath);
            if (targetFile != null)
            {
                Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto };

                CardModel card  = Newtonsoft.Json.JsonConvert.DeserializeObject<CardModel>(targetFile.text, settings);
                //CardModel card = JsonUtility.FromJson<CardModel>(targetFile.text);
                return card;
            }
            return null;
         
        }


    }
}
