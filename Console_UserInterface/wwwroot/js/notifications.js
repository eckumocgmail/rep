function startupNotification( api ) {
    return window.setInterval(() => { checkoutForNotifications(api); },500);
}
function checkoutForNotifications(api) {
    try {
        api.invokeMethodAsync('CheckoutForNotifitcations');
    } catch (err) {
        console.log(err);
        alert(err);
    }
}