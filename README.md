# API - Fluxo de Caixa Financeiro Básico
<p><i>Repositório para versionamento e documentação básica do projeto Fluxo de Caixa no GitHub.</i></p>

| Autor | Última alteração     |
| :------------- | :------------- |
| Flávio dos Santos   | 26 Fevereiro de 2019 |
## 1 - Sobre o projeto

Este é um repositório para mostrar a implementação e o funcionamento de uma aplicação do tipo Web API onde um comerciante precisa controlar seu fluxo de caixa diário com os lançamentos (débitos e créditos) como também um relatório que disponibilize o saldo consolidado.

Nessa primeira versão, para fins didáticos e também como utilizei apenas do meu tempo livre disponível, o relatório será apenas um retorno em JSON, mas em futuro próximo estarei implemetando o retorno de um arquivo PDF representando o relatório.

## 2 - Tecnologias utilizadas



<p display="inline-block">
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403488-185ae58f-8d9f-4893-8516-e2e9d53bdded.png" alt="csharp-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403370-29d0ab19-e406-4581-bc98-838691b4968a.png" alt="fluentvalidation-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403187-df0d20a4-d15b-4f68-b449-450500d1ad49.png" alt="automapper-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403028-b4f6ceec-b1b4-48d9-8fca-4a2adab8227f.png" alt="dapper-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403962-c5b539cf-1f73-4fbf-8937-507f6956b540.png" alt="postgresql-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221404176-630c9bc1-de1c-4b1b-ad74-8e26bc07b6cb.png" alt="dotnetcore-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221405368-aeaed761-e962-4a3b-bc9c-7a5c7f1543b5.png" alt="docker-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221405737-87bc0545-83ea-49cb-8e6d-d5dbb06f91dc.png" alt="ubuntu-logo"/>
</p>

 - C#;
 - Fluent Validation;
 - Auto Mapper;
 - Dapper;
 - PostgreSQL;
 - EF .NET Core 6;
 - Visual Studio 2022;
 - Docker;
 - Linux Ubuntu Server.

## 3 - Arquitetura

<p>
  <img width="480" src="https://user-images.githubusercontent.com/62816438/221408389-4b7a39fe-f81a-4d5a-b7fe-d826ba50ad06.png" alt="arquitetura"/>
</p>


### 1 - Application 

A camada **application** Tem a função de receber todas as requisições http e direcioná-las para a camada **business** para aplicar as validações e regras de negócio.

### 2 - Domain 

É a área de definição dos modelos, entidades, DTOs e Interfaces.

### 3 - Business

Na **business**, concentramos toda a regra de negócio do domínio.

### 4 - Infrastructure

Dividida em duas subcamadas, o Data, onde são realziadas as persistênciasno banco de dados, utilizando ou não algum ORM e a camada **Cross-Cutting**, uma camada destinada a ser utilizada para consumo de API externas.

## 4 - Banco de Dados

O SGBD que estamos utilizando nesse projeto é o [PostgreSQL](https://www.postgresql.org/) e o mesmo estava instalado instalado em um servidor [Linux Ubuntu Server](https://ubuntu.com/download/server), mas nada impede que seja instalado localmente em uma maquina Windows 10 Desktop. 
Para a criação do banco e a a execução dos seus respectivos scripts DDL, utilizamos a ferramenta [Pg Admin 4](https://www.pgadmin.org/download/pgadmin-4-windows/). 

### 4.1 - Modelagem


### 4.2 - Scripts

Scripts necessários para a criação do banco de dados esuas respecitvas  tabelas:

#### 4.2.1 Banco de Dados

```sql
CREATE DATABASE financeiro TEMPLATE = template0 LC_CTYPE = "pt_BR.UTF-8" LC_COLLATE = "pt_BR.UTF-8";
```

##### 4.2.2 - Pessoa

```sql
CREATE TABLE IF NOT EXISTS public.pessoa
(
	id BIGSERIAL NOT NULL,
	nome CHARACTER VARYING(50) NOT NULL,
    hash_nome CHARACTER VARYING(32) NOT NULL,
	dt_inclusao TIMESTAMP WITH TIME ZONE NOT NULL,
	CONSTRAINT pk_pessoa PRIMARY KEY(id)
);

CREATE INDEX ix_pessoa_hash_nome ON public.pessoa( hash_nome );
```

#### 4.2.3 - Cliente

```sql
CREATE TABLE IF NOT EXISTS public.cliente
(
	id BIGSERIAL NOT NULL,
	pessoa_id BIGINT NOT NULL,
	dt_inclusao TIMESTAMP WITH TIME ZONE NOT NULL,
	CONSTRAINT pk_cliente PRIMARY KEY(id), 
	CONSTRAINT fk_cliente_pessoa FOREIGN KEY (pessoa_id) REFERENCES public.pessoa(id)
);
```

#### 4.2.4 - Fornecedor

```sql
CREATE TABLE IF NOT EXISTS public.fornecedor
(
	id BIGSERIAL NOT NULL,
	pessoa_id BIGINT NOT NULL,
	dt_inclusao TIMESTAMP NOT NULL,
	CONSTRAINT pk_fornecedor PRIMARY KEY(id), 
	CONSTRAINT fk_fornecedor_pessoa FOREIGN KEY (pessoa_id) REFERENCES public.pessoa(id)
);
```

#### 4.2.5 - Categoria

```sql
CREATE TABLE IF NOT EXISTS public.categoria
(
    id SERIAL NOT NULL,
    nome CHARACTER VARYING(50) NOT NULL,
    tipo CHAR(1) NOT NULL,
	CONSTRAINT pk_categoria PRIMARY KEY(id) 
);

COMMENT ON TABLE public.categoria IS 'Categoria de Títulos. Tipo: E = Entrada e S = Saída';
```

#### 4.2.6 - Extrato
```sql
CREATE TABLE IF NOT EXISTS public.extrato 
(
    id BIGSERIAL NOT NULL,
    tipo CHAR(1) NOT NULL,
    descricao CHARACTER VARYING(50) NOT NULL,
    valor DECIMAL NOT NULL,
    saldo DECIMAL NOT NULL,
    valor_relatorio DECIMAL NOT NULL,
    dt_extrato TIMESTAMP NOT NULL,
    dt_inclusao TIMESTAMP NOT NULL,
	CONSTRAINT pk_extrato PRIMARY KEY(id) 
);

COMMENT ON TABLE public.categoria IS 'Extrato - Tipo: D = Débito e C = Crédito';
```

#### 4.2.7 - Saldos Diários

```sql
CREATE TABLE IF NOT EXISTS public.saldo_diario 
(
    id BIGSERIAL NOT NULL,
    dt_saldo TIMESTAMP NOT NULL,
    tipo CHAR(1) NOT NULL,
    valor DECIMAL NOT NULL,
    dt_inclusao TIMESTAMP NOT NULL,
    extrato_id BIGINT NOT NULL,
	CONSTRAINT pk_saldo_diario PRIMARY KEY(id), 
	CONSTRAINT fk_sado_diario_extrato FOREIGN KEY (extrato_id) REFERENCES public.extrato(id)
);

CREATE INDEX ix_saldo_diario_periodo ON public.saldo_diario( dt_saldo, tipo );

COMMENT ON TABLE public.saldo_diario IS 'Extrato - Tipo: I = Inicial e F = Final';
```

#### 4.2.8 - Títulos a Pagar

```sql
CREATE TABLE IF NOT EXISTS public.titulo_pagar
(
    id BIGSERIAL NOT NULL,
    categoria_id INT NOT NULL,
    fornecedor_id BIGINT DEFAULT NULL,
    descricao CHARACTER VARYING(50) NOT NULL,
    valor_real DECIMAL NOT NULL,
    dt_real TIMESTAMP,
    dt_inclusao TIMESTAMP NOT NULL,
    extrato_id BIGINT NOT NULL,
	CONSTRAINT pk_titulo_pagar PRIMARY KEY(id), 
	CONSTRAINT fk_titulo_pagar_categoria FOREIGN KEY (categoria_id) REFERENCES public.categoria(id),
	CONSTRAINT fk_titulo_pagar_fornecedor FOREIGN KEY (fornecedor_id) REFERENCES public.fornecedor(id),
	CONSTRAINT fk_titulo_pagar_extrato FOREIGN KEY (extrato_id) REFERENCES public.extrato(id)
);
```

#### 4.2.9 - Títulos a Receber
```SQL
CREATE TABLE IF NOT EXISTS public.titulo_receber
(
    id BIGSERIAL NOT NULL,
    categoria_id INT NOT NULL,
    cliente_id BIGINT DEFAULT NULL,
    descricao CHARACTER VARYING(50) NOT NULL,
    valor_real DECIMAL NOT NULL,
    dt_real TIMESTAMP,
    dt_inclusao TIMESTAMP NOT NULL,
    extrato_id BIGINT NOT NULL,
    CONSTRAINT pk_titulo_receber PRIMARY KEY(id), 
    CONSTRAINT fk_titulo_receber_categoria FOREIGN KEY (categoria_id) REFERENCES public.categoria(id),
    CONSTRAINT fk_titulo_receber_cliente FOREIGN KEY (cliente_id) REFERENCES public.cliente(id),
    CONSTRAINT fk_titulo_receber_extrato FOREIGN KEY (extrato_id) REFERENCES public.extrato(id)
);
```









