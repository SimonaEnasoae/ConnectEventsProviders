use providersDb;

create table Tag(
id varchar(40) NOT NULL PRIMARY KEY,
value varchar(40)
)

create table Provider(
id varchar(40) NOT NULL PRIMARY KEY,
title varchar(40),
description varchar(200),
location varchar(40),
picture_uri varchar(200),
tagId varchar(40) FOREIGN key References Tag(id)
)