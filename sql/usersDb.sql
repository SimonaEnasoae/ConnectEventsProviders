use eventsDb;
create table UserAuths(
id varchar(40)  NOT NULL PRIMARY KEY,
password varchar(30),
email varchar(30),
username varchar(30),
type int)
