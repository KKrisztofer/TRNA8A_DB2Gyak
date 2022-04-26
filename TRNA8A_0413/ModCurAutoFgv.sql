create or replace procedure aut_arnov(szazalek number) is
begin
open cur_a;
loop
    fetch cur_a into a;
    exit when cur_a%notfound;
    update autok set ar=a.ar*(1+(100/szazalek)) where szin = 'piros';
end loop;
close cur_a;
end;


