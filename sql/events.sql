use events;
create table users(
id varchar(40)  NOT NULL PRIMARY KEY,
password varchar(30),
username varchar(30),
type varchar(30))


use eventsDb;
create table tag(
id varchar(40),
value varchar(40),
EventId varchar(40)

)

use eventsDb;
create table event(
id varchar(40) NOT NULL PRIMARY KEY,
title varchar(40),
description varchar(200),
location varchar(40),
start_date DATE,
end_date DATE,
organiser_id varchar(40),
picture_uri varchar(200)
)

use eventsDb;
create table EventTag(
  eventId varchar(40) FOREIGN key References event(id),
  tagId varchar(40) FOREIGN key References tag(id),

)



