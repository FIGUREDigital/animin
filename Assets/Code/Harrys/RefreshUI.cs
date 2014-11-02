using UnityEngine;
using System.Collections;

public class RefreshUI : MonoBehaviour {

	// Use this for initialization
	public void Start(){
		//This method looks through the inventory and applies a graphic to the tab.
		
		bool FoodIconSet = false;
		bool ItemIconSet = false;
		bool MediIconSet = false;
		for (int i=0; i<PersistentData.Singleton.Inventory.Count; ++i) {
			//So. InventoryItemData.Items is a list of InventoryItemBankData. This is not the same as InventoryItemData, which is what is saved in the Animin Profile Data.
			//Therefore we have to run the inventory type through the InventoryItemDataBank to find out its type.
			if(InventoryItemData.Items[(int)PersistentData.Singleton.Inventory[i].Id].ItemType == PopupItemType.Food && !FoodIconSet){
				UIGlobalVariablesScript.Singleton.FoodButton.GetComponent<UIButton>().normalSprite = InventoryItemData.Items[(int)PersistentData.Singleton.Inventory[i].Id].SpriteName;
				UIGlobalVariablesScript.Singleton.FoodButton.GetComponent<InterfaceItemLinkToModelScript>().ItemID = PersistentData.Singleton.Inventory[i].Id;
				FoodIconSet = true;
			} else if(InventoryItemData.Items[(int)PersistentData.Singleton.Inventory[i].Id].ItemType == PopupItemType.Item && !ItemIconSet){
				UIGlobalVariablesScript.Singleton.ItemsButton.GetComponent<UIButton>().normalSprite = InventoryItemData.Items[(int)PersistentData.Singleton.Inventory[i].Id].SpriteName;
				UIGlobalVariablesScript.Singleton.ItemsButton.GetComponent<InterfaceItemLinkToModelScript>().ItemID = PersistentData.Singleton.Inventory[i].Id;
				ItemIconSet = true;
			} else if(InventoryItemData.Items[(int)PersistentData.Singleton.Inventory[i].Id].ItemType == PopupItemType.Medicine && !MediIconSet){
				UIGlobalVariablesScript.Singleton.MedicineButton.GetComponent<UIButton>().normalSprite = InventoryItemData.Items[(int)PersistentData.Singleton.Inventory[i].Id].SpriteName;
				UIGlobalVariablesScript.Singleton.MedicineButton.GetComponent<InterfaceItemLinkToModelScript>().ItemID = PersistentData.Singleton.Inventory[i].Id;
				MediIconSet = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
