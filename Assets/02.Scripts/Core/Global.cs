using System.Collections;
using System.Collections.Generic;

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
        

        public string UserNickName;
        public string NowScene = "";
        public string NextScene = "";
        public bool b_isLoading;
        public string StageNumber = "";
    }
}
