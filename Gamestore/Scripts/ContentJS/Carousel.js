document.addEventListener('DOMContentLoaded', function () {
    var carousel = document.querySelector('#carouselReduction');
    var carouselInner = carousel.querySelector('.carousel-inner');
    var carouselItems = carouselInner.querySelectorAll('.carousel-item');
    var indicators = carousel.querySelector('.carousel-indicators');
    var indicatorsButtons = indicators.querySelectorAll('button');
    var activeIndex = 0;

    // Suppression des slides et indicateurs vides
    carouselItems.forEach(function (item, index) {
        var img = item.querySelector('.img_slide');
        if (img.getAttribute('src') === '' || img.getAttribute('src') === null) {
            item.remove();
            if (indicatorsButtons[index]) {
                indicatorsButtons[index].remove();
            }
        }
    });

    // Réinitialisation des indices des slides et indicateurs
    var remainingSlides = carouselInner.querySelectorAll('.carousel-item');
    var remainingIndicators = indicators.querySelectorAll('button');

    remainingSlides.forEach(function (item, index) {
        if (index === 0) {
            item.classList.add('active');
        } else {
            item.classList.remove('active');
        }
    });

    remainingIndicators.forEach(function (button, index) {
        button.setAttribute('data-bs-slide-to', index);
        button.setAttribute('aria-label', 'Slide ' + index);
        if (index === 0) {
            button.classList.add('active');
        } else {
            button.classList.remove('active');
        }
    });

    // Réinitialise le carrousel
    new bootstrap.Carousel(carousel);
});

document.addEventListener('DOMContentLoaded', function () {
    var carousel = document.querySelector('#carouselSold');
    var carouselInner = carousel.querySelector('.carousel-inner');
    var carouselItems = carouselInner.querySelectorAll('.carousel-item');
    var indicators = carousel.querySelector('.carousel-indicators');
    var indicatorsButtons = indicators.querySelectorAll('button');
    var activeIndex = 0;

    // Suppression des slides et indicateurs vides
    carouselItems.forEach(function (item, index) {
        var img = item.querySelector('.img_slide');
        if (img.getAttribute('src') === '' || img.getAttribute('src') === null) {
            item.remove();
            if (indicatorsButtons[index]) {
                indicatorsButtons[index].remove();
            }
        }
    });

    // Réinitialisation des indices des slides et indicateurs
    var remainingSlides = carouselInner.querySelectorAll('.carousel-item');
    var remainingIndicators = indicators.querySelectorAll('button');

    remainingSlides.forEach(function (item, index) {
        if (index === 0) {
            item.classList.add('active');
        } else {
            item.classList.remove('active');
        }
    });

    remainingIndicators.forEach(function (button, index) {
        button.setAttribute('data-bs-slide-to', index);
        button.setAttribute('aria-label', 'Slide ' + index);
        if (index === 0) {
            button.classList.add('active');
        } else {
            button.classList.remove('active');
        }
    });

    // Réinitialise le carrousel
    new bootstrap.Carousel(carousel);
});