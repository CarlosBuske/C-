Comandos SQl Utilizados - Carlos Eduardo de Almeida Buske

create database TizerTI

CREATE TABLE Empresa(
    Id_Empresa int identity(1,1),
	RazaoSocial varchar(30),
	Cnpj varchar(18),
	Cidade varchar(50),
	UF varchar(5),
    PRIMARY KEY (Id_Empresa)
);

create table Funcionarios(
Id_Funcionario int identity(1,1),
Id_Empresa int,
NomeEmpresa nvarchar(100),
Nome nvarchar(100),
CPF varchar(14),
Telefone nvarchar(30),
Salario decimal,
DataAdmissao nvarchar(30),
DataDemissao nvarchar(30),
Status nvarchar(30),
Primary key(Id_Funcionario)
)

alter table Funcionarios add constraint Id_Empresa foreign key (Id_Empresa) references Empresa (Id_Empresa)

select * from Empresa

select * from Funcionarios