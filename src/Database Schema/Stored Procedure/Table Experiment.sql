create table patient(
tableID int identity PRIMARY KEY,
patientID varchar(30) UNIQUE,
studyUID varchar(70),
birthdate varchar(10),
age varchar(10),
sex char(1),
seriesAvailable int
);
create table name(
patientID int FOREIGN KEY REFERENCES patient(tableID),
patientName varchar(100)
);
create table imageProperties(
seriesID int identity PRIMARY KEY,
patientID int FOREIGN KEY REFERENCES patient(tableID),
seriesUID varchar(70),
modality varchar(15),
creationDate datetime default current_timestamp,
lastModifiedDate datetime default current_timestamp,
bodyPart varchar(20),
studyDescription varchar(50),
seriesDescription varchar(50),
sliceThickness varchar(20)
);
create table images(
fileID int identity PRIMARY KEY,
seriesID int FOREIGN KEY REFERENCES imageProperties(seriesID),
imageUID varchar(70) UNIQUE,
imageNumber varchar(5),
imageBlob varbinary(MAX),
);
create table processedImages(
fileID int identity PRIMARY KEY,
imageUID varchar(70) FOREIGN KEY REFERENCES images(imageUID),
processMethod int,
imageBlob varbinary(MAX)
);
create table timeLog(
logID int identity PRIMARY KEY,
beginTime datetime default current_timestamp
);
create table imageProcessing(
ID int identity PRIMARY KEY,
name varchar(100) NOT NULL,
technique xml
);

select * from patient;
select * from name;
select * from imageProperties;
select * from images;
select * from processedImages;
select * from timeLog;

drop table timeLog;
drop table processedImages;
drop table images;
drop table imageProperties;
drop table name;
drop table patient;

select * from imageProcessing;
drop table imageProcessing;

BACKUP DATABASE medicalImaging
TO DISK = 'C:\Users\Joseph\Documents\Backup\Test2.BAK'

DROP DATABASE medicalImaging;

RESTORE DATABASE medicalImaging
FROM DISK = 'C:\Users\Joseph\Documents\Backup\Test2.BAK'
WITH STATS = 1