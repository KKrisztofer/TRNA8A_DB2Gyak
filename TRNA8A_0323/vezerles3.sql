DECLARE
x number;
a number;
b number;
BEGIN
x:=78;
a:=10;
b:=100;
if x>a and x<b then
dbms_output.put_line('Az x az intervallumon bel�l van');
else
dbms_output.put_line('Az x az intervallumon k�v�l van');
end if;
END;