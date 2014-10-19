using UnityEngine;
using System.Collections;

public class BetweenSceneData
{

    private bool m_ReturnFromMiniGame;
    public bool ReturnFromMiniGame
    {
        get
        {
            return m_ReturnFromMiniGame;
        }
        set
        {
            m_ReturnFromMiniGame = value;
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
