using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

namespace Core
{
    //�÷��̾� �����͸� �ҷ����ų� Ŭ���̾�Ʈ ������ �ٷ궧 Ŭ�� ���忡�� ��� �ִ� �����͸� �����ϴ� Ŭ����
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
        public Dictionary<ItemSO, int> ItemDict = new Dictionary<ItemSO, int>();
        //public string UserNickName = "";
        public int Star = 0;
        public int Stage = 0;


        public void LoadUserData()
        {
            if (File.Exists(Application.persistentDataPath + jsonFilePath))
            {
                string json = File.ReadAllText(Application.persistentDataPath + jsonFilePath);
                GlobalVar gv = JsonUtility.FromJson<GlobalVar>(json);
                Coin = gv.Coin;
                Star = gv.Star;
                ItemDict = gv.ItemDict;
                Stage = gv.Stage;
            }
        }

        public void SaveData()
        {
            GlobalVar gv = new GlobalVar();
            gv.Coin = Coin;
            gv.ItemDict = ItemDict;
            gv.Star = Star;
            gv.Stage = Stage;
            File.WriteAllText(Application.persistentDataPath + jsonFilePath, JsonUtility.ToJson(gv));
        }
    }

    [System.Serializable]
    public class GlobalVar
    {
        public int Coin;
        public Dictionary<ItemSO, int> ItemDict;
        //public string UserNickName;
        public int Star;
        public int Stage;
    }
}
