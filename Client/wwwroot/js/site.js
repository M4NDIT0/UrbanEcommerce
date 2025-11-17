window.initCarousel = () => {
    try {
        console.log('Inicializando carousel...');
        const carouselElement = document.getElementById('carouselExampleIndicators');
        if (!carouselElement) {
            console.error('Elemento del carousel no encontrado');
            return;
        }
        if (!window.bootstrap) {
            console.error('Bootstrap no está definido');
            return;
        }
        var myCarousel = new bootstrap.Carousel(carouselElement, {
            interval: 3000,
            wrap: true
        });
        console.log('Carousel inicializado correctamente');
    } catch (error) {
        console.error('Error al inicializar el carousel:', error);
    }
};

window.scrollToElement = (elementId) => {
    const element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
};