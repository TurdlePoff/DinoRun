using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController s_StoreController = null;
    private static IExtensionProvider s_StoreExtensionProvider = null;

    public static string s_RemoveAds = "remove_ads";

    // Start is called before the first frame update
    void Start()
    {
        if(s_StoreController == null) {
            InitialiseIAP();
        }
    }

    public void OnInitialized(IStoreController _controller, IExtensionProvider _provider) {
        Debug.Log("On initialised: Pass");
        s_StoreController = _controller;
        s_StoreExtensionProvider = _provider;
    }

    public void OnInitializeFailed(InitializationFailureReason _error) {
        Debug.LogError("ERROR: IAP Initialisation failure: " + _error);
    }

    private bool IsInitialised() {
        return s_StoreController != null && s_StoreExtensionProvider != null;
    }

    public void InitialiseIAP() {
        // Check for prior initialisation
        if (IsInitialised()) {
            return;
        }

        // Collect configuration details
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add product with Unity IAP ID
        builder.AddProduct(s_RemoveAds, ProductType.NonConsumable);

        // Initialise Unity IAP
        UnityPurchasing.Initialize(this, builder);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs _arguments) {
        if(string.Equals(_arguments.purchasedProduct.definition.id, s_RemoveAds, System.StringComparison.Ordinal)) {
            Debug.Log("Purchasing product");

            // Remove ads
            PlayerPrefs.SetInt("noAdsPurchased", 1);
            // Code to remove purchase button
        } else {
            Debug.Log("Unrecognised product");
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product _product, PurchaseFailureReason _error) {
        Debug.LogError(string.Format("ERROR: OnPurchaseFailed. Product: '{0}', PurchaseFailureReason: '{1}'", _product.definition.storeSpecificId, _error));
    }

    void BuyProductID(string _productID) {
        // Check for initialisation
        if (IsInitialised()) {
            // Obtain product reference
            Product product = s_StoreController.products.WithID(_productID);

            // Check reference
            if(product != null && product.availableToPurchase) {
                Debug.Log("Purchasing product");
                s_StoreController.InitiatePurchase(product);
            } else {
                Debug.LogError("Product is could not be found or is unavailable for purchase.");
            }
        } else {
            Debug.LogError("ERROR: Product could not be purchased because the store is not initialised.");
        }
    }

}
