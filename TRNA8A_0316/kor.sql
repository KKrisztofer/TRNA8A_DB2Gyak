DECLARE
pi CONSTANT number(3,2) := 3.14;
r number;
T number;
BEGIN
r:=12;
T:=POWER(r,2)*pi;
dbms_output.put_line(T);
END;