# Checkout test project 

Requirements implemented: 

- endpoint for create basket
- endpoint for add articles to basket
- endpoint for retrieving basket summary including VAT calculation  
- endpoint for updating the stauts of a basket
- use Swagger 

Architecture: 

- DDD + event sourcing
- SQL Server + Entity Framework code first 
- unit tests using MSTest & MOQ for mocking 

# Remarks 

- BasketAggregate is single source of thruth 
- read model in SQL (Baskets table)
- event publishing/handling implemented synchronously in memory, however the solution proposed is easily extensible and implementations using Kafka, service bus etc could be added  
- event store (write model) implemented using SQL for convenience (Events table), would otherwise use a NoSQL DB 
- solution would be more fragmented based on the infrastructure choice, example Infrastructure.NoSql or Infrastructure.Write.... for the event store
- repository not unit tested due to DbContext dependency (not easily testable) however in the past used researched and/or implemented several approaches: 
       
       1) using concrete DbContext dependency using EF In Memory database -->downside tests run extremly slow relatively (not feasible for 10.000 unit tests), moreover, argueably the Unit tests become integration tests
       2) wrapping DbContext using an Interface --> downside DbContext methods that are used need to be added to the interface... is overall hacky 
       3) arguably SqlRepository itself should not be tested and be itself considered as a wrapper over DbContext
       
    In my opinion I would chose either 2 or 3 if absolutely had to unit test the DbContext
       
- some implementations just made 'so they work' as they were not relevant for the overal architecture... example InMemoryEventPublisher.cs (using switch, hardcoded event tpyes etc...)
- in real life would not expose infrastructure types on the front-end, example Basket table type from Infrastructure.Common is exposed all the way to front-end API 
