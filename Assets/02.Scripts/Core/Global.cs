using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Text;
using System.Linq;

namespace Core
{
    //플레이어 데이터를 불러오거나 클라이언트 내에서 다룰때 클라 입장에서 들고 있는 데이터를 셋팅하는 클래스
    public class Global
    {
        private static Global m_instance = null;
        public static Global Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new Global();

                return m_instance;

            }
        }


        private const string jsonFilePath = "/UserData.json";
        public string NowScene = "";
        public string NextScene = "";
        public bool b_isLoading;
        public string StageNumber = "";
        public int Coin = 0;
        public Dictionary<string, int> ItemDict = new Dictionary<string, int>();
        public Dictionary<string, int> StageDict = new Dictionary<string, int>();
        //public string UserNickName = "";
        public int Star = 0;
        public int Stage = 0;
        public bool[] ItemUse = new bool[6] {false, false, false, false, false, false};


        public void LoadUserData()
        {
            Debug.Log(Application.persistentDataPath + jsonFilePath);
            if (File.Exists(Application.persistentDataPath + jsonFilePath))
            {
                string json = File.ReadAllText(Application.persistentDataPath + jsonFilePath);
                UnityEngine.Debug.Log(json);
                GlobalVar gv = JsonUtility.FromJson<GlobalVar>(json);
                Coin = gv.Coin;
                Star = gv.Star;
                SetStringToItemDict(gv.ItemDict);
                Stage = gv.Stage;
                SetStringToStageDict(gv.StageDict);

                UnityEngine.Debug.Log(ItemDict.Count);
            }
            Debug.Log("false");
        }

        public void SaveData()
        {
            GlobalVar gv = new GlobalVar();
            gv.Coin = Coin;
            gv.ItemDict = SetDictToString(ItemDict);
            gv.Star = Star;
            gv.Stage = Stage;
            gv.StageDict = SetDictToString(StageDict);
            Debug.Log(gv.ItemDict);
            File.WriteAllText(Application.persistentDataPath + jsonFilePath, JsonUtility.ToJson(gv));
            Debug.Log("File Save Done");
        }


        public void SetStringToItemDict(string str)
        {
            string[] firstIndex = str.Split("/");
            for(int i = 0; i < firstIndex.Length; i++)
            {
                //Debug.Log(firstIndex[i]);
                string[] secondIndex = firstIndex[i].Split(",");
                //Debug.Log($"{secondIndex[0]} : {secondIndex[1]}");
                ItemDict[secondIndex[0]] = int.Parse(secondIndex[1]);
            }

            foreach(KeyValuePair<string, int> kvp in ItemDict)
            {
                //Debug.Log($"{ kvp.Key} : {kvp.Value}");
            }
        }
        public void SetStringToStageDict(string str)
        {
            string[] firstIndex = str.Split("/");
            for (int i = 0; i < firstIndex.Length; i++)
            {
                //Debug.Log(firstIndex[i]);
                string[] secondIndex = firstIndex[i].Split(",");
                //Debug.Log($"{secondIndex[0]} : {secondIndex[1]}");
                StageDict.Add(secondIndex[0], int.Parse(secondIndex[1]));
            }

            foreach (KeyValuePair<string, int> kvp in ItemDict)
            {
                //Debug.Log($"{ kvp.Key} : {kvp.Value}");
            }
        }

        /*public string SetDictToString(Dictionary<ItemSO,int> dict)
        {
            string dictString = "";
            foreach(KeyValuePair<ItemSO,int> kvp in dict)
            {
                string a = string.Format($"{kvp.Key},{kvp.Value}/");
                dictString += a;
            }
            string RemoveText = "/";
            int index = dictString.IndexOf(RemoveText);
            dictString = dictString.Remove(dictString.Length-1, RemoveText.Length);
            Debug.Log(dictString);
            return dictString;
        }*/

        public string SetDictToString(Dictionary<string, int> dict)
        {
            string dictString = "";
            foreach (KeyValuePair<string, int> kvp in dict)
            {
                string a = string.Format($"{kvp.Key},{kvp.Value}/");
                dictString += a;
            }
            string RemoveText = "/";
            dictString = dictString.Remove(dictString.Length - 1, RemoveText.Length);
            Debug.Log(dictString);
            Debug.Log(dictString.Length);
            return dictString;
        }
    }



    [System.Serializable]
    public class GlobalVar
    {
        public int Coin;
        public string ItemDict;
        //public string UserNickName;
        public int Star;
        public int Stage;
        public string StageDict;
    }
}
