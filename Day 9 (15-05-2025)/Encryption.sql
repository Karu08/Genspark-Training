create extension if not exists pgcrypto;

select pgp_sym_encrypt('hello world','pwd') as enc;
select pgp_sym_decrypt(pgp_sym_encrypt('hello world','pwd'),'pwd');

select * from customer;


--1)Create a stored procedure/function to encrypt a given text
create or replace function sp_encrypt_text(plain_text text, passkey text)
returns bytea as
$$
declare
	encrypted_data bytea;
begin
	encrypted_data := pgp_sym_encrypt(plain_text, passkey);
	return encrypted_data;
end;
$$
language plpgsql;

select sp_encrypt_text(
	(select email from customer where customer_id=524), 'mykey'
) as encrypted_data;


--2)Create a stored procedure to compare two encrypted texts
create or replace function sp_compare_encrypted(enc_text1 bytea, enc_text2 bytea, passkey text)
returns boolean as
$$
declare
	decrypt1 text;
	decrypt2 text;
begin
	decrypt1 := pgp_sym_decrypt(enc_text1, passkey);
	decrypt2 := pgp_sym_decrypt(enc_text2, passkey);
	return decrypt1 = decrypt2;
end;
$$
language plpgsql;

select sp_compare_encrypted(
	(select sp_encrypt_text((select email from customer where customer_id=524), 'mykey')),
	(select sp_encrypt_text((select email from customer where customer_id=524), 'mykey')),
	'mykey'
);

select sp_compare_encrypted(
	(select sp_encrypt_text((select first_name from customer where customer_id=1), 'mykey')),
	(select sp_encrypt_text((select last_name from customer where customer_id=1), 'mykey')),
	'mykey'
);


--3)Create a stored procedure to partially mask a given text.
create or replace function sp_mask_text(input_text text)
returns text as
$$
declare
	len int := length(input_text);
	masked_text text;
begin
	masked_text := substring(input_text from 1 for 2) || repeat('*',len-4) || substring(input_text from len-1 for 2);
	return masked_text;
end;
$$
language plpgsql;

select sp_mask_text('Jessica');


--4)Create a procedure to insert into customer with encrypted email and masked name
select * from customer;
create or replace procedure sp_insert(email text, cname text, passkey text, addr_id int, str_id int)
as
$$
declare
	enc_email bytea;
	masked_text text;
begin
	enc_email := sp_encrypt_text(email, passkey);
	masked_text := sp_mask_text(cname);

	insert into customer(first_name, email, address_id, store_id) 
	values(masked_text, enc_email, addr_id, str_id);
end;
$$
language plpgsql;


call sp_insert('ron.abc@hog.org', 'Ronald', 'mykey', 12, 2);


--5)Create a procedure to fetch and display masked first_name and decrypted email for all customers
create or replace function sp_read_customer_masked(passkey text)
returns table(cust_id int, masked_name text, dec_email text)
as
$$
begin
	return query
	select customer_id, sp_mask_text(first_name), 
	pgp_sym_decrypt(sp_encrypt_text(email,passkey), passkey)
	from customer;
end;
$$
language plpgsql;

select sp_read_customer_masked('mykey');