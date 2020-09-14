# CompanyPlatform
.Net Core web api - CRUD with with basic authentication

Credentials are hardcoded:
username = username,
password = password,

It is not self hosted , i was not able to manage it.

App connects with SQLight data base in the file "CompanyPlatform\CompanyWebApi\CompanyDB.db"

SQL created table :

CREATE TABLE "Companies" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Companies" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL,
    "EstablishmentYear" INTEGER NOT NULL
)

CREATE TABLE "Employees" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Employees" PRIMARY KEY AUTOINCREMENT,
    "FirstName" TEXT NULL,
    "LastName" TEXT NULL,
    "DateOfBirth" TEXT NOT NULL,
    "JobTitle" TEXT NOT NULL,
    "CompanyId" INTEGER NULL,
    CONSTRAINT "FK_Employees_Companies_CompanyId" FOREIGN KEY ("CompanyId") REFERENCES "Companies" ("Id") ON DELETE RESTRICT
