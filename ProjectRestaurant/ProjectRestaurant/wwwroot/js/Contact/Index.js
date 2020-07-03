//google map
(function (exports) {
    "use strict";

    function initMap() {
        exports.map = new google.maps.Map(document.getElementById("map"), {
            center: {
                lat: 41.015137,
                lng: 28.979530
            },
            zoom: 8
        });
    }

    exports.initMap = initMap;
})((this.window = this.window || {}));