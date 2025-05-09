--1)Write a cursor that loops through all films and prints titles longer than 120 minutes.
do $$
declare
	film_rec record;
	film_cur cursor for
	select title, length from film where length>120;
begin
	open film_cur;
	loop 
		fetch film_cur into film_rec;
		exit when not found;
		raise notice 'Title: % (% minutes)', film_rec.title, film_rec.length;
	end loop;
	close film_cur;
end;
$$


--2)Create a cursor that iterates through all customers and counts how many rentals each made.
do $$
declare
	cust_rec record;
	rental_count int;
	cust_cur cursor for
	select customer_id, first_name, last_name from customer;
begin
	open cust_cur;
	loop 
		fetch cust_cur into cust_rec;
		exit when not found;
		select count(*) into rental_count from rental
		where customer_id = cust_rec.customer_id;
		raise notice 'Customer: % % - Rentals: %', cust_rec.first_name, cust_rec.last_name, rental_count;
	end loop;
	close cust_cur;
end;
$$


--3)Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.
do $$
declare
	film_rec record;
	rental_cnt int;
	film_cur cursor for
	select film_id, rental_rate from film;
begin
	open film_cur;
	loop 
		fetch film_cur into film_rec;
		exit when not found;
		select count(*) into rental_cnt from rental
		where inventory_id in 
		(select inventory_id from inventory where film_id=film_rec.film_id);

		if rental_cnt < 5 then
			update film
			set rental_rate = rental_rate + 1
			where film_id = film_rec.film_id;
			raise notice 'Updated film id: % (New rate: %)', film_rec.film_id, film_rec.rental_rate+1;
		end if;
	end loop;
	close film_cur;
end;
$$


--4)Create a function using a cursor that collects titles of all films from a particular category.
create or replace function get_titles(cat_name text)
returns table(title text) as
$$
declare
	film_rec record;
	film_cur cursor for
		select f.title from film f
		join film_category fc on f.film_id = fc.film_id
		join category c on fc.category_id = c.category_id
		where c.name = cat_name;
begin
	open film_cur;
	loop
		fetch film_cur into film_rec;
		exit when not found;
		title := film_rec.title;
		return next;
	end loop;
	close film_cur;
end;
$$
language plpgsql;

select * from get_titles('Action');


--5)Loop through all stores and count how many distinct films are available in each store using a cursor.
do $$
declare
	film_rec record;
	film_cnt int;
	film_cur cursor for
	select store_id from store;
begin
	open film_cur;
	loop 
		fetch film_cur into film_rec;
		exit when not found;
		select count(distinct film_id) into film_cnt
		from inventory
		where store_id = film_rec.store_id;

		raise notice 'Store id: %,Distinct films count: %', film_rec.store_id, film_cnt;
	end loop;
	close film_cur;
end;
$$