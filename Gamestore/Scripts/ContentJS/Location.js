window.onload = function () {
    getLocation();
};

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    } else {
        console.log("Geolocation is not supported by this browser.");
    }
}

function showPosition(position) {
    if (!sessionStorage.getItem('pageReloaded')) {
        var latitude = position.coords.latitude;
        var longitude = position.coords.longitude;


        // Fonction pour envoyer des données au serveur via AJAX
        $.ajax({
            type: "POST",
            url: "Panier.aspx/ProcessData",
            data: JSON.stringify({ latitude: latitude, longitude: longitude }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log("Response from server: " + response.d);

                // Définir l'indicateur dans sessionStorage pour éviter les rechargements futurs
                sessionStorage.setItem('pageReloaded', 'true');

                // Recharger la page pour déclencher Page_Load et récupérer les données
                location.reload();
            },
            error: function (error) {
                console.error("Error: " + error);
            }
        });
    }
}

function showError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            console.log("L'utilisateur à refusé la géolocalisation.");
            break;
        case error.POSITION_UNAVAILABLE:
            console.log("Les informations de localisation sont non disponible.");
            break;
        case error.TIMEOUT:
            console.log("Dépassement du temps de récupération.");
            break;
        case error.UNKNOWN_ERROR:
            console.log("Une erreur inconnue est apparu.");
            break;
    }
}