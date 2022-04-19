create or replace trigger workertrigger
before insert or delete or update on workers for each row
begin
    if inserting then
        insert into data_log values(current_timestamp, 'beszúrás', 'workers');
    end if;
    if deleting then
        insert into data_log values(current_timestamp, 'törlés', 'workers');
    end if;
    if updating then
        insert into data_log values(current_timestamp, 'módosítás', 'workers');
    end if;
end;