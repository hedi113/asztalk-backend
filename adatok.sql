-- Table creation
CREATE TABLE TurkeyRoutes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DepartureCity NVARCHAR(50),
    ArrivalCity NVARCHAR(50),
    DepartureHour INT,
    DepartureMinute INT,
    ArrivalHour INT,
    ArrivalMinute INT,
    DistanceKm INT
);

-- Insert data
INSERT INTO TurkeyRoutes 
(DepartureCity, ArrivalCity, DepartureHour, DepartureMinute, ArrivalHour, ArrivalMinute, DistanceKm)
VALUES
('Istanbul','Ankara',6,30,11,15,450),
('Ankara','Izmir',8,45,14,20,585),
('Izmir','Antalya',7,15,12,50,460),
('Antalya','Adana',9,0,13,40,560),
('Adana','Gaziantep',10,20,12,55,225),
('Gaziantep','Konya',11,10,16,30,630),
('Konya','Bursa',6,50,12,10,510),
('Bursa','Istanbul',7,40,9,30,155),
('Istanbul','Antalya',5,55,14,25,700),
('Ankara','Trabzon',12,15,18,45,730),
('Trabzon','Erzurum',9,35,12,5,300),
('Erzurum','Van',13,25,17,10,380),
('Van','Diyarbakir',8,10,12,55,380),
('Diyarbakir','Mersin',7,20,14,0,620),
('Mersin','Kayseri',6,45,12,30,330),
('Kayseri','Ankara',15,10,18,20,320),
('Ankara','Eskisehir',16,0,17,40,230),
('Eskisehir','Izmir',10,50,16,10,480),
('Izmir','Bodrum',9,25,11,5,240),
('Bodrum','Antalya',13,30,18,15,420),
('Antalya','Konya',14,45,19,20,300),
('Konya','Gaziantep',5,30,11,0,630),
('Gaziantep','Istanbul',17,10,23,55,1150),
('Istanbul','Bursa',18,20,20,5,155),
('Bursa','Ankara',11,35,16,25,380);