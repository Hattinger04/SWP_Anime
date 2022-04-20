create database testwebsite collate utf8mb4_general_ci; 

use testwebsite; 

create table users(
	user_id int unsigned not null auto_increment, 
	username varchar(50) not null,
	password varchar(300) not null, 
	email varchar(150) null, 
	birthdate date null, 
	gender int null, 

	constraint user_id_PK primary  key(user_id)
);

insert into users values(null, "Kristof", sha2("Hallo123!", 512), "k@gmail.com", "2004-07-24", "0");
insert into users values(null, "Johannes", sha2("Hallo123!", 512), "j@gmail.com", "2004-03-12", "0");
insert into users values(null, "Gabi", sha2("alsdjf!", 512), "n@gmail.com", "2004-06-10", "0");

select * from users; 