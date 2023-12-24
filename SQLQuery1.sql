CREATE TABLE couvrePlanche (
    id_couvre INTEGER PRIMARY KEY,
    type_couvre VARCHAR(20),
    Mat_couvre FLOAT,
    Main_couvre FLOAT
);

CREATE TABLE Client_couvre (
    Id_client INT PRIMARY KEY NOT NULL,
    Nom_client VARCHAR(20),
    Email VARCHAR(30)
);

CREATE TABLE Demande_client (
    Id_dem INT PRIMARY KEY NOT NULL,
    Id_client INT,
    largeur FLOAT,
    longueur FLOAT
);

INSERT INTO couvrePlanche(type_couvre, Mat_couvre, Main_couvre) VALUES ('Tapis commercial', 1.29, 2.00);
INSERT INTO couvrePlanche(type_couvre, Mat_couvre, Main_couvre) VALUES ('Tapis de qualité', 3.99, 2.25);
INSERT INTO couvrePlanche(type_couvre, Mat_couvre, Main_couvre) VALUES ('Plancher de bois franc', 3.49, 2.25);
INSERT INTO couvrePlanche(type_couvre, Mat_couvre, Main_couvre) VALUES ('Plancher Flottant', 1.99, 2.25);
INSERT INTO couvrePlanche(type_couvre, Mat_couvre, Main_couvre) VALUES ('Céramique', 1.49, 3.25);

UPDATE couvrePlanche SET type_couvre='Tapis marocain', Mat_couvre=2.00, Main_couvre=2.99 WHERE id_couvre=1;

DELETE FROM couvrePlanche WHERE id_couvre=1;


CREATE TABLE users(
id INT NOT NULL PRIMARY KEY IDENTITY,
firstName VARCHAR (100) NOT NULL,
lastName VARCHAR(100) NOT NULL,
userLogin VARCHAR(100) NOT NULL UNIQUE,
userPw VARCHAR (150) NOT NULL,
userType VARCHAR(20) NOT NULL );

INSERT INTO users(firstName,lastName,userLogin,userPw, userType) VALUES
('Jhon','Doe','admin@emsi','123456','admin');
INSERT INTO users(firstName,lastName,userLogin,userPw, userType) VALUES
('Jane','Doe','Jane@emsi','123456','user');