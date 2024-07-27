function graphByItemClick() {
    initializeChartItem()
}

function initializeChartItem(data) {
    var ctx = document.getElementById('salesChartArticle').getContext('2d');
    var datasets = [];
    var labels = new Set(); // Utilisé pour collecter toutes les dates uniques

    // Préparation des datasets pour chaque jeu
    for (const [gameName, sales] of Object.entries(data)) {
        const salesData = [];
        const sortedDates = Object.keys(sales).sort(); // Trier les dates pour garantir l'ordre chronologique

        // S'assurer que chaque date est prise en compte dans les labels globaux
        sortedDates.forEach(date => {
            labels.add(date);
            salesData.push({
                x: date,
                y: sales[date]
            });
        });

        // Créer un dataset pour chaque jeu
        datasets.push({
            label: gameName,
            data: salesData,
            fill: false,
            borderColor: getRandomColor(), // Une couleur aléatoire pour chaque jeu
            borderWidth: 2
        });
    }

    // Convertir les labels en tableau et trier (important pour la cohérence sur l'axe des x)
    labels = Array.from(labels).sort();

    // Création du graphique
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: datasets
        },
        options: {
            scales: {
                x: {
                    type: 'time',
                    time: {
                        unit: 'day',
                        tooltipFormat: 'DD MMM YYYY'
                    },
                    title: {
                        display: true,
                        text: 'Date'
                    }
                },
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Nombre de ventes'
                    }
                }
            },
            plugins: {
                legend: {
                    display: true,
                    position: 'top'
                }
            }
        }
    });
}

// Fonction pour générer une couleur aléatoire pour chaque dataset
function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}