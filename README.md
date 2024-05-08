# Desafio Back End - Aluguel de Motos

## Descrição do Projeto
O projeto "Sistema de Aluguel de Motos" é uma aplicação web que oferece serviços de aluguel de motocicletas. Permite que os usuários realizem diversas operações, como registrar-se, fazer login, alugar motocicletas, gerenciar pedidos e interagir com entregadores.

## Configuração
 - .NET Sdk instalado na máquina
 - PostgreSQL instalado e configurado
 - RabbitMQ instalado e estar sendo executado na sua máquina
 - Docker instalado
   
 ## Rodando o projeto pelo Docker
 1. Clone o repositório.
 2. Acesse a pasta do projeto pelo terminal
 3. Rode o docker compose pelo terminal
    
 ## Rodando o projeto
 1. Clone o repositório.
 2. Abra o no Visual Studio ou uma IDE compatível
 3. No arquivo "appsettings.json" será preciso alterar as strings de conexão para poder funcionar o PostgreSql e o RabbitMQ.
 4. Execute o projeto
  
  ## Funcionalidades
  - Autenticação (Auth)
  - Entregador (DeliveryMan)
  - Motocicleta (Motorcycle)
  - Pedido (Order)
  - Aluguel (Rental)

## Autenticação (Auth)

### Registrar Administrador
`POST /api/Auth/register-admin`

#### Parâmetros da Requisição
- Body:
  - Modelo: [RegisterUserViewModel](#components/schemas/RegisterUserViewModel)

#### Respostas
- 200: Sucesso

### Registrar Usuário
`POST /api/Auth/register-user`

#### Parâmetros da Requisição
- Body:
  - Modelo: [RegisterUserViewModel](#components/schemas/RegisterUserViewModel)

#### Respostas
- 200: Sucesso

### Login
`POST /api/Auth/login`

#### Parâmetros da Requisição
- Body:
  - Modelo: [LoginUserViewModel](#components/schemas/LoginUserViewModel)

#### Respostas
- 200: Sucesso

## Entregador (DeliveryMan)

### Obter Todos os Entregadores
`GET /api/DeliveryMan/get-all`

#### Respostas
- 200: Sucesso
  - Conteúdo:
    - Modelo: [DeliveryManViewModel](#components/schemas/DeliveryManViewModel)

### Obter Entregador Notificado
`GET /api/DeliveryMan/get-notified`

#### Parâmetros da Requisição
- orderId (query) - UUID da Ordem

#### Respostas
- 200: Sucesso
  - Conteúdo:
    - Modelo: [DeliveryManViewModel](#components/schemas/DeliveryManViewModel)

### Adicionar Entregador
`POST /api/DeliveryMan`

#### Parâmetros da Requisição
- Body:
  - Modelo: [DeliveryManViewModel](#components/schemas/DeliveryManViewModel)

#### Respostas
- 200: Sucesso
  - Conteúdo:
    - Modelo: [DeliveryManViewModel](#components/schemas/DeliveryManViewModel)

### Atualizar Entregador
`PUT /api/DeliveryMan`

#### Parâmetros da Requisição
- file (multipart/form-data) - Arquivo

#### Respostas
- 200: Sucesso

## Motocicleta (Motorcycle)

### Obter Motocicletas
`GET /api/Motorcycle/GetMotorcycles`

#### Respostas
- 401: Não Autorizado

### Obter Motocicletas por Placa
`GET /api/Motorcycle/GetMotorcyclesByPlate`

#### Parâmetros da Requisição
- plate (query) - Placa da Motocicleta

#### Respostas
- 200: Sucesso
  - Conteúdo:
    - Modelo: [Motorcycle](#components/schemas/Motorcycle)

### Adicionar Motocicleta
`POST /api/Motorcycle`

#### Parâmetros da Requisição
- Body:
  - Modelo: [MotorcycleViewModel](#components/schemas/MotorcycleViewModel)

#### Respostas
- 200: Sucesso
  - Conteúdo:
    - Modelo: [Motorcycle](#components/schemas/Motorcycle)

### Atualizar Motocicleta
`PUT /api/Motorcycle`

#### Parâmetros da Requisição
- plate (query) - Placa da Motocicleta
- Body:
  - Modelo: [MotorcycleViewModel](#components/schemas/MotorcycleViewModel)

#### Respostas
- 200: Sucesso

### Deletar Motocicleta
`DELETE /api/Motorcycle`

#### Parâmetros da Requisição
- plate (query) - Placa da Motocicleta

#### Respostas
- 200: Sucesso
  - Conteúdo:
    - Modelo: [MotorcycleViewModel](#components/schemas/MotorcycleViewModel)

## Pedido (Order)

### Adicionar Pedido
`POST /api/Order/add-order`

#### Parâmetros da Requisição
- Body:
  - Modelo: [OrderViewModel](#components/schemas/OrderViewModel)

#### Respostas
- 200: Sucesso

### Pegar Pedido
`PUT /api/Order/take-order`

#### Parâmetros da Requisição
- orderId (query) - UUID do Pedido

#### Respostas
- 200: Sucesso

### Finalizar Pedido
`PUT /api/Order/finalize-order`

#### Parâmetros da Requisição
- orderId (query) - UUID do Pedido

#### Respostas
- 200: Sucesso

## Aluguel (Rental)

### Adicionar Aluguel
`POST /api/Rental`

#### Parâmetros da Requisição
- Body:
  - Modelo: [RentalViewModel](#components/schemas/RentalViewModel)

#### Respostas
- 200: Sucesso

### Atualizar Aluguel
`PUT /api/Rental`

#### Parâmetros da Requisição
- rentalId (query) - UUID do Aluguel
- endDate (query) - Data e Hora de Término do Aluguel

#### Respostas
- 200: Sucesso

