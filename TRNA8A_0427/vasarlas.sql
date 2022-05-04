create table Vasarlas(
    sorszam number(38) not null,
    idopont timestamp default current_timestamp,
    tkod char(3) primary key,
    darab number(38) not null,
    vid char(3) not null
);