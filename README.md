# 123 Vendas
Prototipo para gerenciamento de vendas
## Fase 1: Execução da aplicação
**Ir no diretorio onde foi baixado a aplicação e rodar o comando para subir a aplicação nos containers**

    - docker-compose up --build
    - Após todas as aplicações ficarem rodando no docker é necessario abrir o visual studio para executar as migrations
    
**Abrir a ferramenta de acesso a banco de dados de sua preferencia e colocar os dados de acesso da connection string**
    - Verificar se o banco de dados [ vendas ] foi criado
    - caso não tenha sido criado, realizar a criação do mesmo por meio da aplicação cliente escolhida.
**Menssageria Rabbitmq**
    - entrar no endereço http://localhost:15672/  [ou o que o docker fornecerapos o container estiver rodando]
    - as exchanges e filas serão criadas no momento que a aplicação for executada
    
**Executando migrations**  
  - Abrir o Package Manager Console e executar os seguintes comandos
  - add-migration <NOME-DA-MIGRATION> 
  - update-database

 ##   

## Fase 2: Melhorias no código

1. **Melhorias**
   - Executar migrations pelo exec do docker
   - Verificar e obter mais informações em relação a proposta da aplicação para definir o melhor banco de dados, talvez o uso de um banco não relacional
   - Verificar como seria a relação melhor dos itens para vendas para poder aplicar os Principios SOLID com mais eficacia.

## Fase 3: Melhorias diversas

1. **Implementar autenticação da API**
   - Adicionar mecanismos de autenticação para garantir a segurança e a integridade dos dados.
   - Escolher o método de autenticação mais adequado (por exemplo, OAuth, JWT).
   - Documentar o processo de autenticação para desenvolvedores e usuários.

## Fase 3: QUALQUER DÚVIDA, FAVOR ENTRAR EM CONTATO
