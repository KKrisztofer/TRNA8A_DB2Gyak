create or replace trigger tbtrigger before insert on vasarlo for each row
begin
    insert into Naplo5 values('beszúrás', :new.vid || :new.nev || :new.cim, current_timestamp);
end;