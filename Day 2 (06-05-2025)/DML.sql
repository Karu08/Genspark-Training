
create table item(itemname varchar(20) primary key, itemtype varchar(20), itemcolor varchar(20));

create table department(deptname varchar(20) primary key, floor int, phone int);

create table emp(empno int primary key, empname varchar(20), salary int, deptname varchar(20), bossno int,
foreign key(deptname) references department(deptname), foreign key(bossno) references emp(empno));

create table sales(salesno int primary key, saleqty int, itemname varchar(20) not null, deptname varchar(20) not null,
foreign key(itemname) references item(itemname), foreign key(deptname) references department(deptname));



insert into item values('pocket knife-nile','E','brown');
insert into item values('pocket knife-avon','E','brown');
insert into item values('compass','N','');
insert into item values('geo positioning sys','N','');
insert into item values('elephant polo stick','R','bamboo');
insert into item values('camel saddle','R','brown');
insert into item values('sextant','N','');
insert into item values('map measure','N','');
insert into item values('boots-snake proof','C','green');
insert into item values('pith helmet','C','khaki');
insert into item values('hat-polar explorer','C','white');
insert into item values('expl in 10 lessons','B','');
insert into item values('hammock','F','khaki');
insert into item values('how to win frgnfrnds','B','');
insert into item values('map case','E','brown');
insert into item values('safari chair','F','khaki');
insert into item values('safari cooking kit','F','khaki');
insert into item values('stetson','C','black');
insert into item values('tent-2 person','F','khaki');
insert into item values('tent-8 person','F','khaki');

select * from item;



insert into department values('management',5,34);
insert into department values('books',1,81);
insert into department values('clothes',2,24);
insert into department values('equipment',3,57);
insert into department values('furniture',4,14);
insert into department values('navigation',1,41);
insert into department values('recreation',2,29);
insert into department values('accounting',5,35);
insert into department values('purchasing',5,36);
insert into department values('personnel',5,37);
insert into department values('marketing',5,38);

select * from department;

insert into emp values(1,'Alice',75000,'management',null);
insert into emp values(2,'Ned',45000,'marketing',1);
insert into emp values(3,'Andrew',25000,'marketing',2);
insert into emp values(4,'Clare',22000,'marketing',2);
insert into emp values(5,'Todd',38000,'accounting',1);
insert into emp values(6,'Nancy',22000,'accounting',5);
insert into emp values(7,'Brier',43000,'purchasing',1);
insert into emp values(8,'Sarah',56000,'purchasing',7);
insert into emp values(9,'Sophile',35000,'personnel',1);
insert into emp values(10,'Sanjay',15000,'navigation',3);
insert into emp values(11,'Rita',15000,'books',4);
insert into emp values(12,'Gigi',16000,'clothes',4);
insert into emp values(13,'Maggie',11000,'clothes',4);
insert into emp values(14,'Paul',15000,'equipment',3);
insert into emp values(15,'James',15000,'equipment',3);
insert into emp values(16,'Pat',15000,'furniture',3);
insert into emp values(17,'Mark',15000,'recreation',3);

select * from emp;

insert into sales values(101,2,'boots-snake proof','clothes');
insert into sales values(102,1,'pith helmet','clothes');
insert into sales values(103,1,'sextant','navigation');
insert into sales values(104,3,'hat-polar explorer','clothes');
insert into sales values(105,5,'pith helmet','equipment');
insert into sales values(106,2,'pocket knife-nile','clothes');
insert into sales values(107,3,'pocket knife-nile','recreation');
insert into sales values(108,1,'compass','navigation');
insert into sales values(109,5,'geo positioning sys','navigation');
insert into sales values(110,2,'map measure','navigation');

select * from sales;

alter table department add empno int;
alter table department add foreign key(empno) references emp(empno);

update department set empno=1 where phone=34;
update department set empno=4 where phone=81;
update department set empno=4 where phone=24;
update department set empno=3 where phone=57;
update department set empno=3 where phone=14;
update department set empno=3 where phone=41;
update department set empno=4 where phone=29;
update department set empno=5 where phone=35;
update department set empno=7 where phone=36;
update department set empno=9 where phone=37;
update department set empno=2 where phone=38;

select * from department;




