# 🎮 Catálogo de Games - Microserviço .NET 8

Microserviço desenvolvido em **.NET 8** utilizando os padrões **DDD**, **CQRS** e **MediatR**, com arquitetura em camadas (Api, Application, Domain, Infra).  
Persistência realizada com **SQLite** e integração com **Azure Service Bus** para recebimento de eventos de jogos.

---

## 🚀 Tecnologias e Padrões

- [.NET 8](https://dotnet.microsoft.com/)
- **DDD (Domain-Driven Design)**
- **CQRS (Command Query Responsibility Segregation)**
- **MediatR** para orquestração de comandos e queries
- **Entity Framework Core** com **SQLite**
- **Azure Service Bus** para mensageria
- **Minimal API** com Swagger para documentação

---

## 📂 Estrutura de Projetos
src/ Catalogo.Api/  -> Endpoints HTTP (Minimal API, Swagger, HealthChecks) 
Catalogo.Application/  -> Casos de uso (CQRS, Handlers, DTOs, validações) 
Catalogo.Domain/ -> Entidades, agregados, value objects, interfaces de repositórios 
Catalogo.Infra/  -> EF Core, repositórios, DbContext, integração com Azure Service Bus


---

## ⚙️ Configuração

### Banco de Dados
O projeto utiliza **SQLite**. O arquivo `catalogo.db` será criado automaticamente após rodar as migrações.

```bash
dotnet ef migrations add InitialCreate -p Catalogo.Infra -s Catalogo.Api
dotnet ef database update -p Catalogo.Infra -s Catalogo.Api
