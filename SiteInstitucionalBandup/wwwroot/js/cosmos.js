    //CONTEÚDOS NOS IDIOMAS DIFERENTES
    const translations = {
        'PT-BR': {
            title: 'COSMOS',
            mainTitle: 'A fusão dos geeks: Piticas e BandUp fazem joint venture e criam a Cosmos',
            quemSomos: 'QUEM SOMOS?',
            quemSomosTexto: 'Cosmos é a fusão entre duas gigantes do mercado: BandUP! e Piticas. Juntas, essas empresas líderes no ramo da música e moda casual expandiram sua oferta de produtos e serviços, tornando-se uma referência internacional. Com uma ampla gama de produtos licenciados e uma presença marcante em eventos, o Grupo Cosmos é reconhecido por sua excelência, inovação e amor pela cultura pop e geek.',
            oQueFazemos: 'O QUE FAZEMOS?',
            oQueFazemosTexto: 'O Grupo Cosmos é o epicentro da cultura pop e do entretenimento, criando experiências excepcionais para os fãs em todo o mundo. Nós oferecemos uma vasta gama de produtos e serviços, desde roupas estilosas até itens de decoração e colecionáveis, tudo com a qualidade e autenticidade que os fãs merecem. Com uma rede de licenciamentos ampla e colaborações com marcas renomadas, estamos sempre inovando e expandindo nossa presença em eventos e convenções, consolidando nosso compromisso em entregar o melhor da cultura pop para nossos clientes.',
            objetivos: 'OBJETIVOS',
            objetivosTexto: 'O Grupo Cosmos visa promover a sinergia entre as empresas Piticas e BandUP para expandir os negócios, diversificar a oferta de produtos, promover a inovação colaborativa e impulsionar o crescimento sustentável a longo prazo. Com uma abordagem estratégica e orientada para resultados, o grupo está comprometido em alcançar a excelência operacional e moldar o futuro do mercado.',
            ancoraTC: 'Trabalhe Conosco',
            ancoraLojas: 'Lojas',
            ancoraMarcas: 'aMarcas',
            ancoraEventos: 'Eventos',
            ancoraHistoria: 'Nossa História',
            ancoraB2B: 'Seja um cliente B2B',
            aberturaTtl: 'BEM-VINDO,<br>COSMONAUTAS!',
            aberturaSt: 'TODOS ABORDO E AVANTE!'
    
        },
        'EN': {
            title: 'COSMOS',
            mainTitle: 'The geek fusion: Piticas and BandUp form a joint venture and create Cosmos',
            quemSomos: 'WHO ARE WE?',
            quemSomosTexto: 'Cosmos is the merger between two giants of the market: BandUP! and Piticas. Together, these leading companies in the music and casual fashion industry have expanded their product and service offerings, becoming an international reference. With a wide range of licensed products and a strong presence at events, the Cosmos Group is recognized for its excellence, innovation and love for pop culture and geek.',
            oQueFazemos: 'WHAT DO WE DO?',
            oQueFazemosTexto: 'The Cosmos Group is the epicenter of pop culture and entertainment, creating exceptional experiences for fans around the world. We offer a wide range of products and services, from stylish clothes to decorative items and collectibles, all with the quality and authenticity that fans deserve. With a wide network of licensing and collaborations with renowned brands, we are always innovating and expanding our presence at events and conventions, consolidating our commitment to deliver the best of pop culture to our clients.',
            objetivos: 'OBJECTIVES',
            objetivosTexto: 'The Cosmos Group aims to promote synergy between Piticas and BandUP companies to expand business, diversify product offerings, foster collaborative innovation and drive long-term sustainable growth. With a strategic and results-oriented approach, the group is committed to achieving operational excellence and shaping the future of the market.',
            ancoraTC: 'Work With Us',
            ancoraLojas: 'Our Stores',
            ancoraMarcas: 'Our Brands',
            ancoraEventos: 'Events',
            ancoraHistoria: 'Our History',
            ancoraB2B: 'Be a B2B Client',
            aberturaTtl: 'WELLCOME,<br>COSMONAUTAS!',
            aberturaSt: 'ALL ON BOARD AND ONWARD!'
        }
    };
    
    //FUNÇÃO PARA TROCAR O CONTEÚDO
    function mudaIdioma(language) {
        // Atualiza o texto do idioma selecionado
        document.getElementById('selected-lang').textContent = language;
        
        // Atualiza o título da página
        document.title = translations[language].title;
    
        // Atualiza o conteúdo principal
        document.getElementById('titulo-abertura').innerHTML = translations[language].aberturaTtl;
        document.getElementById('subtitulo-abertura').innerHTML = translations[language].aberturaSt;
        document.getElementById('menuTC').textContent = translations[language].ancoraTC;
        document.getElementById('menuLojas').textContent = translations[language].ancoraLojas;
        document.getElementById('menuMarcas').textContent = translations[language].ancoraMarcas;
        document.getElementById('menuEventos').textContent = translations[language].ancoraEventos;
        document.getElementById('menuHistoria').textContent = translations[language].ancoraHistoria;
        document.getElementById('menuB2B').textContent = translations[language].ancoraB2B;
        document.getElementById('titulo-principal').textContent = translations[language].mainTitle;
        document.querySelector('.texto-lateral h1').textContent = translations[language].quemSomos;
        document.querySelector('.texto-lateral p').textContent = translations[language].quemSomosTexto;
        document.querySelector('.parallax-conteudo h1').textContent = translations[language].oQueFazemos;
        document.querySelector('.parallax-conteudo p').textContent = translations[language].oQueFazemosTexto;
        document.querySelector('.segundoBloco-txt h1').textContent = translations[language].objetivos;
        document.querySelector('.segundoBloco-txt p').textContent = translations[language].objetivosTexto;
    }
    
    const paragrafos = document.querySelectorAll(".efeito-fadeIn");
    
    document.addEventListener("scroll", function() {
        paragrafos.forEach(paragrafo => {
            if (visivel(paragrafo)) {
                paragrafo.classList.add("efeito-fadeIn--visible")
            }
        })
    })
    
    function visivel(elemento){
        const rect = elemento.getBoundingClientRect();
        return (rect.bottom > 0 && rect.top < (window.innerHeight - 150 || document.documentElement.clientHeight - 150));
    }
    
    let menu = document.querySelector('#menu-icon');
    let menuItems = document.querySelector('.menu-items');
    let trocaLang = document.querySelector('.troca-lang');
    let dropdownLang = document.querySelector('.dropdown-lang');
    
    menu.onclick = () => {  
        if (dropdownLang.classList.contains('aberto')) {
            dropdownLang.classList.remove('aberto');
        }
        menuItems.classList.toggle('aberto');
    };
    
    trocaLang.onclick = () => {
        if (menuItems.classList.contains('aberto')) {
            menuItems.classList.remove('aberto');
        }
        dropdownLang.classList.toggle('aberto');
    };
    
    document.addEventListener('click', (event) => {
        if (!menu.contains(event.target) && !menuItems.contains(event.target)) {
            menuItems.classList.remove('aberto');
        }
    
        if (!trocaLang.contains(event.target) && !dropdownLang.contains(event.target)) {
            dropdownLang.classList.remove('aberto');
        }
    }); 
    
    
    
    