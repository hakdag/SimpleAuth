-- public."user" definition

-- Drop table

-- DROP TABLE public."user";

CREATE TABLE public."user" (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_DATE,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	username varchar NOT NULL,
	"password" varchar NOT NULL,
	"token" varchar NULL,
	lastlogindate timestamp NULL
);
CREATE UNIQUE INDEX user_username_idx ON public."user" USING btree (username);

-- public."role" definition

-- Drop table

-- DROP TABLE public."role";

CREATE TABLE public."role" (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_DATE,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	"name" varchar NOT NULL
);
CREATE UNIQUE INDEX role_name_idx ON public.role USING btree (name);