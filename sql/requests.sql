use requestsDb;

create table RequestEvent(
id varchar(40)  NOT NULL PRIMARY KEY,
eventId varchar(40),
senderId varchar(40),
receiverId varchar(40),
status int)
