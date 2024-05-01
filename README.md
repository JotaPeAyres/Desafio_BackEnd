# Desafio Back End - Aluguel de Motos

## Descrição do Projeto


## Configuração
 - .NET Sdk instalado na máquina
 - PostgreSQL instalado e configurado
 - RabbitMQ instalado e estar sendo executado na sua máquina

 ## Rodando o projeto
 1. Clone o repositório.
 2. Abra o no Visual Studio ou uma IDE compatível
 3. No arquivo "appsettings.json" será preciso alterar as strings de conexão para poder funcionar o PostgreSql e o RabbitMQ.
 4. Execute o projeto
  
  ## Funcionalidades
  - Auth
  - DeliveryMan
  - Motorcycle
  - Order
  - Rental

#### Auth
- `POST /api/Auth/register-admi`
  - registrar um novo administrador.
  
- `POST /api/Auth/register-user`
  - registrar um novo usuário.
    
- `POST /api/Auth/login`
  - Login de usuários.

- `GET /api/DeliveryMan/get-all`
  - Retorna todos os entregadores cadastrados.

- `GET /api/DeliveryMan/get-notified`
  - Retorna os entregadores notificados para uma determinada ordem.

- `POST /api/DeliveryMan`
  - Registra um novo entregador.
    
- `PUT /api/DeliveryMan`
  - Atualiza a licença de motorista de um entregador.
