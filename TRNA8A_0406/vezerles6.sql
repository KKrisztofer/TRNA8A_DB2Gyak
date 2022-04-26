DECLARE
a number;
b number;
c number;
s number;
T number;
BEGIN
a:=3;
b:=4;
c:=5;
s:=(a+b+c)/2;
T:=sqrt(s*(s-a)*(s-b)*(s-c));
dbms_output.put_line(T);
END;