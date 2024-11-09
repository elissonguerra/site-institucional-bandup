const banner = document.querySelector('.banner');

window.addEventListener('scroll', () => {
    const posicaoScroll = window.scrollY;
    const minScale = 0.5;
    const maxScale = 1;
    const maxBorderRadius = 100;
    const valorEscala = Math.max(minScale, maxScale - posicaoScroll / 3000);
    const valorBorderRadius = Math.min(maxBorderRadius, posicaoScroll / 60);

    banner.style.transform = `scale(${valorEscala})`;
    banner.style.borderRadius = `${valorBorderRadius}px`;
});

const proximoBtn = document.getElementById('proximo-btn');
const anteriorBtn = document.getElementById('anterior-btn');
const containerWrapper = document.querySelector('.cards-container-wrapper');
const modal = document.getElementById('modal');
const modalBackdrop = document.getElementById('modal-backdrop');
const modalText = document.getElementById('modal-text');
const closeBtn = document.querySelector('.modal .close');
let containerAtual = 0;
let touchStartX = 0;
let touchEndX = 0;

function calculaLarguraTotalDoCard() {
    const card = containerWrapper.children[0];
    const cardStyle = window.getComputedStyle(card);
    const cardWidth = card.offsetWidth;
    const cardMarginRight = parseFloat(cardStyle.margin);
    return cardWidth + cardMarginRight * 2;
}

function atualizarBotoes() {
    anteriorBtn.disabled = containerAtual === 0;
    proximoBtn.disabled = containerAtual === totalContainers() - 1;
    anteriorBtn.style.opacity = anteriorBtn.disabled ? 0.5 : 1;
    proximoBtn.style.opacity = proximoBtn.disabled ? 0.5 : 1;
}

function totalContainers() {
    if (window.innerWidth >= 2000) return containerWrapper.children.length - 3;
    if (window.innerWidth <= 525) return containerWrapper.children.length;
    if (window.innerWidth <= 1160) return containerWrapper.children.length - 1;
    if (window.innerWidth < 2000 && window.innerWidth >= 1665) return containerWrapper.children.length - 3;
    return containerWrapper.children.length - 2;
}

function moverContainer(direcao) {
    const larguraTotalDoCard = calculaLarguraTotalDoCard();
    containerAtual += direcao;
    containerAtual = (containerAtual + totalContainers()) % totalContainers();
    containerWrapper.style.transform = `translateX(-${containerAtual * larguraTotalDoCard}px)`;
    atualizarBotoes();
}

proximoBtn.addEventListener('click', () => moverContainer(1));
anteriorBtn.addEventListener('click', () => moverContainer(-1));
atualizarBotoes();

function openModal(content) {
    modalText.innerHTML = content;
    modal.classList.add('active');
    modalBackdrop.classList.add('active');
    document.body.classList.add('modal-active');
}

function closeModal() {
    modal.classList.remove('active');
    modalBackdrop.classList.remove('active');
    document.body.classList.remove('modal-active');
}

document.querySelectorAll('.maisInfo-btn').forEach((btn, index) => {
    btn.addEventListener('click', () => {
        const content = btn.getAttribute('data-modal-content') || `Conteúdo do modal para o card ${index + 1}`;
        openModal(content);
    });
});

closeBtn.addEventListener('click', closeModal);
modalBackdrop.addEventListener('click', closeModal);

const paragrafos = document.querySelectorAll(".efeito-fadeIn");

document.addEventListener("scroll", function() {
    paragrafos.forEach(paragrafo => {
        if (visivel(paragrafo)) {
            paragrafo.classList.add("efeito-fadeIn--visible")
        }
    })
});

function visivel(elemento) {
    const rect = elemento.getBoundingClientRect();
    return (rect.bottom > 0 && rect.top < (window.innerHeight - 150 || document.documentElement.clientHeight - 150));
}

containerWrapper.addEventListener('touchstart', (event) => {
    touchStartX = event.touches[0].clientX;
});

containerWrapper.addEventListener('touchmove', (event) => {
    touchEndX = event.touches[0].clientX;
});

containerWrapper.addEventListener('touchend', () => {
    const touchDiff = touchStartX - touchEndX;

    if (touchDiff > 50) {
        moverContainer(1);
    } else if (touchDiff < -50) {
        moverContainer(-1);
    }
});
