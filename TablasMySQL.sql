
CREATE TABLE candidatos(
id_candidato int primary key auto_increment,
nombre varchar (100) not null unique,
partido varchar(100) not null
);

CREATE TABLE votaciones(
id_voto int primary key auto_increment,
id_candidato int,
constraint fk_voto_usuario foreign key (id_candidato) references candidatos(id_candidato)
);

CREATE TABLE fase_crear_candidatos(
id_fase int primary key auto_increment,
activa boolean
);

CREATE TABLE fase_votaciones(
id_faseVotaciones int primary key auto_increment,
activa boolean
);


