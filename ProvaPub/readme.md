### Parte1Controller
Esse controller foi criado para gerar uma API que sempre retorna um n�mero aleat�rio. 
Voc� pode v�-lo funcionando ao rodar o projeto e na p�gina do Swagger, clique em Parte 1 > Try it Out > Execute.

Esse c�digo, no entanto, tem algum problema: ele sempre retorna o mesmo valor.
Seu trabalho, portanto, � corrigir esse comportamente: cada vez que a chamada � realizada um n�mero diferente dever� ser retornado.

### Parte2Controller
Essa API deveria retornar os produtos cadastrados de forma paginada. O usu�rio informa a p�gina (page) desejada e o sistema retorna os 10 itens da mesma.
O problema � que n�o importa qual n�mero de p�gina � utilizado: os resultados est�o vindo sempre os mesmos. E n�o apenas os 10.

Voc� precisa portanto:
1. Corrigir o bug de pagina��o. Ao passar page=1, deveria ser retornado os 10 (0 a 9) primeiros itens do banco. Ao passar page=2 deveria ser retornado os itens subsequentes (10 a 19), etc
2. Note que na Action do Controller, chamamos o ```ProductService```. Fazemos isso, instanciando o mesmo (```var productService = new ProductService(_ctx);```). Essa � uma pr�tica ruim. Altere o c�digo para que utilize Inje��o de Depend�ncia.
3. Agora, explore os arquivos ```/Models/CustomerList``` e ```/Models/ProductList```. Eles s�o bem parecidos. De fato, deve haver uma forma melhor de criar esses objetos, com menos repeti��o de c�digo. Fa�a essa altera��o.
4. Da mesma forma, como voc� melhoraria o ```CustomerService```e o ```ProductService``` para evitar repeti��o de c�digo?

### Parte3Controller
Essa API cria o pagamento de uma compra (```PlaceOrder```). Verifique o m�todo ```PayOrder``` da classe ```OrderService```.
Voc� deve ter percebido que existem diversas formas de pagamento (Pix, cart�o de cr�dito, paypal), certo?
Essa classe, no entanto, � problem�tica. Imagine que ter�amos que incluir um novo m�todo de pagamento, seria mais um ```if```na estrutura.

Voc� precisa:
1. Fa�a uma altera��o na arquitetura para que fique mais bem estruturado e preparado para o futuro.
Tenha certeza que o princ�pio Open-Closed ser� respeitado.

### Parte4Controller
Essa API faz uma valida��o de neg�cio e retorna se o consumidor pode realizar uma compra.
Verifique o arquivo ```CanPurchase``` da classe ```CustomerService``` e note que ele aplica diversas regras de neg�cio.

Seu trabalho aqui ser�:
1. Crie testes unit�rios para este m�todo. Tente obter o m�ximo de cobertura poss�vel. Se precisar, pode rearquitetar o c�digo para facilitar nos testes.

Voc� pode utilizar qualquer framework de testes que desejar. 

## Como entregar
Oba! Terminou tudinho? Agora fa�a o seguinte:
1. Fa�a ```push``` para seu reposit�rio (sim, aquele que voc� criou l� em cima. Nada de fork).
2. Forne�a acesso ao reposit�rio no GitHub para o usu�rio ```sandercamargo```
2. Preencha o formul�rio abaixo:
[https://forms.gle/mHipmDJJnij7FRHE7](https://forms.gle/mHipmDJJnij7FRHE7)

A gente te responde em breve, ok?