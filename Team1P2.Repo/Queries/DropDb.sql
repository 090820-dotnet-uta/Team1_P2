select * from MediaTags;
select * from Notes;
select * from Blurbs;
select * from Users;
select * from Medias;
select * from Tags;
select * from FollowingEntries;

-- Proper order to drop all tables
drop table MediaTags;
drop table FollowingEntries;
drop table Notes;
drop table Blurbs;
drop table Users;
drop table Medias;
drop table Tags;