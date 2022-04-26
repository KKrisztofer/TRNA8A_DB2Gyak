DECLARE
a number;
b number;
c number;
BEGIN
a:=7;
b:=3;
c:=4;
if a+b>c and a+c>b and b+c>a then
dbms_output.put_line('A háromszög szerkeszthetõ');
else
dbms_output.put_line('A háromszög nem szerkeszthetõ');
end if;
END;