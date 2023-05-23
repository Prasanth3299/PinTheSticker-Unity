using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class IAPManager : MonoBehaviour
{

    private static UnityIAPStoreListener m_Singleton;
    public void Awake()
    {
        // Initialize IAP once.
        if (m_Singleton == null)
        {
            m_Singleton = new UnityIAPStoreListener();
            m_Singleton.InitializeIAP();
        }
    }
    public void OnStylePackClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("style_pack"));
    }
    public void OnHeadwearPackClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("headwear_pack"));
    }
    public void OnMusicPack1Clicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("music_pack_1"));
    }
    public void OnWholePackClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("whole_pack"));
    }
    public void OnMessageStickersClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("message_stickers"));
    }
    public void OnMusicPack2Clicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("music_pack_2"));
    }
    public void OnSpecialStickersClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("special_stickers"));
    }
    public void OnFacePackClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("face_pack"));
    }
    public void OnMusicPack3Clicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("music_pack_3"));
    }
    public void OnPaidSpinWheelClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("paid_spin_wheel"));
    }
    public void OnRemoveAdsClicked()
    {
#if UNITY_IOS

        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("remove_ads_ios"));
#else
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("remove_ads_android"));
#endif
    }
    public void OnAllMusicClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("all_music"));
    }
    public void OnPetStickersClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("pet_stickers"));
    }
    public void OnGlitterStickersClicked()
    {
        m_Singleton.m_Controller.InitiatePurchase(m_Singleton.m_Controller.products.WithID("glitter_stickers"));
    }
}
public class UnityIAPStoreListener : IStoreListener
{
    // Unity IAP objects 
    public IStoreController m_Controller;
    public IAppleExtensions m_AppleExtensions;

    private int m_SelectedItemIndex = -1; // -1 == no product

    /// <summary>
    /// This will be called when Unity IAP has finished initialising.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_Controller = controller;
        m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();

        Debug.Log("Available items:");
        foreach (var item in controller.products.all)
        {
            if (item.availableToPurchase)
            {
                Debug.Log(string.Join(" - ",
                    new[]
                    {
                        item.metadata.localizedTitle,
                        item.metadata.localizedDescription,
                        item.metadata.isoCurrencyCode,
                        item.metadata.localizedPrice.ToString(),
                        item.metadata.localizedPriceString
                    }));
            }
        }
        m_AppleExtensions.RestoreTransactions(OnTransactionsRestored);
    }

    /// <summary>
    /// This will be called when a purchase completes.
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        //Debug.Log("Purchase OK: " + e.purchasedProduct.definition.id);
        //Debug.Log("Receipt: " + e.purchasedProduct.receipt);

        // Now that my purchase history has changed, update its UI
        
        if (e.purchasedProduct.definition.id.Contains("remove_ads"))
        {
            GameObject.Find("ScriptManager").transform.GetComponent<MusicSound>().OnRemoveAdsPurchaseSuccessfull();
        }
        else if (e.purchasedProduct.definition.id.Contains("paid_spin_wheel"))
        {
            GameObject.Find("ScriptManager").transform.GetComponent<Wheel>().OnPaidSpinPurchaseSuccessfull();
        }
        else
        {
            GameObject.Find("ScriptManager").transform.GetComponent<ShopScene>().OnPurchaseConfirmed();
        }
        // Indicate we have handled this purchase, we will not be informed of it again.
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// This will be called is an attempted purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product item, PurchaseFailureReason r)
    {
        Debug.Log("Purchase failed: " + item.definition.id);
        Debug.Log(r);
        if(item.definition.id == "remove_ads")
        {
            GameObject.Find("ScriptManager").transform.GetComponent<MusicSound>().OnRemoveAdsPurchaseFailed();
        }
        else if (item.definition.id.Contains("paid_spin_wheel"))
        {
            GameObject.Find("ScriptManager").transform.GetComponent<Wheel>().OnPaidSpinPurchaseFailed();
        }
        else
        {
            GameObject.Find("ScriptManager").transform.GetComponent<ShopScene>().OnPurchaseFailed();
        }
        
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Billing failed to initialize!");
        switch (error)
        {
            case InitializationFailureReason.AppNotKnown:
                Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");
                break;
            case InitializationFailureReason.PurchasingUnavailable:
                // Ask the user if billing is disabled in device settings.
                Debug.Log("Billing disabled!");
                break;
            case InitializationFailureReason.NoProductsAvailable:
                // Developer configuration error; check product metadata.
                Debug.Log("No products available for purchase!");
                break;
        }
    }

    public void InitializeIAP()
    {
        //Debug.Log("Initializing Unity IAP - should be called once");

        var module = StandardPurchasingModule.Instance();
        module.useMockBillingSystem = true;
        var builder = ConfigurationBuilder.Instance(module);

        //builder.Configure<IGooglePlayConfiguration>().SetPublicKey("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2O/9/H7jYjOsLFT/uSy3ZEk5KaNg1xx60RN7yWJaoQZ7qMeLy4hsVB3IpgMXgiYFiKELkBaUEkObiPDlCxcHnWVlhnzJBvTfeCPrYNVOOSJFZrXdotp5L0iS2NVHjnllM+HA1M0W2eSNjdYzdLmZl1bxTpXa4th+dVli9lZu7B7C2ly79i/hGTmvaClzPBNyX+Rtj7Bmo336zh2lYbRdpD5glozUq+10u91PMDPH+jqhx10eyZpiapr8dFqXl5diMiobknw9CgcjxqMTVBQHK6hS0qYKPmUDONquJn280fBs1PTeA6NMG03gb9FLESKFclcuEZtvM8ZwMMRxSLA9GwIDAQAB");
        builder.AddProduct("style_pack", ProductType.Consumable, new IDs
        {
            {"style_pack", GooglePlay.Name},
            {"style_pack", AppleAppStore.Name},
            {"style_pack", MacAppStore.Name},
            {"style_pack", WinRT.Name}
        });
        builder.AddProduct("headwear_pack", ProductType.Consumable, new IDs
        {
            {"headwear_pack", GooglePlay.Name},
            {"headwear_pack", AppleAppStore.Name},
            {"headwear_pack", MacAppStore.Name},
            {"headwear_pack", WinRT.Name}
        });
        builder.AddProduct("music_pack_1", ProductType.Consumable, new IDs
        {
            {"music_pack_1", GooglePlay.Name},
            {"music_pack_1", AppleAppStore.Name},
            {"music_pack_1", MacAppStore.Name},
            {"music_pack_1", WinRT.Name}
        });
        builder.AddProduct("whole_pack", ProductType.Consumable, new IDs
        {
            {"whole_pack", GooglePlay.Name},
            {"whole_pack", AppleAppStore.Name},
            {"whole_pack", MacAppStore.Name},
            {"whole_pack", WinRT.Name}
        });
        builder.AddProduct("message_stickers", ProductType.Consumable, new IDs
        {
            {"message_stickers", GooglePlay.Name},
            {"message_stickers", AppleAppStore.Name},
            {"message_stickers", MacAppStore.Name},
            {"message_stickers", WinRT.Name}
        });
        builder.AddProduct("music_pack_2", ProductType.Consumable, new IDs
        {
            {"music_pack_2", GooglePlay.Name},
            {"music_pack_2", AppleAppStore.Name},
            {"music_pack_2", MacAppStore.Name},
            {"music_pack_2", WinRT.Name}
        });
        builder.AddProduct("special_stickers", ProductType.Consumable, new IDs
        {
            {"special_stickers", GooglePlay.Name},
            {"special_stickers", AppleAppStore.Name},
            {"special_stickers", MacAppStore.Name},
            {"special_stickers", WinRT.Name}
        });
        builder.AddProduct("face_pack", ProductType.Consumable, new IDs
        {
            {"face_pack", GooglePlay.Name},
            {"face_pack", AppleAppStore.Name},
            {"face_pack", MacAppStore.Name},
            {"face_pack", WinRT.Name}
        });
        builder.AddProduct("music_pack_3", ProductType.Consumable, new IDs
        {
            {"music_pack_3", GooglePlay.Name},
            {"music_pack_3", AppleAppStore.Name},
            {"music_pack_3", MacAppStore.Name},
            {"music_pack_3", WinRT.Name}
        });
        builder.AddProduct("paid_spin_wheel", ProductType.Consumable, new IDs
        {
            {"paid_spin_wheel", GooglePlay.Name},
            {"paid_spin_wheel", AppleAppStore.Name},
            {"paid_spin_wheel", MacAppStore.Name},
            {"paid_spin_wheel", WinRT.Name}
        });
        builder.AddProduct("remove_ads_android", ProductType.Consumable, new IDs
        {
            {"remove_ads", GooglePlay.Name},
            //{"remove_ads", AppleAppStore.Name},
            //{"remove_ads", MacAppStore.Name},
            {"remove_ads", WinRT.Name}
        });
        builder.AddProduct("all_music", ProductType.Consumable, new IDs
        {
            {"all_music", GooglePlay.Name},
            {"all_music", AppleAppStore.Name},
            {"all_music", MacAppStore.Name},
            {"all_music", WinRT.Name}
        });
        builder.AddProduct("pet_stickers", ProductType.Consumable, new IDs
        {
            {"pet_stickers", GooglePlay.Name},
            {"pet_stickers", AppleAppStore.Name},
            {"pet_stickers", MacAppStore.Name},
            {"pet_stickers", WinRT.Name}
        });
        builder.AddProduct("glitter_stickers", ProductType.Consumable, new IDs
        {
            {"glitter_stickers", GooglePlay.Name},
            {"glitter_stickers", AppleAppStore.Name},
            {"glitter_stickers", MacAppStore.Name},
            {"glitter_stickers", WinRT.Name}
        });
        builder.AddProduct("remove_ads_ios", ProductType.NonConsumable, new IDs
        {
            //{"remove_ads", GooglePlay.Name},
            {"remove_ads", AppleAppStore.Name},
            {"remove_ads", MacAppStore.Name},
            //{"remove_ads", WinRT.Name}
        });
        /*builder.AddProduct("sword", ProductType.NonConsumable, new IDs
        {
            {"com.unity3d.unityiap.unityiapdemo.sword.c", GooglePlay.Name},
            {"com.unity3d.unityiap.unityiapdemo.sword.6", AppleAppStore.Name},
            {"com.unity3d.unityiap.unityiapdemo.sword.mac", MacAppStore.Name},
            {"com.unity3d.unityiap.unityiapdemo.sword", WindowsPhone8.Name}
        });
        builder.AddProduct("subscription", ProductType.Subscription, new IDs
        {
            {"com.unity3d.unityiap.unityiapdemo.subscription", GooglePlay.Name, AppleAppStore.Name}
        });
        */
        // Now we're ready to initialize Unity IAP.
        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// This will be called after a call to <extension>.RestoreTransactions().
    /// </summary>
    private void OnTransactionsRestored(bool success)
    {
        Debug.Log("Transactions restored.");
        //PlayerPrefs.SetInt("NoAds", 1);
    }

    /// <summary>
    /// iOS Specific.
    /// This is called as part of Apple's 'Ask to buy' functionality,
    /// when a purchase is requested by a minor and referred to a parent
    /// for approval.
    /// 
    /// When the purchase is approved or rejected, the normal purchase events
    /// will fire.
    /// </summary>
    /// <param name="item">Item.</param>
    private void OnDeferred(Product item)
    {
        Debug.Log("Purchase deferred: " + item.definition.id);
    }
}
