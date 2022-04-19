create or replace trigger orderrigger
before insert or delete or update on orders for each row
begin
    if inserting then
        insert into data_log values(current_timestamp, 'besz�r�s', 'orders');
    end if;
    if deleting then
        insert into data_log values(current_timestamp, 't�rl�s', 'orders');
    end if;
    if updating then
        insert into data_log values(current_timestamp, 'm�dos�t�s', 'orders');
    end if;
end;