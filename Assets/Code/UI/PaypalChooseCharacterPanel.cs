using UnityEngine;
using System.Collections;

public class PaypalChooseCharacterPanel : MonoBehaviour {

    public PersistentData.TypesOfAnimin AniminType;	
    public decimal Price = 4.99m;
	
	// Update is called once per frame
	void OnClick () 
    {
        UIGlobalVariablesScript.Singleton.LaunchWebview(AniminType, Price);
        this.gameObject.transform.parent.gameObject.SetActive(false);
	}
}
