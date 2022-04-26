CREATE OR REPLACE PROCEDURE tlpfor AS
    CURSOR cur IS SELECT * FROM Kategoria;
BEGIN
    FOR cv IN cur
    LOOP
        dbms_output.put_line('Név: '||cv.nev);
        dbms_output.put_line('Feldolgozva: '||cur%rowcount);
    END LOOP;
END;

