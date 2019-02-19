Migration
PM> add-migration -c eGoatDDDDbContext Initial -project eGoatDDD.Persistence
PM> update-database

Drop Databases
Use eGoatDDD;
	DROP TABLE __EFMigrationsHistory,
	AspNetRoleClaims,
	AspNetUserClaims,
	AspNetUserLogins,
	AspNetUserRoles,
	AspNetUsers,
	AspNetUserTokens,
	Package,
	Categories,
	Product,
	AppUser,
	Loan,
	[Loan Details],
	AspNetRoles