using UnityEngine;
using System.Collections;

public class CannonClashTutorialOneAnimationScript : MonoBehaviour {

    [SerializeField]
    private UISprite Strawberry;
    [SerializeField]
    private UISprite Blueberry;
    [SerializeField]
    private UISprite Tick;
    [SerializeField]
    private UISprite Cross;


    private float m_Timer;

    private const float m_VisibleFor = 3f;
    private const float m_FadeDuration = 0.5f;

    private const float BeginFadeOut1 = m_VisibleFor;
    private const float EndFadeOut1 = m_VisibleFor + m_FadeDuration; //Also Begin Fade In 2

    private const float EndFadeIn2 = m_VisibleFor + m_FadeDuration + m_FadeDuration;
    private const float BeginFadeOut2 = m_VisibleFor + m_FadeDuration + m_FadeDuration + m_VisibleFor;
    private const float EndFadeOut2 = m_VisibleFor + m_FadeDuration + m_FadeDuration + m_VisibleFor + m_FadeDuration;

    private const float EndFadeIn1 = m_VisibleFor + m_FadeDuration + m_FadeDuration + m_VisibleFor + m_FadeDuration + m_FadeDuration;

    // Use this for initialization
    void Start () {
        Strawberry.alpha =0;
        Tick.alpha =0;
        Blueberry.alpha =1;
        Cross.alpha =1;

        m_Timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        m_Timer += Time.deltaTime;


        if (0 <= m_Timer && m_Timer < BeginFadeOut1)
        {
            //Debug.Log("Timer : [" + 0 + "<=" + m_Timer + "<" + BeginFadeOut1 + "];");
            Blueberry.alpha = 1;
            Cross.alpha = 1;
        }
        else if (BeginFadeOut1 <= m_Timer && m_Timer < EndFadeOut1)
        {
            //Debug.Log("Timer : [" + BeginFadeOut1 + "<=" + m_Timer + "<" + EndFadeOut1 + "];");
            float diff = EndFadeOut1 - m_Timer;
            float alpha = diff / m_FadeDuration;

            Blueberry.alpha = alpha;
            Cross.alpha = alpha;
        }
        else if (EndFadeOut1 <= m_Timer && m_Timer < EndFadeIn2)
        {
            //Debug.Log("Timer : [" + EndFadeOut1 + "<=" + m_Timer + "<" + EndFadeIn2 + "];");
            float diff = EndFadeIn2 - m_Timer;
            float alpha = 1 - (diff / m_FadeDuration);

            Strawberry.alpha = alpha;
            Tick.alpha = alpha;
        }
        else if (EndFadeIn2 <= m_Timer && m_Timer < BeginFadeOut2)
        {
            //Debug.Log("Timer : [" + EndFadeIn2 + "<=" + m_Timer + "<" + BeginFadeOut2 + "];");
            Strawberry.alpha = 1;
            Tick.alpha = 1;
        }
        else if (BeginFadeOut2 <= m_Timer && m_Timer < EndFadeOut2)
        {
            //Debug.Log("Timer : [" + BeginFadeOut2 + "<=" + m_Timer + "<" + EndFadeOut2 + "];");
            float diff = EndFadeOut2 - m_Timer;
            float alpha = diff / m_FadeDuration;

            Strawberry.alpha = alpha;
            Tick.alpha = alpha;
        }
        else if (EndFadeOut2 <= m_Timer && m_Timer < EndFadeIn1)
        {
            //Debug.Log("Timer : [" + EndFadeOut2 + "<=" + m_Timer + "<" + EndFadeIn1 + "];");
            float diff = EndFadeIn1 - m_Timer;
            float alpha = 1 - (diff / m_FadeDuration);

            Blueberry.alpha = alpha;
            Cross.alpha = alpha;
        }
        else if (EndFadeIn1 <= m_Timer)
        {
            //Debug.Log("Timer : [" + m_Timer + "<" + EndFadeIn1 + "];");
            m_Timer = 0;
        }
	}
}
