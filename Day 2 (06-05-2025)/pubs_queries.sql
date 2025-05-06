use pubs;
select title from titles;

select * from titles where pub_id='1389';

select * from titles where price between 10 and 15;

select * from titles where price IS NULL;

select title from titles where title like 'The%';

select title from titles where title not like '%v%';

select * from titles order by royalty;

select * from titles order by pub_id desc, type asc, price desc;

select type, avg(price) as avg_price from titles group by type;

select distinct type from titles;

select top 2 title from titles order by price desc;

select * from titles where type='business' and price<20 and advance>7000;

select pub_id, count(title) as no_of_books from titles  
where price between 15 and 25 and title like '%It%' group by pub_id having count(title)>2 order by count(title);

select au_id, au_lname, au_fname, state from authors where state='CA';

select state, count(au_id) as cnt from authors group by state;