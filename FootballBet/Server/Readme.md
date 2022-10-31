# EF
dotnet ef database update --project FootballBet.Repository --startup-project FootballBet/Server

dotnet ef migrations add InitialCreate --project FootballBet.Repository --startup-project FootballBet/Server
