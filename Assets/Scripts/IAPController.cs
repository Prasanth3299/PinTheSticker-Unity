using UnityEngine;
using UnityEngine.Purchasing;

public class IAPController : IStoreListener
{

    private IStoreController controller;
    private IExtensionProvider extensions;
    string removeAds = "remove_ads";
    public IAPController()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(removeAds, ProductType.Consumable, new IDs
        {
            {removeAds, GooglePlay.Name},
            {removeAds, MacAppStore.Name}
        });

        UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
        extensions.GetExtension<IAppleExtensions>().RestoreTransactions(result => {
            if (result)
            {
                // This does not mean anything was restored,
                // merely that the restoration process succeeded.
            }
            else
            {
                // Restoration failed.
            }
        });
        foreach (var product in controller.products.all)
        {
            Debug.Log(product.metadata.localizedTitle);
            Debug.Log(product.metadata.localizedDescription);
            Debug.Log(product.metadata.localizedPriceString);
        }
    }

    /// <summary>
    /// Called when Unity IAP encounters an unrecoverable initialization error.
    ///
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {

    }
    public void OnPurchaseClicked(string productId)
    {
        controller.InitiatePurchase(productId);
    }
}