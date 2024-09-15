function startUpdateGeoPosition(referenceObject) {
    return window.setInterval(() => {

        console.log('updateGeoPosition');
        navigator.geolocation.getCurrentPosition((position) => {

            console.log(position.coords.latitude, position.coords.longitude);
            try {
                referenceObject.invokeMethodAsync('OnGeoPositionChanged', position.coords.latitude, position.coords.longitude );
            } catch (err) {
                console.log(err);
                alert(err);
            }
        });

    }, 3000);
 
}