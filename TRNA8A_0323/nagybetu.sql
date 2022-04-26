DECLARE
nev varchar2(100);
BEGIN
nev:='Kerekes Krisztofer';
dbms_output.put_line(Upper(nev));
dbms_output.put_line(Lower(nev));
END;