DECLARE
a number;
b number;
c number;
n number;
BEGIN
n:=20;
a:=0;
b:=1;
dbms_output.put_line(0);
dbms_output.put_line(1);
FOR i IN 2..n
LOOP
    c:=a+b;
    dbms_output.put_line(c);
    a:=b;
    b:=c;
END LOOP;
END;