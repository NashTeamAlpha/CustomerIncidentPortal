Create Table Departments
( 
	DepartmentId integer Not Null Primary Key,
	DepartmentName char(50) Not Null
);

INSERT INTO Departments (DepartmentName) VALUES ('Apparel');
INSERT INTO Departments (DepartmentName) VALUES ('Electronics');
INSERT INTO Departments (DepartmentName) VALUES ('Toys & Games');
INSERT INTO Departments (DepartmentName) VALUES ('Books');
INSERT INTO Departments (DepartmentName) VALUES ('Home Furnishings');

Create Table IncidentTypes
(
	IncidentTypeId integer Not Null Primary Key,
	IncidentTypeName char (50) Not Null
);

INSERT INTO IncidentTypes (IncidentTypeName) VALUES ('Defective Product');
INSERT INTO IncidentTypes (IncidentTypeName) VALUES ('Product Not Delivered');
INSERT INTO IncidentTypes (IncidentTypeName) VALUES ('Unhappy With Product');
INSERT INTO IncidentTypes (IncidentTypeName) VALUES ('Request For Information');
INSERT INTO IncidentTypes (IncidentTypeName) VALUES ('Fraudulent Charge');
INSERT INTO IncidentTypes (IncidentTypeName) VALUES ('Shipping Info Update');
INSERT INTO IncidentTypes (IncidentTypeName) VALUES ('Other');

Create Table Employees
(
	EmployeeId integer Not Null Primary Key,
	FirstName char (50) Not Null,
	LastName char (50) Not Null,
	IsAdmin boolean Not Null,
	DepartmentId integer Not Null,
	StartDate DateTime Not Null,
	Foreign Key (DepartmentId) References Departments(DepartmentId)
);
Insert Into Employees (FirstName, LastName, IsAdmin, DepartmentId, StartDate) 
values ('Grant', 'Regnier', 'false', '1', '2013-11-3');
Insert Into Employees (FirstName, LastName, IsAdmin, DepartmentId, StartDate) 
values ('Chris', 'Smalley', 'True', '2', '2013-11-3');
Insert Into Employees (FirstName, LastName, IsAdmin, DepartmentId, StartDate) 
values ('Zack', 'Repass', 'false', '3', '2013-11-3');
Insert Into Employees (FirstName, LastName, IsAdmin, DepartmentId, StartDate) 
values ('Delaine', 'Wendling', 'True', '4', '2013-11-3');
Insert Into Employees (FirstName, LastName, IsAdmin, DepartmentId, StartDate) 
values ('Jamie', 'Duke', 'false', '5', '2013-11-3');
Insert Into Employees (FirstName, LastName, IsAdmin, DepartmentId, StartDate) 
values ('Debbie', 'Bourne', 'True', '5', '2013-11-3');

Create Table Incidents
(
	IncidentId integer Not Null Primary Key,
	Resolution char (225) Null,
	IsResolved boolean Not Null,
	EmployeeId integer Not Null,
	OrderId integer Not Null,
	IncidentTypeId integer Not Null,
	CustomerFirstName char (50) Not Null,
	CustomerLastName char (50) Not Null,
	Foreign Key (EmployeeId) References Employees(EmployeeId),
	Foreign Key (IncidentTypeId) References IncidentTypes(IncidentTypeId)
);
