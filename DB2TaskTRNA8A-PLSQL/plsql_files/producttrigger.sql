create or replace trigger producttrigger
before insert or delete or update on products for each row
begin
    if inserting then
        insert into data_log values(current_timestamp, 'besz�r�s', 'products');
    end if;
    if deleting then
        insert into data_log values(current_timestamp, 't�rl�s', 'products');
    end if;
    if updating then
        insert into data_log values(current_timestamp, 'm�dos�t�s', 'products');
    end if;
end;