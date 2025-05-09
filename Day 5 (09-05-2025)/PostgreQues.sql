--1)List all films with their length and rental rate, sorted by length descending.
select title, length, rental_rate from film
order by length desc;

--2)Find the top 5 customers who have rented the most films.
select * from rental;
select * from customer;
select customer.customer_id Customer, count(rental.rental_id) Count_Films
from customer join rental on customer.customer_id = rental.customer_id
group by customer.customer_id order by count_films desc limit 5;

--3)Display all films that have never been rented.
select * from film;
select * from inventory;
select f.film_id, r.rental_id from film f left outer join inventory i
on f.film_id = i.film_id left outer join rental r
on i.inventory_id = r.inventory_id where r.rental_id is null
order by film_id;


--4)List all actors who appeared in the film ‘Academy Dinosaur’.
select * from film;
select * from actor;
select fa.actor_id, concat(a.first_name,' ',a.last_name), f.title
from film f
join film_actor fa on f.film_id = fa.film_id
join actor a on fa.actor_id = a.actor_id
where f.title = 'Academy Dinosaur' order by actor_id;


--5)List each customer along with the total number of rentals they made and the total amount paid.
select c.customer_id, count(r.rental_id) count_rentals, sum(p.amount) total_amount
from customer c join rental r on c.customer_id = r.customer_id
join payment p on r.rental_id = p.rental_id
group by c.customer_id order by customer_id;


--6)Using a CTE, show the top 3 rented movies by number of rentals.
select * from film;
select * from inventory;
select * from rental;
with rental_count as
(select f.title, count(rental_id) count_rentals
from inventory i join rental r on i.inventory_id = r.inventory_id
join film f on f.film_id = i.film_id
group by f.title) 

select * from rental_count order by count_rentals desc limit 3;


--7)Find customers who have rented more than the average number of films.
with customer_rental as
(select customer_id, count(rental_id) count_rental
from rental group by customer_id),
avg_rental as 
(select avg(count_rental) avg_count from customer_rental)

select concat(c.first_name,' ',c.last_name) customer, cr.count_rental count_above_avg
from customer c join customer_rental cr
on c.customer_id = cr.customer_id join avg_rental ar
on cr.count_rental > ar.avg_count order by count_above_avg;


--8)Write a function that returns the total number of rentals for a given customer ID.
select * from rental;
create function get_total_rentals(cust_id int)
returns int
as $$
declare total_rentals int;
begin
	select count(rental_id) into total_rentals from rental 
	where customer_id = cust_id;
	return total_rentals;
end;
$$ 
language plpgsql;

select get_total_rentals(1);


--9)Write a stored procedure that updates the rental rate of a film by film ID and new rate.
select * from film;
create procedure update_rental_rate(fm_id INT, new_rate NUMERIC)
as $$
begin
	update film
	set rental_rate = new_rate
	where film_id = fm_id;
end;
$$ 
language plpgsql;

call update_rental_rate(133,5.99);

select * from film where film_id=133;


--10)Write a procedure to list overdue rentals (return date is NULL and rental date older than 7 days).
select * from rental;
create or replace procedure get_overdue_rentals()
as $$
declare rid int;
begin
	select rental_id into rid from rental
	where return_date is null and rental_date < now()-interval '7 days';
end;
$$ 
language plpgsql;

call get_overdue_rentals();


