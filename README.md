# Sistema de Gerenciamento de Notas - Alunos e Professores

## Descrição

Este é um software de gerenciamento de notas desenvolvido em **C#** com **.NET**. O sistema permite que professores possam registrar e gerenciar as notas dos alunos, enquanto os alunos podem visualizar suas notas e seu desempenho. Os administradores são responsáveis por cadastrar novos professores, alunos, turmas e matérias. O objetivo é facilitar o acompanhamento do rendimento acadêmico de forma organizada e eficiente.

## Funcionalidades

- **Cadastro de Professores e Alunos**: Professores e alunos podem ser cadastrados no sistema com suas respectivas informações.
- **Cadastro de Turmas e Matérias**: Turmas e matérias podem ser cadastradas no sistema com suas respectivas informações.
- **Registro de Notas**: Professores podem adicionar e editar notas para os alunos nas disciplinas específicas.
- **Visualização de Notas**: Alunos podem visualizar suas notas em cada disciplina.
- **Autenticação**: Login separado para alunos, professores e administradores, com permissões específicas.

## Tecnologias Utilizadas

- **C#**
- **.NET Framework/Core**
- **Entity Framework** (para gerenciamento de banco de dados)
- **SQL Server** (para persistência de dados)
- **ASP.NET MVC**

## Como Executar

### Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) (versão 8.0 ou superior)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads).

### Passos para rodar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/LucasHFerreira99/GerenciamentoDeNotas.git

2. Navegue até o diretório do projeto:
   ```bash
   cd GerenciamentoDeNotas
   
1. Configure a string de conexão com o banco de dados no arquivo **appsettings.json**.
   ```bash
    "DefaultConnection": "server=NOME_DO_SERVER; database=NOME_DO_BANCO;trusted_connection=true; trustservercertificate=true"
   
4. Execute o comando para criar o banco de dados:
   ```bash
   dotnet ef database update
   
5. Execute o comando para rodar a aplicação:
   ```bash
   dotnet run

## Como Usar

### Administradores

1. Faça login com suas credenciais de administrador (usuário padrão inicial **admin** e senha **admin**).
2. Acesse o menu de **Usuários** para adicionar, editar ou remover usuários.
3. Acesse o menu de **Matérias** para adicionar, editar ou remover matérias.
4. Acesse o menu de **Turmas** para adicionar ou remover matérias, como tambem adicionar novos alunos a uma turma em especifico.
5. Acesse o menu de **Gerenciar Professores** para adicionar ou remover uma turma e uma matéria a um professor.
   
### Professores

1. Faça login com suas credenciais de professor.
2. A pagina inicial te dará a lista de turmas que esse professor dá aula.
3. Acesse alguma turma para alterar as notas dos alunos.

### Alunos

1. Faça login com suas credenciais de aluno.
2. A pagina inicial te dará a lista de matérias que esse aluno está matriculado.
3. Acesse alguma matéria para visualizar as notas com mais detalhes.

## Contribuições

Contribuições são bem-vindas! Se você encontrar algum bug ou quiser adicionar novas funcionalidades, sinta-se à vontade para abrir uma [issue](https://github.com/LucasHFerreira99/GerenciamentoDeNotas/issues) ou enviar um pull request.

## Licença

Este projeto está licenciado sob a Licença MIT.

