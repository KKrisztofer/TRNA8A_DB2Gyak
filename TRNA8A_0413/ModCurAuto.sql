create or replace procedure aut_arnov(szazalek number) is
begin
    update piros_auto set ar=ar*(1+(100/szazalek)) where szin = 'piros';
end;

