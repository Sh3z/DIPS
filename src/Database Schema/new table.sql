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
