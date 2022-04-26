DECLARE
a auto.ar%type;
BEGIN
SELECT AVG(ar) INTO a FROM auto;
COMMIT;
dbms_output.put_line(a);
END;