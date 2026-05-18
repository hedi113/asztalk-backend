CREATE TABLE ForceUsers
(
    Id INT PRIMARY KEY,
    Name NVARCHAR(100),
    OrderType NVARCHAR(10),
    Species NVARCHAR(50),
    Homeworld NVARCHAR(50),
    Era NVARCHAR(50),
    Rank NVARCHAR(50),
    LightsaberColor NVARCHAR(20),
    Master NVARCHAR(100),
    Apprentice NVARCHAR(100),
    ForceSpecialty NVARCHAR(100),
    IsAlive BIT
);

INSERT INTO ForceUsers (Id, Name, OrderType, Species, Homeworld, Era, Rank, LightsaberColor, Master, Apprentice, ForceSpecialty, IsAlive)
VALUES
(1,'Yoda','Jedi','Unknown','Unknown','CloneWars','Master','Green','None','Luke Skywalker','Force Wisdom',0),
(2,'Luke Skywalker','Jedi','Human','Tatooine','Rebellion','Master','Green','Yoda','Ben Solo','Force Projection',1),
(3,'Obi-Wan Kenobi','Jedi','Human','Stewjon','CloneWars','Master','Blue','Qui-Gon Jinn','Anakin Skywalker','Mind Trick',0),
(4,'Anakin Skywalker','Jedi','Human','Tatooine','CloneWars','Knight','Blue','Obi-Wan Kenobi','Ahsoka Tano','Force Power',0),
(5,'Mace Windu','Jedi','Human','Haruun Kal','CloneWars','Master','Purple','None','Depa Billaba','Vaapad',0),
(6,'Ahsoka Tano','Jedi','Human','Shili','CloneWars','Padawan','Green','Anakin Skywalker','None','Force Stealth',1),
(7,'Kit Fisto','Jedi','Nautolan','Glee Anselm','CloneWars','Master','Green','None','None','Underwater Combat',1),
(8,'Plo Koon','Jedi','Kel Dor','Dorin','CloneWars','Master','Blue','None','Aayla Secura','Force Lightning Resistance',0),
(9,'Saesee Tiin','Jedi','Iktotchi','Iktotch','CloneWars','Master','Blue','None','None','Starfighter Combat',0),
(10,'Ki-Adi-Mundi','Jedi','Cerean','Cerea','CloneWars','Master','Blue','None','Stass Allie','Force Strategy',0),
(11,'Darth Sidious','Sith','Human','Naboo','Empire','DarkLord','Red','Darth Plagueis','Darth Vader','Force Lightning',0),
(12,'Darth Vader','Sith','Human','Tatooine','Empire','Lord','Red','Darth Sidious','None','Force Choke',0),
(13,'Darth Maul','Sith','Zabrak','Dathomir','CloneWars','Apprentice','Red','Darth Sidious','None','Double Saber',0),
(14,'Count Dooku','Sith','Human','Serenno','CloneWars','Lord','Red','Darth Sidious','Asajj Ventress','Force Lightning',0),
(15,'Kylo Ren','Sith','Human','Chandrila','FirstOrder','Lord','Red','Luke Skywalker','None','Force Freeze',0),
(16,'Asajj Ventress','Sith','Human','Dathomir','CloneWars','Apprentice','Red','Count Dooku','None','Stealth',0),
(17,'Revan','Jedi','Human','Korriban','OldRepublic','Master','Purple','None','Meetra Surik','Force Tactics',0),
(18,'Bastila Shan','Jedi','Human','Coruscant','OldRepublic','Master','Yellow','Stark Hyperspace','None','Force Battle Meditation',0),
(19,'Exar Kun','Sith','Human','Dantooine','OldRepublic','DarkLord','Red','None','None','Force Magic',0),
(20,'Malak','Sith','Human','Mandalore','OldRepublic','Lord','Red','Exar Kun','None','Force Combat',0),
(21,'Depa Billaba','Jedi','Human','Korriban','CloneWars','Master','Blue','Mace Windu','None','Force Stealth',0),
(22,'Ben Solo','Jedi','Human','Chandrila','FirstOrder','Padawan','Blue','Luke Skywalker','None','Force Projection',1),
(23,'Luminara Unduli','Jedi','Mirialan','Mirial','CloneWars','Master','Blue','None','Aayla Secura','Force Healing',0),
(24,'Aayla Secura','Jedi','Twi''lek','Ryloth','CloneWars','Master','Blue','Kit Fisto','None','Agility',0),
(25,'Qui-Gon Jinn','Jedi','Human','Coruscant','CloneWars','Master','Green','None','Obi-Wan Kenobi','Force Meditation',0);