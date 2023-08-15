# Odata-TodoListApp (Work In Progress)
This is an experimental todo list application. 

Main motivations: 
1. Try out Odata (and how it works with inheritance)
2. Try setting up shadow property using EF fluent API (to test how this can complement to DDD using C#)
3. Setup docker compose for integration test (spin up both Service application and Database)

Implementation notes:
1. Use Hexagonal architecture (ease the change of DB for testing - test using in-memory db)
2. MSSQL for integration test
3. Start with Domain Driven Design
