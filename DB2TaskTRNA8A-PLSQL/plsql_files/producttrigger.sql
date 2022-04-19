create or replace trigger producttrigger
before insert or delete or update on products for each row
begin
    if inserting then
        insert into data_log values(current_timestamp, 'beszúrás', 'products');
    end if;
    if deleting then
        insert into data_log values(current_timestamp, 'törlés', 'products');
    end if;
    if updating then
        insert into data_log values(current_timestamp, 'módosítás', 'products');
    end if;
end;