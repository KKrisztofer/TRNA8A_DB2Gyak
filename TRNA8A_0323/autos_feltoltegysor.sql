DECLARE
rsz varchar2(6);
tipus varchar2(20);
szin varchar2(10);
kor number(2);
ar number(10);
BEGIN
rsz:='ABC123';
tipus:='Audi';
szin:='fekete';
kor:=3;
ar:=8000000;
INSERT INTO auto(rsz, tipus, szin, kor, ar)
VALUES(rsz, tipus, szin, kor, ar);
COMMIT;
END;