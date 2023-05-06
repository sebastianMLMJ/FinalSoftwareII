
CREATE TABLE candidatos(
id_candidato int primary key auto_increment,
nombre varchar (100) not null unique,
partido varchar(100) not null
);

CREATE TABLE votante (
id_votante int primary key auto_increment,
nombre varchar(50)
);

CREATE TABLE votaciones(
id_voto int primary key auto_increment,
id_candidato int,
id_votante int,
fecha_hora datetime,
ip varchar(50),
constraint fk_voto_candidato foreign key (id_candidato) references candidatos(id_candidato),
constraint fk_voto_votante foreign key (id_votante) references votante(id_votante)
);

CREATE TABLE fase_crear_candidatos(
id_fase int primary key auto_increment,
activa boolean
);

CREATE TABLE fase_votaciones(
id_faseVotaciones int primary key auto_increment,
activa boolean
);

CREATE TABLE fraude(
id_fraude int primary key auto_increment,
fraudes int
);
