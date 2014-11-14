using UnityEngine;
using System.Collections;

public class HandPointer : MonoBehaviour{


    private UITexture m_Child;
    private Vector3 m_CornerOrig;

    void Start(){
        m_Child = gameObject.transform.GetChild(0).gameObject.GetComponent<UITexture>();
        m_CornerOrig = m_Child.localCorners[3] + m_Child.transform.localPosition;
    }
    void Update(){
        Vector3 pos = gameObject.transform.localPosition;

        Vector3 handCornerOrig = (pos + m_CornerOrig);

        UIPanel rootPanel = UIGlobalVariablesScript.Singleton.UIRoot.GetComponent<UIPanel>();
        float left = rootPanel.localCorners[0].x;
        float bottom = rootPanel.localCorners[0].y;
        float right = rootPanel.localCorners[2].x;
        float top = rootPanel.localCorners[2].y;

        Quaternion rot = Quaternion.identity;

        bool outsideX = handCornerOrig.x >= right;
        bool outsideY = handCornerOrig.y <= bottom;
        if (outsideX && outsideY)
            rot.eulerAngles = new Vector3(0, 0, 180);
        else if (outsideX)
            rot.eulerAngles = new Vector3(0, 0, 270);
        else if (outsideY)
            rot.eulerAngles = new Vector3(0, 0, 90);

        transform.localRotation = rot;


        int val = (int)(453f + (50f * Mathf.Sin(5f * Time.timeSinceLevelLoad)));
        m_Child.SetDimensions(val, val);
    }
}
