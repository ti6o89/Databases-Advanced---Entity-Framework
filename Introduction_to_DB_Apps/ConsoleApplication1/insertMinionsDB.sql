USE MinionsDB

CREATE TABLE Villains
(
Id INT IDENTITY,
Name VARCHAR(30),
EvilnessFactor VARCHAR(15),
CONSTRAINT PK_Villains PRIMARY KEY(Id)
)

CREATE TABLE Towns
(
Id INT IDENTITY,
Name VARCHAR(30),
Country VARCHAR(30),
CONSTRAINT PK_Towns PRIMARY KEY(Id),
)

CREATE TABLE Minions
(
Id INT IDENTITY,
Name VARCHAR(30),
Age INT,
TownId INT,
CONSTRAINT PK_Minions PRIMARY KEY(Id),
CONSTRAINT FK_Minions_Towns FOREIGN KEY(TownId) REFERENCES Towns(Id)
)

CREATE TABLE MinionsVillains
(
MinionId INT,
VillainId INT,
CONSTRAINT PK_MinionsVillains PRIMARY KEY(MinionId, VillainId),
CONSTRAINT FK_MinionsVillains_Minions FOREIGN KEY(MinionId) REFERENCES Minions(Id),
CONSTRAINT FK_MinionsVillains_Villains FOREIGN KEY(VillainId) REFERENCES Villains(Id)
)

INSERT INTO Towns(Name, Country)
VALUES
('Sofia', 'Bulgaria'),
('Plovdiv', 'Bulgaria'),
('Paris', 'France'),
('Berlin', 'Germany'),
('Liverpool', 'England')

INSERT INTO Minions(Name, Age, TownId)
Values
('Bob', 15, 2),
('Kevin', 23, 5),
('Dave', 28, 1),
('Stuart', 31, 3),
('Jerry', 19, 4)

INSERT INTO Villains(Name, EvilnessFactor)
VALUES
('Gru', 'good'),
('Balthazar Bratt', 'bad'),
('Vector', 'evil'),
('Scarlet Overkill', 'super evil'),
('Mr. Perkins', 'bad')

INSERT INTO MinionsVillains(MinionId, VillainId)
VALUES
(1, 1),
(1, 3),
(1, 5),
(2, 4),
(2, 5),
(3, 1),
(4, 2),
(4, 3),
(4, 4),
(5, 1),
(5, 5)