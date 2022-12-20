using System.Collections;
using System.Collections.Generic;

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
        

        public string UserNickName;
        public string NowScene = "";
        public string NextScene = "";
        public bool b_isLoading;
    }
}
