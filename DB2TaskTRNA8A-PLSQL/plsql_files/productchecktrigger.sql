create or replace trigger productupdatechecktrigger before update on products for each row
begin
    if not ((:new.price / (:old.price / 100)) >= 80 and (:new.price / (:old.price / 100)) <= 120) then
        if :old.price < :new.price then
            :new.price := :old.price * 1.2;
        end if;
        if :new.price < :old.price then
            :new.price := :old.price * 0.8;
        end if;
        dbms_output.put_line('A m�dos�t�s m�rt�ke meghaladja a 20%-ot, ez�rt korl�tozva lett!');
    end if;
end;