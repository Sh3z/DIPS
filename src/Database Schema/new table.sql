create table patient(
tableID int identity PRIMARY KEY,
patientID varchar(30) UNIQUE,
birthdate varchar(10),
age varchar(10),
sex char(1),
seriesAvailable int
);
create table name(
patientID int FOREIGN KEY REFERENCES patient(tableID),
patientName varchar(50)
);
create table imageProperties(
seriesID int identity PRIMARY KEY,
patientID int FOREIGN KEY REFERENCES patient(tableID),
modality varchar(15),
imageAcquisitionDate datetime,
importToDatabaseDate datetime default current_timestamp,
bodyPart varchar(20),
studyDescription varchar(50),
seriesDescription varchar(50),
sliceThickness varchar(20)
);
create table images(
fileID int identity PRIMARY KEY,
seriesID int FOREIGN KEY REFERENCES imageProperties(seriesID),
imageNumber varchar(5),
imageBlob varbinary(MAX),
processed bit,
);

select * from patient;
select * from name;
select * from imageProperties;
select * from images;

drop table images;
drop table imageProperties;
drop table name;
drop table patient;