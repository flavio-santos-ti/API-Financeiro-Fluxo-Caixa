# API - Fluxo de Caixa Financeiro Básico
<p><i>Repositório para versionamento e documentação básica do projeto Fluxo de Caixa no GitHub.</i></p>

## Sobre o projeto

Este é um repositório para mostrar a implementação e o funcionamento de uma aplicação do tipo Web API onde um comerciante precisa controlar seu fluxo de caixa diário com os lançamentos (débitos e créditos) como também um relatório que disponibilize o saldo consolidado.

Nessa primeira versão, para fins didáticos e também como utilizei apenas do meu tempo livre disponível, o relatório será apenas um retorno em JSON, mas em futuro próximo estarei implemetando o retorno de um arquivo PDF representando o relatório.

## Tecnologias utilizadas



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

## Arquitetura

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

## Banco de Dados

O SGBD que estamos utilizando nesse projeto é o [PostgreSQL](https://www.postgresql.org/) e instalado em um servidor [Linux Ubuntu Server](https://ubuntu.com/download/server), mas poderá ser instalado em uma maquina Windows também. 
Para a criação do banco e suas respectivas tabelas, estou utilizado a ferramenta [Pg Admin 4](https://www.pgadmin.org/download/pgadmin-4-windows/).

### Scripts

```sql
CREATE DATABASE financeiro TEMPLATE = template0 LC_CTYPE = "pt_BR.UTF-8" LC_COLLATE = "pt_BR.UTF-8";
```




Your new HTML website will immediately have publishable and documented code, and all these features:

 - Deploy automatically using GitHub Pages
 - Test locally with VS Code and the Tasks feature (uses `rake` behind scenes)
 - Clean folder structure
 - Automatic testing using GitHub Actions
 - Tests for broken links, broken HTML and other problems
 - HTTPS by default
 - Documentation for contributors

## How to use this

First clone or [download a release](https://github.com/fulldecent/html-website-template/releases), that is the starting point for your site.

THIS LIST IS EASY, CHECK IT OFF ONE-BY-ONE BABY!

 - [ ] Open `index.html` in your favorite text editor and make a great web page, add other content if necessary.
 - [ ] Fix all validation testing errors (see **Build instructions**, below)
 - [ ] Upload your website source code to GitHub or other collaboration point
 - [ ] Replace all details below, inspire people to contribute to your project.
 - [ ] Update the release script in `Rakefile` with details to publish to your server.
 - [ ] Delete all this crap up here.
 - [ ] Publish the site (full steps are under **Deploy** below in case you forget)
 - [ ] Set up HTTPS on your website, some [hints are here](https://github.com/fulldecent/html-website-template/wiki/How-to-set-up-HTTPS)

THEN YOU'RE DONE, GO STAR [html-website-template](https://github.com/fulldecent/html-website-template) FOR UPDATES.

---

# My First Website About Horses

[![CI Status](http://img.shields.io/travis/fulldecent/html-website-template.svg?style=flat)](https://travis-ci.org/fulldecent/html-website-template)

This website is published at https://example.com/horses/

![screen shot 2017-03-16 at 6 30 58 pm](https://cloud.githubusercontent.com/assets/382183/24021325/cb3aaa9a-0a76-11e7-8182-6138b1d3c0c2.png)

## Mission

This website exists to help educate the world about horses. There are so many kinds of horses and they are all just so magical. After you read these pages you will definitely want to get one for yourself!

## Build instructions

We test and publish this website using a few simple tools. Please set up these tools (takes about 3 minutes) to contribute seriously to our project:

1. Set up Ruby on your system
   * For macOS, open Terminal.app and paste in these commands
     * [Install Homebrew](https://brew.sh/) (click the link and paste that one command into Terminal.app)
     * `brew install ruby`
   * For Linux Mint, open Terminal and paste in these commands
     * `sudo apt-get install ruby-dev build-essential libxml2 libxslt-dev libcurl4-openssl-dev`
2. `gem update --system`
2. `gem install bundler`
3. `export NOKOGIRI_USE_SYSTEM_LIBRARIES=true`
3. `bundle install`

Now you are done setting up. Use this command to build the website.

```sh
bundle exec rake build
```

You can now access the website by pointing your browser to the `BUILD` folder or running a command like `cd BUILD; php -S localhost:8000`.

Also, you can check for common problems on our website automatically, just run this command.

```sh
bundle exec rake test
```

## Deploy instructions

Use this command to publish the website online to our server.

```sh
bundle exec rake publish
```

You can only run that command if you have authorized SSH keys on your computer.

## Author

Mary Smith and [other contributors](https://github.com/fulldecent/html-website-template/graphs/contributors) made this website with love.

## License

Copyright 2017 Mary Smith. All rights reserved.

