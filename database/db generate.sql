-- public."user" definition

-- Drop table

-- DROP TABLE public."user";

CREATE TABLE public."user" (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
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
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	"name" varchar NOT NULL
);
CREATE UNIQUE INDEX role_name_idx ON public.role USING btree (name);

-- public.userrole definition

-- Drop table

-- DROP TABLE public.userrole;

CREATE TABLE public.userrole (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	userid int8 NOT NULL,
	roleid int8 NOT NULL
);

-- public."permission" definition

-- Drop table

-- DROP TABLE public."permission";

CREATE TABLE public."permission" (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	"name" varchar NOT NULL
);
CREATE UNIQUE INDEX permission_name_idx ON public.permission USING btree (name);

-- public.rolepermission definition

-- Drop table

-- DROP TABLE public.rolepermission;

CREATE TABLE public.rolepermission (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	roleid int8 NOT NULL,
	permissionid int8 NOT NULL
);
CREATE UNIQUE INDEX rolepermission_roleid_idx ON public.rolepermission USING btree (roleid, permissionid);

-- public.userpermission definition

-- Drop table

-- DROP TABLE public.userpermission;

CREATE TABLE public.userpermission (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	userid int8 NOT NULL,
	permissionid int8 NOT NULL
);
CREATE UNIQUE INDEX userpermission_userid_idx ON public.userpermission USING btree (userid, permissionid);

-- public.passwordhistory definition

-- Drop table

-- DROP TABLE public.passwordhistory;

CREATE TABLE public.passwordhistory (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	userid int8 NOT NULL,
	passwordhash varchar NOT NULL
);

-- public.useraccountlock definition

-- Drop table

-- DROP TABLE public.useraccountlock;

CREATE TABLE public.useraccountlock (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	userid int8 NOT NULL
);
CREATE UNIQUE INDEX useraccountlock_userid_idx ON public.useraccountlock USING btree (userid);

-- public.authenticateattempt definition

-- Drop table

-- DROP TABLE public.authenticateattempt;

CREATE TABLE public.authenticateattempt (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	userid int8 NOT NULL
);

-- public.passwordresetkey definition

-- Drop table

-- DROP TABLE public.passwordresetkey;

CREATE TABLE public.passwordresetkey (
	id int8 NOT NULL GENERATED ALWAYS AS IDENTITY,
	createddate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updateddate timestamp NULL,
	isdeleted bool NOT NULL DEFAULT false,
	userid int8 NOT NULL,
	resetkey varchar NOT NULL
);

