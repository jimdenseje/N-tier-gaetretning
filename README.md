# N-tier-gaetretning
Dette er et skole project.

## hvad kunne man have gjord bedre
Kun have brugt DTO'er i Business Logik Laget

## Vigtigt i forholdet til migration over flere projecter i en solution
meget vigtigt at man ikke tilføjer et mellemrum til et projekt navn for ellers virker "dotnet ef migrations" komandoen til "database migration" ikke

## oprettelse af en migration
dotnet ef migrations add InitialCreate --project "DataAccessLayer" --startup-project "API"

## udførelse af database migration på databasen
dotnet ef database update --project "DataAccessLayer" --startup-project "API"

## her er includeret appsettings.json
Dette er normalt et no go.
Inkluder aldrig følsomme oplysninger som forbindelsesstrenge eller hemmelige nøgler i din kodebase.
Gem dem i stedet et sikker sted, som i dine miljøvariabler eller i en hemmelig nøgle manager.
