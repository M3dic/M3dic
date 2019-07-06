create database if not exists M3dic ;

create table register
(
id int auto_increment not null primary key,
first_name varchar(50) not null,
last_name varchar(50) ,
password varchar(50) not null,
Gender enum('Male', 'Female', 'Other')not null default 'Other',
age int not null,
phone_number varchar(50),
date varchar(50) not null,
email varchar(50),
address varchar(50),
city varchar(50),
sequre_number varchar(10) not null,
placed enum('true','false') default 'false',
CNIC varchar(50)
);

create table admins
(
id int auto_increment primary key,
name varchar(50) not null,
password varchar(20) not null,
special_id varchar(10) unique,
email varchar(50) ,
phone_number varchar(50),
age int,
salary varchar(20),
CNIC varchar(50)
);

insert into admins (name,password,special_id,email) values('Ivo','08857490200','A100','ivo_radev2@abv.bg');


create table doctors
(
id int auto_increment primary key,
staffid varchar(10) not null,
first_name varchar(50) not null,
last_name varchar(50) ,
password varchar(50) not null,
Gender enum('Male', 'Female', 'Other')not null,
age int not null,
phone_number varchar(50) ,
datejoined varchar(50) ,
email varchar(50) ,
line varchar(50) ,
city varchar(50) ,
qualification varchar(50) ,
timings varchar(20) ,
salary varchar(20) ,
CNIC varchar(50)
);

create table receptionist
(
id int auto_increment primary key,
staffid varchar(10) not null,
first_name varchar(50) not null,
last_name varchar(50) ,
password varchar(50) not null,
Gender enum('Male', 'Female', 'Other')not null,
age int not null,
phone_number varchar(50) ,
datejoined varchar(50) ,
email varchar(50) ,
line varchar(50) ,
city varchar(50) ,
qualification varchar(50),
timings varchar(20),
salary varchar(20) ,
CNIC varchar(50)
);

create table pharmacist
(
id int auto_increment primary key,
staffid varchar(10) not null,
first_name varchar(50) not null,
last_name varchar(50),
password varchar(50) not null,
Gender enum('Male', 'Female', 'Other'),
age int,
phone_number varchar(50),
datejoined varchar(50),
email varchar(50),
line varchar(50),
city varchar(50),
qualification varchar(50) ,
timings varchar(20) ,
salary varchar(20) ,
CNIC varchar(50)
);

create table floors 
(
room_number int not null,
room_type varchar(10),
room_fee varchar(10),
floor_number int not null
);

create table beds
(
room_n int not null,
bed_number varchar(10) not null,
isfree enum('true','false'),
floor_n int not null
);


create table patients_appointment
(
placed_appointment int auto_increment primary key,
patient_id varchar(10) not null,
patient_name varchar(20) not null,
doctor_name varchar(50),
dignose varchar(200),
floor_placed int,
room_placed int,
bed_number varchar(15),
appointment_data varchar(50),
appointment_time varchar(20),
appontment_fee varchar(10)
);

create table billreceipt
(
billid varchar(50),
patientid varchar(50),
patientname varchar(50),
medicineexpense varchar(50),
hospitalexpense varchar(50),
total varchar(50),
discount varchar(50),
amounttopay varchar(50),
amouuntpaid varchar(50)
);

create table medicines
(
id int auto_increment primary key,
name varchar(50),
description varchar(2000),
price varchar(50),
quantity varchar(50),
mgf varchar(50),
expiry varchar(50),
manufacturer varchar(50)
);

create table hospital
(
hospital_id int primary key,
hospital_name varchar(50),
hospital_city varchar(20),
hospital_town varchar(20)
);

ALTER TABLE admins
ADD COLUMN hospital_id int AFTER CNIC;
ALTER TABLE admins
ADD CONSTRAINT fk_hospital
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE beds
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitalb
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE billreceipt
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitalbill
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE doctors
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitald
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE floors
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitalf
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE medicines
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitalm
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE patients_appointment
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitalpa
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE pharmacist
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitalp
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE prescription
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitalpr
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE receptionist
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitalr
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 

ALTER TABLE register
ADD COLUMN hospital_id int,
ADD CONSTRAINT fk_hospitalre
FOREIGN KEY (hospital_id) REFERENCES hospital(hospital_id); 