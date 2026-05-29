CREATE TABLE Faculty (
facId varchar(100) constraint pk_facId primary key,
facName varchar(100) not null,
facDOB date,
facGender varchar(50),
facMob varchar(13),
facEmail varchar(50),
facAddress text,
);