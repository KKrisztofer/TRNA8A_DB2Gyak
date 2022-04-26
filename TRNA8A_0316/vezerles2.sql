DECLARE
x number;
y number;
BEGIN
x:=7;
y:=5;
if x>y then
dbms_output.put_line('Az x a nagyobb');
elsif y>x then
dbms_output.put_line('Az y a nagyobb');
else
dbms_output.put_line('A két szám egyenlõ');
end if;
END;