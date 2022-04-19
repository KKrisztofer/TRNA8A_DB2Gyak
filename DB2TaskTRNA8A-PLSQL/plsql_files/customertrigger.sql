create or replace trigger customertrigger
before insert or delete or update on customers for each row
begin
    if inserting then
        insert into data_log values(current_timestamp, 'beszúrás', 'customers');
    end if;
    if deleting then
        insert into data_log values(current_timestamp, 'törlés', 'customers');
    end if;
    if updating then
        insert into data_log values(current_timestamp, 'módosítás', 'customers');
    end if;
end;