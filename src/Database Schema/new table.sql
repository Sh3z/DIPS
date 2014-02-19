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
lastModifiedDate datetime default current_timestamp,
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
select * from timeLog;

drop table timeLog;
drop table images;
drop table imageProperties;
drop table name;
drop table patient;

select * from imageProcessing;
drop table imageProcessing;

SELECT @@SERVERNAME AS 'Server Name';

select backup_size from msdb..backupset;
select compressed_backup_size from msdb..backupset;

BACKUP DATABASE medicalImaging
TO DISK = 'C:\Users\Yeh\Desktop\localbackup\Test4.BAK'

DROP DATABASE medicalImaging;

RESTORE DATABASE medicalImaging
FROM DISK = 'C:\Users\Yeh\Desktop\localbackup\Test2.BAK'
WITH STATS = 1

insert into patient (patientID) VALUES ('TEST2');
insert into imageProperties (patientID,modality,sliceThickness) VALUES (12,'HEHE',3);

select avg(CONVERT(FLOAT,sliceThickness)),IP.patientID from patient P join imageProperties IP on P.tableID = IP.patientID 
group by IP.patientID order by avg(CONVERT(FLOAT,sliceThickness)) desc;


SELECT distinct IP.patientID, IP.lastModifiedDate
FROM patient P join imageProperties IP on P.tableID = IP.patientID
order by IP.lastModifiedDate desc

order by avg(CONVERT(FLOAT,IP.sliceThickness)) desc
order by IP.lastModifiedDate desc
order by P.seriesAvailable desc