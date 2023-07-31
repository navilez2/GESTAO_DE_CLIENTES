//Developed by: Nathan Alebrgoni Avilez
class TabPage {


    constructor(container) {
        this.container = container;
    }

    //Configuração inicial do componente
    Init() {
        let abas = Array.from(this.container.querySelectorAll('[data-type="header-button"]'));
        let defaultPage = Array.from(this.container.querySelectorAll('[data-default-page="true"]'));

        abas.forEach((element) => {

            //Ao iniciar a pagina, a pagina default é aprimeira que aparece
            if (element.getAttribute('data-default-page') == 'true') {
                (document.getElementById(element.getAttribute('data-target'))).style = 'display:contents'
            }
            else {
                (document.getElementById(element.getAttribute('data-target'))).style = 'display: none'
            }

            //Adiciona ao evento de click as paginas que irão aparecer para determinado Header
            element.addEventListener('click', () => {
                let page = document.getElementById(element.getAttribute('data-target'))

                //Abas que foram clicadas
                page.style = 'display:contents'
                element.classList.add('disable', 'active');

                //Abas que nao foram clicadas
                abas.forEach((aba) => {
                    if (element.textContent != aba.textContent) {
                        let page = document.getElementById(aba.getAttribute('data-target'))
                        aba.classList.remove('disable', 'active')
                        page.style = 'display:none'
                    }
                })
            })
        });




    }

}