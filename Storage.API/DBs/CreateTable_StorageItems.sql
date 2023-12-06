use InsuranceDb;
go

create table StorageItems
(
	ID int identity(1, 1) not null primary key,
	InsuranceGuid uniqueidentifier not null default newid() unique,
	CustomerNumber nvarchar(max) not null, --SSN
	FirstName nvarchar(max) not null,
	LastName nvarchar(max) not null
);
