/*
* STEP 1: Install Prime31 Plugin
* STEP 2: StartStore with ids setup in apple and google play servers when you start the app
* STEP 3: Call BuyItem(string id) to buy item
* STEP 4: Wait for CurrentPurchaseStatus to go to either success or fail
*/
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopManager
{
    #region Singleton

    private static ShopManager s_Instance;

    public static ShopManager Instance
    {
        get
        {
            if ( s_Instance == null )
            {
                s_Instance = new ShopManager();
            }
            return s_Instance;
        }
    }

    #endregion

    public enum ShopState
    {
        Idle,
        TransitioningToShop,
        InShop
    }
    
    private ShopState m_CurrentShopState = ShopState.Idle;

    public string Error;

    public class Item
    {
        public string ID;
        public string Price;

        public Item( string id )
        {
            ID = id;
        }
    }




    private const float TIMEOUT_TIME = 10.0f;
    private float m_TimeoutTimer = TIMEOUT_TIME;
    

    public ShopManager()
    {
#if UNITY_IOS
		StoreKitManager.purchaseSuccessfulEvent+=purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent += purchaseCancelled;
		StoreKitManager.purchaseFailedEvent += purchaseFailed;
		StoreKitManager.productListReceivedEvent += productListReceivedEvent;
		StoreKitManager.restoreTransactionsFailedEvent += restoreTransactionsFailed;
		StoreKitManager.restoreTransactionsFinishedEvent += restoreTransactionsFinished;
		StoreKitManager.restoreTransactionsFailedEvent += restoreTransactionsFailed;
		StoreKitManager.restoreTransactionsFinishedEvent += restoreTransactionsFinished;
#elif UNITY_ANDROID
        GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
        GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
        GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
        GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
        GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
        GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
        GoogleIABManager.purchaseFailedEvent += purchaseFailed;
        GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
        GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
#endif
        
    }

    public enum PurchaseStatus
    {
        Idle,
        InProgress,
        Fail,
        Success,
        Cancel
    }

    public static PurchaseStatus CurrentPurchaseStatus;

    public enum RestoreStatus
    {
        Idle,
        InProgress,
        Fail,
        Success
    }

    public static RestoreStatus CurrentRestoreStatus;
	





	void restoreTransactionsFailed( string error )
	{
        CurrentRestoreStatus = RestoreStatus.Fail;
		Debug.Log( "restoreTransactionsFailed: " + error );
	}
	
	
	void restoreTransactionsFinished()
	{
        CurrentRestoreStatus = RestoreStatus.Success;
		Debug.Log( "restoreTransactionsFinished" );
	}

#if UNITY_IOS
    private void productListReceivedEvent( List<StoreKitProduct> productList )
    {
        Debug.Log( "productListReceivedEvent. total products received: " + productList.Count );

        // print the products to the console
        foreach( StoreKitProduct product in productList )
            Debug.Log( product.ToString() + "\n" );
		
    }
#endif

	void purchaseFailed( string error )
	{
        
        CurrentPurchaseStatus = PurchaseStatus.Fail;
	    Error = error;
	
		Debug.Log( "purchase failed with error: " + error );
        //GUIDebug.Log("purchase failed with error: " + error);
     }   
	

	void purchaseCancelled( string error )
	{
        CurrentPurchaseStatus = PurchaseStatus.Cancel;
		Debug.Log( "purchase cancelled with error: " + error );
        //GUIDebug.Log("purchase cancelled with error: " + error);
	}

#if UNITY_IOS
	void purchaseSuccessful( StoreKitTransaction transaction )
	{
        if( CurrentRestoreStatus == RestoreStatus.InProgress || CurrentRestoreStatus == RestoreStatus.Success )
        {
            // PlayerPrefs.SetInt("BOUGHT_" + ResourcesManager.Instance.GetProductFromID(transaction.productIdentifier), 1);
        }
        else
        {
            CurrentPurchaseStatus = PurchaseStatus.Success;
        }
		Debug.Log( "purchased product: " + transaction );
	}

#elif UNITY_ANDROID
    void purchaseSucceededEvent(GooglePurchase purchase)
    {
        CurrentPurchaseStatus = PurchaseStatus.Success;
        Debug.Log("purchaseSucceededEvent: " + purchase);
    }

    void consumePurchaseSucceededEvent(GooglePurchase purchase)
    {
        Debug.Log("consumePurchaseSucceededEvent: " + purchase);
    }

    void consumePurchaseFailedEvent(string error)
    {
        Debug.Log("consumePurchaseFailedEvent: " + error);
    }

    private void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
    {
        Debug.Log( "queryInventorySucceededEvent" );
        //GUIDebug.Log("queryInventorySucceededEvent");
        Prime31.Utils.logObject( purchases );
        Prime31.Utils.logObject( skus );

        for( int i = 0; i < purchases.Count; i++ )
        {
            //GUIDebug.Log( "purchases " + purchases[ i ] );
        }
        
        for( int i = 0; i < skus.Count; i++ )
        {
            //GUIDebug.Log( "skus " + skus[ i ].productId );
        }
    }


    void queryInventoryFailedEvent(string error)
    {
        Debug.Log("queryInventoryFailedEvent: " + error);
        //GUIDebug.Log("queryInventoryFailedEvent: " + error);
    }

    void purchaseCompleteAwaitingVerificationEvent(string purchaseData, string signature)
    {
        Debug.Log("purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature);
    }

    void billingSupportedEvent()
    {
        Debug.Log("billingSupportedEvent");
    }


    void billingNotSupportedEvent(string error)
    {
        Debug.Log("billingNotSupportedEvent: " + error);
    }
#endif
	
	public void RestoreItems()
	{
		CurrentRestoreStatus = RestoreStatus.InProgress;
#if UNITY_IOS
		StoreKitBinding.restoreCompletedTransactions();
#elif UNITY_ANDROID

#endif
	}
	

    public void Update()
    {
        switch ( CurrentPurchaseStatus )
        {
            case PurchaseStatus.Idle:
                break;
            case PurchaseStatus.InProgress:
            {
                if ( m_TimeoutTimer > 0.0f )
                {
                    m_TimeoutTimer = Mathf.Max(0.0f, m_TimeoutTimer - Time.deltaTime);
                }
                else if ( m_TimeoutTimer == 0.0f )
                {
                    CurrentPurchaseStatus = PurchaseStatus.Fail;
                }
                break;
            }
            case PurchaseStatus.Success:
            {
                m_TimeoutTimer = TIMEOUT_TIME;
                DrawTransactionCompleted();
                GetItem();
                break;
            }
            case PurchaseStatus.Cancel:
            case PurchaseStatus.Fail:
            {
                m_TimeoutTimer = TIMEOUT_TIME;
                DrawTransactionFailed();
                break;
            }
        }
    }

    private void DrawTransactionFailed()
    {
        DrawPopupScreen();
        
    }

    private void DrawTransactionCompleted()
    {
        DrawPopupScreen();
    }

    private void GetItem()
    {
        m_CurrentShopState = ShopState.Idle;
    }

    public void BuyItem( string productID = "" )
    {
        if( CurrentPurchaseStatus == PurchaseStatus.Idle )
        {
            CurrentPurchaseStatus = PurchaseStatus.InProgress;

            string id = productID; //GetProductID( "LevelsBundle" + LevelSelectScreen.LevelToLoad  );

			if( HasBought() || Application.isEditor )
            {
                CurrentPurchaseStatus = PurchaseStatus.Success;
            }
            else
            {
#if UNITY_ANDROID
                GoogleIAB.purchaseProduct( productID );
#elif UNITY_IOS
                StoreKitBinding.purchaseProduct( id, 1 );
#endif
            }
        }
    }

	public bool HasBought(string productID = "")
	{
		bool beenBought = false;
		#if UNITY_IOS
		List<StoreKitTransaction> transactions =  StoreKitBinding.getAllSavedTransactions();
		
		
		foreach( StoreKitTransaction storeKitTransaction in transactions )
		{
			if( storeKitTransaction.productIdentifier == productID )
			{
				beenBought = true;
			}
		}
		#endif

		return beenBought;
	}


    private void DrawPopupScreen()
    {

    }

    private void GoInShopCallback( int dummy )
    {
        if ( m_CurrentShopState != ShopState.InShop )
        {
            m_CurrentShopState = ShopState.InShop;

        }
    }


    private void ReturnToStateCallback( int dummy )
    {
        //FlurryBinding.logEvent( "Shop - Return", false );
        
        m_CurrentShopState = ShopState.Idle;
    }

    public void StartStore(string[] shopItems)
    {
        //m_CurrentShopState = ShopState.InShop;
        string[] ids = new string[ shopItems.Length ];
        for( int i = 0; i < ids.Length; i++ )
        {
            ids[ i ] = shopItems[ i ];
        }
#if UNITY_IOS
		StoreKitBinding.requestProductData( ids );
#elif UNITY_ANDROID
        GoogleIAB.queryInventory( ids );
#endif
    }

    public void EndStore()
    {
        m_CurrentShopState = ShopState.Idle;
    }
}
//#endif