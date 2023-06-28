# Folha de Pagamento

Api para gerir as informa��es do contra cheque do funcionario 
Informa��es:	
	- Salario Liquido
	- Descontos em folha
		- Inss
		- IrPf
		- Fgts
		- Beneficios ( vale alimenta��o, vale transporte e plano de sa�de )

### Pr�-requisitos

Para executar o projeto � preciso ter o docker e docker compose instalados e configurado na m�quina.

[Download Docker](https://docs.docker.com/get-docker/) 

Nele j� estara a imagem do SqlServer e o build da aplica��o que dever� rodar atraves da Url: 
https://docs.docker.com/get-docker/
link: https://localhost:32033/swagger/index.html
[ContraCheque](https://localhost:32033/swagger/index.html)

### Tecnologias utilizadas
- ``.NetCore 6``
- ``Sql Server 2022``
- ``Entity Framework``
- ``Fluent Validation``
- ``AutoMapper``
- ``Nunit``
- ``Moq``

## ?? Funcionalidades do projeto

- `employee/insert`: Insere um Funcionario na base de dados e retorna o Id cadastrado
- `employee/update`: Atualizada Funcionario na base de dados, podendo desativar o mesmo atraves do campo actived
- `employee/delete`: Exclui o cadastro do funcionario da base
- `employee/getAllEmployee`: Busca funcionario por Id cadastrado
- `employee/getEmployeeById`: Busca Todos Funcionario cadastrados na base
- `payslip/getPayslipAllEmployee`: Busca todos os Funcionario e retorna o calculdo do contracheque de cada um
- `payslip/getPayslipByIdEmployee`: Busca o Funcionario por Id informado e retorna o calculdo do contracheque 
expondo as informa��es de desconto e salario liquido do funcionario

## ?? Executando o projeto 

- Clonar o projeto
- Na raiz do projeto, onde est� o arquivo .sln executar o comando
-  - docker-compose up para fazer o build da aplica��o, baixar as imagens necessarias ( sqlserver) e subir os containers.
	- 
