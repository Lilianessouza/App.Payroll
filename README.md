# Folha de Pagamento

Api para gerir as informações do contra cheque do funcionario 
Informações:	
- Salario Liquido
- Descontos em folha
 	- ``INSS``
	- ``IRPF``
	- `` FGTS``
	- `` Beneficios Descontados ( vale transporte, plano dental e plano de saúde )``

### Pré-requisitos

Para executar o projeto é preciso ter o docker e docker compose instalados e configurado na máquina.

[Download Docker](https://docs.docker.com/get-docker/) 

Nele já estara a imagem do SqlServer e o build da aplicação que deverá rodar atraves da Url: 

[Payroll](https://localhost:32033/swagger/index.html/)

### Tecnologias e Frameworks utilizados
- ``.NetCore 6``
- ``Sql Server 2022``
- ``Entity Framework``
- ``Fluent Validation``
- ``AutoMapper``
- ``Nunit``
- ``Moq``

##  Funcionalidades do projeto

- `employee/insert`: Insere um Funcionario na base de dados e retorna o Id cadastrado
- `employee/update`: Atualizada Funcionario na base de dados, podendo desativar o mesmo atraves do campo actived
- `employee/delete`: Exclui o cadastro do funcionario da base
- `employee/getAllEmployee`: Busca funcionario por Id cadastrado
- `employee/getEmployeeById`: Busca Todos Funcionario cadastrados na base
- `payslip/getPayslipAllEmployee`: Busca todos os Funcionario e retorna o calculdo do contracheque de cada um
- `payslip/getPayslipByIdEmployee`: Busca o Funcionario por Id informado e retorna o calculdo do contracheque 
expondo as informações de desconto e salario liquido do funcionario

## Executando o projeto 

Via Docker
- Clonar o projeto
- Na raiz do projeto, onde está o arquivo .sln executar o seguinte comando para build: 
  	- docker-compose build
 - para executar via docker e subir o banco
	- docker-compose up
 - 
Manual via linha de comando
- Criar banco de dados
 	- Após clonar o projeto será necessario criar o banco de dados no sqlServer.
	o script de criação está em ./schemas/create.sql.
	O script cria o banco de dados dbpayroll, a tabela employee e a migrations.
  	-No projeto, dentro da pasta /Api verificar o arquivo appsettings.json e alterar as credenciais de string de conexão do banco para o usuário que ira acesswar o banco (grants de insert,selct,update e delete).
- Executar Api
 	- No terminal do windos cmd valide se tem a versão do dotnet instalação
    		 ``dotnet --version``
     caso não tenha ele instalado verifique o link [Instalar .net 6](https://learn.microsoft.com/pt-br/dotnet/core/install/windows?tabs=net60/)
	- Faça o  build da solução
 		``dotnet build App.sln``
    	- entre dentro da  pasta Api
     		``cd /api``
     	- Execute para executar o projeto ``dotnet run``
      	- O Projeto irá subir na url : (https://localhost:32033/swagger/index.html)  			
