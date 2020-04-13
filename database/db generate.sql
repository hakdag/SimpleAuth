-- public."user" definition

-- Drop table

-- DROP TABLE public."user";

CREATE TABLE public."user" (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate date NOT NULL DEFAULT CURRENT_DATE,
	updateddate date NULL,
	isdeleted bool NOT NULL DEFAULT false,
	username varchar NOT NULL,
	"password" varchar NOT NULL,
	"token" varchar NULL,
	lastlogindate date NULL
);
CREATE UNIQUE INDEX user_username_idx ON public."user" USING btree (username);