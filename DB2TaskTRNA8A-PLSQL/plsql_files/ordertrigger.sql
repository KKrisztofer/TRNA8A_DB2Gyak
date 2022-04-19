create or replace trigger orderrigger
before insert or delete or update on orders for each row
begin
    if inserting then
        insert into data_log values(current_timestamp, 'beszúrás', 'orders');
    end if;
    if deleting then
        insert into data_log values(current_timestamp, 'törlés', 'orders');
    end if;
    if updating then
        insert into data_log values(current_timestamp, 'módosítás', 'orders');
    end if;
end;