toc.dat                                                                                             0000600 0004000 0002000 00000010706 14612203372 0014443 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        PGDMP   5    1                |            EMPLOYEE_MANAGEMENT_SYSTEM    16.1    16.1     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false         �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false         �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false         �           1262    41649    EMPLOYEE_MANAGEMENT_SYSTEM    DATABASE     �   CREATE DATABASE "EMPLOYEE_MANAGEMENT_SYSTEM" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
 ,   DROP DATABASE "EMPLOYEE_MANAGEMENT_SYSTEM";
                postgres    false         �            1259    41650 
   Department    TABLE     c   CREATE TABLE public."Department" (
    "Id" integer NOT NULL,
    "Name" character varying(128)
);
     DROP TABLE public."Department";
       public         heap    postgres    false         �            1259    41667    Department_Id_seq    SEQUENCE     �   ALTER TABLE public."Department" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Department_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    215         �            1259    41653    Employee    TABLE       CREATE TABLE public."Employee" (
    "Id" integer NOT NULL,
    "FirstName" character varying(128),
    "LastName" character varying(128),
    "DeptId" integer,
    "Age" integer,
    "Email" character varying(128),
    "Education" character varying(123),
    "Company" character varying(128),
    "Experience" character varying(128),
    "Package" character varying(128),
    "Gender" character varying(128),
    "IsDelete" bit(1),
    "Created Date" timestamp without time zone,
    "Modified Date" timestamp without time zone
);
    DROP TABLE public."Employee";
       public         heap    postgres    false         �            1259    41668    Employee_Id_seq    SEQUENCE     �   ALTER TABLE public."Employee" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Employee_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    216         �          0    41650 
   Department 
   TABLE DATA           4   COPY public."Department" ("Id", "Name") FROM stdin;
    public          postgres    false    215       4841.dat �          0    41653    Employee 
   TABLE DATA           �   COPY public."Employee" ("Id", "FirstName", "LastName", "DeptId", "Age", "Email", "Education", "Company", "Experience", "Package", "Gender", "IsDelete", "Created Date", "Modified Date") FROM stdin;
    public          postgres    false    216       4842.dat �           0    0    Department_Id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."Department_Id_seq"', 1, true);
          public          postgres    false    217         �           0    0    Employee_Id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public."Employee_Id_seq"', 6, true);
          public          postgres    false    218         V           2606    41657    Department Department_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public."Department"
    ADD CONSTRAINT "Department_pkey" PRIMARY KEY ("Id");
 H   ALTER TABLE ONLY public."Department" DROP CONSTRAINT "Department_pkey";
       public            postgres    false    215         X           2606    41661    Employee Employee_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Employee"
    ADD CONSTRAINT "Employee_pkey" PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public."Employee" DROP CONSTRAINT "Employee_pkey";
       public            postgres    false    216         Y           2606    41662    Employee Employee_DeptId_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Employee"
    ADD CONSTRAINT "Employee_DeptId_fkey" FOREIGN KEY ("DeptId") REFERENCES public."Department"("Id") NOT VALID;
 K   ALTER TABLE ONLY public."Employee" DROP CONSTRAINT "Employee_DeptId_fkey";
       public          postgres    false    215    4694    216                                                                  4841.dat                                                                                            0000600 0004000 0002000 00000000012 14612203372 0014243 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	IT
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      4842.dat                                                                                            0000600 0004000 0002000 00000001220 14612203372 0014246 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        3	Krisha	Lukka	1	21	krisha@gmail.com	Graduate	Tatvasoft	21	6.12 L	Female	0	2024-04-24 16:23:29.463761	\N
2	Krinal	Shah	1	21	krinal@gmail.com	Graduate	Tatvasoft	1	6.12 L	Female	0	2024-04-24 16:23:29.463761	\N
1	Krina	Bhalodiya	1	21	krina@gmail.com	Graduate	Tatvasoft	1	10 L	\N	0	2024-04-24 16:23:29.463761	2024-04-24 18:04:59.400798
4	Krishn	thakkat	1	21	krishn@gmail.com	Graduate	Tatvasoft	1	10.12 L	Male	0	2024-04-24 18:31:34.190254	\N
5	komal	malkan	1	21	komal@gmail.com	Graduate	Tatvasoft	1	10.12 L	Female	0	2024-04-24 18:32:07.327136	\N
6	shreyansh	padmani	1	21	shreyansh@gmail.com	Graduate	Tatvasoft	1	10.12 L	Male	0	2024-04-24 18:32:32.65263	\N
\.


                                                                                                                                                                                                                                                                                                                                                                                restore.sql                                                                                         0000600 0004000 0002000 00000010704 14612203372 0015366 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        --
-- NOTE:
--
-- File paths need to be edited. Search for $$PATH$$ and
-- replace it with the path to the directory containing
-- the extracted data files.
--
--
-- PostgreSQL database dump
--

-- Dumped from database version 16.1
-- Dumped by pg_dump version 16.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE "EMPLOYEE_MANAGEMENT_SYSTEM";
--
-- Name: EMPLOYEE_MANAGEMENT_SYSTEM; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "EMPLOYEE_MANAGEMENT_SYSTEM" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';


ALTER DATABASE "EMPLOYEE_MANAGEMENT_SYSTEM" OWNER TO postgres;

\connect "EMPLOYEE_MANAGEMENT_SYSTEM"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Department; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Department" (
    "Id" integer NOT NULL,
    "Name" character varying(128)
);


ALTER TABLE public."Department" OWNER TO postgres;

--
-- Name: Department_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Department" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Department_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Employee; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Employee" (
    "Id" integer NOT NULL,
    "FirstName" character varying(128),
    "LastName" character varying(128),
    "DeptId" integer,
    "Age" integer,
    "Email" character varying(128),
    "Education" character varying(123),
    "Company" character varying(128),
    "Experience" character varying(128),
    "Package" character varying(128),
    "Gender" character varying(128),
    "IsDelete" bit(1),
    "Created Date" timestamp without time zone,
    "Modified Date" timestamp without time zone
);


ALTER TABLE public."Employee" OWNER TO postgres;

--
-- Name: Employee_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Employee" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Employee_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Data for Name: Department; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Department" ("Id", "Name") FROM stdin;
\.
COPY public."Department" ("Id", "Name") FROM '$$PATH$$/4841.dat';

--
-- Data for Name: Employee; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Employee" ("Id", "FirstName", "LastName", "DeptId", "Age", "Email", "Education", "Company", "Experience", "Package", "Gender", "IsDelete", "Created Date", "Modified Date") FROM stdin;
\.
COPY public."Employee" ("Id", "FirstName", "LastName", "DeptId", "Age", "Email", "Education", "Company", "Experience", "Package", "Gender", "IsDelete", "Created Date", "Modified Date") FROM '$$PATH$$/4842.dat';

--
-- Name: Department_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Department_Id_seq"', 1, true);


--
-- Name: Employee_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Employee_Id_seq"', 6, true);


--
-- Name: Department Department_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Department"
    ADD CONSTRAINT "Department_pkey" PRIMARY KEY ("Id");


--
-- Name: Employee Employee_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Employee"
    ADD CONSTRAINT "Employee_pkey" PRIMARY KEY ("Id");


--
-- Name: Employee Employee_DeptId_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Employee"
    ADD CONSTRAINT "Employee_DeptId_fkey" FOREIGN KEY ("DeptId") REFERENCES public."Department"("Id") NOT VALID;


--
-- PostgreSQL database dump complete
--

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            