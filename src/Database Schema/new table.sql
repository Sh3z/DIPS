create table patient(
tableID int identity PRIMARY KEY,
patientID varchar(30) UNIQUE,
birthdate varchar(10),
age varchar(10),
sex char(1) NOT NULL,
seriesAvailable int NOT NULL
);
create table name(
patientID int FOREIGN KEY REFERENCES patient(tableID),
firstName varchar(30),
lastName varchar(30)
);
create table imageProperties(
seriesID int identity PRIMARY KEY,
patientID int FOREIGN KEY REFERENCES patient(tableID),
imageDateTime datetime NOT NULL,
bodyPart varchar(20),
studyDescription varchar(50),
seriesDescription varchar(50),
sliceThickness varchar(20)
);
create table images(
fileID int identity PRIMARY KEY,
seriesID int NOT NULL FOREIGN KEY REFERENCES imageProperties(seriesID),
imageNumber varchar(5),
imageBlob varbinary(MAX),
processed bit NOT NULL,
);

select * from patient;
select * from name;
select * from imageProperties;
select * from images;

drop table images;
drop table imageProperties;
drop table name;
drop table patient;