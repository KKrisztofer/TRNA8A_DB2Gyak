create or replace procedure VDbKiir is
rows_number number;
begin
    select count(*)
    into rows_number
    from Vasarlas;
    dbms_output.put_line('Rekordok sz�ma: ' || rows_number);
end;