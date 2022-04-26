CREATE OR REPLACE PROCEDURE tlp AS
    CURSOR cur IS SELECT * FROM Kategoria;
    CV cur%rowtype;
BEGIN
    OPEN cur;
    LOOP
        FETCH cur INTO cv;
        EXIT WHEN cur%notfound;
        dbms_output.put_line('Név: '||cv.nev);
        dbms_output.put_line('Feldolgozva: '||cur%notfound);
    END LOOP;
    CLOSE cur;
END;

