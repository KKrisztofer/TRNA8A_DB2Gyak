DECLARE
ev number(2);
BEGIN
ev:=7;
UPDATE auto
SET ar = ar*0.9
WHERE kor > ev;
COMMIT;
END;