using Calc4Life.Helpers;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calc4Life.Services.PurchasingServices
{
    public class ConstantsPurchasingService
    {
        public async Task<bool> PurchaseNonConsumableItem(string productId, string payload)
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync();
                if (!connected) return false; //we are offline or can't connect, don't try to purchase

                //check purchases
                var purchase = await billing.PurchaseAsync(productId, ItemType.InAppPurchase, payload);

                //possibility that a null came through.
                if (purchase == null)
                {
                    //did not purchase
                    Settings.ConstProductPurchased = false;
                    return false;
                }
                if (purchase.State == PurchaseState.Purchased)
                {
                    //purchased!
                    Settings.ConstProductPurchased = true;
                    return true;
                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
#if DEBUG
                Debug.WriteLine("Error: " + purchaseEx);
#endif
            }
            catch (Exception ex)
            {
                //Something else has gone wrong, log it
#if DEBUG
                Debug.WriteLine("Issue connecting: " + ex);
#endif
            }
            finally
            {
                await billing.DisconnectAsync();
            }
            return false;
        }

        public async Task<bool> IsItemPurchased(string productId)
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync();

                if (!connected) return false;//Couldn't connect

                //check purchases
                var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);

                //check for null just incase
                if (purchases?.Any(p => p.ProductId == productId) ?? false)
                {
                    //Purchase restored
                    Settings.ConstProductPurchased = true;
                    return true;
                }
                else
                {
                    //no purchases found
                    Settings.ConstProductPurchased = false;
                    return false;
                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
#if DEBUG
                Debug.WriteLine("Error: " + purchaseEx);
#endif
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
    }
}

