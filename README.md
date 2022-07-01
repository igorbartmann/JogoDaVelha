# JogoDaVelha

Este projeto foi desenvolvido utilizando o template padrão de Aplicativo do Console para atender o objetivo de rodá-lo como um sistema CLI.
O projeto visa o desenvolvimento do jogo da velha no formato digital.

<br/>

> Regras:
* O jogo ocorre em um tabuleiro 3x3;
* O jogo será para duas pessoas jogarem, alternadamente;
* O jogador 1, sempre será o X e sempre iniciará o jogo;
* O jogador 2, sempre será a O e sempre será o segundo a jogar;
* O jogo pode ter 3 resultados: vitória do jogador 1, vitória do jogador 2 ou empate;
* Ganha o jogador que primeiro formar uma reta na diagonal, vertical ou horizontal do tabuleiro.

<br/>

> Como utilizar o aplicativo:
* Ao rodar o aplicativo, será aberto o menu principal, no qual é possível selecionar a opção 'Jogar', digitando o valor 1 (um) ou 'Sair', digitando o valor 2 (dois).
* Ao digitar o valor 1 (um), será aberta a tela do jogo, na etapa de configuração. Nesta fase, o usuário deve informar os nomes dos jogadores conforme solicitado pelo aplicativo. Realizada a configuração do jogadores, o jogo irá iniciar automaticamente, exibindo o tabuleiro e solicitando, de forma alternada, que os jogadores realizem as jogadas.  Para efetuar uma jogada, basta informar o número da linha (horizontal) e o número da coluna (vertical) desejada, com valores entre 1 e 3 para cada uma das opções, pois temos como base um tabuleiro de dimensão 3x3.
* No decorrer do jogo, o tabuleiro é atualizado, exibindo sempre o estado atual para os jogadores. Quando um jogador formar uma reta, de forma a ganhar o jogo, automaticamente o sistema cessará a solicitação de inputs para as jogadas e informará os dados do jogador vencedor. Entre os dados exibidos estão: nome, quantidade de vitórias, quantidade de empates e quantidade de derrotas, de acordo com os resultados que o jogador obteve ao longo das partidas.
* Após a exibição dos dados, o sistema lançará uma pergunta, visando definir se o usuário deseja jogar uma nova partida. Caso a resposta seja 'Sim', opção de número 1 (um), então o tabuleiro será reinicializado e uma nova partida iniciará, mantendo em memória os dados dos jogadores. Caso a resposta seja 'Não', opção de número 2 (dois), o usuário voltará para o menu principal do aplicativo, no qual poderá, novamente, selecionar se deseja jogar ou sair do aplicativo. Caso o usuário opte por sair, então será exibida a tela de finalização do aplicativo e o mesmo terá a sua execução finalizada.
