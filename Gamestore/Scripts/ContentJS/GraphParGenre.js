function graphByGenreClick() {
    initializeChartGenre()
}

function initializeChartGenre(data) {
    var ctx = document.getElementById('salesChart').getContext('2d');
    var datasets = [];
    var labels = new Set();

    // Préparation des données pour le graphique
    for (let item in data) {
        let salesData = [];
        for (let date in data[item]) {
            labels.add(date);
            salesData.push({ x: date, y: data[item][date] });  // Modification pour utiliser un format adapté à Chart.js
        }
        datasets.push({
            label: item,
            data: salesData,
            fill: false,
            borderColor: getRandomColor(),
            lineTension: 0.1
        });
    }

    labels = Array.from(labels).sort();

    if (window.myChart) {
        window.myChart.destroy(); // Détruit le graphique existant si nécessaire
    }

    window.myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: datasets
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Nombre de ventes'
                    }
                },
                x: {
                    title: {
                        display: true,
                        text: 'Date'
                    }
                }
            }
        }
    });
}
//Fonction pour générer une couleur aléatoire
function randomColor() {
    const r = Math.floor(Math.random() * 255);
    const g = Math.floor(Math.random() * 255);
    const b = Math.floor(Math.random() * 255);
    return `rgba(${r}, ${g}, ${b}, 0.5)`;
}