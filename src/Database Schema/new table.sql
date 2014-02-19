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

select Sum(datalength(imageBlob)) from images;
insert into patient values ('AB12124567','12124567',NULL,'F',NULL);
delete from images where seriesID in (select seriesID from imageProperties where patientID = (select tableID from patient where patientID = 'KJ15168546'));
delete from imageProperties where patientID in (select tableID from patient where patientID = 'KJ15168546');
delete from name where patientID = (select tableID from patient where patientID = 'KJ15168546');
delete from patient where patientID = 'KJ15168546'
select top(1) beginTime from timeLog;

select COUNT(distinct patientID) from imageProperties where modality = NULL;
select (MAX(tableID) + 1) from patient;
select MAX(RIGHT(patientID,LEN(patientID)-CHARINDEX('_',patientID)) + 1) from patient where patientID like 'NULL%'

select * from patient where birthdate = '';

select * from imageProperties where CAST(imageAcquisitionDate as DATE) > DATEADD(year,-3,CAST(current_timestamp as DATE));
select * from imageProperties where importToDatabaseDate > DATEADD(month,-3,current_timestamp);
select * from imageProperties where CAST(importToDatabaseDate as DATE) >= DATEADD(DAY,(-7*2),CAST(current_timestamp as DATE));
select * from imageProperties where imageAcquisitionDate between '2013-01-01' and '2005-01-01';
select * from imageProperties where imageAcquisitionDate between '1801-01-01' and '3000-01-01';

select CAST(importToDatabaseDate as DATE) from imageProperties;
select CAST(current_timestamp as DATE) from imageProperties;


select P.patientID, I.fileID from patient P inner join imageProperties IP on P.tableID = IP.patientID join images I on IP.seriesID = I.seriesID;

select * from patient where sex in (select distinct sex from patient);
select * from imageProperties where imageAcquisitionDate > DATEADD(year,-3,current_timestamp);
select * from imageProperties where modality in (select distinct modality from imageProperties);
select distinct top(3) sliceThickness from imageProperties order by sliceThickness desc;
select * from images where processed = 0;

