using UnityEngine;
using System.Collections;

public class BetweenSceneData
{

    private bool m_MiniGameCelebration;
    public bool MiniGameCelebration
    {
        get
        {
            return m_MiniGameCelebration;
        }
        set
        {
            m_MiniGameCelebration = value;
        }
    }

    public void DoCelebration(){

        Debug.Log("Minigame Celebration test : [" + m_MiniGameCelebration + "];");
        if (m_MiniGameCelebration)
        {
            
        }
    }


    #region Singleton
    static BetweenSceneData m_Instance;
    public static BetweenSceneData Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new BetweenSceneData();
            return m_Instance;
        }
    }
    #endregion
}
