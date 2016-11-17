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
	IncidentTypeName char (50) Not Null,
	Label1 char(255),
	Label2 char(255)
);

INSERT INTO IncidentTypes (IncidentTypeName, Label1, Label2) VALUES ('Defective Product', 'This order is replaceable', 'This order is refundable');
INSERT INTO IncidentTypes (IncidentTypeName, Label1, Label2) VALUES ('Product Not Delivered', 'This order is replaceable', 'This order is refundable');
INSERT INTO IncidentTypes (IncidentTypeName, Label1, Label2) VALUES ('Unhappy With Product', 'This order is replaceable', 'This order is refundable');
INSERT INTO IncidentTypes (IncidentTypeName, Label1, Label2) VALUES ('Request For Information', 'Non-Transactional Incident', '');
INSERT INTO IncidentTypes (IncidentTypeName, Label1, Label2) VALUES ('Fraudulent Charge', 'This order is refundable', '');
INSERT INTO IncidentTypes (IncidentTypeName, Label1, Label2) VALUES ('Shipping Info Update', 'Non-Transactional Incident', '');
INSERT INTO IncidentTypes (IncidentTypeName, Label1, Label2) VALUES ('Other', 'Contact Administrator if unsure how to proceed', '');


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
