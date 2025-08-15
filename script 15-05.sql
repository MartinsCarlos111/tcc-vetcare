create database bdVetCare;
create database teste2;

use bdVetCare;
use teste2;

CREATE TABLE tbPayment (
idPayment int PRIMARY KEY auto_increment,
nameFormOfPayment varchar(255)
);

CREATE TABLE tbAdmin (
idAdmin int PRIMARY KEY auto_increment,
nameAdmin varchar(255),
passwordAdmin char(8),
emailAdmin varchar(255)
);

CREATE TABLE tbLogin (
idLogin int PRIMARY KEY auto_increment,
idAdmin int,
idCustomer int,
loginStatus bit,
FOREIGN KEY(idAdmin) REFERENCES tbAdmin(idAdmin),
FOREIGN KEY(idCustomer) REFERENCES tbCustomer(idCustomer)
);

CREATE TABLE tbPlan (
idPlan int PRIMARY KEY auto_increment,
namePlan varchar(255),
descriptionPlan varchar(255),
pricePlan decimal(10,2)
);

CREATE TABLE tbProduct (
idProduct int PRIMARY KEY auto_increment,
idSupplier int,
nameProduct Varchar(255),
descriptionProduct Varchar(255),
unitPrice decimal(10,2),
imageProduct varchar(255),
FOREIGN KEY(idSupplier) REFERENCES tbSupplier(idSupplier)
);

CREATE TABLE tbSale (
idSale int PRIMARY KEY auto_increment,
idCustomer int,
idPayment int,
totalPrice decimal(10,2),
dateSale date,
quantitySale int,
FOREIGN KEY(idCustomer) REFERENCES tbCustomer(idCustomer),
FOREIGN KEY(idPayment) REFERENCES tbPayment(idPayment)
);

CREATE TABLE tbSupplier (
idSupplier int PRIMARY KEY auto_increment,
nameSupplier varchar(255),
cnpj char(15)
);

CREATE TABLE tbService (
idService Integer PRIMARY KEY auto_increment,
nameService varchar(255),
priceService decimal(10,2),
descriptionService varchar(255)
);

CREATE TABLE tbPet (
idAnimal int PRIMARY KEY auto_increment,
-- idCustomer int, --
nameAnimal varchar(255),
breedAnimal varchar(255),
ageAnimal int,
genderAnimal char(2),
speciesAnimal varchar(255)
--  KEY(idCustomer) REFERENCES tbCustomer(idCustomer)--
);
drop TABLE tbPet;

CREATE TABLE tbSchedule (
idSchedule int PRIMARY KEY auto_increment,
idCustomer int,
idAnimal int,
idService int,
dateSchedule varchar(15), -- Date --
timeSchedule varchar(10),
observations varchar(255),
FOREIGN KEY(idAnimal) REFERENCES tbPet(idAnimal),
FOREIGN KEY(idCustomer) REFERENCES tbCustomer(idCustomer),
FOREIGN KEY(idService) REFERENCES tbService(idService)
);
select * from tbSchedule;
drop table tbSchedule;
CREATE TABLE tbCustomer (
idCustomer int PRIMARY KEY auto_increment,
nameCustomer varchar(80),
cpfCustomer char(11),
emailCustomer varchar(255),
passwordCustomer varchar(15),
phoneCustomer char(11)
);

drop table tbCustomer;

CREATE TABLE tbAddressCustomer (
idAddress int PRIMARY KEY auto_increment,
unitApartmentNumber varchar(255),
state varchar(255),
city varchar(255),
zipCode char(9),
streetName varchar(255),
streetNumber int
);

CREATE TABLE tbContem (
idService Integer,
idPlan Integer,
FOREIGN KEY(idService) REFERENCES tbService(idService),
FOREIGN KEY(idPlan) REFERENCES tbPlan(idPlan)
);

CREATE TABLE tbPossui (
idSupplier Integer,
idProduct Integer,
FOREIGN KEY(idSupplier) REFERENCES tbSupplier (idSupplier),
FOREIGN KEY(idProduct) REFERENCES tbProduct (idProduct)
);

CREATE TABLE tbCompoe (
idPayment Integer,
idSale Integer,
FOREIGN KEY(idSale) REFERENCES tbSale (idSale)
);

CREATE TABLE tbEngloba (
idProduct Integer,
idSale Integer,
FOREIGN KEY(idProduct) REFERENCES tbProduct (idProduct),
FOREIGN KEY(idSale) REFERENCES tbSale (idSale)
);

CREATE TABLE tbAcessa (
idLogin Integer,
idCustomer Integer,
FOREIGN KEY(idLogin) REFERENCES tbLogin (idLogin),
FOREIGN KEY(idCustomer) REFERENCES tbCustomer (idCustomer)
);

CREATE TABLE tbDispoe (
idService Integer,
idSchedule Integer,
FOREIGN KEY(idSchedule) REFERENCES tbSchedule (idSchedule)
);

ALTER TABLE tbSale ADD FOREIGN KEY(idCustomer) REFERENCES tbCustomer (idCustomer);
ALTER TABLE tbPet ADD FOREIGN KEY(idCustomer) REFERENCES tbCustomer (idCustomer);
ALTER TABLE tbSchedule ADD FOREIGN KEY(idCustomer) REFERENCES tbCustomer (idCustomer);


delimiter $$
create procedure spSelectCPFCustomer(VCpfCustomer char(11))
begin
Select cpfCustomer from tbCustomer where cpfCustomer = VCpfCustomer;
end $$
delimiter ;



delimiter $$
create procedure spSelectEmailCustomer(VEmailCustomer varchar(255))
begin
Select emailCustomer from tbCustomer where emailCustomer = VEmailCustomer;
end $$
delimiter ;

delimiter $$
create procedure spSelectCnpjSuppler(Vcnpj varchar(15))
begin
Select cnpj from tbSupplier where cnpj = Vcnpj;
end $$
delimiter ;


select * from tbService;
select * from tbPet;
select * from tbSupplier;
select * from tbProduct;

