let startMap, endMap, startMarker, endMarker, dotNetHelper;

window.initializeMaps = function (startElement, endElement, dotNetRef) {
    dotNetHelper = dotNetRef;
    
    // Initialize start location map
    startMap = new google.maps.Map(startElement, {
        center: { lat: 0, lng: 0 },
        zoom: 2
    });

    // Initialize end location map
    endMap = new google.maps.Map(endElement, {
        center: { lat: 0, lng: 0 },
        zoom: 2
    });

    // Add click handlers for maps
    startMap.addListener('click', function(e) {
        const latLng = e.latLng;
        updateStartMarker(latLng.lat(), latLng.lng());
        dotNetHelper.invokeMethodAsync('OnStartLocationSelected', latLng.lat(), latLng.lng());
    });

    endMap.addListener('click', function(e) {
        const latLng = e.latLng;
        updateEndMarker(latLng.lat(), latLng.lng());
        dotNetHelper.invokeMethodAsync('OnEndLocationSelected', latLng.lat(), latLng.lng());
    });
};

window.updateStartMarker = function (lat, lng) {
    if (startMarker) {
        startMarker.setMap(null);
    }
    startMarker = new google.maps.Marker({
        position: { lat: lat, lng: lng },
        map: startMap
    });
    startMap.setCenter({ lat: lat, lng: lng });
    startMap.setZoom(15);
};

window.updateEndMarker = function (lat, lng) {
    if (endMarker) {
        endMarker.setMap(null);
    }
    endMarker = new google.maps.Marker({
        position: { lat: lat, lng: lng },
        map: endMap
    });
    endMap.setCenter({ lat: lat, lng: lng });
    endMap.setZoom(15);
};

window.getCurrentPosition = function () {
    return new Promise((resolve, reject) => {
        if (!navigator.geolocation) {
            reject('Geolocation is not supported by your browser');
        }

        navigator.geolocation.getCurrentPosition(
            position => {
                resolve({
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude
                });
            },
            error => {
                reject(error.message);
            }
        );
    });
};