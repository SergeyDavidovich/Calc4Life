using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calc4Life.Services.PurchasingServices
{
    public class PurchasingService
    {
        public async Task PurchaseNonConsumableItem(string productId, string payload)
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync();
                if (!connected) return; //we are offline or can't connect, don't try to purchase

                //check purchases
                var purchase = await billing.PurchaseAsync(productId, ItemType.InAppPurchase, payload);

                //possibility that a null came through.
                if (purchase == null)
                {
                    //did not purchase
                    return;
                }
                if (purchase.State == PurchaseState.Purchased)
                {
                    //purchased!
                    SavePurchasedItem(productId);
                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
                Debug.WriteLine("Error: " + purchaseEx);
            }
            catch (Exception ex)
            {
                //Something else has gone wrong, log it
                Debug.WriteLine("Issue connecting: " + ex);
            }
            finally
            {
                await billing.DisconnectAsync();
            }
        }

        public async Task<bool> PurchaseConsumableItem(string productId, string payload)
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync();
                if (!connected)
                {
                    //we are offline or can't connect, don't try to purchase
                    return false;
                }
                //check purchases
                var purchase = await billing.PurchaseAsync(productId, ItemType.InAppPurchase, payload);

                //possibility that a null came through.
                if (purchase == null)
                {
                    //did not purchase
                    return false;
                }
                else if (purchase.State == PurchaseState.Purchased)
                {
                    //If we are on iOS we are done, else try to consume the purchase
                    //Device.RuntimePlatform comes from Xamarin.Forms, you can also use a conditional flag or the DeviceInfo plugin
                    if (Xamarin.Forms.Device.RuntimePlatform == Device.iOS)
                        return false;

                    //purchased, we can now consume the item or do it later
                    var consumedItem = await CrossInAppBilling.Current.ConsumePurchaseAsync(purchase.ProductId, purchase.PurchaseToken);

                    if (consumedItem != null)
                    {
                        //Consumed!!
                        SavePurchasedItem(productId);
                        return true;
                    }
                    return false;
                }
                else
                    return false;
            }
            //catch (InAppBillingPurchaseException purchaseEx)
            //{
            //    //Billing Exception handle this based on the type
            //    Debug.WriteLine("Error: " + purchaseEx);
            //    //throw new InAppBillingPurchaseException(new PurchaseError());
            //}
            //catch (Exception ex)
            //{
            //    //Something else has gone wrong, log it
            //    Debug.WriteLine("Issue connecting: " + ex);
            //    //throw new Exception("Issue connecting", ex.InnerException);
            //}
            finally
            {
                await billing.DisconnectAsync();
            }

        }

        public async Task<bool> WasItemPurchased(string productId)
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync();

                if (!connected)
                {
                    //Couldn't connect
                    return false;
                }

                //check purchases
                var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);

                //check for null just incase
                if (purchases?.Any(p => p.ProductId == productId) ?? false)
                {
                    //Purchase restored
                    return true;
                }
                else
                {
                    //no purchases found
                    return false;
                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
                Debug.WriteLine("Error: " + purchaseEx);
            }
            catch (Exception ex)
            {
                //Something has gone wrong
            }
            finally
            {
                await billing.DisconnectAsync();
            }

            return false;
        }

        public bool IsPurchasedItemSaved(string productId)
        {
            string str = (string)Application.Current.Properties[productId];
            if (str == productId)
                return true;
            else
                return false;
        }

        private void SavePurchasedItem(string productId)
        {
            App.Current.Properties["constants_unblocked"] = productId;
        }

    }
}

