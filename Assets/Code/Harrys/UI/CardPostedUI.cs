using UnityEngine;
using System.Collections;

public class CardPostedUI : MonoBehaviour {


    private float StartFadeoutAt = 2;
    private float StopFadeoutAt = 4;

    private float m_Timer;

    private UIPanel m_Panel;

    void Start(){
        m_Panel = this.GetComponent<UIPanel>();
        m_Timer = 0;
        if (m_Panel!=null)m_Panel.alpha = 1;
    }

    void Update () {
        m_Timer += Time.deltaTime;

        if (StartFadeoutAt <= m_Timer && m_Timer < StopFadeoutAt && m_Panel != null)
        {
            Debug.Log("showing");
            m_Panel.alpha -= (1.0f / (StopFadeoutAt - StartFadeoutAt)) * Time.deltaTime;
        }
        else if (StopFadeoutAt <= m_Timer && m_Panel != null)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }
    public static void Instantiate(){
        GameObject go = GameObject.Instantiate( (Resources.Load("Prefabs/UI - CardPosted")) ,Vector3.zero,Quaternion.identity) as GameObject;
        go.transform.parent = UIGlobalVariablesScript.Singleton.UIRoot.transform;
        go.transform.localScale = Vector3.one;
    }
}
