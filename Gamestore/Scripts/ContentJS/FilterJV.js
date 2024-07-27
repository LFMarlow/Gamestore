$(document).ready(function () {

    // Fonction pour afficher/masquer les divs en fonction des cases cochées
    function updateDivs() {

        // Si aucune case genre n'est cochée, afficher toutes les divs
        if ($('#MainContent_CheckBoxGenre :checked').length === 0 && $('#MainContent_CheckBoxPrice :checked').length === 0) {
            $(".genreGame").fadeIn();

        } else {

            //Pour chaques divs avec la classe "genreGame", on récupére l'id et le prix contenu dans le data-price
            $(".genreGame").each(function () {
                var div = $(this);
                var divId = div.attr("id");
                var divPrice = parseInt(div.data("price"));
                var showDiv = false;
                var genreMatches = false;

                //Pour caque case "genre" coché, on vérifi si la valeur coché est "inclus" dans l'id des divs
                $('#MainContent_CheckBoxGenre :checked').each(function () {
                    var value = $(this).val();
                    if (divId.includes(value)) {
                        genreMatches = true;
                    }
                });

                var priceMatches = false;
                // Pour chaque case prix cochée, on compare avec le data-price de chaque div pour n'afficher que celles qui sont dans la portée choisie
                $('#MainContent_CheckBoxPrice :checked').each(function () {
                    var priceValue = $(this).val();
                    if (priceValue === "60+") {
                        if (divPrice > 60) {
                            priceMatches = true;
                        }
                    } else {
                        var priceRange = priceValue.split('-');
                        var minPrice = parseInt(priceRange[0]);
                        var maxPrice = parseInt(priceRange[1]);
                        if (divPrice >= minPrice && divPrice <= maxPrice) {
                            priceMatches = true;
                        }
                    }
                });


                if (($('#MainContent_CheckBoxGenre :checked').length === 0 || genreMatches) &&
                    ($('#MainContent_CheckBoxPrice :checked').length === 0 || priceMatches)) {
                    showDiv = true;
                }

                if (showDiv) {
                    div.fadeIn();
                } else {
                    div.fadeOut();
                }
            });
        }
    }

    // Mettre à jour les divs chaque fois qu'une case genre est cochée/décochée
    $('#MainContent_CheckBoxGenre').change(function () {
        updateDivs();
    });

    // Mettre à jour les divs chaque fois qu'une case prix est cochée/décochée
    $('#MainContent_CheckBoxPrice').change(function () {
        updateDivs();
    });

});