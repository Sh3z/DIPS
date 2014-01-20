create table patient(
id int identity PRIMARY KEY,
patientID varchar(30) UNIQUE,
birthdate varchar(10),
age varchar(10),
sex char(1) NOT NULL,
seriesAvailable int NOT NULL
);
create table name(
id int FOREIGN KEY REFERENCES patient(id),
firstName varchar(30),
lastName varchar(30)
);
create table imageProperties(
imageID varchar(15) NOT NULL PRIMARY KEY,
id int FOREIGN KEY REFERENCES patient(id),
imageDateTime datetime NOT NULL,
bodyPart varchar(20),
studyDescription varchar(50),
seriesDescription varchar(50),
sliceThickness varchar(20)
);
create table images(
fileID varchar(20) NOT NULL PRIMARY KEY,
imageID varchar(15) NOT NULL FOREIGN KEY REFERENCES imageProperties(imageID),
imageBlob varbinary(MAX),
processed bit NOT NULL
);