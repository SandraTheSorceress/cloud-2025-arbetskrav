	CREATE DATABASE todo_database;

	use todo_database;

	create table todo (
	id int,
	description text
	);

	insert into todo values (1, "plant flowers");
	insert into todo values (2, "go on a picnic");
	insert into todo values (3, "take a nature walk");
	insert into todo values (4, "study for the cloud exam");

	commit;
	select * from todo;