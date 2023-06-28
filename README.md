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

[ContraCheque](https://localhost:32033/swagger/index.html/)

### Tecnologias utilizadas
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

- Clonar o projeto
- Na raiz do projeto, onde está o arquivo .sln executar o seguinte comando: 
  - ``docker-compose up`` .
Este comando executa o build da aplicação, baixa as imagens necessarias ( sqlserver) e sobe os containers.
	
