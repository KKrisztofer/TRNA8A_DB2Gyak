DECLARE
veznev varchar2(50);
kernev varchar2(50);
BEGIN
veznev:='Kerekes ';
kernev:='Krisztofer';
dbms_output.put_line(veznev||kernev);
END;