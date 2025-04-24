<h1 align="center">
  <img src="./assets/logo.png" height="300" width="300" alt="Logo Campus Connect" /><br>
</h1>

Com o objetivo de unificar e simplificar todo o processo de organização do transporte entre cidades e centros de ensino, o Campus Connect surge como alternativa tecnlogia para um problema que - até então - era resolvido de forma não muito tecnologica assim...

<!-- ![GitHub License](https://img.shields.io/github/license/Lucas-Woibau/Campus Connect?labelColor=101010) -->
<!-- ![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/Lucas-Woibau/Campus Connect/workflow.yml?style=flat&labelColor=101010) -->

## Stack

<!-- Front -->

![HTML](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![CSS](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
![Sass](https://img.shields.io/badge/Sass-CC6699?style=for-the-badge&logo=sass&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-323330?style=for-the-badge&logo=JavaScript&logoColor=F7DF1E)
![Bootstrap](https://img.shields.io/badge/Bootstrap-712cf9?style=for-the-badge&logo=bootstrap&logoColor=712cf9&color=fff)

<!-- Back/Framework -->

![C#](https://img.shields.io/badge/C%23-0077ff?style=for-the-badge&logo=csharp&logoColor=white)
![DotNET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

<!-- Server -->

![IIS](https://img.shields.io/badge/MICROSOFT%20IIS-0078D4?style=for-the-badge&logo=microsoft&logoColor=white)

<!-- DB Engine -->

![MySQL](https://img.shields.io/badge/mysql-4479A1.svg?style=for-the-badge&logo=mysql&logoColor=white)

<!-- Testing Stuff -->

<!-- ![Selenium](https://img.shields.io/badge/selenium-%43B02A?style=for-the-badge&logo=selenium&logoColor=white) -->
<!-- ![pytest](https://img.shields.io/badge/pytest-0094e7.svg?style=for-the-badge&logo=pytest&logoColor=ffffff) -->
<!-- ![Jest](https://img.shields.io/badge/jest-C21325?style=for-the-badge&logo=jest&logoColor=white) -->
<!-- ![Cypress](https://img.shields.io/badge/cypress-E5E5E5?style=for-the-badge&logo=cypress&logoColor=058a5e) -->
<!-- ![JUnit5](https://img.shields.io/badge/JUnit5-dc524a?style=for-the-badge&logo=JUnit5&logoColor=ffffff) -->

<!-- Host -->

<!-- ![Render](https://img.shields.io/badge/Render-46E3B7?style=for-the-badge&logo=render&logoColor=000&color=fff) -->
<!-- ![Vercel](https://img.shields.io/badge/vercel-000000.svg?style=for-the-badge&logo=vercel&logoColor=white) -->
<!-- ![Netlify](https://img.shields.io/badge/netlify-000000.svg?style=for-the-badge&logo=netlify&logoColor=#00C7B7) -->
<!-- ![Heroku](https://img.shields.io/badge/heroku-430098.svg?style=for-the-badge&logo=heroku&logoColor=white) -->
<!-- ![Firebase](https://img.shields.io/badge/firebase-ffaa00.svg?style=for-the-badge&logo=firebase&logoColor=ff0000) -->
<!-- ![Supabase](https://img.shields.io/badge/Supabase-181818?style=for-the-badge&logo=supabase&logoColor=3ecf8e) -->

<!-- Misc -->

<!-- ![ESLint](https://img.shields.io/badge/ESLint-4B3263?style=for-the-badge&logo=eslint&logoColor=white) -->
<!-- ![Babel](https://img.shields.io/badge/Babel-F9DC3e?style=for-the-badge&logo=babel&logoColor=black) -->
<!-- ![CodeCov](https://img.shields.io/badge/codecov-ff0077.svg?style=for-the-badge&logo=codecov&logoColor=white) -->
<!-- ![Swagger](https://img.shields.io/badge/Swagger-004400?style=for-the-badge&logo=swagger&logoColor=00ff00) -->
<!-- ![Material for MkDocs](https://img.shields.io/badge/Material%20for%20MkDocs-fff?style=for-the-badge&logo=material-for-mkdocs&logoColor=526cfe) -->

<!-- CI/CD -->

![GitHub](https://img.shields.io/badge/GitHub-fff?style=for-the-badge&logo=github&logoColor=181717)

<!-- ![GitHub Pages](https://img.shields.io/badge/GitHub%20Pages-fff?style=for-the-badge&logo=github-pages&logoColor=222222) -->
<!-- ![GitHub Actions](https://img.shields.io/badge/GitHub%20Actions-2088ff?style=for-the-badge&logo=github-actions&logoColor=fff) -->

## Arquitetura

Campus Connect é uma aplicação web desenvolvida para facilitar a gestão e integração de estudantes universitários que utilizam o transporte intermunicipal para instituições de ensino. O sistema centraliza informações de alunos de três cidades distintas, cruzando dados com rotas e linhas de transporte, a fim de otimizar a organização logística e o uso dos recursos disponíveis.

O projeto visa resolver desafios comuns enfrentados por estudantes que dependem de transporte coletivo entre cidades para frequentar suas aulas. Ao integrar dados de alunos, rotas e instituições, o Campus Connect fornece uma base sólida para gestores de transporte estudantil tomarem decisões mais eficientes, ao mesmo tempo em que oferece aos alunos uma experiência mais transparente e organizada.

### Estrutura MVC - ASP.Net Core

```mermaid
flowchart LR


Client((Client))

subgraph SYSTEM
    Model((Model))
    View[View]
    Controller{Controller}
    Database[(Database)]
end


Client --http:request--> Controller

Controller --data--> View
Controller --data request--> Model

Model --mysql:request--> Database

Database --mysql:response--> Model

Model --data response--> Controller
View --dynamic template--> Controller

Controller --http:response--> Client


style Client fill:#cfc,color:#070,stroke:#070;
style Controller fill:#ccf,color:#00a,stroke:#00a;
style Model fill:#ffc,color:#f70,stroke:#f70;
style Database fill:#ffc,color:#f70,stroke:#f70;
style View fill:#fcc,color:#f00,stroke:#f00;
style SYSTEM fill:#474a4a,color:#fff,stroke:#fff;

linkStyle default color:#fff
linkStyle 0,1,2,3 stroke:#0fa
linkStyle 4,5,6,7 stroke:#0aa
```

### Models

```mermaid
erDiagram
direction TB


ApplicationUser {
    string Nome "Not Null"
    string Sobrenome "Not Null"
    string Cidade "Not Null"
    string Rota "Not Null"
    string Instituicao "Not Null"
    string Matricula "Not Null"
    string Curso "Not Null"
    string Periodo "Not Null"
}

ProfileDto {
    string(100) Nome "Not Null"
    string(100) Sobrenome "Not Null"
    string(20) Telefone "Not Null"
    string(100) Email "Not Null"
    string(100) Cidade "Not Null"
    string(100) Rota "Not Null"
    string(100) Instituicao "Not Null"
    string NovaInstituicao "Not Null"
    string Matricula "Not Null"
    string(100) Curso "Not Null"
    string Periodo "Not Null"
}

RegisterDto {
    string(100) Nome "Not Null"
    string(100) Sobrenome "Not Null"
    string(100) Telefone "Not Null"
    string(100) Email "Not Null"
    string(100) Cidade "Not Null"
    string(100) Rota "Not Null"
    string(100) Instituicao "Not Null"
    string NovaInstituicao "Not Null"
    string Matricula "Not Null"
    string(100) Curso "Not Null"
    string Periodo "Not Null"
    string(100) Senha "Not Null"
    string ConfirmarSenha "Not Null"
}

PasswordDto {
    string(100) SenhaAtual "Not Null"
    string(100) NovaSenha "Not Null"
    string ConfirmarSenha "Not Null"
}
```

```mermaid
erDiagram
direction TB


PasswordResetDto {
    string Email "Not Null"
    string(100) Senha "Not Null"
    string ConfirmarSenha "Not Null"
}

LoginDto {
    string(100) Email "Not Null"
    string(100) Senha "Not Null"
    bool LembrarDeMim "Not Null"
}

SearchUsers {
    string Search "Nullable"
    string Rota "Nullable"
    string Cidade "Nullable"
    string Instituicao "Nullable"
}

ErrorViewModel {
    string RequestId "Nullable"
    bool ShowRequestId "Not Null"
}
```

### DB Relations

```mermaid
erDiagram
direction LR

AspNetUsers {
    string Id PK "Not Null"
    string Nome "Not Null"
    string Sobrenome "Not Null"
    string Cidade "Not Null"
    string Rota "Not Null"
    string Instituicao "Not Null"
    string Matricula "Not Null"
    string Curso "Not Null"
    string Periodo "Not Null"
    string UserName "Not Null"
    string NormalizedUserName "Not Null"
    string Email "Not Null"
    string NormalizedEmail "Not Null"
    bool EmailConfirmed "Not Null"
    string PasswordHash "Not Null"
    string SecurityStamp "Not Null"
    string ConcurrencyStamp "Not Null"
    string PhoneNumber "Not Null"
    bool PhoneNumberConfirmed "Not Null"
    bool TwoFactorEnabled "Not Null"
    datetime LockoutEnd "Not Null"
    bool LockoutEnabled "Not Null"
    int AccessFailedCount "Not Null"
}

AspNetRoles {
    string Id PK "Not Null"
    string Name "Nullable"
    string NormalizedName "Nullable"
    string ConcurrencyStamp "Nullable"
}

AspNetUserRoles {
    string UserId PK, FK "Not Null"
    string RoleId PK, FK "Not Null"
}

AspNetUserClaims {
    int Id PK "Not Null"
    string UserId FK "Not Null"
    string ClaimType "Not Null"
    string ClaimValue "Not Null"
}

AspNetRoleClaims {
    int Id PK "Not Null"
    string RoleId FK "Not Null"
    string ClaimType "Not Null"
    string ClaimValue "Not Null"
}

AspNetUserLogins {
    string LoginProvider PK "Not Null"
    string ProviderKey PK "Not Null"
    string ProviderDisplayName "Not Null"
    string UserId FK "Not Null"
}

AspNetUserTokens {
    string UserId PK, FK "Not Null"
    string LoginProvider PK "Not Null"
    string Name PK "Not Null"
    string Value "Not Null"
}

AspNetUsers ||--o{ AspNetUserClaims : has
AspNetUsers ||--o{ AspNetUserLogins : has
AspNetUsers ||--o{ AspNetUserTokens : has
AspNetRoles ||--o{ AspNetRoleClaims : has
AspNetUsers ||--o{ AspNetUserRoles : has
AspNetRoles ||--o{ AspNetUserRoles : has
```

## Execução

Antes de iniciar com o desenvolvimento e os comandos, é importante definir as variáveis de ambiente no seu ambiente de desenvolvimento. Abaixo a listagem de quais definir:

| Variável  | Tipo     | Necessidade            | Default | Descrição                  |
| :-------- | :------- | :--------------------- | :------ | :------------------------- |
| `EXAMPLE` | `string` | [Required \| Optional] | `Foo`   | Lorem ipsum dolor sit amet |

### Ação

`comando`

<!--
LISTA DE POSSÍVEIS AÇÕES

Linter
Checagem de Tipos
Conversão (e.g. TS -> JS)
Buscar/iniciar Migrações (Atualizações) de Banco de Dados
Atualizar Estrutura do Banco de Dados com Novas Migrações
Iniciar Testes Automatizados
Popular Banco de Dados para Execução Local
Iniciar o Servidor
 -->

<!-- ## To-Do List

- [ ] Lista
- [ ] de
- [ ] Tarefas -->

## Licença

This project is under [MIT - Massachusetts Institute of Technology](https://choosealicense.com/licenses/mit/). A short and simple permissive license with conditions only requiring preservation of copyright and license notices. Licensed works, modifications, and larger works may be distributed under different terms and without source code.
