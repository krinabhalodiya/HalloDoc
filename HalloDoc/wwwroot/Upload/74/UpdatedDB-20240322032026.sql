toc.dat                                                                                             0000600 0004000 0002000 00000305700 14577030366 0014457 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        PGDMP       %                |            HalloDoc    16.1    16.1    g           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false         h           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false         i           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false         j           1262    32782    HalloDoc    DATABASE     �   CREATE DATABASE "HalloDoc" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
    DROP DATABASE "HalloDoc";
                postgres    false         �            1259    32783    User    TABLE     )  CREATE TABLE public."User" (
    userid integer NOT NULL,
    aspnetuserid character varying(128),
    firstname character varying(100) NOT NULL,
    lastname character varying(100),
    email character varying(50) NOT NULL,
    mobile character varying(20),
    ismobile bit(1),
    street character varying(100),
    city character varying(100),
    state character varying(100),
    regionid integer,
    zipcode character varying(10),
    strmonth character varying(20),
    intyear integer,
    intdate integer,
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    status smallint,
    isdeleted bit(1),
    ip character varying(20),
    isrequestwithemail bit(1)
);
    DROP TABLE public."User";
       public         heap    postgres    false         �            1259    32788    User_userid_seq    SEQUENCE     �   CREATE SEQUENCE public."User_userid_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public."User_userid_seq";
       public          postgres    false    215         k           0    0    User_userid_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public."User_userid_seq" OWNED BY public."User".userid;
          public          postgres    false    216         �            1259    32789    admin    TABLE     �  CREATE TABLE public.admin (
    adminid integer NOT NULL,
    aspnetuserid character varying(128) NOT NULL,
    firstname character varying(100) NOT NULL,
    lastname character varying(100),
    email character varying(50) NOT NULL,
    mobile character varying(20),
    address1 character varying(500),
    address2 character varying(500),
    city character varying(100),
    regionid integer,
    zip character varying(10),
    altphone character varying(20),
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    status smallint,
    isdeleted bit(1),
    roleid integer
);
    DROP TABLE public.admin;
       public         heap    postgres    false         �            1259    32794    admin_adminid_seq    SEQUENCE     �   CREATE SEQUENCE public.admin_adminid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.admin_adminid_seq;
       public          postgres    false    217         l           0    0    admin_adminid_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.admin_adminid_seq OWNED BY public.admin.adminid;
          public          postgres    false    218         �            1259    32795    adminregion    TABLE     �   CREATE TABLE public.adminregion (
    adminregionid integer NOT NULL,
    adminid integer NOT NULL,
    regionid integer NOT NULL
);
    DROP TABLE public.adminregion;
       public         heap    postgres    false         �            1259    32798    adminregion_adminregionid_seq    SEQUENCE     �   CREATE SEQUENCE public.adminregion_adminregionid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 4   DROP SEQUENCE public.adminregion_adminregionid_seq;
       public          postgres    false    219         m           0    0    adminregion_adminregionid_seq    SEQUENCE OWNED BY     _   ALTER SEQUENCE public.adminregion_adminregionid_seq OWNED BY public.adminregion.adminregionid;
          public          postgres    false    220         �            1259    32799    aspnetroles    TABLE     v   CREATE TABLE public.aspnetroles (
    id character varying(128) NOT NULL,
    name character varying(256) NOT NULL
);
    DROP TABLE public.aspnetroles;
       public         heap    postgres    false         �            1259    32802    aspnetuserroles    TABLE     w   CREATE TABLE public.aspnetuserroles (
    userid character varying(128) NOT NULL,
    roleid character varying(128)
);
 #   DROP TABLE public.aspnetuserroles;
       public         heap    postgres    false         �            1259    32805    aspnetusers    TABLE     p  CREATE TABLE public.aspnetusers (
    id character varying(128) NOT NULL,
    username character varying(256) NOT NULL,
    passwordhash character varying(255),
    email character varying(256),
    phonenumber character varying(20),
    "CreatedDate" timestamp without time zone NOT NULL,
    ip character varying(20),
    modifieddate timestamp without time zone
);
    DROP TABLE public.aspnetusers;
       public         heap    postgres    false         �            1259    32810    blockrequests    TABLE     `  CREATE TABLE public.blockrequests (
    blockrequestid integer NOT NULL,
    phonenumber character varying(50),
    email character varying(50),
    isactive bit(1),
    reason text,
    requestid character varying(50) NOT NULL,
    ip character varying(20),
    createddate timestamp without time zone,
    modifieddate timestamp without time zone
);
 !   DROP TABLE public.blockrequests;
       public         heap    postgres    false         �            1259    32815     blockrequests_blockrequestid_seq    SEQUENCE     �   CREATE SEQUENCE public.blockrequests_blockrequestid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 7   DROP SEQUENCE public.blockrequests_blockrequestid_seq;
       public          postgres    false    224         n           0    0     blockrequests_blockrequestid_seq    SEQUENCE OWNED BY     e   ALTER SEQUENCE public.blockrequests_blockrequestid_seq OWNED BY public.blockrequests.blockrequestid;
          public          postgres    false    225         �            1259    32816    business    TABLE     n  CREATE TABLE public.business (
    businessid integer NOT NULL,
    name character varying(100) NOT NULL,
    address1 character varying(500),
    address2 character varying(500),
    city character varying(50),
    regionid integer,
    zipcode character varying(10),
    phonenumber character varying(20),
    faxnumber character varying(20),
    isregistered bit(1),
    createdby character varying(128),
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    status smallint,
    isdeleted bit(1),
    ip character varying(20)
);
    DROP TABLE public.business;
       public         heap    postgres    false         �            1259    32821    business_businessid_seq    SEQUENCE     �   CREATE SEQUENCE public.business_businessid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.business_businessid_seq;
       public          postgres    false    226         o           0    0    business_businessid_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.business_businessid_seq OWNED BY public.business.businessid;
          public          postgres    false    227         �            1259    32822    casetag    TABLE     i   CREATE TABLE public.casetag (
    casetagid integer NOT NULL,
    name character varying(50) NOT NULL
);
    DROP TABLE public.casetag;
       public         heap    postgres    false         �            1259    32825    casetag_casetagid_seq    SEQUENCE     �   CREATE SEQUENCE public.casetag_casetagid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE public.casetag_casetagid_seq;
       public          postgres    false    228         p           0    0    casetag_casetagid_seq    SEQUENCE OWNED BY     O   ALTER SEQUENCE public.casetag_casetagid_seq OWNED BY public.casetag.casetagid;
          public          postgres    false    229         �            1259    32826 	   concierge    TABLE     �  CREATE TABLE public.concierge (
    conciergeid integer NOT NULL,
    conciergename character varying(100) NOT NULL,
    address character varying(150),
    street character varying(50) NOT NULL,
    city character varying(50) NOT NULL,
    state character varying(50) NOT NULL,
    zipcode character varying(50) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    regionid integer NOT NULL,
    ip character varying(20)
);
    DROP TABLE public.concierge;
       public         heap    postgres    false         �            1259    32829    concierge_conciergeid_seq    SEQUENCE     �   CREATE SEQUENCE public.concierge_conciergeid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.concierge_conciergeid_seq;
       public          postgres    false    230         q           0    0    concierge_conciergeid_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.concierge_conciergeid_seq OWNED BY public.concierge.conciergeid;
          public          postgres    false    231         �            1259    32830    emaillog    TABLE       CREATE TABLE public.emaillog (
    emaillogid numeric(9,0) NOT NULL,
    emailtemplate text NOT NULL,
    subjectname character varying(200) NOT NULL,
    emailid character varying(200) NOT NULL,
    confirmationnumber character varying(200),
    filepath text,
    roleid integer,
    requestid integer,
    adminid integer,
    physicianid integer,
    createdate timestamp without time zone NOT NULL,
    sentdate timestamp without time zone,
    isemailsent bit(1),
    senttries integer,
    action integer
);
    DROP TABLE public.emaillog;
       public         heap    postgres    false                    1259    41028    encounterform    TABLE     �  CREATE TABLE public.encounterform (
    encounterformid integer NOT NULL,
    requestid integer,
    historyofpresentillnessorinjury text,
    medicalhistory text,
    medications text,
    allergies text,
    temp text,
    hr text,
    rr text,
    bloodpressuresystolic text,
    bloodpressurediastolic text,
    o2 text,
    pain text,
    heent text,
    cv text,
    chest text,
    abd text,
    extremities text,
    skin text,
    neuro text,
    other text,
    diagnosis text,
    treatment_plan text,
    medicaldispensed text,
    procedures text,
    followup text,
    adminid integer,
    physicianid integer,
    isfinalize boolean DEFAULT false NOT NULL,
    createddate timestamp without time zone,
    modifieddate timestamp without time zone
);
 !   DROP TABLE public.encounterform;
       public         heap    postgres    false                    1259    41027 !   encounterform_encounterformid_seq    SEQUENCE     �   CREATE SEQUENCE public.encounterform_encounterformid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public.encounterform_encounterformid_seq;
       public          postgres    false    282         r           0    0 !   encounterform_encounterformid_seq    SEQUENCE OWNED BY     g   ALTER SEQUENCE public.encounterform_encounterformid_seq OWNED BY public.encounterform.encounterformid;
          public          postgres    false    281         �            1259    32835    healthprofessionals    TABLE     i  CREATE TABLE public.healthprofessionals (
    vendorid integer NOT NULL,
    vendorname character varying(100) NOT NULL,
    profession integer,
    faxnumber character varying(50) NOT NULL,
    address character varying(150),
    city character varying(100),
    state character varying(50),
    zip character varying(50),
    regionid integer,
    createddate timestamp without time zone NOT NULL,
    modifieddate timestamp without time zone,
    phonenumber character varying(100),
    isdeleted bit(1),
    ip character varying(20),
    email character varying(50),
    businesscontact character varying(100)
);
 '   DROP TABLE public.healthprofessionals;
       public         heap    postgres    false         �            1259    32840     healthprofessionals_vendorid_seq    SEQUENCE     �   CREATE SEQUENCE public.healthprofessionals_vendorid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 7   DROP SEQUENCE public.healthprofessionals_vendorid_seq;
       public          postgres    false    233         s           0    0     healthprofessionals_vendorid_seq    SEQUENCE OWNED BY     e   ALTER SEQUENCE public.healthprofessionals_vendorid_seq OWNED BY public.healthprofessionals.vendorid;
          public          postgres    false    234         �            1259    32841    healthprofessionaltype    TABLE       CREATE TABLE public.healthprofessionaltype (
    healthprofessionalid integer NOT NULL,
    professionname character varying(50) NOT NULL,
    createddate timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    isactive bit(1),
    isdeleted bit(1)
);
 *   DROP TABLE public.healthprofessionaltype;
       public         heap    postgres    false         �            1259    32845 /   healthprofessionaltype_healthprofessionalid_seq    SEQUENCE     �   CREATE SEQUENCE public.healthprofessionaltype_healthprofessionalid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 F   DROP SEQUENCE public.healthprofessionaltype_healthprofessionalid_seq;
       public          postgres    false    235         t           0    0 /   healthprofessionaltype_healthprofessionalid_seq    SEQUENCE OWNED BY     �   ALTER SEQUENCE public.healthprofessionaltype_healthprofessionalid_seq OWNED BY public.healthprofessionaltype.healthprofessionalid;
          public          postgres    false    236         �            1259    32846    menu    TABLE     �   CREATE TABLE public.menu (
    menuid integer NOT NULL,
    name character varying(50) NOT NULL,
    accounttype smallint NOT NULL,
    sortorder integer
);
    DROP TABLE public.menu;
       public         heap    postgres    false         �            1259    32849    menu_menuid_seq    SEQUENCE     �   CREATE SEQUENCE public.menu_menuid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.menu_menuid_seq;
       public          postgres    false    237         u           0    0    menu_menuid_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.menu_menuid_seq OWNED BY public.menu.menuid;
          public          postgres    false    238         �            1259    32850    orderdetails    TABLE     _  CREATE TABLE public.orderdetails (
    id integer NOT NULL,
    vendorid integer,
    requestid integer,
    faxnumber character varying(50),
    email character varying(50),
    businesscontact character varying(100),
    prescription text,
    noofrefill integer,
    createddate timestamp without time zone,
    createdby character varying(100)
);
     DROP TABLE public.orderdetails;
       public         heap    postgres    false         �            1259    32855    orderdetails_id_seq    SEQUENCE     �   CREATE SEQUENCE public.orderdetails_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.orderdetails_id_seq;
       public          postgres    false    239         v           0    0    orderdetails_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.orderdetails_id_seq OWNED BY public.orderdetails.id;
          public          postgres    false    240         �            1259    32856 	   physician    TABLE     �  CREATE TABLE public.physician (
    physicianid integer NOT NULL,
    aspnetuserid character varying(128),
    firstname character varying(100) NOT NULL,
    lastname character varying(100),
    email character varying(50) NOT NULL,
    mobile character varying(20),
    medicallicense character varying(500),
    photo character varying(100),
    adminnotes character varying(500),
    isagreementdoc bit(1),
    isbackgrounddoc bit(1),
    istrainingdoc bit(1),
    isnondisclosuredoc bit(1),
    address1 character varying(500),
    address2 character varying(500),
    city character varying(100),
    regionid integer,
    zip character varying(10),
    altphone character varying(20),
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    status smallint,
    businessname character varying(100) NOT NULL,
    businesswebsite character varying(200) NOT NULL,
    isdeleted bit(1),
    roleid integer,
    npinumber character varying(500),
    islicensedoc bit(1),
    signature character varying(100),
    iscredentialdoc bit(1),
    istokengenerate bit(1),
    syncemailaddress character varying(50)
);
    DROP TABLE public.physician;
       public         heap    postgres    false         �            1259    32861    physician_physicianid_seq    SEQUENCE     �   CREATE SEQUENCE public.physician_physicianid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.physician_physicianid_seq;
       public          postgres    false    241         w           0    0    physician_physicianid_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.physician_physicianid_seq OWNED BY public.physician.physicianid;
          public          postgres    false    242         �            1259    32862    physicianlocation    TABLE       CREATE TABLE public.physicianlocation (
    locationid integer NOT NULL,
    physicianid integer NOT NULL,
    latitude numeric(9,5),
    longitude numeric(9,5),
    createddate timestamp without time zone,
    physicianname character varying(50),
    address character varying(500)
);
 %   DROP TABLE public.physicianlocation;
       public         heap    postgres    false         �            1259    32867     physicianlocation_locationid_seq    SEQUENCE     �   CREATE SEQUENCE public.physicianlocation_locationid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 7   DROP SEQUENCE public.physicianlocation_locationid_seq;
       public          postgres    false    243         x           0    0     physicianlocation_locationid_seq    SEQUENCE OWNED BY     e   ALTER SEQUENCE public.physicianlocation_locationid_seq OWNED BY public.physicianlocation.locationid;
          public          postgres    false    244         �            1259    32868    physiciannotification    TABLE     �   CREATE TABLE public.physiciannotification (
    id integer NOT NULL,
    physicianid integer NOT NULL,
    isnotificationstopped bit(1) NOT NULL
);
 )   DROP TABLE public.physiciannotification;
       public         heap    postgres    false         �            1259    32871    physiciannotification_id_seq    SEQUENCE     �   CREATE SEQUENCE public.physiciannotification_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 3   DROP SEQUENCE public.physiciannotification_id_seq;
       public          postgres    false    245         y           0    0    physiciannotification_id_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE public.physiciannotification_id_seq OWNED BY public.physiciannotification.id;
          public          postgres    false    246         �            1259    32872    physicianregion    TABLE     �   CREATE TABLE public.physicianregion (
    physicianregionid integer NOT NULL,
    physicianid integer NOT NULL,
    regionid integer NOT NULL
);
 #   DROP TABLE public.physicianregion;
       public         heap    postgres    false         �            1259    32875 %   physicianregion_physicianregionid_seq    SEQUENCE     �   CREATE SEQUENCE public.physicianregion_physicianregionid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 <   DROP SEQUENCE public.physicianregion_physicianregionid_seq;
       public          postgres    false    247         z           0    0 %   physicianregion_physicianregionid_seq    SEQUENCE OWNED BY     o   ALTER SEQUENCE public.physicianregion_physicianregionid_seq OWNED BY public.physicianregion.physicianregionid;
          public          postgres    false    248         �            1259    32876    region    TABLE     �   CREATE TABLE public.region (
    regionid integer NOT NULL,
    name character varying(50) NOT NULL,
    abbreviation character varying(50)
);
    DROP TABLE public.region;
       public         heap    postgres    false         �            1259    32879    region_regionid_seq    SEQUENCE     �   CREATE SEQUENCE public.region_regionid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.region_regionid_seq;
       public          postgres    false    249         {           0    0    region_regionid_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.region_regionid_seq OWNED BY public.region.regionid;
          public          postgres    false    250         �            1259    32880    request    TABLE       CREATE TABLE public.request (
    requestid integer NOT NULL,
    requesttypeid integer NOT NULL,
    userid integer,
    firstname character varying(100),
    lastname character varying(100),
    phonenumber character varying(23),
    email character varying(50),
    status smallint NOT NULL,
    physicianid integer,
    confirmationnumber character varying(20),
    createddate timestamp without time zone NOT NULL,
    isdeleted bit(1),
    modifieddate timestamp without time zone,
    declinedby character varying(250),
    isurgentemailsent bit(1) NOT NULL,
    lastwellnessdate timestamp without time zone,
    ismobile bit(1),
    calltype smallint,
    completedbyphysician bit(1),
    lastreservationdate timestamp without time zone,
    accepteddate timestamp without time zone,
    relationname character varying(100),
    casenumber character varying(50),
    ip character varying(20),
    casetag character varying(50),
    casetagphysician character varying(50),
    patientaccountid character varying(128),
    createduserid integer
);
    DROP TABLE public.request;
       public         heap    postgres    false         �            1259    32885    request_requestid_seq    SEQUENCE     �   CREATE SEQUENCE public.request_requestid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE public.request_requestid_seq;
       public          postgres    false    251         |           0    0    request_requestid_seq    SEQUENCE OWNED BY     O   ALTER SEQUENCE public.request_requestid_seq OWNED BY public.request.requestid;
          public          postgres    false    252         �            1259    32886    requestbusiness    TABLE     �   CREATE TABLE public.requestbusiness (
    requestbusinessid integer NOT NULL,
    requestid integer,
    businessid integer,
    ip character varying(20)
);
 #   DROP TABLE public.requestbusiness;
       public         heap    postgres    false         �            1259    32889 %   requestbusiness_requestbusinessid_seq    SEQUENCE     �   CREATE SEQUENCE public.requestbusiness_requestbusinessid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 <   DROP SEQUENCE public.requestbusiness_requestbusinessid_seq;
       public          postgres    false    253         }           0    0 %   requestbusiness_requestbusinessid_seq    SEQUENCE OWNED BY     o   ALTER SEQUENCE public.requestbusiness_requestbusinessid_seq OWNED BY public.requestbusiness.requestbusinessid;
          public          postgres    false    254         �            1259    32890    requestclient    TABLE     �  CREATE TABLE public.requestclient (
    requestclientid integer NOT NULL,
    requestid integer NOT NULL,
    firstname character varying(100) NOT NULL,
    lastname character varying(100),
    phonenumber character varying(23),
    location character varying(100),
    address character varying(500),
    regionid integer,
    notimobile character varying(20),
    notiemail character varying(50),
    notes character varying(500),
    email character varying(50),
    strmonth character varying(20),
    intyear integer,
    intdate integer,
    ismobile bit(1),
    street character varying(100),
    city character varying(100),
    state character varying(100),
    zipcode character varying(10),
    communicationtype smallint,
    remindreservationcount smallint,
    remindhousecallcount smallint,
    issetfollowupsent smallint,
    ip character varying(20),
    isreservationremindersent smallint,
    latitude numeric(9,0),
    longitude numeric(9,0)
);
 !   DROP TABLE public.requestclient;
       public         heap    postgres    false                     1259    32895 !   requestclient_requestclientid_seq    SEQUENCE     �   CREATE SEQUENCE public.requestclient_requestclientid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public.requestclient_requestclientid_seq;
       public          postgres    false    255         ~           0    0 !   requestclient_requestclientid_seq    SEQUENCE OWNED BY     g   ALTER SEQUENCE public.requestclient_requestclientid_seq OWNED BY public.requestclient.requestclientid;
          public          postgres    false    256                    1259    32896    requestclosed    TABLE        CREATE TABLE public.requestclosed (
    requestclosedid integer NOT NULL,
    requestid integer NOT NULL,
    requeststatuslogid integer NOT NULL,
    phynotes character varying(500),
    clientnotes character varying(500),
    ip character varying(20)
);
 !   DROP TABLE public.requestclosed;
       public         heap    postgres    false                    1259    32901 !   requestclosed_requestclosedid_seq    SEQUENCE     �   CREATE SEQUENCE public.requestclosed_requestclosedid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public.requestclosed_requestclosedid_seq;
       public          postgres    false    257                    0    0 !   requestclosed_requestclosedid_seq    SEQUENCE OWNED BY     g   ALTER SEQUENCE public.requestclosed_requestclosedid_seq OWNED BY public.requestclosed.requestclosedid;
          public          postgres    false    258                    1259    32902    requestconcierge    TABLE     �   CREATE TABLE public.requestconcierge (
    id integer NOT NULL,
    requestid integer NOT NULL,
    conciergeid integer NOT NULL,
    ip character varying(20)
);
 $   DROP TABLE public.requestconcierge;
       public         heap    postgres    false                    1259    32905    requestconcierge_id_seq    SEQUENCE     �   CREATE SEQUENCE public.requestconcierge_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.requestconcierge_id_seq;
       public          postgres    false    259         �           0    0    requestconcierge_id_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.requestconcierge_id_seq OWNED BY public.requestconcierge.id;
          public          postgres    false    260                    1259    32906    requestnotes    TABLE       CREATE TABLE public.requestnotes (
    requestnotesid integer NOT NULL,
    requestid integer NOT NULL,
    strmonth character varying(20),
    intyear integer,
    intdate integer,
    physiciannotes character varying(500),
    adminnotes character varying(500),
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    ip character varying(20),
    administrativenotes character varying(500)
);
     DROP TABLE public.requestnotes;
       public         heap    postgres    false                    1259    32911    requestnotes_requestnotesid_seq    SEQUENCE     �   CREATE SEQUENCE public.requestnotes_requestnotesid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 6   DROP SEQUENCE public.requestnotes_requestnotesid_seq;
       public          postgres    false    261         �           0    0    requestnotes_requestnotesid_seq    SEQUENCE OWNED BY     c   ALTER SEQUENCE public.requestnotes_requestnotesid_seq OWNED BY public.requestnotes.requestnotesid;
          public          postgres    false    262                    1259    32912    requeststatuslog    TABLE     m  CREATE TABLE public.requeststatuslog (
    requeststatuslogid integer NOT NULL,
    requestid integer NOT NULL,
    status smallint NOT NULL,
    physicianid integer,
    adminid integer,
    transtophysicianid integer,
    notes character varying(500),
    createddate timestamp without time zone NOT NULL,
    ip character varying(20),
    transtoadmin bit(1)
);
 $   DROP TABLE public.requeststatuslog;
       public         heap    postgres    false                    1259    32917 '   requeststatuslog_requeststatuslogid_seq    SEQUENCE     �   CREATE SEQUENCE public.requeststatuslog_requeststatuslogid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 >   DROP SEQUENCE public.requeststatuslog_requeststatuslogid_seq;
       public          postgres    false    263         �           0    0 '   requeststatuslog_requeststatuslogid_seq    SEQUENCE OWNED BY     s   ALTER SEQUENCE public.requeststatuslog_requeststatuslogid_seq OWNED BY public.requeststatuslog.requeststatuslogid;
          public          postgres    false    264         	           1259    32918    requesttype    TABLE     q   CREATE TABLE public.requesttype (
    requesttypeid integer NOT NULL,
    name character varying(50) NOT NULL
);
    DROP TABLE public.requesttype;
       public         heap    postgres    false         
           1259    32921    requesttype_requesttypeid_seq    SEQUENCE     �   CREATE SEQUENCE public.requesttype_requesttypeid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 4   DROP SEQUENCE public.requesttype_requesttypeid_seq;
       public          postgres    false    265         �           0    0    requesttype_requesttypeid_seq    SEQUENCE OWNED BY     _   ALTER SEQUENCE public.requesttype_requesttypeid_seq OWNED BY public.requesttype.requesttypeid;
          public          postgres    false    266                    1259    32922    requestwisefile    TABLE     �  CREATE TABLE public.requestwisefile (
    requestwisefileid integer NOT NULL,
    requestid integer NOT NULL,
    filename character varying(500) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    physicianid integer,
    adminid integer,
    doctype smallint,
    isfrontside bit(1),
    iscompensation bit(1),
    ip character varying(20),
    isfinalize bit(1),
    isdeleted bit(1),
    ispatientrecords bit(1)
);
 #   DROP TABLE public.requestwisefile;
       public         heap    postgres    false                    1259    32927 %   requestwisefile_requestwisefileid_seq    SEQUENCE     �   CREATE SEQUENCE public.requestwisefile_requestwisefileid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 <   DROP SEQUENCE public.requestwisefile_requestwisefileid_seq;
       public          postgres    false    267         �           0    0 %   requestwisefile_requestwisefileid_seq    SEQUENCE OWNED BY     o   ALTER SEQUENCE public.requestwisefile_requestwisefileid_seq OWNED BY public.requestwisefile.requestwisefileid;
          public          postgres    false    268                    1259    32928    role    TABLE     }  CREATE TABLE public.role (
    roleid integer NOT NULL,
    name character varying(50) NOT NULL,
    accounttype smallint NOT NULL,
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    isdeleted bit(1) NOT NULL,
    ip character varying(20)
);
    DROP TABLE public.role;
       public         heap    postgres    false                    1259    32931    role_roleid_seq    SEQUENCE     �   CREATE SEQUENCE public.role_roleid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.role_roleid_seq;
       public          postgres    false    269         �           0    0    role_roleid_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.role_roleid_seq OWNED BY public.role.roleid;
          public          postgres    false    270                    1259    32932    rolemenu    TABLE     |   CREATE TABLE public.rolemenu (
    rolemenuid integer NOT NULL,
    roleid integer NOT NULL,
    menuid integer NOT NULL
);
    DROP TABLE public.rolemenu;
       public         heap    postgres    false                    1259    32935    rolemenu_rolemenuid_seq    SEQUENCE     �   CREATE SEQUENCE public.rolemenu_rolemenuid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.rolemenu_rolemenuid_seq;
       public          postgres    false    271         �           0    0    rolemenu_rolemenuid_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.rolemenu_rolemenuid_seq OWNED BY public.rolemenu.rolemenuid;
          public          postgres    false    272                    1259    32936    shift    TABLE     O  CREATE TABLE public.shift (
    shiftid integer NOT NULL,
    physicianid integer NOT NULL,
    startdate date NOT NULL,
    isrepeat bit(1) NOT NULL,
    weekdays character(7),
    repeatupto integer,
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    ip character varying(20)
);
    DROP TABLE public.shift;
       public         heap    postgres    false                    1259    32939    shift_shiftid_seq    SEQUENCE     �   CREATE SEQUENCE public.shift_shiftid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.shift_shiftid_seq;
       public          postgres    false    273         �           0    0    shift_shiftid_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.shift_shiftid_seq OWNED BY public.shift.shiftid;
          public          postgres    false    274                    1259    32940    shiftdetail    TABLE       CREATE TABLE public.shiftdetail (
    shiftdetailid integer NOT NULL,
    shiftid integer NOT NULL,
    shiftdate timestamp without time zone NOT NULL,
    regionid integer,
    starttime time without time zone NOT NULL,
    endtime time without time zone NOT NULL,
    status smallint NOT NULL,
    isdeleted bit(1) NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    lastrunningdate timestamp without time zone,
    eventid character varying(100),
    issync bit(1)
);
    DROP TABLE public.shiftdetail;
       public         heap    postgres    false                    1259    32943    shiftdetail_shiftdetailid_seq    SEQUENCE     �   CREATE SEQUENCE public.shiftdetail_shiftdetailid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 4   DROP SEQUENCE public.shiftdetail_shiftdetailid_seq;
       public          postgres    false    275         �           0    0    shiftdetail_shiftdetailid_seq    SEQUENCE OWNED BY     _   ALTER SEQUENCE public.shiftdetail_shiftdetailid_seq OWNED BY public.shiftdetail.shiftdetailid;
          public          postgres    false    276                    1259    32944    shiftdetailregion    TABLE     �   CREATE TABLE public.shiftdetailregion (
    shiftdetailregionid integer NOT NULL,
    shiftdetailid integer NOT NULL,
    regionid integer NOT NULL,
    isdeleted bit(1)
);
 %   DROP TABLE public.shiftdetailregion;
       public         heap    postgres    false                    1259    32947 )   shiftdetailregion_shiftdetailregionid_seq    SEQUENCE     �   CREATE SEQUENCE public.shiftdetailregion_shiftdetailregionid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 @   DROP SEQUENCE public.shiftdetailregion_shiftdetailregionid_seq;
       public          postgres    false    277         �           0    0 )   shiftdetailregion_shiftdetailregionid_seq    SEQUENCE OWNED BY     w   ALTER SEQUENCE public.shiftdetailregion_shiftdetailregionid_seq OWNED BY public.shiftdetailregion.shiftdetailregionid;
          public          postgres    false    278                    1259    32948    smslog    TABLE     �  CREATE TABLE public.smslog (
    smslogid integer NOT NULL,
    smstemplate character varying(100),
    mobilenumber character varying(50) NOT NULL,
    confirmationnumber character varying(200),
    roleid integer,
    adminid integer,
    requestid integer,
    physicianid integer,
    createdate timestamp without time zone NOT NULL,
    sentdate timestamp without time zone,
    issmssent bit(1),
    senttries integer NOT NULL,
    action integer
);
    DROP TABLE public.smslog;
       public         heap    postgres    false                    1259    32951    smslog_smslogid_seq    SEQUENCE     �   CREATE SEQUENCE public.smslog_smslogid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.smslog_smslogid_seq;
       public          postgres    false    279         �           0    0    smslog_smslogid_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.smslog_smslogid_seq OWNED BY public.smslog.smslogid;
          public          postgres    false    280         �           2604    32952    User userid    DEFAULT     n   ALTER TABLE ONLY public."User" ALTER COLUMN userid SET DEFAULT nextval('public."User_userid_seq"'::regclass);
 <   ALTER TABLE public."User" ALTER COLUMN userid DROP DEFAULT;
       public          postgres    false    216    215         �           2604    32953    admin adminid    DEFAULT     n   ALTER TABLE ONLY public.admin ALTER COLUMN adminid SET DEFAULT nextval('public.admin_adminid_seq'::regclass);
 <   ALTER TABLE public.admin ALTER COLUMN adminid DROP DEFAULT;
       public          postgres    false    218    217         �           2604    32954    adminregion adminregionid    DEFAULT     �   ALTER TABLE ONLY public.adminregion ALTER COLUMN adminregionid SET DEFAULT nextval('public.adminregion_adminregionid_seq'::regclass);
 H   ALTER TABLE public.adminregion ALTER COLUMN adminregionid DROP DEFAULT;
       public          postgres    false    220    219         �           2604    32955    blockrequests blockrequestid    DEFAULT     �   ALTER TABLE ONLY public.blockrequests ALTER COLUMN blockrequestid SET DEFAULT nextval('public.blockrequests_blockrequestid_seq'::regclass);
 K   ALTER TABLE public.blockrequests ALTER COLUMN blockrequestid DROP DEFAULT;
       public          postgres    false    225    224         �           2604    32956    business businessid    DEFAULT     z   ALTER TABLE ONLY public.business ALTER COLUMN businessid SET DEFAULT nextval('public.business_businessid_seq'::regclass);
 B   ALTER TABLE public.business ALTER COLUMN businessid DROP DEFAULT;
       public          postgres    false    227    226                     2604    32957    casetag casetagid    DEFAULT     v   ALTER TABLE ONLY public.casetag ALTER COLUMN casetagid SET DEFAULT nextval('public.casetag_casetagid_seq'::regclass);
 @   ALTER TABLE public.casetag ALTER COLUMN casetagid DROP DEFAULT;
       public          postgres    false    229    228                    2604    32958    concierge conciergeid    DEFAULT     ~   ALTER TABLE ONLY public.concierge ALTER COLUMN conciergeid SET DEFAULT nextval('public.concierge_conciergeid_seq'::regclass);
 D   ALTER TABLE public.concierge ALTER COLUMN conciergeid DROP DEFAULT;
       public          postgres    false    231    230                    2604    41031    encounterform encounterformid    DEFAULT     �   ALTER TABLE ONLY public.encounterform ALTER COLUMN encounterformid SET DEFAULT nextval('public.encounterform_encounterformid_seq'::regclass);
 L   ALTER TABLE public.encounterform ALTER COLUMN encounterformid DROP DEFAULT;
       public          postgres    false    282    281    282                    2604    32959    healthprofessionals vendorid    DEFAULT     �   ALTER TABLE ONLY public.healthprofessionals ALTER COLUMN vendorid SET DEFAULT nextval('public.healthprofessionals_vendorid_seq'::regclass);
 K   ALTER TABLE public.healthprofessionals ALTER COLUMN vendorid DROP DEFAULT;
       public          postgres    false    234    233                    2604    32960 +   healthprofessionaltype healthprofessionalid    DEFAULT     �   ALTER TABLE ONLY public.healthprofessionaltype ALTER COLUMN healthprofessionalid SET DEFAULT nextval('public.healthprofessionaltype_healthprofessionalid_seq'::regclass);
 Z   ALTER TABLE public.healthprofessionaltype ALTER COLUMN healthprofessionalid DROP DEFAULT;
       public          postgres    false    236    235                    2604    32961    menu menuid    DEFAULT     j   ALTER TABLE ONLY public.menu ALTER COLUMN menuid SET DEFAULT nextval('public.menu_menuid_seq'::regclass);
 :   ALTER TABLE public.menu ALTER COLUMN menuid DROP DEFAULT;
       public          postgres    false    238    237                    2604    32962    orderdetails id    DEFAULT     r   ALTER TABLE ONLY public.orderdetails ALTER COLUMN id SET DEFAULT nextval('public.orderdetails_id_seq'::regclass);
 >   ALTER TABLE public.orderdetails ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    240    239                    2604    32963    physician physicianid    DEFAULT     ~   ALTER TABLE ONLY public.physician ALTER COLUMN physicianid SET DEFAULT nextval('public.physician_physicianid_seq'::regclass);
 D   ALTER TABLE public.physician ALTER COLUMN physicianid DROP DEFAULT;
       public          postgres    false    242    241                    2604    32964    physicianlocation locationid    DEFAULT     �   ALTER TABLE ONLY public.physicianlocation ALTER COLUMN locationid SET DEFAULT nextval('public.physicianlocation_locationid_seq'::regclass);
 K   ALTER TABLE public.physicianlocation ALTER COLUMN locationid DROP DEFAULT;
       public          postgres    false    244    243         	           2604    32965    physiciannotification id    DEFAULT     �   ALTER TABLE ONLY public.physiciannotification ALTER COLUMN id SET DEFAULT nextval('public.physiciannotification_id_seq'::regclass);
 G   ALTER TABLE public.physiciannotification ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    246    245         
           2604    32966 !   physicianregion physicianregionid    DEFAULT     �   ALTER TABLE ONLY public.physicianregion ALTER COLUMN physicianregionid SET DEFAULT nextval('public.physicianregion_physicianregionid_seq'::regclass);
 P   ALTER TABLE public.physicianregion ALTER COLUMN physicianregionid DROP DEFAULT;
       public          postgres    false    248    247                    2604    32967    region regionid    DEFAULT     r   ALTER TABLE ONLY public.region ALTER COLUMN regionid SET DEFAULT nextval('public.region_regionid_seq'::regclass);
 >   ALTER TABLE public.region ALTER COLUMN regionid DROP DEFAULT;
       public          postgres    false    250    249                    2604    32968    request requestid    DEFAULT     v   ALTER TABLE ONLY public.request ALTER COLUMN requestid SET DEFAULT nextval('public.request_requestid_seq'::regclass);
 @   ALTER TABLE public.request ALTER COLUMN requestid DROP DEFAULT;
       public          postgres    false    252    251                    2604    32969 !   requestbusiness requestbusinessid    DEFAULT     �   ALTER TABLE ONLY public.requestbusiness ALTER COLUMN requestbusinessid SET DEFAULT nextval('public.requestbusiness_requestbusinessid_seq'::regclass);
 P   ALTER TABLE public.requestbusiness ALTER COLUMN requestbusinessid DROP DEFAULT;
       public          postgres    false    254    253                    2604    32970    requestclient requestclientid    DEFAULT     �   ALTER TABLE ONLY public.requestclient ALTER COLUMN requestclientid SET DEFAULT nextval('public.requestclient_requestclientid_seq'::regclass);
 L   ALTER TABLE public.requestclient ALTER COLUMN requestclientid DROP DEFAULT;
       public          postgres    false    256    255                    2604    32971    requestclosed requestclosedid    DEFAULT     �   ALTER TABLE ONLY public.requestclosed ALTER COLUMN requestclosedid SET DEFAULT nextval('public.requestclosed_requestclosedid_seq'::regclass);
 L   ALTER TABLE public.requestclosed ALTER COLUMN requestclosedid DROP DEFAULT;
       public          postgres    false    258    257                    2604    32972    requestconcierge id    DEFAULT     z   ALTER TABLE ONLY public.requestconcierge ALTER COLUMN id SET DEFAULT nextval('public.requestconcierge_id_seq'::regclass);
 B   ALTER TABLE public.requestconcierge ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    260    259                    2604    32973    requestnotes requestnotesid    DEFAULT     �   ALTER TABLE ONLY public.requestnotes ALTER COLUMN requestnotesid SET DEFAULT nextval('public.requestnotes_requestnotesid_seq'::regclass);
 J   ALTER TABLE public.requestnotes ALTER COLUMN requestnotesid DROP DEFAULT;
       public          postgres    false    262    261                    2604    32974 #   requeststatuslog requeststatuslogid    DEFAULT     �   ALTER TABLE ONLY public.requeststatuslog ALTER COLUMN requeststatuslogid SET DEFAULT nextval('public.requeststatuslog_requeststatuslogid_seq'::regclass);
 R   ALTER TABLE public.requeststatuslog ALTER COLUMN requeststatuslogid DROP DEFAULT;
       public          postgres    false    264    263                    2604    32975    requesttype requesttypeid    DEFAULT     �   ALTER TABLE ONLY public.requesttype ALTER COLUMN requesttypeid SET DEFAULT nextval('public.requesttype_requesttypeid_seq'::regclass);
 H   ALTER TABLE public.requesttype ALTER COLUMN requesttypeid DROP DEFAULT;
       public          postgres    false    266    265                    2604    32976 !   requestwisefile requestwisefileid    DEFAULT     �   ALTER TABLE ONLY public.requestwisefile ALTER COLUMN requestwisefileid SET DEFAULT nextval('public.requestwisefile_requestwisefileid_seq'::regclass);
 P   ALTER TABLE public.requestwisefile ALTER COLUMN requestwisefileid DROP DEFAULT;
       public          postgres    false    268    267                    2604    32977    role roleid    DEFAULT     j   ALTER TABLE ONLY public.role ALTER COLUMN roleid SET DEFAULT nextval('public.role_roleid_seq'::regclass);
 :   ALTER TABLE public.role ALTER COLUMN roleid DROP DEFAULT;
       public          postgres    false    270    269                    2604    32978    rolemenu rolemenuid    DEFAULT     z   ALTER TABLE ONLY public.rolemenu ALTER COLUMN rolemenuid SET DEFAULT nextval('public.rolemenu_rolemenuid_seq'::regclass);
 B   ALTER TABLE public.rolemenu ALTER COLUMN rolemenuid DROP DEFAULT;
       public          postgres    false    272    271                    2604    32979    shift shiftid    DEFAULT     n   ALTER TABLE ONLY public.shift ALTER COLUMN shiftid SET DEFAULT nextval('public.shift_shiftid_seq'::regclass);
 <   ALTER TABLE public.shift ALTER COLUMN shiftid DROP DEFAULT;
       public          postgres    false    274    273                    2604    32980    shiftdetail shiftdetailid    DEFAULT     �   ALTER TABLE ONLY public.shiftdetail ALTER COLUMN shiftdetailid SET DEFAULT nextval('public.shiftdetail_shiftdetailid_seq'::regclass);
 H   ALTER TABLE public.shiftdetail ALTER COLUMN shiftdetailid DROP DEFAULT;
       public          postgres    false    276    275                    2604    32981 %   shiftdetailregion shiftdetailregionid    DEFAULT     �   ALTER TABLE ONLY public.shiftdetailregion ALTER COLUMN shiftdetailregionid SET DEFAULT nextval('public.shiftdetailregion_shiftdetailregionid_seq'::regclass);
 T   ALTER TABLE public.shiftdetailregion ALTER COLUMN shiftdetailregionid DROP DEFAULT;
       public          postgres    false    278    277                    2604    32982    smslog smslogid    DEFAULT     r   ALTER TABLE ONLY public.smslog ALTER COLUMN smslogid SET DEFAULT nextval('public.smslog_smslogid_seq'::regclass);
 >   ALTER TABLE public.smslog ALTER COLUMN smslogid DROP DEFAULT;
       public          postgres    false    280    279         !          0    32783    User 
   TABLE DATA             COPY public."User" (userid, aspnetuserid, firstname, lastname, email, mobile, ismobile, street, city, state, regionid, zipcode, strmonth, intyear, intdate, createdby, createddate, modifiedby, modifieddate, status, isdeleted, ip, isrequestwithemail) FROM stdin;
    public          postgres    false    215       5153.dat #          0    32789    admin 
   TABLE DATA           �   COPY public.admin (adminid, aspnetuserid, firstname, lastname, email, mobile, address1, address2, city, regionid, zip, altphone, createdby, createddate, modifiedby, modifieddate, status, isdeleted, roleid) FROM stdin;
    public          postgres    false    217       5155.dat %          0    32795    adminregion 
   TABLE DATA           G   COPY public.adminregion (adminregionid, adminid, regionid) FROM stdin;
    public          postgres    false    219       5157.dat '          0    32799    aspnetroles 
   TABLE DATA           /   COPY public.aspnetroles (id, name) FROM stdin;
    public          postgres    false    221       5159.dat (          0    32802    aspnetuserroles 
   TABLE DATA           9   COPY public.aspnetuserroles (userid, roleid) FROM stdin;
    public          postgres    false    222       5160.dat )          0    32805    aspnetusers 
   TABLE DATA           v   COPY public.aspnetusers (id, username, passwordhash, email, phonenumber, "CreatedDate", ip, modifieddate) FROM stdin;
    public          postgres    false    223       5161.dat *          0    32810    blockrequests 
   TABLE DATA           �   COPY public.blockrequests (blockrequestid, phonenumber, email, isactive, reason, requestid, ip, createddate, modifieddate) FROM stdin;
    public          postgres    false    224       5162.dat ,          0    32816    business 
   TABLE DATA           �   COPY public.business (businessid, name, address1, address2, city, regionid, zipcode, phonenumber, faxnumber, isregistered, createdby, createddate, modifiedby, modifieddate, status, isdeleted, ip) FROM stdin;
    public          postgres    false    226       5164.dat .          0    32822    casetag 
   TABLE DATA           2   COPY public.casetag (casetagid, name) FROM stdin;
    public          postgres    false    228       5166.dat 0          0    32826 	   concierge 
   TABLE DATA           �   COPY public.concierge (conciergeid, conciergename, address, street, city, state, zipcode, createddate, regionid, ip) FROM stdin;
    public          postgres    false    230       5168.dat 2          0    32830    emaillog 
   TABLE DATA           �   COPY public.emaillog (emaillogid, emailtemplate, subjectname, emailid, confirmationnumber, filepath, roleid, requestid, adminid, physicianid, createdate, sentdate, isemailsent, senttries, action) FROM stdin;
    public          postgres    false    232       5170.dat d          0    41028    encounterform 
   TABLE DATA           �  COPY public.encounterform (encounterformid, requestid, historyofpresentillnessorinjury, medicalhistory, medications, allergies, temp, hr, rr, bloodpressuresystolic, bloodpressurediastolic, o2, pain, heent, cv, chest, abd, extremities, skin, neuro, other, diagnosis, treatment_plan, medicaldispensed, procedures, followup, adminid, physicianid, isfinalize, createddate, modifieddate) FROM stdin;
    public          postgres    false    282       5220.dat 3          0    32835    healthprofessionals 
   TABLE DATA           �   COPY public.healthprofessionals (vendorid, vendorname, profession, faxnumber, address, city, state, zip, regionid, createddate, modifieddate, phonenumber, isdeleted, ip, email, businesscontact) FROM stdin;
    public          postgres    false    233       5171.dat 5          0    32841    healthprofessionaltype 
   TABLE DATA           x   COPY public.healthprofessionaltype (healthprofessionalid, professionname, createddate, isactive, isdeleted) FROM stdin;
    public          postgres    false    235       5173.dat 7          0    32846    menu 
   TABLE DATA           D   COPY public.menu (menuid, name, accounttype, sortorder) FROM stdin;
    public          postgres    false    237       5175.dat 9          0    32850    orderdetails 
   TABLE DATA           �   COPY public.orderdetails (id, vendorid, requestid, faxnumber, email, businesscontact, prescription, noofrefill, createddate, createdby) FROM stdin;
    public          postgres    false    239       5177.dat ;          0    32856 	   physician 
   TABLE DATA           �  COPY public.physician (physicianid, aspnetuserid, firstname, lastname, email, mobile, medicallicense, photo, adminnotes, isagreementdoc, isbackgrounddoc, istrainingdoc, isnondisclosuredoc, address1, address2, city, regionid, zip, altphone, createdby, createddate, modifiedby, modifieddate, status, businessname, businesswebsite, isdeleted, roleid, npinumber, islicensedoc, signature, iscredentialdoc, istokengenerate, syncemailaddress) FROM stdin;
    public          postgres    false    241       5179.dat =          0    32862    physicianlocation 
   TABLE DATA           ~   COPY public.physicianlocation (locationid, physicianid, latitude, longitude, createddate, physicianname, address) FROM stdin;
    public          postgres    false    243       5181.dat ?          0    32868    physiciannotification 
   TABLE DATA           W   COPY public.physiciannotification (id, physicianid, isnotificationstopped) FROM stdin;
    public          postgres    false    245       5183.dat A          0    32872    physicianregion 
   TABLE DATA           S   COPY public.physicianregion (physicianregionid, physicianid, regionid) FROM stdin;
    public          postgres    false    247       5185.dat C          0    32876    region 
   TABLE DATA           >   COPY public.region (regionid, name, abbreviation) FROM stdin;
    public          postgres    false    249       5187.dat E          0    32880    request 
   TABLE DATA           �  COPY public.request (requestid, requesttypeid, userid, firstname, lastname, phonenumber, email, status, physicianid, confirmationnumber, createddate, isdeleted, modifieddate, declinedby, isurgentemailsent, lastwellnessdate, ismobile, calltype, completedbyphysician, lastreservationdate, accepteddate, relationname, casenumber, ip, casetag, casetagphysician, patientaccountid, createduserid) FROM stdin;
    public          postgres    false    251       5189.dat G          0    32886    requestbusiness 
   TABLE DATA           W   COPY public.requestbusiness (requestbusinessid, requestid, businessid, ip) FROM stdin;
    public          postgres    false    253       5191.dat I          0    32890    requestclient 
   TABLE DATA           s  COPY public.requestclient (requestclientid, requestid, firstname, lastname, phonenumber, location, address, regionid, notimobile, notiemail, notes, email, strmonth, intyear, intdate, ismobile, street, city, state, zipcode, communicationtype, remindreservationcount, remindhousecallcount, issetfollowupsent, ip, isreservationremindersent, latitude, longitude) FROM stdin;
    public          postgres    false    255       5193.dat K          0    32896    requestclosed 
   TABLE DATA           r   COPY public.requestclosed (requestclosedid, requestid, requeststatuslogid, phynotes, clientnotes, ip) FROM stdin;
    public          postgres    false    257       5195.dat M          0    32902    requestconcierge 
   TABLE DATA           J   COPY public.requestconcierge (id, requestid, conciergeid, ip) FROM stdin;
    public          postgres    false    259       5197.dat O          0    32906    requestnotes 
   TABLE DATA           �   COPY public.requestnotes (requestnotesid, requestid, strmonth, intyear, intdate, physiciannotes, adminnotes, createdby, createddate, modifiedby, modifieddate, ip, administrativenotes) FROM stdin;
    public          postgres    false    261       5199.dat Q          0    32912    requeststatuslog 
   TABLE DATA           �   COPY public.requeststatuslog (requeststatuslogid, requestid, status, physicianid, adminid, transtophysicianid, notes, createddate, ip, transtoadmin) FROM stdin;
    public          postgres    false    263       5201.dat S          0    32918    requesttype 
   TABLE DATA           :   COPY public.requesttype (requesttypeid, name) FROM stdin;
    public          postgres    false    265       5203.dat U          0    32922    requestwisefile 
   TABLE DATA           �   COPY public.requestwisefile (requestwisefileid, requestid, filename, createddate, physicianid, adminid, doctype, isfrontside, iscompensation, ip, isfinalize, isdeleted, ispatientrecords) FROM stdin;
    public          postgres    false    267       5205.dat W          0    32928    role 
   TABLE DATA           z   COPY public.role (roleid, name, accounttype, createdby, createddate, modifiedby, modifieddate, isdeleted, ip) FROM stdin;
    public          postgres    false    269       5207.dat Y          0    32932    rolemenu 
   TABLE DATA           >   COPY public.rolemenu (rolemenuid, roleid, menuid) FROM stdin;
    public          postgres    false    271       5209.dat [          0    32936    shift 
   TABLE DATA           |   COPY public.shift (shiftid, physicianid, startdate, isrepeat, weekdays, repeatupto, createdby, createddate, ip) FROM stdin;
    public          postgres    false    273       5211.dat ]          0    32940    shiftdetail 
   TABLE DATA           �   COPY public.shiftdetail (shiftdetailid, shiftid, shiftdate, regionid, starttime, endtime, status, isdeleted, modifiedby, modifieddate, lastrunningdate, eventid, issync) FROM stdin;
    public          postgres    false    275       5213.dat _          0    32944    shiftdetailregion 
   TABLE DATA           d   COPY public.shiftdetailregion (shiftdetailregionid, shiftdetailid, regionid, isdeleted) FROM stdin;
    public          postgres    false    277       5215.dat a          0    32948    smslog 
   TABLE DATA           �   COPY public.smslog (smslogid, smstemplate, mobilenumber, confirmationnumber, roleid, adminid, requestid, physicianid, createdate, sentdate, issmssent, senttries, action) FROM stdin;
    public          postgres    false    279       5217.dat �           0    0    User_userid_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."User_userid_seq"', 24, true);
          public          postgres    false    216         �           0    0    admin_adminid_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.admin_adminid_seq', 6, true);
          public          postgres    false    218         �           0    0    adminregion_adminregionid_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('public.adminregion_adminregionid_seq', 16, true);
          public          postgres    false    220         �           0    0     blockrequests_blockrequestid_seq    SEQUENCE SET     N   SELECT pg_catalog.setval('public.blockrequests_blockrequestid_seq', 5, true);
          public          postgres    false    225         �           0    0    business_businessid_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.business_businessid_seq', 14, true);
          public          postgres    false    227         �           0    0    casetag_casetagid_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.casetag_casetagid_seq', 7, true);
          public          postgres    false    229         �           0    0    concierge_conciergeid_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public.concierge_conciergeid_seq', 3, true);
          public          postgres    false    231         �           0    0 !   encounterform_encounterformid_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.encounterform_encounterformid_seq', 1, true);
          public          postgres    false    281         �           0    0     healthprofessionals_vendorid_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.healthprofessionals_vendorid_seq', 10, true);
          public          postgres    false    234         �           0    0 /   healthprofessionaltype_healthprofessionalid_seq    SEQUENCE SET     ^   SELECT pg_catalog.setval('public.healthprofessionaltype_healthprofessionalid_seq', 10, true);
          public          postgres    false    236         �           0    0    menu_menuid_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.menu_menuid_seq', 1, false);
          public          postgres    false    238         �           0    0    orderdetails_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.orderdetails_id_seq', 7, true);
          public          postgres    false    240         �           0    0    physician_physicianid_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public.physician_physicianid_seq', 1, false);
          public          postgres    false    242         �           0    0     physicianlocation_locationid_seq    SEQUENCE SET     N   SELECT pg_catalog.setval('public.physicianlocation_locationid_seq', 6, true);
          public          postgres    false    244         �           0    0    physiciannotification_id_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public.physiciannotification_id_seq', 1, false);
          public          postgres    false    246         �           0    0 %   physicianregion_physicianregionid_seq    SEQUENCE SET     S   SELECT pg_catalog.setval('public.physicianregion_physicianregionid_seq', 5, true);
          public          postgres    false    248         �           0    0    region_regionid_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.region_regionid_seq', 5, true);
          public          postgres    false    250         �           0    0    request_requestid_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public.request_requestid_seq', 85, true);
          public          postgres    false    252         �           0    0 %   requestbusiness_requestbusinessid_seq    SEQUENCE SET     S   SELECT pg_catalog.setval('public.requestbusiness_requestbusinessid_seq', 8, true);
          public          postgres    false    254         �           0    0 !   requestclient_requestclientid_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public.requestclient_requestclientid_seq', 80, true);
          public          postgres    false    256         �           0    0 !   requestclosed_requestclosedid_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public.requestclosed_requestclosedid_seq', 1, false);
          public          postgres    false    258         �           0    0    requestconcierge_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.requestconcierge_id_seq', 3, true);
          public          postgres    false    260         �           0    0    requestnotes_requestnotesid_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('public.requestnotes_requestnotesid_seq', 4, true);
          public          postgres    false    262         �           0    0 '   requeststatuslog_requeststatuslogid_seq    SEQUENCE SET     V   SELECT pg_catalog.setval('public.requeststatuslog_requeststatuslogid_seq', 67, true);
          public          postgres    false    264         �           0    0    requesttype_requesttypeid_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('public.requesttype_requesttypeid_seq', 1, false);
          public          postgres    false    266         �           0    0 %   requestwisefile_requestwisefileid_seq    SEQUENCE SET     T   SELECT pg_catalog.setval('public.requestwisefile_requestwisefileid_seq', 58, true);
          public          postgres    false    268         �           0    0    role_roleid_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.role_roleid_seq', 1, false);
          public          postgres    false    270         �           0    0    rolemenu_rolemenuid_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.rolemenu_rolemenuid_seq', 1, false);
          public          postgres    false    272         �           0    0    shift_shiftid_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.shift_shiftid_seq', 1, false);
          public          postgres    false    274         �           0    0    shiftdetail_shiftdetailid_seq    SEQUENCE SET     L   SELECT pg_catalog.setval('public.shiftdetail_shiftdetailid_seq', 1, false);
          public          postgres    false    276         �           0    0 )   shiftdetailregion_shiftdetailregionid_seq    SEQUENCE SET     X   SELECT pg_catalog.setval('public.shiftdetailregion_shiftdetailregionid_seq', 1, false);
          public          postgres    false    278         �           0    0    smslog_smslogid_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.smslog_smslogid_seq', 1, false);
          public          postgres    false    280                    2606    32984    User User_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY (userid);
 <   ALTER TABLE ONLY public."User" DROP CONSTRAINT "User_pkey";
       public            postgres    false    215                     2606    32986    admin admin_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT admin_pkey PRIMARY KEY (adminid);
 :   ALTER TABLE ONLY public.admin DROP CONSTRAINT admin_pkey;
       public            postgres    false    217         "           2606    32988    adminregion adminregion_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public.adminregion
    ADD CONSTRAINT adminregion_pkey PRIMARY KEY (adminregionid);
 F   ALTER TABLE ONLY public.adminregion DROP CONSTRAINT adminregion_pkey;
       public            postgres    false    219         $           2606    32990    aspnetroles aspnetroles_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.aspnetroles
    ADD CONSTRAINT aspnetroles_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.aspnetroles DROP CONSTRAINT aspnetroles_pkey;
       public            postgres    false    221         &           2606    40975 $   aspnetuserroles aspnetuserroles_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.aspnetuserroles
    ADD CONSTRAINT aspnetuserroles_pkey PRIMARY KEY (userid);
 N   ALTER TABLE ONLY public.aspnetuserroles DROP CONSTRAINT aspnetuserroles_pkey;
       public            postgres    false    222         (           2606    32994    aspnetusers aspnetusers_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.aspnetusers
    ADD CONSTRAINT aspnetusers_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.aspnetusers DROP CONSTRAINT aspnetusers_pkey;
       public            postgres    false    223         *           2606    32996     blockrequests blockrequests_pkey 
   CONSTRAINT     j   ALTER TABLE ONLY public.blockrequests
    ADD CONSTRAINT blockrequests_pkey PRIMARY KEY (blockrequestid);
 J   ALTER TABLE ONLY public.blockrequests DROP CONSTRAINT blockrequests_pkey;
       public            postgres    false    224         ,           2606    32998    business business_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.business
    ADD CONSTRAINT business_pkey PRIMARY KEY (businessid);
 @   ALTER TABLE ONLY public.business DROP CONSTRAINT business_pkey;
       public            postgres    false    226         .           2606    33000    casetag casetag_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.casetag
    ADD CONSTRAINT casetag_pkey PRIMARY KEY (casetagid);
 >   ALTER TABLE ONLY public.casetag DROP CONSTRAINT casetag_pkey;
       public            postgres    false    228         0           2606    33002    concierge concierge_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public.concierge
    ADD CONSTRAINT concierge_pkey PRIMARY KEY (conciergeid);
 B   ALTER TABLE ONLY public.concierge DROP CONSTRAINT concierge_pkey;
       public            postgres    false    230         2           2606    33004    emaillog emaillog_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.emaillog
    ADD CONSTRAINT emaillog_pkey PRIMARY KEY (emaillogid);
 @   ALTER TABLE ONLY public.emaillog DROP CONSTRAINT emaillog_pkey;
       public            postgres    false    232         d           2606    41036     encounterform encounterform_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public.encounterform
    ADD CONSTRAINT encounterform_pkey PRIMARY KEY (encounterformid);
 J   ALTER TABLE ONLY public.encounterform DROP CONSTRAINT encounterform_pkey;
       public            postgres    false    282         4           2606    33006 ,   healthprofessionals healthprofessionals_pkey 
   CONSTRAINT     p   ALTER TABLE ONLY public.healthprofessionals
    ADD CONSTRAINT healthprofessionals_pkey PRIMARY KEY (vendorid);
 V   ALTER TABLE ONLY public.healthprofessionals DROP CONSTRAINT healthprofessionals_pkey;
       public            postgres    false    233         6           2606    33008 2   healthprofessionaltype healthprofessionaltype_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.healthprofessionaltype
    ADD CONSTRAINT healthprofessionaltype_pkey PRIMARY KEY (healthprofessionalid);
 \   ALTER TABLE ONLY public.healthprofessionaltype DROP CONSTRAINT healthprofessionaltype_pkey;
       public            postgres    false    235         8           2606    33010    menu menu_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.menu
    ADD CONSTRAINT menu_pkey PRIMARY KEY (menuid);
 8   ALTER TABLE ONLY public.menu DROP CONSTRAINT menu_pkey;
       public            postgres    false    237         :           2606    33012    orderdetails orderdetails_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.orderdetails
    ADD CONSTRAINT orderdetails_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.orderdetails DROP CONSTRAINT orderdetails_pkey;
       public            postgres    false    239         <           2606    33014    physician physician_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_pkey PRIMARY KEY (physicianid);
 B   ALTER TABLE ONLY public.physician DROP CONSTRAINT physician_pkey;
       public            postgres    false    241         >           2606    33016 (   physicianlocation physicianlocation_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public.physicianlocation
    ADD CONSTRAINT physicianlocation_pkey PRIMARY KEY (locationid);
 R   ALTER TABLE ONLY public.physicianlocation DROP CONSTRAINT physicianlocation_pkey;
       public            postgres    false    243         @           2606    33018 0   physiciannotification physiciannotification_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public.physiciannotification
    ADD CONSTRAINT physiciannotification_pkey PRIMARY KEY (id);
 Z   ALTER TABLE ONLY public.physiciannotification DROP CONSTRAINT physiciannotification_pkey;
       public            postgres    false    245         B           2606    33020 $   physicianregion physicianregion_pkey 
   CONSTRAINT     q   ALTER TABLE ONLY public.physicianregion
    ADD CONSTRAINT physicianregion_pkey PRIMARY KEY (physicianregionid);
 N   ALTER TABLE ONLY public.physicianregion DROP CONSTRAINT physicianregion_pkey;
       public            postgres    false    247         D           2606    33022    region region_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.region
    ADD CONSTRAINT region_pkey PRIMARY KEY (regionid);
 <   ALTER TABLE ONLY public.region DROP CONSTRAINT region_pkey;
       public            postgres    false    249         F           2606    33024    request request_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.request
    ADD CONSTRAINT request_pkey PRIMARY KEY (requestid);
 >   ALTER TABLE ONLY public.request DROP CONSTRAINT request_pkey;
       public            postgres    false    251         H           2606    33026 $   requestbusiness requestbusiness_pkey 
   CONSTRAINT     q   ALTER TABLE ONLY public.requestbusiness
    ADD CONSTRAINT requestbusiness_pkey PRIMARY KEY (requestbusinessid);
 N   ALTER TABLE ONLY public.requestbusiness DROP CONSTRAINT requestbusiness_pkey;
       public            postgres    false    253         J           2606    33028     requestclient requestclient_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public.requestclient
    ADD CONSTRAINT requestclient_pkey PRIMARY KEY (requestclientid);
 J   ALTER TABLE ONLY public.requestclient DROP CONSTRAINT requestclient_pkey;
       public            postgres    false    255         L           2606    33030     requestclosed requestclosed_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public.requestclosed
    ADD CONSTRAINT requestclosed_pkey PRIMARY KEY (requestclosedid);
 J   ALTER TABLE ONLY public.requestclosed DROP CONSTRAINT requestclosed_pkey;
       public            postgres    false    257         N           2606    33032 &   requestconcierge requestconcierge_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.requestconcierge
    ADD CONSTRAINT requestconcierge_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.requestconcierge DROP CONSTRAINT requestconcierge_pkey;
       public            postgres    false    259         P           2606    33034    requestnotes requestnotes_pkey 
   CONSTRAINT     h   ALTER TABLE ONLY public.requestnotes
    ADD CONSTRAINT requestnotes_pkey PRIMARY KEY (requestnotesid);
 H   ALTER TABLE ONLY public.requestnotes DROP CONSTRAINT requestnotes_pkey;
       public            postgres    false    261         R           2606    33036 &   requeststatuslog requeststatuslog_pkey 
   CONSTRAINT     t   ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_pkey PRIMARY KEY (requeststatuslogid);
 P   ALTER TABLE ONLY public.requeststatuslog DROP CONSTRAINT requeststatuslog_pkey;
       public            postgres    false    263         T           2606    33038    requesttype requesttype_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public.requesttype
    ADD CONSTRAINT requesttype_pkey PRIMARY KEY (requesttypeid);
 F   ALTER TABLE ONLY public.requesttype DROP CONSTRAINT requesttype_pkey;
       public            postgres    false    265         V           2606    33040 $   requestwisefile requestwisefile_pkey 
   CONSTRAINT     q   ALTER TABLE ONLY public.requestwisefile
    ADD CONSTRAINT requestwisefile_pkey PRIMARY KEY (requestwisefileid);
 N   ALTER TABLE ONLY public.requestwisefile DROP CONSTRAINT requestwisefile_pkey;
       public            postgres    false    267         X           2606    33042    role role_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.role
    ADD CONSTRAINT role_pkey PRIMARY KEY (roleid);
 8   ALTER TABLE ONLY public.role DROP CONSTRAINT role_pkey;
       public            postgres    false    269         Z           2606    33044    rolemenu rolemenu_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.rolemenu
    ADD CONSTRAINT rolemenu_pkey PRIMARY KEY (rolemenuid);
 @   ALTER TABLE ONLY public.rolemenu DROP CONSTRAINT rolemenu_pkey;
       public            postgres    false    271         \           2606    33046    shift shift_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.shift
    ADD CONSTRAINT shift_pkey PRIMARY KEY (shiftid);
 :   ALTER TABLE ONLY public.shift DROP CONSTRAINT shift_pkey;
       public            postgres    false    273         ^           2606    33048    shiftdetail shiftdetail_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public.shiftdetail
    ADD CONSTRAINT shiftdetail_pkey PRIMARY KEY (shiftdetailid);
 F   ALTER TABLE ONLY public.shiftdetail DROP CONSTRAINT shiftdetail_pkey;
       public            postgres    false    275         `           2606    33050 (   shiftdetailregion shiftdetailregion_pkey 
   CONSTRAINT     w   ALTER TABLE ONLY public.shiftdetailregion
    ADD CONSTRAINT shiftdetailregion_pkey PRIMARY KEY (shiftdetailregionid);
 R   ALTER TABLE ONLY public.shiftdetailregion DROP CONSTRAINT shiftdetailregion_pkey;
       public            postgres    false    277         b           2606    33052    smslog smslog_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.smslog
    ADD CONSTRAINT smslog_pkey PRIMARY KEY (smslogid);
 <   ALTER TABLE ONLY public.smslog DROP CONSTRAINT smslog_pkey;
       public            postgres    false    279         e           2606    33053    User User_aspnetuserid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_aspnetuserid_fkey" FOREIGN KEY (aspnetuserid) REFERENCES public.aspnetusers(id);
 I   ALTER TABLE ONLY public."User" DROP CONSTRAINT "User_aspnetuserid_fkey";
       public          postgres    false    215    223    4904         f           2606    33058 $   adminregion adminregion_adminid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.adminregion
    ADD CONSTRAINT adminregion_adminid_fkey FOREIGN KEY (adminid) REFERENCES public.admin(adminid);
 N   ALTER TABLE ONLY public.adminregion DROP CONSTRAINT adminregion_adminid_fkey;
       public          postgres    false    219    217    4896         g           2606    33063 %   adminregion adminregion_regionid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.adminregion
    ADD CONSTRAINT adminregion_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);
 O   ALTER TABLE ONLY public.adminregion DROP CONSTRAINT adminregion_regionid_fkey;
       public          postgres    false    249    219    4932         h           2606    33073 +   aspnetuserroles aspnetuserroles_userid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.aspnetuserroles
    ADD CONSTRAINT aspnetuserroles_userid_fkey FOREIGN KEY (userid) REFERENCES public.aspnetusers(id);
 U   ALTER TABLE ONLY public.aspnetuserroles DROP CONSTRAINT aspnetuserroles_userid_fkey;
       public          postgres    false    222    223    4904         i           2606    33078     business business_createdby_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.business
    ADD CONSTRAINT business_createdby_fkey FOREIGN KEY (createdby) REFERENCES public.aspnetusers(id);
 J   ALTER TABLE ONLY public.business DROP CONSTRAINT business_createdby_fkey;
       public          postgres    false    4904    226    223         j           2606    33083 !   business business_modifiedby_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.business
    ADD CONSTRAINT business_modifiedby_fkey FOREIGN KEY (modifiedby) REFERENCES public.aspnetusers(id);
 K   ALTER TABLE ONLY public.business DROP CONSTRAINT business_modifiedby_fkey;
       public          postgres    false    226    223    4904         k           2606    33088 !   concierge concierge_regionid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.concierge
    ADD CONSTRAINT concierge_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);
 K   ALTER TABLE ONLY public.concierge DROP CONSTRAINT concierge_regionid_fkey;
       public          postgres    false    4932    249    230         �           2606    41042 (   encounterform encounterform_adminid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.encounterform
    ADD CONSTRAINT encounterform_adminid_fkey FOREIGN KEY (adminid) REFERENCES public.admin(adminid);
 R   ALTER TABLE ONLY public.encounterform DROP CONSTRAINT encounterform_adminid_fkey;
       public          postgres    false    282    4896    217         �           2606    41047 ,   encounterform encounterform_physicianid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.encounterform
    ADD CONSTRAINT encounterform_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);
 V   ALTER TABLE ONLY public.encounterform DROP CONSTRAINT encounterform_physicianid_fkey;
       public          postgres    false    4924    241    282         �           2606    41037 *   encounterform encounterform_requestid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.encounterform
    ADD CONSTRAINT encounterform_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);
 T   ALTER TABLE ONLY public.encounterform DROP CONSTRAINT encounterform_requestid_fkey;
       public          postgres    false    251    4934    282         l           2606    33093 7   healthprofessionals healthprofessionals_profession_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.healthprofessionals
    ADD CONSTRAINT healthprofessionals_profession_fkey FOREIGN KEY (profession) REFERENCES public.healthprofessionaltype(healthprofessionalid);
 a   ALTER TABLE ONLY public.healthprofessionals DROP CONSTRAINT healthprofessionals_profession_fkey;
       public          postgres    false    235    4918    233         m           2606    33098 %   physician physician_aspnetuserid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_aspnetuserid_fkey FOREIGN KEY (aspnetuserid) REFERENCES public.aspnetusers(id);
 O   ALTER TABLE ONLY public.physician DROP CONSTRAINT physician_aspnetuserid_fkey;
       public          postgres    false    223    241    4904         n           2606    33103 "   physician physician_createdby_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_createdby_fkey FOREIGN KEY (createdby) REFERENCES public.aspnetusers(id);
 L   ALTER TABLE ONLY public.physician DROP CONSTRAINT physician_createdby_fkey;
       public          postgres    false    223    4904    241         o           2606    33108 #   physician physician_modifiedby_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_modifiedby_fkey FOREIGN KEY (modifiedby) REFERENCES public.aspnetusers(id);
 M   ALTER TABLE ONLY public.physician DROP CONSTRAINT physician_modifiedby_fkey;
       public          postgres    false    241    223    4904         p           2606    33113 !   physician physician_regionid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);
 K   ALTER TABLE ONLY public.physician DROP CONSTRAINT physician_regionid_fkey;
       public          postgres    false    249    241    4932         q           2606    33118 4   physicianlocation physicianlocation_physicianid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.physicianlocation
    ADD CONSTRAINT physicianlocation_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);
 ^   ALTER TABLE ONLY public.physicianlocation DROP CONSTRAINT physicianlocation_physicianid_fkey;
       public          postgres    false    243    241    4924         r           2606    33123 <   physiciannotification physiciannotification_physicianid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.physiciannotification
    ADD CONSTRAINT physiciannotification_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);
 f   ALTER TABLE ONLY public.physiciannotification DROP CONSTRAINT physiciannotification_physicianid_fkey;
       public          postgres    false    245    4924    241         s           2606    33128 0   physicianregion physicianregion_physicianid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.physicianregion
    ADD CONSTRAINT physicianregion_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);
 Z   ALTER TABLE ONLY public.physicianregion DROP CONSTRAINT physicianregion_physicianid_fkey;
       public          postgres    false    247    4924    241         t           2606    33133 -   physicianregion physicianregion_regionid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.physicianregion
    ADD CONSTRAINT physicianregion_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);
 W   ALTER TABLE ONLY public.physicianregion DROP CONSTRAINT physicianregion_regionid_fkey;
       public          postgres    false    247    249    4932         u           2606    33138     request request_physicianid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.request
    ADD CONSTRAINT request_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);
 J   ALTER TABLE ONLY public.request DROP CONSTRAINT request_physicianid_fkey;
       public          postgres    false    241    251    4924         v           2606    33143    request request_userid_fkey    FK CONSTRAINT     ~   ALTER TABLE ONLY public.request
    ADD CONSTRAINT request_userid_fkey FOREIGN KEY (userid) REFERENCES public."User"(userid);
 E   ALTER TABLE ONLY public.request DROP CONSTRAINT request_userid_fkey;
       public          postgres    false    4894    215    251         w           2606    33148 /   requestbusiness requestbusiness_businessid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestbusiness
    ADD CONSTRAINT requestbusiness_businessid_fkey FOREIGN KEY (businessid) REFERENCES public.business(businessid);
 Y   ALTER TABLE ONLY public.requestbusiness DROP CONSTRAINT requestbusiness_businessid_fkey;
       public          postgres    false    4908    226    253         x           2606    33153 .   requestbusiness requestbusiness_requestid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestbusiness
    ADD CONSTRAINT requestbusiness_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);
 X   ALTER TABLE ONLY public.requestbusiness DROP CONSTRAINT requestbusiness_requestid_fkey;
       public          postgres    false    253    251    4934         y           2606    33158 )   requestclient requestclient_regionid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestclient
    ADD CONSTRAINT requestclient_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);
 S   ALTER TABLE ONLY public.requestclient DROP CONSTRAINT requestclient_regionid_fkey;
       public          postgres    false    4932    255    249         z           2606    33163 *   requestclient requestclient_requestid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestclient
    ADD CONSTRAINT requestclient_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);
 T   ALTER TABLE ONLY public.requestclient DROP CONSTRAINT requestclient_requestid_fkey;
       public          postgres    false    4934    255    251         {           2606    33168 *   requestclosed requestclosed_requestid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestclosed
    ADD CONSTRAINT requestclosed_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);
 T   ALTER TABLE ONLY public.requestclosed DROP CONSTRAINT requestclosed_requestid_fkey;
       public          postgres    false    4934    257    251         |           2606    33173 3   requestclosed requestclosed_requeststatuslogid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestclosed
    ADD CONSTRAINT requestclosed_requeststatuslogid_fkey FOREIGN KEY (requeststatuslogid) REFERENCES public.requeststatuslog(requeststatuslogid);
 ]   ALTER TABLE ONLY public.requestclosed DROP CONSTRAINT requestclosed_requeststatuslogid_fkey;
       public          postgres    false    4946    257    263         }           2606    33178 2   requestconcierge requestconcierge_conciergeid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestconcierge
    ADD CONSTRAINT requestconcierge_conciergeid_fkey FOREIGN KEY (conciergeid) REFERENCES public.concierge(conciergeid);
 \   ALTER TABLE ONLY public.requestconcierge DROP CONSTRAINT requestconcierge_conciergeid_fkey;
       public          postgres    false    4912    259    230         ~           2606    33183 0   requestconcierge requestconcierge_requestid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestconcierge
    ADD CONSTRAINT requestconcierge_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);
 Z   ALTER TABLE ONLY public.requestconcierge DROP CONSTRAINT requestconcierge_requestid_fkey;
       public          postgres    false    251    259    4934                    2606    33188 (   requestnotes requestnotes_requestid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestnotes
    ADD CONSTRAINT requestnotes_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);
 R   ALTER TABLE ONLY public.requestnotes DROP CONSTRAINT requestnotes_requestid_fkey;
       public          postgres    false    261    4934    251         �           2606    33193 .   requeststatuslog requeststatuslog_adminid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_adminid_fkey FOREIGN KEY (adminid) REFERENCES public.admin(adminid);
 X   ALTER TABLE ONLY public.requeststatuslog DROP CONSTRAINT requeststatuslog_adminid_fkey;
       public          postgres    false    263    4896    217         �           2606    33198 2   requeststatuslog requeststatuslog_physicianid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);
 \   ALTER TABLE ONLY public.requeststatuslog DROP CONSTRAINT requeststatuslog_physicianid_fkey;
       public          postgres    false    4924    263    241         �           2606    33203 0   requeststatuslog requeststatuslog_requestid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);
 Z   ALTER TABLE ONLY public.requeststatuslog DROP CONSTRAINT requeststatuslog_requestid_fkey;
       public          postgres    false    263    251    4934         �           2606    33208 9   requeststatuslog requeststatuslog_transtophysicianid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_transtophysicianid_fkey FOREIGN KEY (transtophysicianid) REFERENCES public.physician(physicianid);
 c   ALTER TABLE ONLY public.requeststatuslog DROP CONSTRAINT requeststatuslog_transtophysicianid_fkey;
       public          postgres    false    241    4924    263         �           2606    33213 ,   requestwisefile requestwisefile_adminid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestwisefile
    ADD CONSTRAINT requestwisefile_adminid_fkey FOREIGN KEY (adminid) REFERENCES public.admin(adminid);
 V   ALTER TABLE ONLY public.requestwisefile DROP CONSTRAINT requestwisefile_adminid_fkey;
       public          postgres    false    217    267    4896         �           2606    33218 0   requestwisefile requestwisefile_physicianid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestwisefile
    ADD CONSTRAINT requestwisefile_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);
 Z   ALTER TABLE ONLY public.requestwisefile DROP CONSTRAINT requestwisefile_physicianid_fkey;
       public          postgres    false    241    267    4924         �           2606    33223 .   requestwisefile requestwisefile_requestid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.requestwisefile
    ADD CONSTRAINT requestwisefile_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);
 X   ALTER TABLE ONLY public.requestwisefile DROP CONSTRAINT requestwisefile_requestid_fkey;
       public          postgres    false    4934    251    267         �           2606    33228    rolemenu rolemenu_menuid_fkey    FK CONSTRAINT     ~   ALTER TABLE ONLY public.rolemenu
    ADD CONSTRAINT rolemenu_menuid_fkey FOREIGN KEY (menuid) REFERENCES public.menu(menuid);
 G   ALTER TABLE ONLY public.rolemenu DROP CONSTRAINT rolemenu_menuid_fkey;
       public          postgres    false    271    237    4920         �           2606    33233    rolemenu rolemenu_roleid_fkey    FK CONSTRAINT     ~   ALTER TABLE ONLY public.rolemenu
    ADD CONSTRAINT rolemenu_roleid_fkey FOREIGN KEY (roleid) REFERENCES public.role(roleid);
 G   ALTER TABLE ONLY public.rolemenu DROP CONSTRAINT rolemenu_roleid_fkey;
       public          postgres    false    269    4952    271         �           2606    33238    shift shift_createdby_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shift
    ADD CONSTRAINT shift_createdby_fkey FOREIGN KEY (createdby) REFERENCES public.aspnetusers(id);
 D   ALTER TABLE ONLY public.shift DROP CONSTRAINT shift_createdby_fkey;
       public          postgres    false    273    223    4904         �           2606    33243    shift shift_physicianid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shift
    ADD CONSTRAINT shift_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);
 F   ALTER TABLE ONLY public.shift DROP CONSTRAINT shift_physicianid_fkey;
       public          postgres    false    4924    241    273         �           2606    33248 '   shiftdetail shiftdetail_modifiedby_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shiftdetail
    ADD CONSTRAINT shiftdetail_modifiedby_fkey FOREIGN KEY (modifiedby) REFERENCES public.aspnetusers(id);
 Q   ALTER TABLE ONLY public.shiftdetail DROP CONSTRAINT shiftdetail_modifiedby_fkey;
       public          postgres    false    4904    275    223         �           2606    33253 $   shiftdetail shiftdetail_shiftid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shiftdetail
    ADD CONSTRAINT shiftdetail_shiftid_fkey FOREIGN KEY (shiftid) REFERENCES public.shift(shiftid);
 N   ALTER TABLE ONLY public.shiftdetail DROP CONSTRAINT shiftdetail_shiftid_fkey;
       public          postgres    false    273    275    4956         �           2606    33258 1   shiftdetailregion shiftdetailregion_regionid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shiftdetailregion
    ADD CONSTRAINT shiftdetailregion_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);
 [   ALTER TABLE ONLY public.shiftdetailregion DROP CONSTRAINT shiftdetailregion_regionid_fkey;
       public          postgres    false    4932    277    249         �           2606    33263 6   shiftdetailregion shiftdetailregion_shiftdetailid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.shiftdetailregion
    ADD CONSTRAINT shiftdetailregion_shiftdetailid_fkey FOREIGN KEY (shiftdetailid) REFERENCES public.shiftdetail(shiftdetailid);
 `   ALTER TABLE ONLY public.shiftdetailregion DROP CONSTRAINT shiftdetailregion_shiftdetailid_fkey;
       public          postgres    false    4958    275    277                                                                        5153.dat                                                                                            0000600 0004000 0002000 00000001505 14577030366 0014263 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        23	535fed3e-3a45-410e-bf8f-2168616833f12	Admin	Patel	admin@gmail.com	\N	\N	tulip bunglows	Ahmedabad	Gujarat	\N	123456	8	2003	24	535fed3e-3a45-410e-bf8f-2168616833f12	2024-01-06 14:31:05.649486	\N	\N	\N	\N	\N	\N
24	535fed3e-3a45-410e-bf8f-2168616833f2	Physician1	Patel	Physician@gmail.com	\N	\N	palladium mall	Ahmedabad	Gujarat	\N	123456	8	2003	22	535fed3e-3a45-410e-bf8f-2168616833f12	2024-03-06 12:03:05.649486	\N	\N	\N	\N	\N	\N
21	535fed3e-3a45-410e-bf8f-2168616833f1	krina	Bhalodiya	abc@gmail.com	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	535fed3e-3a45-410e-bf8f-2168616833f1	2024-02-12 14:31:05.649486	\N	\N	\N	\N	\N	\N
22	a3f8e19d-db2e-4a30-9927-529d0de87bab	krina123	wer	200540107186@darshan.ac.in	\N	1	abc	abc	abc	1	360005	8	2003	24	a3f8e19d-db2e-4a30-9927-529d0de87bab	2024-02-12 14:35:43.351004	\N	2024-02-20 14:34:48.903794	1	\N	\N	1
\.


                                                                                                                                                                                           5155.dat                                                                                            0000600 0004000 0002000 00000000322 14577030366 0014261 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	535fed3e-3a45-410e-bf8f-2168616833f12	admin	Patel	admin@gmail.com	\N	palladium mall	alpha mall1	ahmedabad	1	360005	9328699999	535fed3e-3a45-410e-bf8f-2168616833f12	2024-03-05 14:31:05.515492	\N	\N	1	0	1
\.


                                                                                                                                                                                                                                                                                                              5157.dat                                                                                            0000600 0004000 0002000 00000000014 14577030366 0014261 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        16	1	5
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    5159.dat                                                                                            0000600 0004000 0002000 00000000042 14577030366 0014264 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        3	Patient
2	Provider
1	Admin
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              5160.dat                                                                                            0000600 0004000 0002000 00000000173 14577030366 0014261 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        535fed3e-3a45-410e-bf8f-2168616833f12	1
535fed3e-3a45-410e-bf8f-2168616833f2	2
535fed3e-3a45-410e-bf8f-2168616833f1	3
\.


                                                                                                                                                                                                                                                                                                                                                                                                     5161.dat                                                                                            0000600 0004000 0002000 00000002145 14577030366 0014263 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        535fed3e-3a45-410e-bf8f-2168616833f3	Physician2	123	Physician2@gmail.com	123456789	2024-02-12 14:31:05.515492	\N	\N
535fed3e-3a45-410e-bf8f-2168616833f4	Physician3	123	Physician3@gmail.com	123456789	2024-02-12 14:31:05.515492	\N	\N
535fed3e-3a45-410e-bf8f-2168616833f5	Physician4	123	Physician4@gmail.com	123456789	2024-02-12 14:31:05.515492	\N	\N
535fed3e-3a45-410e-bf8f-2168616833f6	Physician5	123	Physician5@gmail.com	123456789	2024-02-12 14:31:05.515492	\N	\N
535fed3e-3a45-410e-bf8f-2168616833f2	Physician1	123	Physician1@gmail.com	123456789	2024-02-12 14:31:05.515492	\N	\N
a3f8e19d-db2e-4a30-9927-529d0de87bab	krina	123	200540107186@darshan.ac.in	9328699842	2024-02-12 14:35:43.222875	\N	2024-02-28 17:04:47.980372
535fed3e-3a45-410e-bf8f-2168616833f12	Admin	AQAAAAIAAYagAAAAENU+sq3skZTHzshkJzGbs4khlk+awQoVv6xwmvPo6+OoNIhMPmb32/XqmFjhgtL3aA==	admin@gmail.com	9328693286	2024-03-05 14:31:05.515492	\N	\N
535fed3e-3a45-410e-bf8f-2168616833f1	krina	AQAAAAIAAYagAAAAENU+sq3skZTHzshkJzGbs4khlk+awQoVv6xwmvPo6+OoNIhMPmb32/XqmFjhgtL3aA==	abc@gmail.com	123456789	2024-02-12 14:31:05.515492	\N	2024-02-16 12:37:00.021014
\.


                                                                                                                                                                                                                                                                                                                                                                                                                           5162.dat                                                                                            0000600 0004000 0002000 00000000526 14577030366 0014265 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        3	9328699842	200540107186@darshan.ac.in	\N	I don't want to take this case	84	\N	2024-02-29 10:34:45.624695	2024-02-29 10:34:45.624696
4	9328699842	abc@gmail	\N	i want to block this case	81	\N	2024-03-01 10:48:55.731841	2024-03-01 10:48:55.731892
5	9328699842	abc@gmail.com	\N	\N	71	\N	2024-03-14 17:38:48.481852	2024-03-14 17:38:48.4819
\.


                                                                                                                                                                          5164.dat                                                                                            0000600 0004000 0002000 00000002124 14577030366 0014263 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	11	\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-09 09:00:50.293558	\N	\N	\N	\N	\N
2	11	\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-09 09:06:44.269679	\N	\N	\N	\N	\N
3	Business Business 	\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-09 09:11:41.880594	\N	\N	\N	\N	\N
4	KrinaBhalodiya	\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-12 11:29:05.913828	\N	\N	\N	\N	\N
5	KrinaBhalodiya	\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-12 11:29:31.39293	\N	\N	\N	\N	\N
6	KrinaBhalodiya	\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-12 11:40:19.219766	\N	\N	\N	\N	\N
7		\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-20 14:05:33.654669	\N	\N	\N	\N	\N
8		\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-20 14:07:45.789928	\N	\N	\N	\N	\N
9		\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-20 14:08:25.106117	\N	\N	\N	\N	\N
10		\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-20 14:11:05.784995	\N	\N	\N	\N	\N
11		\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-20 14:11:11.604782	\N	\N	\N	\N	\N
12		\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-21 10:25:37.899788	\N	\N	\N	\N	\N
13	krinaBhalodiya	\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-21 11:19:39.873382	\N	\N	\N	\N	\N
14	krinabhalodiya	\N	\N	\N	\N	\N	\N	\N	\N	\N	2024-02-21 11:21:12.07014	\N	\N	\N	\N	\N
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                            5166.dat                                                                                            0000600 0004000 0002000 00000000254 14577030366 0014267 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	Not appropriate for service
2	Out of Service Area
3	Insurance Issue
4	Cost Issue
5	No Respone to call or text, left message
6	Referral to Clinic or Hospital
7	Other
\.


                                                                                                                                                                                                                                                                                                                                                    5168.dat                                                                                            0000600 0004000 0002000 00000000362 14577030366 0014271 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	1Concierge 1	\N	1	1	1	1	2024-02-09 09:07:20.353357	1	\N
2	krina 200540107186@darshan.ac.in	\N	krina	Bhalodiya	asdfaf	121321	2024-02-09 18:48:01.765666	1	\N
3	krina bhalodiya	\N	krina	Bhalodiya	asdfaf	abc	2024-02-21 12:24:02.33914	1	\N
\.


                                                                                                                                                                                                                                                                              5170.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014254 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           5220.dat                                                                                            0000600 0004000 0002000 00000000217 14577030366 0014255 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	61	\N	\N	\N	\N	\N	11	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	1	\N	f	2024-03-20 19:40:36.501384	2024-03-21 14:24:46.213863
\.


                                                                                                                                                                                                                                                                                                                                                                                 5171.dat                                                                                            0000600 0004000 0002000 00000002575 14577030366 0014273 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	Dhruv	10	+44 161 999 0000	sadhuvasvani0	Rajkot	Gujrat	360005	5	2024-03-07 14:00:57.100128	\N	0123456789	\N	\N	dhruv@gmail.com	0123456789
2	Hetanshi	9	+44 161 999 9999	sadhuvasvani9	Rajkot	Gujrat	360005	5	2024-03-07 14:00:57.100128	\N	9012345678	\N	\N	hetanshi@gmail.com	9012345678
3	Dhruvi	8	+44 161 999 8888	sadhuvasvani8	Rajkot	Gujrat	360005	4	2024-03-07 14:00:57.100128	\N	8901234567	\N	\N	dhruvi@gmail.com	8901234567
4	Misha	7	+44 161 999 7777	sadhuvasvani7	Rajkot	Gujrat	360005	4	2024-03-07 14:00:57.100128	\N	7890123456	\N	\N	misha@gmail.com	7890123456
5	Aayushi	6	+44 161 999 6666	sadhuvasvani6	Rajkot	Gujrat	360005	3	2024-03-07 14:00:57.100128	\N	6789012345	\N	\N	aayushi@gmail.com	6789012345
6	Komal	5	+44 161 999 5555	sadhuvasvani5	Rajkot	Gujrat	360005	3	2024-03-07 14:00:57.100128	\N	5678901234	\N	\N	komal@gmail.com	5678901234
7	Krisha	4	+44 161 999 4444	sadhuvasvani4	Rajkot	Gujrat	360005	2	2024-03-07 14:00:57.100128	\N	4567890123	\N	\N	krisha@gmail.com	4567890123
8	Krishn	3	+44 161 999 3333	sadhuvasvani3	Rajkot	Gujrat	360005	2	2024-03-07 14:00:57.100128	\N	3456789012	\N	\N	krishn@gmail.com	3456789012
9	Krinal	2	+44 161 999 2222	sadhuvasvani2	Rajkot	Gujrat	360005	1	2024-03-07 14:00:57.100128	\N	2345678901	\N	\N	krinal@gmail.com	2345678901
10	Krina	1	+44 161 999 1111	sadhuvasvani1	Rajkot	Gujrat	360005	1	2024-03-07 14:00:57.100128	\N	1234567890	\N	\N	krina@gmail.com	1234567890
\.


                                                                                                                                   5173.dat                                                                                            0000600 0004000 0002000 00000000774 14577030366 0014274 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	Athletic trainer	2024-03-07 14:00:57.100128	\N	\N
2	Dentist	2024-03-07 14:00:57.100128	\N	\N
3	Orthoptist	2024-03-07 14:00:57.100128	\N	\N
4	Skin specialist	2024-03-07 14:00:57.100128	\N	\N
5	Pharmacist	2024-03-07 14:00:57.100128	\N	\N
6	Medical physicist	2024-03-07 14:00:57.100128	\N	\N
7	Dietitian	2024-03-07 14:00:57.100128	\N	\N
8	Cardiac sonographer	2024-03-07 14:00:57.100128	\N	\N
9	Audiologist	2024-03-07 14:00:57.100128	\N	\N
10	Emergency medicine paramedic	2024-03-07 14:00:57.100128	\N	\N
\.


    5175.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014261 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           5177.dat                                                                                            0000600 0004000 0002000 00000001656 14577030366 0014300 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	5	83	+44 161 999 6666	aayushi@gmail.com	6789012345	provide all tablets as soon as possible	2	2024-03-07 14:26:31.579021	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f
2	2	72	+44 161 999 9999	hetanshi@gmail.com	9012345678	abc	\N	2024-03-07 14:36:03.692069	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f
3	8	83	+44 161 999 3333	krishn@gmail.com	3456789012	xyz	1	2024-03-07 14:36:48.110532	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f
4	4	83	+44 161 999 7777	misha@gmail.com	7890123456	provide diet chart	\N	2024-03-07 14:44:10.458288	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f
5	-1	61	fgy	fgy	uy	gfhf	7	2024-03-14 17:36:35.670947	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f
6	10	61	+44 161 999 1111	krina@gmail.com	1234567890	123456	2	2024-03-21 11:04:17.536401	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f
7	9	61	+44 161 999 2222	krinal@gmail.com	2345678901	care	2	2024-03-21 11:05:48.437447	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f
\.


                                                                                  5179.dat                                                                                            0000600 0004000 0002000 00000002170 14577030366 0014272 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        5	535fed3e-3a45-410e-bf8f-2168616833f2	Misha	Pansara	misha@gmail.com	1234567890	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	5	\N	\N	a3f8e19d-db2e-4a30-9927-529d0de87bab	2024-02-12 14:31:05.515492	\N	\N	\N	bn5	bn5.com	\N	\N	\N	\N	\N	\N	\N	\N
4	535fed3e-3a45-410e-bf8f-2168616833f3	Dhruvi	Patel	dhruvi@gmail.com	1234567890	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	4	\N	\N	a3f8e19d-db2e-4a30-9927-529d0de87bab	2024-02-12 14:31:05.515492	\N	\N	\N	bn4	bn4.com	\N	\N	\N	\N	\N	\N	\N	\N
3	535fed3e-3a45-410e-bf8f-2168616833f4	Komal	Malkan	komal@gmail.com	1234567890	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	3	\N	\N	a3f8e19d-db2e-4a30-9927-529d0de87bab	2024-02-12 14:31:05.515492	\N	\N	\N	bn3	bn3.com	\N	\N	\N	\N	\N	\N	\N	\N
2	535fed3e-3a45-410e-bf8f-2168616833f5	Aayushi	Dhruva	aayushi@gmail.com	1234567890	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	2	\N	\N	a3f8e19d-db2e-4a30-9927-529d0de87bab	2024-02-12 14:31:05.515492	\N	\N	\N	bn2	bn2.com	\N	\N	\N	\N	\N	\N	\N	\N
1	535fed3e-3a45-410e-bf8f-2168616833f6	Krina	Bhalodiya	krina@gmail.com	1234567890	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	1	\N	\N	a3f8e19d-db2e-4a30-9927-529d0de87bab	2024-02-12 14:31:05.515492	\N	\N	\N	bn1	bn1.com	\N	\N	\N	\N	\N	\N	\N	\N
\.


                                                                                                                                                                                                                                                                                                                                                                                                        5181.dat                                                                                            0000600 0004000 0002000 00000000545 14577030366 0014267 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	5	22.30390	70.80220	2024-02-12 14:31:05.515492	Misha	\N
2	4	25.46700	91.36620	2024-02-12 14:31:05.515492	Dhruvi	\N
3	3	13.70000	72.18333	2024-02-12 14:31:05.515492	Komal	\N
4	2	15.29930	74.12400	2024-02-12 14:31:05.515492	Aayushi	\N
5	1	12.97160	77.59460	2024-02-12 14:31:05.515492	Krina	\N
6	5	32.53870	75.97100	2024-02-12 14:31:05.515492	Krinal	\N
\.


                                                                                                                                                           5183.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014260 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           5185.dat                                                                                            0000600 0004000 0002000 00000000043 14577030366 0014264 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	5	5
2	4	4
3	3	3
4	2	2
5	1	1
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             5187.dat                                                                                            0000600 0004000 0002000 00000000067 14577030366 0014274 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	North	N
2	South	S
3	East	E
4	West	W
5	Central	C
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                         5189.dat                                                                                            0000600 0004000 0002000 00000005411 14577030366 0014274 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        63	1	22	krina	wer	9328699842	200540107186@darshan.ac.in	1	\N	123	2024-02-13 09:17:31.396463	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
81	3	22	krina	Bhalodiya	9328699842	abc@gmail	9	\N	123	2024-02-21 11:19:40.08544	\N	2024-03-14 10:52:19.064706	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	1	\N	\N	\N
74	2	22	krina	wer	9328699842	abc@gmail.com	9	\N	123	2024-02-16 09:53:38.984579	\N	2024-03-14 15:06:05.704052	\N	0	\N	\N	\N	\N	\N	\N	sister	\N	\N	6	\N	\N	\N
72	2	22	krina	Bhalodiya	9328699842	abcd@gmail.com	3	\N	123	2024-02-16 09:34:23.07075	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	ddd	\N	\N	3	\N	\N	\N
71	2	22	krina	Bhalodiya	9328699842	abc@gmail.com	11	\N	123	2024-02-16 09:20:10.380449	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	krina	\N	\N	4	\N	\N	\N
85	2	22	krina123	wer	9328699842	200540107186@darshan.ac.in	2	4	123	2024-02-21 15:20:34.930448	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
59	1	22	krina	wer	9328699842	abc@gmail.com	3	\N	123	2024-02-12 14:35:43.469665	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	3	\N	\N	\N
83	4	22	krina	Bhalodiya	9328699842	200540107186@darshan.ac.in	2	1	123	2024-02-21 12:24:04.050104	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	3	\N	\N	\N
62	4	21	krina	Bhalodiya	9328699842	200540107186@darshan.ac.in	1	\N	123	2024-02-13 09:09:17.356018	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
64	3	22	krina	wer	9328699842	abc@gmail.com	1	\N	123	2024-02-13 09:41:29.952675	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
65	2	22	krina	Bhalodiya	9328699842	abc@gmail.com	1	\N	123	2024-02-13 12:28:12.742179	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
60	3	22	krina	Bhalodiya	9328699842	abc@gmail.com	1	\N	123	2024-02-12 14:43:53.861411	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	1	\N	\N	\N
67	4	22	krina	wer	9328699842	abc@gmail.com	1	2	123	2024-02-15 17:37:15.581958	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
68	2	22	krina	wer1f2ghfhfgh	9328699842	abc@gmail.com	1	1	123	2024-02-15 19:04:53.936242	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
69	4	22	krina	wer1f2ghfhfgh	45645645654645	abc@gmail.com	1	4	123	2024-02-15 19:05:22.141848	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
70	2	22	krina	Bhalodiya	9328699842	200540107186@darshan.ac.in	1	2	123	2024-02-15 19:14:10.997956	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
73	2	22	krina	wer	9328699842	abc@gmail.com	1	3	123	2024-02-16 09:42:42.267499	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	krina	\N	\N	\N	\N	\N	\N
61	2	21	1	1	9328699842	200540107186@darshan.ac.in	4	5	123	2024-02-12 17:11:24.263757	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N
84	2	22	krina	Bhalodiya	9328699842	200540107186@darshan.ac.in	2	3	123	2024-02-21 15:06:17.305294	\N	\N	\N	0	\N	\N	\N	\N	\N	\N	krina	\N	\N	\N	\N	\N	\N
82	3	22	krina	bhalodiya	1234567890	abcdefgh@gmail.com	9	2	123	2024-02-21 11:21:12.07432	\N	2024-03-14 10:46:07.483793	\N	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	4	\N	\N	\N
\.


                                                                                                                                                                                                                                                       5191.dat                                                                                            0000600 0004000 0002000 00000000033 14577030366 0014260 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        7	81	13	\N
8	82	14	\N
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     5193.dat                                                                                            0000600 0004000 0002000 00000007326 14577030366 0014276 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        58	62	krina	Bhalodiya	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	4	\N	\N	abc	200540107186@darshan.ac.in	August	2003	2	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
59	63	krina	wer	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	5	\N	\N	fhsdhdhs	abc@gmail.com	August	2003	3	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
60	64	krina	wer	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	1	\N	\N	ewgftvsadfg	abc@gmail.com	August	2003	4	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
61	65	krina	Bhalodiya	123456789	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	2	\N	\N	rtstse	abc@gmail.com	August	2003	5	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
62	67	krina	wer	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	3	\N	\N	ewgftvsadfg	abc@gmail.com	August	2003	6	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
63	68	krina	wer1f2ghfhfgh	1234554784	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	4	\N	\N	ewgftvsadfg	abc@gmail.com	August	2003	7	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
64	69	krina	wer1f2ghfhfgh	45645645654645	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	5	\N	\N	ewgftvsadfg	abc@gmail.com	August	2003	8	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
69	74	krina12	Bhalodiya	1234567890	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	5	\N	\N	ewgftvsadfg	123@gmail.com	August	2003	13	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
65	70	krina	Bhalodiya	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	1	\N	\N	ewgftvsadfg	200540107186@darshan.ac.in	August	2003	9	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
66	71	krina	Bhalodiya	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	2	\N	\N	ewgftvsadfg	abc@gmail.com	August	2003	10	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
67	72	krina	Bhalodiya	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	3	\N	\N	ewgftvsadfg	abcd@gmail.com	August	2003	11	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
68	73	krina	wer	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	4	\N	\N	ewgftvsadfg	abcdefg@gmail.com	August	2003	12	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
55	59	krina	wer	1234567891	\N	abcMall,AHMEDABAD,GUJARAT,123456	1	\N	\N	wer	abc@gmail.com	August	2003	20	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
57	61	1	1	11	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	3	\N	\N	\N	200540107186@darshan.ac.in	August	2003	1	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
76	81	krina	Bhalodiya	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	1	\N	\N	ewgftvsadfg	123@gmail.com	August	2003	14	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
77	82	krina	BHALODIYA	123456789	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	2	\N	\N	ewgftvsadfg	123@gmail.com	August	2003	15	\N	ALPHAMALL	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
78	83	krina	Bhalodiya	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	3	\N	\N	ewgftvsadfg	200540107186@darshan.ac.in	August	2003	16	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
79	84	krina	Bhalodiya	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	4	\N	\N	ewgftvsadfg	abc@gmail.com	August	2003	17	\N	ALPHA Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
80	85	Krina	wer	789456123	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	1	\N	\N	krina	200540107186@darshan.ac.in	December	2003	30	\N	ISCKON	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
56	60	krina	Bhalodiya	9328699842	\N	Palladium Mall,AHMEDABAD,GUJARAT,123456	2	\N	\N	skin burn	abc@gmail.com	August	2003	19	\N	Palladium Mall	AHMEDABAD	GUJARAT	123456	\N	\N	\N	\N	\N	\N	\N	\N
\.


                                                                                                                                                                                                                                                                                                          5195.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014263 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           5197.dat                                                                                            0000600 0004000 0002000 00000000017 14577030366 0014270 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        3	83	3	\N
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 5199.dat                                                                                            0000600 0004000 0002000 00000001075 14577030366 0014277 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        1	83	\N	\N	\N	-	case is pending	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f	2024-03-04 11:31:17.938568	\N	2024-03-04 17:49:54.997446	\N	\N
2	70	\N	\N	\N	critical case	\N	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f	2024-03-05 09:28:30.286345	\N	\N	\N	\N
3	69	\N	\N	\N	-	\N	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f	2024-03-05 15:19:17.669385	\N	\N	\N	\N
4	60	\N	\N	\N	I'm not able to provide this type of treatment	Critical case provider not found updated	001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f	2024-03-08 14:42:04.255735	\N	2024-03-20 19:07:09.956628	\N	\N
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                   5201.dat                                                                                            0000600 0004000 0002000 00000004635 14577030366 0014264 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        21	61	1	\N	\N	\N	Case cancle by physician	2024-02-28 18:40:51.701218	\N	1
22	60	1	\N	\N	\N	Case cancel by physician and assigned to admin	2024-02-28 18:40:51.701218	\N	1
17	72	3	\N	\N	\N	i want to cancel this request	2024-02-29 14:18:29.965055	\N	\N
18	71	3	\N	\N	\N	i don't want to provide service to this patient	2024-02-29 14:19:14.971415	\N	\N
19	74	3	\N	\N	\N	Cancel this case	2024-03-01 10:45:50.071562	\N	\N
25	82	10	\N	\N	\N	\N	2024-03-01 16:46:30.178523	\N	\N
28	73	10	\N	\N	\N	\N	2024-03-04 11:38:08.267768	\N	\N
35	85	2	1	\N	\N	Skin related issue	2024-03-04 17:29:49.852545	\N	\N
36	85	2	1	\N	4	treat well	2024-03-04 17:30:02.48949	\N	\N
37	85	2	4	\N	5	transfered to misha for further treatment	2024-03-05 09:22:29.306027	\N	\N
38	83	3	\N	\N	\N	Cancel this case	2024-03-05 09:23:44.875805	\N	\N
39	70	2	2	\N	\N	Accident case	2024-03-05 09:27:59.761926	\N	\N
40	60	3	\N	\N	\N	Provider Not found	2024-03-08 14:41:04.018042	\N	\N
41	85	4	\N	\N	\N	\N	2024-03-11 18:22:48.182076	\N	\N
42	70	7	\N	\N	\N	i don't want treatment from this physician or hospital	2024-03-11 18:23:47.080428	\N	\N
43	69	2	4	\N	\N	Diet case	2024-03-12 14:17:51.607343	\N	\N
44	69	7	\N	\N	\N	i'm not agree with this agreement	2024-03-12 14:25:00.827828	\N	\N
45	68	2	1	\N	\N	Eye surgery	2024-03-12 14:26:46.715438	\N	\N
46	68	4	\N	\N	\N	\N	2024-03-12 14:27:05.715479	\N	\N
47	67	2	2	\N	\N	Case cancelled by admin	2024-03-12 18:12:31.144076	\N	\N
48	67	4	\N	\N	\N	\N	2024-03-12 18:13:38.50524	\N	\N
49	61	2	5	\N	\N	to assign this request,select and search another physician.\r\n	2024-03-13 17:26:43.037577	\N	\N
50	61	4	\N	\N	\N	\N	2024-03-13 17:27:48.640793	\N	\N
51	81	3	\N	\N	\N	Patient Name : krina Bhalodiya\r\n\r\n	2024-03-13 17:29:04.266595	\N	\N
52	84	2	5	\N	\N	123	2024-03-13 17:42:06.080305	\N	\N
53	83	2	3	\N	\N	123	2024-03-13 17:48:07.517046	\N	\N
54	84	2	5	\N	3	exchange	2024-03-13 17:49:48.500296	\N	\N
55	82	3	\N	\N	\N	xyz	2024-03-13 18:05:46.190062	\N	\N
56	74	3	\N	\N	\N	abc	2024-03-13 18:06:05.163255	\N	\N
60	82	9	\N	\N	\N	\N	2024-03-14 10:46:07.490111	\N	\N
61	81	9	\N	\N	\N	\N	2024-03-14 10:52:19.097729	\N	\N
62	74	9	\N	\N	\N	\N	2024-03-14 15:06:05.739579	\N	\N
63	72	3	\N	\N	\N	cancel case	2024-03-14 17:14:30.381042	\N	\N
64	85	2	1	\N	\N	case assigned\r\n	2024-03-18 13:35:29.197641	\N	\N
65	85	2	1	\N	4	transfered	2024-03-18 13:37:18.038485	\N	\N
66	59	3	\N	\N	\N	abc	2024-03-20 12:26:31.392582	\N	\N
67	83	2	3	\N	1	\N	2024-03-21 15:48:15.613607	\N	\N
\.


                                                                                                   5203.dat                                                                                            0000600 0004000 0002000 00000000063 14577030366 0014255 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        4	Concierge
1	Business
2	Patient\n
3	Family\n
\.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                             5205.dat                                                                                            0000600 0004000 0002000 00000003744 14577030366 0014270 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        37	85	/Upload/85/Fig33. Month wise Scheduling-20240305121106.jpg	2024-03-05 12:11:06.517	\N	\N	\N	\N	\N	\N	\N	0	\N
46	85	/Upload/85/krina-20240305054535.sql	2024-03-05 17:45:35.976294	\N	1	\N	\N	\N	\N	\N	1	\N
38	70	/Upload/70/7774236_3736765-20240305014219.jpg	2024-03-05 13:42:19.497216	\N	1	\N	\N	\N	\N	\N	0	\N
35	85	/Upload/85/7768657_3763028-20240305104826.jpg	2024-03-05 10:48:26.688947	\N	1	\N	\N	\N	\N	\N	1	\N
40	85	/Upload/85/Fig8. Admin dashboard in To Close state-20240305024652.jpg	2024-03-05 14:46:52.170095	\N	1	\N	\N	\N	\N	\N	1	\N
41	85	/Upload/85/SRS-23-24-Trainees-21Feb24-20240305025646.pdf	2024-03-05 14:56:46.712305	\N	1	\N	\N	\N	\N	\N	1	\N
47	84	/Upload/84/Fig39. Provider Location-20240305105915-20240313054909.jpg	2024-03-13 17:49:09.270694	\N	1	\N	\N	\N	\N	\N	0	\N
49	74	/Upload/74/Fig39. Provider Location-20240305105915 (1)-20240314103648.jpg	2024-03-14 10:36:48.290593	\N	1	\N	\N	\N	\N	\N	0	\N
48	81	/Upload/81/Fig33. Month wise Scheduling-20240305121106-20240313064432.jpg	2024-03-13 18:44:32.446319	\N	1	\N	\N	\N	\N	\N	1	\N
50	61	/Upload/61/Fig39. Provider Location-20240305105915 (1)-20240314103648-20240314053706.jpg	2024-03-14 17:37:06.849516	\N	1	\N	\N	\N	\N	\N	1	\N
51	61	/Upload/61/paper-20240314053716.pdf	2024-03-14 17:37:16.067431	\N	1	\N	\N	\N	\N	\N	1	\N
52	83	/Upload/83/-20240321093745.bash_history	2024-03-21 09:37:45.4436	\N	1	\N	\N	\N	\N	\N	1	\N
53	83	/Upload/83/-20240321093811.gitconfig	2024-03-21 09:38:11.806942	\N	1	\N	\N	\N	\N	\N	0	\N
54	83	/Upload/83/-20240321101906.bash_history	2024-03-21 10:19:06.152554	\N	1	\N	\N	\N	\N	\N	0	\N
57	83	/Upload/83/SRS-23-24-Trainees-8March24-20240321043345.pdf	2024-03-21 16:33:45.971946	\N	1	\N	\N	\N	\N	\N	0	\N
58	83	/Upload/83/DB0544_2 Final-20240321043431.pdf	2024-03-21 16:34:31.338955	\N	1	\N	\N	\N	\N	\N	0	\N
36	85	/Upload/85/Fig39. Provider Location-20240305105915.jpg	2024-03-05 10:59:15.063386	1	\N	\N	\N	\N	\N	\N	0	\N
39	85	/Upload/85/11116016_415-20240305015756.jpg	2024-03-05 13:57:56.603483	\N	1	\N	\N	\N	\N	\N	0	\N
\.


                            5207.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014255 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           5209.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014257 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           5211.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014250 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           5213.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014252 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           5215.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014254 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           5217.dat                                                                                            0000600 0004000 0002000 00000000005 14577030366 0014256 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        \.


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           restore.sql                                                                                         0000600 0004000 0002000 00000243012 14577030366 0015401 0                                                                                                    ustar 00postgres                        postgres                        0000000 0000000                                                                                                                                                                        --
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

DROP DATABASE "HalloDoc";
--
-- Name: HalloDoc; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "HalloDoc" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';


ALTER DATABASE "HalloDoc" OWNER TO postgres;

\connect "HalloDoc"

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
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    userid integer NOT NULL,
    aspnetuserid character varying(128),
    firstname character varying(100) NOT NULL,
    lastname character varying(100),
    email character varying(50) NOT NULL,
    mobile character varying(20),
    ismobile bit(1),
    street character varying(100),
    city character varying(100),
    state character varying(100),
    regionid integer,
    zipcode character varying(10),
    strmonth character varying(20),
    intyear integer,
    intdate integer,
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    status smallint,
    isdeleted bit(1),
    ip character varying(20),
    isrequestwithemail bit(1)
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- Name: User_userid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."User_userid_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."User_userid_seq" OWNER TO postgres;

--
-- Name: User_userid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."User_userid_seq" OWNED BY public."User".userid;


--
-- Name: admin; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.admin (
    adminid integer NOT NULL,
    aspnetuserid character varying(128) NOT NULL,
    firstname character varying(100) NOT NULL,
    lastname character varying(100),
    email character varying(50) NOT NULL,
    mobile character varying(20),
    address1 character varying(500),
    address2 character varying(500),
    city character varying(100),
    regionid integer,
    zip character varying(10),
    altphone character varying(20),
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    status smallint,
    isdeleted bit(1),
    roleid integer
);


ALTER TABLE public.admin OWNER TO postgres;

--
-- Name: admin_adminid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.admin_adminid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.admin_adminid_seq OWNER TO postgres;

--
-- Name: admin_adminid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.admin_adminid_seq OWNED BY public.admin.adminid;


--
-- Name: adminregion; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.adminregion (
    adminregionid integer NOT NULL,
    adminid integer NOT NULL,
    regionid integer NOT NULL
);


ALTER TABLE public.adminregion OWNER TO postgres;

--
-- Name: adminregion_adminregionid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.adminregion_adminregionid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.adminregion_adminregionid_seq OWNER TO postgres;

--
-- Name: adminregion_adminregionid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.adminregion_adminregionid_seq OWNED BY public.adminregion.adminregionid;


--
-- Name: aspnetroles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.aspnetroles (
    id character varying(128) NOT NULL,
    name character varying(256) NOT NULL
);


ALTER TABLE public.aspnetroles OWNER TO postgres;

--
-- Name: aspnetuserroles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.aspnetuserroles (
    userid character varying(128) NOT NULL,
    roleid character varying(128)
);


ALTER TABLE public.aspnetuserroles OWNER TO postgres;

--
-- Name: aspnetusers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.aspnetusers (
    id character varying(128) NOT NULL,
    username character varying(256) NOT NULL,
    passwordhash character varying(255),
    email character varying(256),
    phonenumber character varying(20),
    "CreatedDate" timestamp without time zone NOT NULL,
    ip character varying(20),
    modifieddate timestamp without time zone
);


ALTER TABLE public.aspnetusers OWNER TO postgres;

--
-- Name: blockrequests; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.blockrequests (
    blockrequestid integer NOT NULL,
    phonenumber character varying(50),
    email character varying(50),
    isactive bit(1),
    reason text,
    requestid character varying(50) NOT NULL,
    ip character varying(20),
    createddate timestamp without time zone,
    modifieddate timestamp without time zone
);


ALTER TABLE public.blockrequests OWNER TO postgres;

--
-- Name: blockrequests_blockrequestid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.blockrequests_blockrequestid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.blockrequests_blockrequestid_seq OWNER TO postgres;

--
-- Name: blockrequests_blockrequestid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.blockrequests_blockrequestid_seq OWNED BY public.blockrequests.blockrequestid;


--
-- Name: business; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.business (
    businessid integer NOT NULL,
    name character varying(100) NOT NULL,
    address1 character varying(500),
    address2 character varying(500),
    city character varying(50),
    regionid integer,
    zipcode character varying(10),
    phonenumber character varying(20),
    faxnumber character varying(20),
    isregistered bit(1),
    createdby character varying(128),
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    status smallint,
    isdeleted bit(1),
    ip character varying(20)
);


ALTER TABLE public.business OWNER TO postgres;

--
-- Name: business_businessid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.business_businessid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.business_businessid_seq OWNER TO postgres;

--
-- Name: business_businessid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.business_businessid_seq OWNED BY public.business.businessid;


--
-- Name: casetag; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.casetag (
    casetagid integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.casetag OWNER TO postgres;

--
-- Name: casetag_casetagid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.casetag_casetagid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.casetag_casetagid_seq OWNER TO postgres;

--
-- Name: casetag_casetagid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.casetag_casetagid_seq OWNED BY public.casetag.casetagid;


--
-- Name: concierge; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.concierge (
    conciergeid integer NOT NULL,
    conciergename character varying(100) NOT NULL,
    address character varying(150),
    street character varying(50) NOT NULL,
    city character varying(50) NOT NULL,
    state character varying(50) NOT NULL,
    zipcode character varying(50) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    regionid integer NOT NULL,
    ip character varying(20)
);


ALTER TABLE public.concierge OWNER TO postgres;

--
-- Name: concierge_conciergeid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.concierge_conciergeid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.concierge_conciergeid_seq OWNER TO postgres;

--
-- Name: concierge_conciergeid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.concierge_conciergeid_seq OWNED BY public.concierge.conciergeid;


--
-- Name: emaillog; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.emaillog (
    emaillogid numeric(9,0) NOT NULL,
    emailtemplate text NOT NULL,
    subjectname character varying(200) NOT NULL,
    emailid character varying(200) NOT NULL,
    confirmationnumber character varying(200),
    filepath text,
    roleid integer,
    requestid integer,
    adminid integer,
    physicianid integer,
    createdate timestamp without time zone NOT NULL,
    sentdate timestamp without time zone,
    isemailsent bit(1),
    senttries integer,
    action integer
);


ALTER TABLE public.emaillog OWNER TO postgres;

--
-- Name: encounterform; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.encounterform (
    encounterformid integer NOT NULL,
    requestid integer,
    historyofpresentillnessorinjury text,
    medicalhistory text,
    medications text,
    allergies text,
    temp text,
    hr text,
    rr text,
    bloodpressuresystolic text,
    bloodpressurediastolic text,
    o2 text,
    pain text,
    heent text,
    cv text,
    chest text,
    abd text,
    extremities text,
    skin text,
    neuro text,
    other text,
    diagnosis text,
    treatment_plan text,
    medicaldispensed text,
    procedures text,
    followup text,
    adminid integer,
    physicianid integer,
    isfinalize boolean DEFAULT false NOT NULL,
    createddate timestamp without time zone,
    modifieddate timestamp without time zone
);


ALTER TABLE public.encounterform OWNER TO postgres;

--
-- Name: encounterform_encounterformid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.encounterform_encounterformid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.encounterform_encounterformid_seq OWNER TO postgres;

--
-- Name: encounterform_encounterformid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.encounterform_encounterformid_seq OWNED BY public.encounterform.encounterformid;


--
-- Name: healthprofessionals; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.healthprofessionals (
    vendorid integer NOT NULL,
    vendorname character varying(100) NOT NULL,
    profession integer,
    faxnumber character varying(50) NOT NULL,
    address character varying(150),
    city character varying(100),
    state character varying(50),
    zip character varying(50),
    regionid integer,
    createddate timestamp without time zone NOT NULL,
    modifieddate timestamp without time zone,
    phonenumber character varying(100),
    isdeleted bit(1),
    ip character varying(20),
    email character varying(50),
    businesscontact character varying(100)
);


ALTER TABLE public.healthprofessionals OWNER TO postgres;

--
-- Name: healthprofessionals_vendorid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.healthprofessionals_vendorid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.healthprofessionals_vendorid_seq OWNER TO postgres;

--
-- Name: healthprofessionals_vendorid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.healthprofessionals_vendorid_seq OWNED BY public.healthprofessionals.vendorid;


--
-- Name: healthprofessionaltype; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.healthprofessionaltype (
    healthprofessionalid integer NOT NULL,
    professionname character varying(50) NOT NULL,
    createddate timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    isactive bit(1),
    isdeleted bit(1)
);


ALTER TABLE public.healthprofessionaltype OWNER TO postgres;

--
-- Name: healthprofessionaltype_healthprofessionalid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.healthprofessionaltype_healthprofessionalid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.healthprofessionaltype_healthprofessionalid_seq OWNER TO postgres;

--
-- Name: healthprofessionaltype_healthprofessionalid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.healthprofessionaltype_healthprofessionalid_seq OWNED BY public.healthprofessionaltype.healthprofessionalid;


--
-- Name: menu; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.menu (
    menuid integer NOT NULL,
    name character varying(50) NOT NULL,
    accounttype smallint NOT NULL,
    sortorder integer
);


ALTER TABLE public.menu OWNER TO postgres;

--
-- Name: menu_menuid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.menu_menuid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.menu_menuid_seq OWNER TO postgres;

--
-- Name: menu_menuid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.menu_menuid_seq OWNED BY public.menu.menuid;


--
-- Name: orderdetails; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.orderdetails (
    id integer NOT NULL,
    vendorid integer,
    requestid integer,
    faxnumber character varying(50),
    email character varying(50),
    businesscontact character varying(100),
    prescription text,
    noofrefill integer,
    createddate timestamp without time zone,
    createdby character varying(100)
);


ALTER TABLE public.orderdetails OWNER TO postgres;

--
-- Name: orderdetails_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.orderdetails_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.orderdetails_id_seq OWNER TO postgres;

--
-- Name: orderdetails_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.orderdetails_id_seq OWNED BY public.orderdetails.id;


--
-- Name: physician; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.physician (
    physicianid integer NOT NULL,
    aspnetuserid character varying(128),
    firstname character varying(100) NOT NULL,
    lastname character varying(100),
    email character varying(50) NOT NULL,
    mobile character varying(20),
    medicallicense character varying(500),
    photo character varying(100),
    adminnotes character varying(500),
    isagreementdoc bit(1),
    isbackgrounddoc bit(1),
    istrainingdoc bit(1),
    isnondisclosuredoc bit(1),
    address1 character varying(500),
    address2 character varying(500),
    city character varying(100),
    regionid integer,
    zip character varying(10),
    altphone character varying(20),
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    status smallint,
    businessname character varying(100) NOT NULL,
    businesswebsite character varying(200) NOT NULL,
    isdeleted bit(1),
    roleid integer,
    npinumber character varying(500),
    islicensedoc bit(1),
    signature character varying(100),
    iscredentialdoc bit(1),
    istokengenerate bit(1),
    syncemailaddress character varying(50)
);


ALTER TABLE public.physician OWNER TO postgres;

--
-- Name: physician_physicianid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.physician_physicianid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.physician_physicianid_seq OWNER TO postgres;

--
-- Name: physician_physicianid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.physician_physicianid_seq OWNED BY public.physician.physicianid;


--
-- Name: physicianlocation; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.physicianlocation (
    locationid integer NOT NULL,
    physicianid integer NOT NULL,
    latitude numeric(9,5),
    longitude numeric(9,5),
    createddate timestamp without time zone,
    physicianname character varying(50),
    address character varying(500)
);


ALTER TABLE public.physicianlocation OWNER TO postgres;

--
-- Name: physicianlocation_locationid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.physicianlocation_locationid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.physicianlocation_locationid_seq OWNER TO postgres;

--
-- Name: physicianlocation_locationid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.physicianlocation_locationid_seq OWNED BY public.physicianlocation.locationid;


--
-- Name: physiciannotification; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.physiciannotification (
    id integer NOT NULL,
    physicianid integer NOT NULL,
    isnotificationstopped bit(1) NOT NULL
);


ALTER TABLE public.physiciannotification OWNER TO postgres;

--
-- Name: physiciannotification_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.physiciannotification_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.physiciannotification_id_seq OWNER TO postgres;

--
-- Name: physiciannotification_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.physiciannotification_id_seq OWNED BY public.physiciannotification.id;


--
-- Name: physicianregion; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.physicianregion (
    physicianregionid integer NOT NULL,
    physicianid integer NOT NULL,
    regionid integer NOT NULL
);


ALTER TABLE public.physicianregion OWNER TO postgres;

--
-- Name: physicianregion_physicianregionid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.physicianregion_physicianregionid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.physicianregion_physicianregionid_seq OWNER TO postgres;

--
-- Name: physicianregion_physicianregionid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.physicianregion_physicianregionid_seq OWNED BY public.physicianregion.physicianregionid;


--
-- Name: region; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.region (
    regionid integer NOT NULL,
    name character varying(50) NOT NULL,
    abbreviation character varying(50)
);


ALTER TABLE public.region OWNER TO postgres;

--
-- Name: region_regionid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.region_regionid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.region_regionid_seq OWNER TO postgres;

--
-- Name: region_regionid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.region_regionid_seq OWNED BY public.region.regionid;


--
-- Name: request; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.request (
    requestid integer NOT NULL,
    requesttypeid integer NOT NULL,
    userid integer,
    firstname character varying(100),
    lastname character varying(100),
    phonenumber character varying(23),
    email character varying(50),
    status smallint NOT NULL,
    physicianid integer,
    confirmationnumber character varying(20),
    createddate timestamp without time zone NOT NULL,
    isdeleted bit(1),
    modifieddate timestamp without time zone,
    declinedby character varying(250),
    isurgentemailsent bit(1) NOT NULL,
    lastwellnessdate timestamp without time zone,
    ismobile bit(1),
    calltype smallint,
    completedbyphysician bit(1),
    lastreservationdate timestamp without time zone,
    accepteddate timestamp without time zone,
    relationname character varying(100),
    casenumber character varying(50),
    ip character varying(20),
    casetag character varying(50),
    casetagphysician character varying(50),
    patientaccountid character varying(128),
    createduserid integer
);


ALTER TABLE public.request OWNER TO postgres;

--
-- Name: request_requestid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.request_requestid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.request_requestid_seq OWNER TO postgres;

--
-- Name: request_requestid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.request_requestid_seq OWNED BY public.request.requestid;


--
-- Name: requestbusiness; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.requestbusiness (
    requestbusinessid integer NOT NULL,
    requestid integer,
    businessid integer,
    ip character varying(20)
);


ALTER TABLE public.requestbusiness OWNER TO postgres;

--
-- Name: requestbusiness_requestbusinessid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.requestbusiness_requestbusinessid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.requestbusiness_requestbusinessid_seq OWNER TO postgres;

--
-- Name: requestbusiness_requestbusinessid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.requestbusiness_requestbusinessid_seq OWNED BY public.requestbusiness.requestbusinessid;


--
-- Name: requestclient; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.requestclient (
    requestclientid integer NOT NULL,
    requestid integer NOT NULL,
    firstname character varying(100) NOT NULL,
    lastname character varying(100),
    phonenumber character varying(23),
    location character varying(100),
    address character varying(500),
    regionid integer,
    notimobile character varying(20),
    notiemail character varying(50),
    notes character varying(500),
    email character varying(50),
    strmonth character varying(20),
    intyear integer,
    intdate integer,
    ismobile bit(1),
    street character varying(100),
    city character varying(100),
    state character varying(100),
    zipcode character varying(10),
    communicationtype smallint,
    remindreservationcount smallint,
    remindhousecallcount smallint,
    issetfollowupsent smallint,
    ip character varying(20),
    isreservationremindersent smallint,
    latitude numeric(9,0),
    longitude numeric(9,0)
);


ALTER TABLE public.requestclient OWNER TO postgres;

--
-- Name: requestclient_requestclientid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.requestclient_requestclientid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.requestclient_requestclientid_seq OWNER TO postgres;

--
-- Name: requestclient_requestclientid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.requestclient_requestclientid_seq OWNED BY public.requestclient.requestclientid;


--
-- Name: requestclosed; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.requestclosed (
    requestclosedid integer NOT NULL,
    requestid integer NOT NULL,
    requeststatuslogid integer NOT NULL,
    phynotes character varying(500),
    clientnotes character varying(500),
    ip character varying(20)
);


ALTER TABLE public.requestclosed OWNER TO postgres;

--
-- Name: requestclosed_requestclosedid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.requestclosed_requestclosedid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.requestclosed_requestclosedid_seq OWNER TO postgres;

--
-- Name: requestclosed_requestclosedid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.requestclosed_requestclosedid_seq OWNED BY public.requestclosed.requestclosedid;


--
-- Name: requestconcierge; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.requestconcierge (
    id integer NOT NULL,
    requestid integer NOT NULL,
    conciergeid integer NOT NULL,
    ip character varying(20)
);


ALTER TABLE public.requestconcierge OWNER TO postgres;

--
-- Name: requestconcierge_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.requestconcierge_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.requestconcierge_id_seq OWNER TO postgres;

--
-- Name: requestconcierge_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.requestconcierge_id_seq OWNED BY public.requestconcierge.id;


--
-- Name: requestnotes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.requestnotes (
    requestnotesid integer NOT NULL,
    requestid integer NOT NULL,
    strmonth character varying(20),
    intyear integer,
    intdate integer,
    physiciannotes character varying(500),
    adminnotes character varying(500),
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    ip character varying(20),
    administrativenotes character varying(500)
);


ALTER TABLE public.requestnotes OWNER TO postgres;

--
-- Name: requestnotes_requestnotesid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.requestnotes_requestnotesid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.requestnotes_requestnotesid_seq OWNER TO postgres;

--
-- Name: requestnotes_requestnotesid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.requestnotes_requestnotesid_seq OWNED BY public.requestnotes.requestnotesid;


--
-- Name: requeststatuslog; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.requeststatuslog (
    requeststatuslogid integer NOT NULL,
    requestid integer NOT NULL,
    status smallint NOT NULL,
    physicianid integer,
    adminid integer,
    transtophysicianid integer,
    notes character varying(500),
    createddate timestamp without time zone NOT NULL,
    ip character varying(20),
    transtoadmin bit(1)
);


ALTER TABLE public.requeststatuslog OWNER TO postgres;

--
-- Name: requeststatuslog_requeststatuslogid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.requeststatuslog_requeststatuslogid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.requeststatuslog_requeststatuslogid_seq OWNER TO postgres;

--
-- Name: requeststatuslog_requeststatuslogid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.requeststatuslog_requeststatuslogid_seq OWNED BY public.requeststatuslog.requeststatuslogid;


--
-- Name: requesttype; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.requesttype (
    requesttypeid integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.requesttype OWNER TO postgres;

--
-- Name: requesttype_requesttypeid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.requesttype_requesttypeid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.requesttype_requesttypeid_seq OWNER TO postgres;

--
-- Name: requesttype_requesttypeid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.requesttype_requesttypeid_seq OWNED BY public.requesttype.requesttypeid;


--
-- Name: requestwisefile; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.requestwisefile (
    requestwisefileid integer NOT NULL,
    requestid integer NOT NULL,
    filename character varying(500) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    physicianid integer,
    adminid integer,
    doctype smallint,
    isfrontside bit(1),
    iscompensation bit(1),
    ip character varying(20),
    isfinalize bit(1),
    isdeleted bit(1),
    ispatientrecords bit(1)
);


ALTER TABLE public.requestwisefile OWNER TO postgres;

--
-- Name: requestwisefile_requestwisefileid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.requestwisefile_requestwisefileid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.requestwisefile_requestwisefileid_seq OWNER TO postgres;

--
-- Name: requestwisefile_requestwisefileid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.requestwisefile_requestwisefileid_seq OWNED BY public.requestwisefile.requestwisefileid;


--
-- Name: role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.role (
    roleid integer NOT NULL,
    name character varying(50) NOT NULL,
    accounttype smallint NOT NULL,
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    isdeleted bit(1) NOT NULL,
    ip character varying(20)
);


ALTER TABLE public.role OWNER TO postgres;

--
-- Name: role_roleid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.role_roleid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.role_roleid_seq OWNER TO postgres;

--
-- Name: role_roleid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.role_roleid_seq OWNED BY public.role.roleid;


--
-- Name: rolemenu; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.rolemenu (
    rolemenuid integer NOT NULL,
    roleid integer NOT NULL,
    menuid integer NOT NULL
);


ALTER TABLE public.rolemenu OWNER TO postgres;

--
-- Name: rolemenu_rolemenuid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.rolemenu_rolemenuid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.rolemenu_rolemenuid_seq OWNER TO postgres;

--
-- Name: rolemenu_rolemenuid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.rolemenu_rolemenuid_seq OWNED BY public.rolemenu.rolemenuid;


--
-- Name: shift; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.shift (
    shiftid integer NOT NULL,
    physicianid integer NOT NULL,
    startdate date NOT NULL,
    isrepeat bit(1) NOT NULL,
    weekdays character(7),
    repeatupto integer,
    createdby character varying(128) NOT NULL,
    createddate timestamp without time zone NOT NULL,
    ip character varying(20)
);


ALTER TABLE public.shift OWNER TO postgres;

--
-- Name: shift_shiftid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.shift_shiftid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.shift_shiftid_seq OWNER TO postgres;

--
-- Name: shift_shiftid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.shift_shiftid_seq OWNED BY public.shift.shiftid;


--
-- Name: shiftdetail; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.shiftdetail (
    shiftdetailid integer NOT NULL,
    shiftid integer NOT NULL,
    shiftdate timestamp without time zone NOT NULL,
    regionid integer,
    starttime time without time zone NOT NULL,
    endtime time without time zone NOT NULL,
    status smallint NOT NULL,
    isdeleted bit(1) NOT NULL,
    modifiedby character varying(128),
    modifieddate timestamp without time zone,
    lastrunningdate timestamp without time zone,
    eventid character varying(100),
    issync bit(1)
);


ALTER TABLE public.shiftdetail OWNER TO postgres;

--
-- Name: shiftdetail_shiftdetailid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.shiftdetail_shiftdetailid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.shiftdetail_shiftdetailid_seq OWNER TO postgres;

--
-- Name: shiftdetail_shiftdetailid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.shiftdetail_shiftdetailid_seq OWNED BY public.shiftdetail.shiftdetailid;


--
-- Name: shiftdetailregion; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.shiftdetailregion (
    shiftdetailregionid integer NOT NULL,
    shiftdetailid integer NOT NULL,
    regionid integer NOT NULL,
    isdeleted bit(1)
);


ALTER TABLE public.shiftdetailregion OWNER TO postgres;

--
-- Name: shiftdetailregion_shiftdetailregionid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.shiftdetailregion_shiftdetailregionid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.shiftdetailregion_shiftdetailregionid_seq OWNER TO postgres;

--
-- Name: shiftdetailregion_shiftdetailregionid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.shiftdetailregion_shiftdetailregionid_seq OWNED BY public.shiftdetailregion.shiftdetailregionid;


--
-- Name: smslog; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.smslog (
    smslogid integer NOT NULL,
    smstemplate character varying(100),
    mobilenumber character varying(50) NOT NULL,
    confirmationnumber character varying(200),
    roleid integer,
    adminid integer,
    requestid integer,
    physicianid integer,
    createdate timestamp without time zone NOT NULL,
    sentdate timestamp without time zone,
    issmssent bit(1),
    senttries integer NOT NULL,
    action integer
);


ALTER TABLE public.smslog OWNER TO postgres;

--
-- Name: smslog_smslogid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.smslog_smslogid_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.smslog_smslogid_seq OWNER TO postgres;

--
-- Name: smslog_smslogid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.smslog_smslogid_seq OWNED BY public.smslog.smslogid;


--
-- Name: User userid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User" ALTER COLUMN userid SET DEFAULT nextval('public."User_userid_seq"'::regclass);


--
-- Name: admin adminid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.admin ALTER COLUMN adminid SET DEFAULT nextval('public.admin_adminid_seq'::regclass);


--
-- Name: adminregion adminregionid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.adminregion ALTER COLUMN adminregionid SET DEFAULT nextval('public.adminregion_adminregionid_seq'::regclass);


--
-- Name: blockrequests blockrequestid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.blockrequests ALTER COLUMN blockrequestid SET DEFAULT nextval('public.blockrequests_blockrequestid_seq'::regclass);


--
-- Name: business businessid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.business ALTER COLUMN businessid SET DEFAULT nextval('public.business_businessid_seq'::regclass);


--
-- Name: casetag casetagid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.casetag ALTER COLUMN casetagid SET DEFAULT nextval('public.casetag_casetagid_seq'::regclass);


--
-- Name: concierge conciergeid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.concierge ALTER COLUMN conciergeid SET DEFAULT nextval('public.concierge_conciergeid_seq'::regclass);


--
-- Name: encounterform encounterformid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.encounterform ALTER COLUMN encounterformid SET DEFAULT nextval('public.encounterform_encounterformid_seq'::regclass);


--
-- Name: healthprofessionals vendorid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.healthprofessionals ALTER COLUMN vendorid SET DEFAULT nextval('public.healthprofessionals_vendorid_seq'::regclass);


--
-- Name: healthprofessionaltype healthprofessionalid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.healthprofessionaltype ALTER COLUMN healthprofessionalid SET DEFAULT nextval('public.healthprofessionaltype_healthprofessionalid_seq'::regclass);


--
-- Name: menu menuid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.menu ALTER COLUMN menuid SET DEFAULT nextval('public.menu_menuid_seq'::regclass);


--
-- Name: orderdetails id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orderdetails ALTER COLUMN id SET DEFAULT nextval('public.orderdetails_id_seq'::regclass);


--
-- Name: physician physicianid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physician ALTER COLUMN physicianid SET DEFAULT nextval('public.physician_physicianid_seq'::regclass);


--
-- Name: physicianlocation locationid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicianlocation ALTER COLUMN locationid SET DEFAULT nextval('public.physicianlocation_locationid_seq'::regclass);


--
-- Name: physiciannotification id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physiciannotification ALTER COLUMN id SET DEFAULT nextval('public.physiciannotification_id_seq'::regclass);


--
-- Name: physicianregion physicianregionid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicianregion ALTER COLUMN physicianregionid SET DEFAULT nextval('public.physicianregion_physicianregionid_seq'::regclass);


--
-- Name: region regionid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.region ALTER COLUMN regionid SET DEFAULT nextval('public.region_regionid_seq'::regclass);


--
-- Name: request requestid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.request ALTER COLUMN requestid SET DEFAULT nextval('public.request_requestid_seq'::regclass);


--
-- Name: requestbusiness requestbusinessid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestbusiness ALTER COLUMN requestbusinessid SET DEFAULT nextval('public.requestbusiness_requestbusinessid_seq'::regclass);


--
-- Name: requestclient requestclientid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestclient ALTER COLUMN requestclientid SET DEFAULT nextval('public.requestclient_requestclientid_seq'::regclass);


--
-- Name: requestclosed requestclosedid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestclosed ALTER COLUMN requestclosedid SET DEFAULT nextval('public.requestclosed_requestclosedid_seq'::regclass);


--
-- Name: requestconcierge id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestconcierge ALTER COLUMN id SET DEFAULT nextval('public.requestconcierge_id_seq'::regclass);


--
-- Name: requestnotes requestnotesid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestnotes ALTER COLUMN requestnotesid SET DEFAULT nextval('public.requestnotes_requestnotesid_seq'::regclass);


--
-- Name: requeststatuslog requeststatuslogid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requeststatuslog ALTER COLUMN requeststatuslogid SET DEFAULT nextval('public.requeststatuslog_requeststatuslogid_seq'::regclass);


--
-- Name: requesttype requesttypeid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requesttype ALTER COLUMN requesttypeid SET DEFAULT nextval('public.requesttype_requesttypeid_seq'::regclass);


--
-- Name: requestwisefile requestwisefileid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestwisefile ALTER COLUMN requestwisefileid SET DEFAULT nextval('public.requestwisefile_requestwisefileid_seq'::regclass);


--
-- Name: role roleid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role ALTER COLUMN roleid SET DEFAULT nextval('public.role_roleid_seq'::regclass);


--
-- Name: rolemenu rolemenuid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rolemenu ALTER COLUMN rolemenuid SET DEFAULT nextval('public.rolemenu_rolemenuid_seq'::regclass);


--
-- Name: shift shiftid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift ALTER COLUMN shiftid SET DEFAULT nextval('public.shift_shiftid_seq'::regclass);


--
-- Name: shiftdetail shiftdetailid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shiftdetail ALTER COLUMN shiftdetailid SET DEFAULT nextval('public.shiftdetail_shiftdetailid_seq'::regclass);


--
-- Name: shiftdetailregion shiftdetailregionid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shiftdetailregion ALTER COLUMN shiftdetailregionid SET DEFAULT nextval('public.shiftdetailregion_shiftdetailregionid_seq'::regclass);


--
-- Name: smslog smslogid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.smslog ALTER COLUMN smslogid SET DEFAULT nextval('public.smslog_smslogid_seq'::regclass);


--
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."User" (userid, aspnetuserid, firstname, lastname, email, mobile, ismobile, street, city, state, regionid, zipcode, strmonth, intyear, intdate, createdby, createddate, modifiedby, modifieddate, status, isdeleted, ip, isrequestwithemail) FROM stdin;
\.
COPY public."User" (userid, aspnetuserid, firstname, lastname, email, mobile, ismobile, street, city, state, regionid, zipcode, strmonth, intyear, intdate, createdby, createddate, modifiedby, modifieddate, status, isdeleted, ip, isrequestwithemail) FROM '$$PATH$$/5153.dat';

--
-- Data for Name: admin; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.admin (adminid, aspnetuserid, firstname, lastname, email, mobile, address1, address2, city, regionid, zip, altphone, createdby, createddate, modifiedby, modifieddate, status, isdeleted, roleid) FROM stdin;
\.
COPY public.admin (adminid, aspnetuserid, firstname, lastname, email, mobile, address1, address2, city, regionid, zip, altphone, createdby, createddate, modifiedby, modifieddate, status, isdeleted, roleid) FROM '$$PATH$$/5155.dat';

--
-- Data for Name: adminregion; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.adminregion (adminregionid, adminid, regionid) FROM stdin;
\.
COPY public.adminregion (adminregionid, adminid, regionid) FROM '$$PATH$$/5157.dat';

--
-- Data for Name: aspnetroles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.aspnetroles (id, name) FROM stdin;
\.
COPY public.aspnetroles (id, name) FROM '$$PATH$$/5159.dat';

--
-- Data for Name: aspnetuserroles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.aspnetuserroles (userid, roleid) FROM stdin;
\.
COPY public.aspnetuserroles (userid, roleid) FROM '$$PATH$$/5160.dat';

--
-- Data for Name: aspnetusers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.aspnetusers (id, username, passwordhash, email, phonenumber, "CreatedDate", ip, modifieddate) FROM stdin;
\.
COPY public.aspnetusers (id, username, passwordhash, email, phonenumber, "CreatedDate", ip, modifieddate) FROM '$$PATH$$/5161.dat';

--
-- Data for Name: blockrequests; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.blockrequests (blockrequestid, phonenumber, email, isactive, reason, requestid, ip, createddate, modifieddate) FROM stdin;
\.
COPY public.blockrequests (blockrequestid, phonenumber, email, isactive, reason, requestid, ip, createddate, modifieddate) FROM '$$PATH$$/5162.dat';

--
-- Data for Name: business; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.business (businessid, name, address1, address2, city, regionid, zipcode, phonenumber, faxnumber, isregistered, createdby, createddate, modifiedby, modifieddate, status, isdeleted, ip) FROM stdin;
\.
COPY public.business (businessid, name, address1, address2, city, regionid, zipcode, phonenumber, faxnumber, isregistered, createdby, createddate, modifiedby, modifieddate, status, isdeleted, ip) FROM '$$PATH$$/5164.dat';

--
-- Data for Name: casetag; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.casetag (casetagid, name) FROM stdin;
\.
COPY public.casetag (casetagid, name) FROM '$$PATH$$/5166.dat';

--
-- Data for Name: concierge; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.concierge (conciergeid, conciergename, address, street, city, state, zipcode, createddate, regionid, ip) FROM stdin;
\.
COPY public.concierge (conciergeid, conciergename, address, street, city, state, zipcode, createddate, regionid, ip) FROM '$$PATH$$/5168.dat';

--
-- Data for Name: emaillog; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.emaillog (emaillogid, emailtemplate, subjectname, emailid, confirmationnumber, filepath, roleid, requestid, adminid, physicianid, createdate, sentdate, isemailsent, senttries, action) FROM stdin;
\.
COPY public.emaillog (emaillogid, emailtemplate, subjectname, emailid, confirmationnumber, filepath, roleid, requestid, adminid, physicianid, createdate, sentdate, isemailsent, senttries, action) FROM '$$PATH$$/5170.dat';

--
-- Data for Name: encounterform; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.encounterform (encounterformid, requestid, historyofpresentillnessorinjury, medicalhistory, medications, allergies, temp, hr, rr, bloodpressuresystolic, bloodpressurediastolic, o2, pain, heent, cv, chest, abd, extremities, skin, neuro, other, diagnosis, treatment_plan, medicaldispensed, procedures, followup, adminid, physicianid, isfinalize, createddate, modifieddate) FROM stdin;
\.
COPY public.encounterform (encounterformid, requestid, historyofpresentillnessorinjury, medicalhistory, medications, allergies, temp, hr, rr, bloodpressuresystolic, bloodpressurediastolic, o2, pain, heent, cv, chest, abd, extremities, skin, neuro, other, diagnosis, treatment_plan, medicaldispensed, procedures, followup, adminid, physicianid, isfinalize, createddate, modifieddate) FROM '$$PATH$$/5220.dat';

--
-- Data for Name: healthprofessionals; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.healthprofessionals (vendorid, vendorname, profession, faxnumber, address, city, state, zip, regionid, createddate, modifieddate, phonenumber, isdeleted, ip, email, businesscontact) FROM stdin;
\.
COPY public.healthprofessionals (vendorid, vendorname, profession, faxnumber, address, city, state, zip, regionid, createddate, modifieddate, phonenumber, isdeleted, ip, email, businesscontact) FROM '$$PATH$$/5171.dat';

--
-- Data for Name: healthprofessionaltype; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.healthprofessionaltype (healthprofessionalid, professionname, createddate, isactive, isdeleted) FROM stdin;
\.
COPY public.healthprofessionaltype (healthprofessionalid, professionname, createddate, isactive, isdeleted) FROM '$$PATH$$/5173.dat';

--
-- Data for Name: menu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.menu (menuid, name, accounttype, sortorder) FROM stdin;
\.
COPY public.menu (menuid, name, accounttype, sortorder) FROM '$$PATH$$/5175.dat';

--
-- Data for Name: orderdetails; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.orderdetails (id, vendorid, requestid, faxnumber, email, businesscontact, prescription, noofrefill, createddate, createdby) FROM stdin;
\.
COPY public.orderdetails (id, vendorid, requestid, faxnumber, email, businesscontact, prescription, noofrefill, createddate, createdby) FROM '$$PATH$$/5177.dat';

--
-- Data for Name: physician; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.physician (physicianid, aspnetuserid, firstname, lastname, email, mobile, medicallicense, photo, adminnotes, isagreementdoc, isbackgrounddoc, istrainingdoc, isnondisclosuredoc, address1, address2, city, regionid, zip, altphone, createdby, createddate, modifiedby, modifieddate, status, businessname, businesswebsite, isdeleted, roleid, npinumber, islicensedoc, signature, iscredentialdoc, istokengenerate, syncemailaddress) FROM stdin;
\.
COPY public.physician (physicianid, aspnetuserid, firstname, lastname, email, mobile, medicallicense, photo, adminnotes, isagreementdoc, isbackgrounddoc, istrainingdoc, isnondisclosuredoc, address1, address2, city, regionid, zip, altphone, createdby, createddate, modifiedby, modifieddate, status, businessname, businesswebsite, isdeleted, roleid, npinumber, islicensedoc, signature, iscredentialdoc, istokengenerate, syncemailaddress) FROM '$$PATH$$/5179.dat';

--
-- Data for Name: physicianlocation; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.physicianlocation (locationid, physicianid, latitude, longitude, createddate, physicianname, address) FROM stdin;
\.
COPY public.physicianlocation (locationid, physicianid, latitude, longitude, createddate, physicianname, address) FROM '$$PATH$$/5181.dat';

--
-- Data for Name: physiciannotification; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.physiciannotification (id, physicianid, isnotificationstopped) FROM stdin;
\.
COPY public.physiciannotification (id, physicianid, isnotificationstopped) FROM '$$PATH$$/5183.dat';

--
-- Data for Name: physicianregion; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.physicianregion (physicianregionid, physicianid, regionid) FROM stdin;
\.
COPY public.physicianregion (physicianregionid, physicianid, regionid) FROM '$$PATH$$/5185.dat';

--
-- Data for Name: region; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.region (regionid, name, abbreviation) FROM stdin;
\.
COPY public.region (regionid, name, abbreviation) FROM '$$PATH$$/5187.dat';

--
-- Data for Name: request; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.request (requestid, requesttypeid, userid, firstname, lastname, phonenumber, email, status, physicianid, confirmationnumber, createddate, isdeleted, modifieddate, declinedby, isurgentemailsent, lastwellnessdate, ismobile, calltype, completedbyphysician, lastreservationdate, accepteddate, relationname, casenumber, ip, casetag, casetagphysician, patientaccountid, createduserid) FROM stdin;
\.
COPY public.request (requestid, requesttypeid, userid, firstname, lastname, phonenumber, email, status, physicianid, confirmationnumber, createddate, isdeleted, modifieddate, declinedby, isurgentemailsent, lastwellnessdate, ismobile, calltype, completedbyphysician, lastreservationdate, accepteddate, relationname, casenumber, ip, casetag, casetagphysician, patientaccountid, createduserid) FROM '$$PATH$$/5189.dat';

--
-- Data for Name: requestbusiness; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.requestbusiness (requestbusinessid, requestid, businessid, ip) FROM stdin;
\.
COPY public.requestbusiness (requestbusinessid, requestid, businessid, ip) FROM '$$PATH$$/5191.dat';

--
-- Data for Name: requestclient; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.requestclient (requestclientid, requestid, firstname, lastname, phonenumber, location, address, regionid, notimobile, notiemail, notes, email, strmonth, intyear, intdate, ismobile, street, city, state, zipcode, communicationtype, remindreservationcount, remindhousecallcount, issetfollowupsent, ip, isreservationremindersent, latitude, longitude) FROM stdin;
\.
COPY public.requestclient (requestclientid, requestid, firstname, lastname, phonenumber, location, address, regionid, notimobile, notiemail, notes, email, strmonth, intyear, intdate, ismobile, street, city, state, zipcode, communicationtype, remindreservationcount, remindhousecallcount, issetfollowupsent, ip, isreservationremindersent, latitude, longitude) FROM '$$PATH$$/5193.dat';

--
-- Data for Name: requestclosed; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.requestclosed (requestclosedid, requestid, requeststatuslogid, phynotes, clientnotes, ip) FROM stdin;
\.
COPY public.requestclosed (requestclosedid, requestid, requeststatuslogid, phynotes, clientnotes, ip) FROM '$$PATH$$/5195.dat';

--
-- Data for Name: requestconcierge; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.requestconcierge (id, requestid, conciergeid, ip) FROM stdin;
\.
COPY public.requestconcierge (id, requestid, conciergeid, ip) FROM '$$PATH$$/5197.dat';

--
-- Data for Name: requestnotes; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.requestnotes (requestnotesid, requestid, strmonth, intyear, intdate, physiciannotes, adminnotes, createdby, createddate, modifiedby, modifieddate, ip, administrativenotes) FROM stdin;
\.
COPY public.requestnotes (requestnotesid, requestid, strmonth, intyear, intdate, physiciannotes, adminnotes, createdby, createddate, modifiedby, modifieddate, ip, administrativenotes) FROM '$$PATH$$/5199.dat';

--
-- Data for Name: requeststatuslog; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.requeststatuslog (requeststatuslogid, requestid, status, physicianid, adminid, transtophysicianid, notes, createddate, ip, transtoadmin) FROM stdin;
\.
COPY public.requeststatuslog (requeststatuslogid, requestid, status, physicianid, adminid, transtophysicianid, notes, createddate, ip, transtoadmin) FROM '$$PATH$$/5201.dat';

--
-- Data for Name: requesttype; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.requesttype (requesttypeid, name) FROM stdin;
\.
COPY public.requesttype (requesttypeid, name) FROM '$$PATH$$/5203.dat';

--
-- Data for Name: requestwisefile; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.requestwisefile (requestwisefileid, requestid, filename, createddate, physicianid, adminid, doctype, isfrontside, iscompensation, ip, isfinalize, isdeleted, ispatientrecords) FROM stdin;
\.
COPY public.requestwisefile (requestwisefileid, requestid, filename, createddate, physicianid, adminid, doctype, isfrontside, iscompensation, ip, isfinalize, isdeleted, ispatientrecords) FROM '$$PATH$$/5205.dat';

--
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.role (roleid, name, accounttype, createdby, createddate, modifiedby, modifieddate, isdeleted, ip) FROM stdin;
\.
COPY public.role (roleid, name, accounttype, createdby, createddate, modifiedby, modifieddate, isdeleted, ip) FROM '$$PATH$$/5207.dat';

--
-- Data for Name: rolemenu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.rolemenu (rolemenuid, roleid, menuid) FROM stdin;
\.
COPY public.rolemenu (rolemenuid, roleid, menuid) FROM '$$PATH$$/5209.dat';

--
-- Data for Name: shift; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.shift (shiftid, physicianid, startdate, isrepeat, weekdays, repeatupto, createdby, createddate, ip) FROM stdin;
\.
COPY public.shift (shiftid, physicianid, startdate, isrepeat, weekdays, repeatupto, createdby, createddate, ip) FROM '$$PATH$$/5211.dat';

--
-- Data for Name: shiftdetail; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.shiftdetail (shiftdetailid, shiftid, shiftdate, regionid, starttime, endtime, status, isdeleted, modifiedby, modifieddate, lastrunningdate, eventid, issync) FROM stdin;
\.
COPY public.shiftdetail (shiftdetailid, shiftid, shiftdate, regionid, starttime, endtime, status, isdeleted, modifiedby, modifieddate, lastrunningdate, eventid, issync) FROM '$$PATH$$/5213.dat';

--
-- Data for Name: shiftdetailregion; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.shiftdetailregion (shiftdetailregionid, shiftdetailid, regionid, isdeleted) FROM stdin;
\.
COPY public.shiftdetailregion (shiftdetailregionid, shiftdetailid, regionid, isdeleted) FROM '$$PATH$$/5215.dat';

--
-- Data for Name: smslog; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.smslog (smslogid, smstemplate, mobilenumber, confirmationnumber, roleid, adminid, requestid, physicianid, createdate, sentdate, issmssent, senttries, action) FROM stdin;
\.
COPY public.smslog (smslogid, smstemplate, mobilenumber, confirmationnumber, roleid, adminid, requestid, physicianid, createdate, sentdate, issmssent, senttries, action) FROM '$$PATH$$/5217.dat';

--
-- Name: User_userid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_userid_seq"', 24, true);


--
-- Name: admin_adminid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.admin_adminid_seq', 6, true);


--
-- Name: adminregion_adminregionid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.adminregion_adminregionid_seq', 16, true);


--
-- Name: blockrequests_blockrequestid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.blockrequests_blockrequestid_seq', 5, true);


--
-- Name: business_businessid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.business_businessid_seq', 14, true);


--
-- Name: casetag_casetagid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.casetag_casetagid_seq', 7, true);


--
-- Name: concierge_conciergeid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.concierge_conciergeid_seq', 3, true);


--
-- Name: encounterform_encounterformid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.encounterform_encounterformid_seq', 1, true);


--
-- Name: healthprofessionals_vendorid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.healthprofessionals_vendorid_seq', 10, true);


--
-- Name: healthprofessionaltype_healthprofessionalid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.healthprofessionaltype_healthprofessionalid_seq', 10, true);


--
-- Name: menu_menuid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.menu_menuid_seq', 1, false);


--
-- Name: orderdetails_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.orderdetails_id_seq', 7, true);


--
-- Name: physician_physicianid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.physician_physicianid_seq', 1, false);


--
-- Name: physicianlocation_locationid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.physicianlocation_locationid_seq', 6, true);


--
-- Name: physiciannotification_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.physiciannotification_id_seq', 1, false);


--
-- Name: physicianregion_physicianregionid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.physicianregion_physicianregionid_seq', 5, true);


--
-- Name: region_regionid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.region_regionid_seq', 5, true);


--
-- Name: request_requestid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.request_requestid_seq', 85, true);


--
-- Name: requestbusiness_requestbusinessid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.requestbusiness_requestbusinessid_seq', 8, true);


--
-- Name: requestclient_requestclientid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.requestclient_requestclientid_seq', 80, true);


--
-- Name: requestclosed_requestclosedid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.requestclosed_requestclosedid_seq', 1, false);


--
-- Name: requestconcierge_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.requestconcierge_id_seq', 3, true);


--
-- Name: requestnotes_requestnotesid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.requestnotes_requestnotesid_seq', 4, true);


--
-- Name: requeststatuslog_requeststatuslogid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.requeststatuslog_requeststatuslogid_seq', 67, true);


--
-- Name: requesttype_requesttypeid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.requesttype_requesttypeid_seq', 1, false);


--
-- Name: requestwisefile_requestwisefileid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.requestwisefile_requestwisefileid_seq', 58, true);


--
-- Name: role_roleid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.role_roleid_seq', 1, false);


--
-- Name: rolemenu_rolemenuid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.rolemenu_rolemenuid_seq', 1, false);


--
-- Name: shift_shiftid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.shift_shiftid_seq', 1, false);


--
-- Name: shiftdetail_shiftdetailid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.shiftdetail_shiftdetailid_seq', 1, false);


--
-- Name: shiftdetailregion_shiftdetailregionid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.shiftdetailregion_shiftdetailregionid_seq', 1, false);


--
-- Name: smslog_smslogid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.smslog_smslogid_seq', 1, false);


--
-- Name: User User_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY (userid);


--
-- Name: admin admin_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.admin
    ADD CONSTRAINT admin_pkey PRIMARY KEY (adminid);


--
-- Name: adminregion adminregion_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.adminregion
    ADD CONSTRAINT adminregion_pkey PRIMARY KEY (adminregionid);


--
-- Name: aspnetroles aspnetroles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.aspnetroles
    ADD CONSTRAINT aspnetroles_pkey PRIMARY KEY (id);


--
-- Name: aspnetuserroles aspnetuserroles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.aspnetuserroles
    ADD CONSTRAINT aspnetuserroles_pkey PRIMARY KEY (userid);


--
-- Name: aspnetusers aspnetusers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.aspnetusers
    ADD CONSTRAINT aspnetusers_pkey PRIMARY KEY (id);


--
-- Name: blockrequests blockrequests_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.blockrequests
    ADD CONSTRAINT blockrequests_pkey PRIMARY KEY (blockrequestid);


--
-- Name: business business_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.business
    ADD CONSTRAINT business_pkey PRIMARY KEY (businessid);


--
-- Name: casetag casetag_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.casetag
    ADD CONSTRAINT casetag_pkey PRIMARY KEY (casetagid);


--
-- Name: concierge concierge_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.concierge
    ADD CONSTRAINT concierge_pkey PRIMARY KEY (conciergeid);


--
-- Name: emaillog emaillog_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.emaillog
    ADD CONSTRAINT emaillog_pkey PRIMARY KEY (emaillogid);


--
-- Name: encounterform encounterform_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.encounterform
    ADD CONSTRAINT encounterform_pkey PRIMARY KEY (encounterformid);


--
-- Name: healthprofessionals healthprofessionals_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.healthprofessionals
    ADD CONSTRAINT healthprofessionals_pkey PRIMARY KEY (vendorid);


--
-- Name: healthprofessionaltype healthprofessionaltype_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.healthprofessionaltype
    ADD CONSTRAINT healthprofessionaltype_pkey PRIMARY KEY (healthprofessionalid);


--
-- Name: menu menu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.menu
    ADD CONSTRAINT menu_pkey PRIMARY KEY (menuid);


--
-- Name: orderdetails orderdetails_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orderdetails
    ADD CONSTRAINT orderdetails_pkey PRIMARY KEY (id);


--
-- Name: physician physician_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_pkey PRIMARY KEY (physicianid);


--
-- Name: physicianlocation physicianlocation_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicianlocation
    ADD CONSTRAINT physicianlocation_pkey PRIMARY KEY (locationid);


--
-- Name: physiciannotification physiciannotification_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physiciannotification
    ADD CONSTRAINT physiciannotification_pkey PRIMARY KEY (id);


--
-- Name: physicianregion physicianregion_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicianregion
    ADD CONSTRAINT physicianregion_pkey PRIMARY KEY (physicianregionid);


--
-- Name: region region_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.region
    ADD CONSTRAINT region_pkey PRIMARY KEY (regionid);


--
-- Name: request request_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.request
    ADD CONSTRAINT request_pkey PRIMARY KEY (requestid);


--
-- Name: requestbusiness requestbusiness_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestbusiness
    ADD CONSTRAINT requestbusiness_pkey PRIMARY KEY (requestbusinessid);


--
-- Name: requestclient requestclient_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestclient
    ADD CONSTRAINT requestclient_pkey PRIMARY KEY (requestclientid);


--
-- Name: requestclosed requestclosed_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestclosed
    ADD CONSTRAINT requestclosed_pkey PRIMARY KEY (requestclosedid);


--
-- Name: requestconcierge requestconcierge_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestconcierge
    ADD CONSTRAINT requestconcierge_pkey PRIMARY KEY (id);


--
-- Name: requestnotes requestnotes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestnotes
    ADD CONSTRAINT requestnotes_pkey PRIMARY KEY (requestnotesid);


--
-- Name: requeststatuslog requeststatuslog_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_pkey PRIMARY KEY (requeststatuslogid);


--
-- Name: requesttype requesttype_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requesttype
    ADD CONSTRAINT requesttype_pkey PRIMARY KEY (requesttypeid);


--
-- Name: requestwisefile requestwisefile_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestwisefile
    ADD CONSTRAINT requestwisefile_pkey PRIMARY KEY (requestwisefileid);


--
-- Name: role role_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT role_pkey PRIMARY KEY (roleid);


--
-- Name: rolemenu rolemenu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rolemenu
    ADD CONSTRAINT rolemenu_pkey PRIMARY KEY (rolemenuid);


--
-- Name: shift shift_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift
    ADD CONSTRAINT shift_pkey PRIMARY KEY (shiftid);


--
-- Name: shiftdetail shiftdetail_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shiftdetail
    ADD CONSTRAINT shiftdetail_pkey PRIMARY KEY (shiftdetailid);


--
-- Name: shiftdetailregion shiftdetailregion_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shiftdetailregion
    ADD CONSTRAINT shiftdetailregion_pkey PRIMARY KEY (shiftdetailregionid);


--
-- Name: smslog smslog_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.smslog
    ADD CONSTRAINT smslog_pkey PRIMARY KEY (smslogid);


--
-- Name: User User_aspnetuserid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_aspnetuserid_fkey" FOREIGN KEY (aspnetuserid) REFERENCES public.aspnetusers(id);


--
-- Name: adminregion adminregion_adminid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.adminregion
    ADD CONSTRAINT adminregion_adminid_fkey FOREIGN KEY (adminid) REFERENCES public.admin(adminid);


--
-- Name: adminregion adminregion_regionid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.adminregion
    ADD CONSTRAINT adminregion_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);


--
-- Name: aspnetuserroles aspnetuserroles_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.aspnetuserroles
    ADD CONSTRAINT aspnetuserroles_userid_fkey FOREIGN KEY (userid) REFERENCES public.aspnetusers(id);


--
-- Name: business business_createdby_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.business
    ADD CONSTRAINT business_createdby_fkey FOREIGN KEY (createdby) REFERENCES public.aspnetusers(id);


--
-- Name: business business_modifiedby_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.business
    ADD CONSTRAINT business_modifiedby_fkey FOREIGN KEY (modifiedby) REFERENCES public.aspnetusers(id);


--
-- Name: concierge concierge_regionid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.concierge
    ADD CONSTRAINT concierge_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);


--
-- Name: encounterform encounterform_adminid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.encounterform
    ADD CONSTRAINT encounterform_adminid_fkey FOREIGN KEY (adminid) REFERENCES public.admin(adminid);


--
-- Name: encounterform encounterform_physicianid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.encounterform
    ADD CONSTRAINT encounterform_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);


--
-- Name: encounterform encounterform_requestid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.encounterform
    ADD CONSTRAINT encounterform_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);


--
-- Name: healthprofessionals healthprofessionals_profession_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.healthprofessionals
    ADD CONSTRAINT healthprofessionals_profession_fkey FOREIGN KEY (profession) REFERENCES public.healthprofessionaltype(healthprofessionalid);


--
-- Name: physician physician_aspnetuserid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_aspnetuserid_fkey FOREIGN KEY (aspnetuserid) REFERENCES public.aspnetusers(id);


--
-- Name: physician physician_createdby_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_createdby_fkey FOREIGN KEY (createdby) REFERENCES public.aspnetusers(id);


--
-- Name: physician physician_modifiedby_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_modifiedby_fkey FOREIGN KEY (modifiedby) REFERENCES public.aspnetusers(id);


--
-- Name: physician physician_regionid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physician
    ADD CONSTRAINT physician_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);


--
-- Name: physicianlocation physicianlocation_physicianid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicianlocation
    ADD CONSTRAINT physicianlocation_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);


--
-- Name: physiciannotification physiciannotification_physicianid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physiciannotification
    ADD CONSTRAINT physiciannotification_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);


--
-- Name: physicianregion physicianregion_physicianid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicianregion
    ADD CONSTRAINT physicianregion_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);


--
-- Name: physicianregion physicianregion_regionid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.physicianregion
    ADD CONSTRAINT physicianregion_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);


--
-- Name: request request_physicianid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.request
    ADD CONSTRAINT request_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);


--
-- Name: request request_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.request
    ADD CONSTRAINT request_userid_fkey FOREIGN KEY (userid) REFERENCES public."User"(userid);


--
-- Name: requestbusiness requestbusiness_businessid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestbusiness
    ADD CONSTRAINT requestbusiness_businessid_fkey FOREIGN KEY (businessid) REFERENCES public.business(businessid);


--
-- Name: requestbusiness requestbusiness_requestid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestbusiness
    ADD CONSTRAINT requestbusiness_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);


--
-- Name: requestclient requestclient_regionid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestclient
    ADD CONSTRAINT requestclient_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);


--
-- Name: requestclient requestclient_requestid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestclient
    ADD CONSTRAINT requestclient_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);


--
-- Name: requestclosed requestclosed_requestid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestclosed
    ADD CONSTRAINT requestclosed_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);


--
-- Name: requestclosed requestclosed_requeststatuslogid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestclosed
    ADD CONSTRAINT requestclosed_requeststatuslogid_fkey FOREIGN KEY (requeststatuslogid) REFERENCES public.requeststatuslog(requeststatuslogid);


--
-- Name: requestconcierge requestconcierge_conciergeid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestconcierge
    ADD CONSTRAINT requestconcierge_conciergeid_fkey FOREIGN KEY (conciergeid) REFERENCES public.concierge(conciergeid);


--
-- Name: requestconcierge requestconcierge_requestid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestconcierge
    ADD CONSTRAINT requestconcierge_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);


--
-- Name: requestnotes requestnotes_requestid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestnotes
    ADD CONSTRAINT requestnotes_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);


--
-- Name: requeststatuslog requeststatuslog_adminid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_adminid_fkey FOREIGN KEY (adminid) REFERENCES public.admin(adminid);


--
-- Name: requeststatuslog requeststatuslog_physicianid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);


--
-- Name: requeststatuslog requeststatuslog_requestid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);


--
-- Name: requeststatuslog requeststatuslog_transtophysicianid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requeststatuslog
    ADD CONSTRAINT requeststatuslog_transtophysicianid_fkey FOREIGN KEY (transtophysicianid) REFERENCES public.physician(physicianid);


--
-- Name: requestwisefile requestwisefile_adminid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestwisefile
    ADD CONSTRAINT requestwisefile_adminid_fkey FOREIGN KEY (adminid) REFERENCES public.admin(adminid);


--
-- Name: requestwisefile requestwisefile_physicianid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestwisefile
    ADD CONSTRAINT requestwisefile_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);


--
-- Name: requestwisefile requestwisefile_requestid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.requestwisefile
    ADD CONSTRAINT requestwisefile_requestid_fkey FOREIGN KEY (requestid) REFERENCES public.request(requestid);


--
-- Name: rolemenu rolemenu_menuid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rolemenu
    ADD CONSTRAINT rolemenu_menuid_fkey FOREIGN KEY (menuid) REFERENCES public.menu(menuid);


--
-- Name: rolemenu rolemenu_roleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rolemenu
    ADD CONSTRAINT rolemenu_roleid_fkey FOREIGN KEY (roleid) REFERENCES public.role(roleid);


--
-- Name: shift shift_createdby_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift
    ADD CONSTRAINT shift_createdby_fkey FOREIGN KEY (createdby) REFERENCES public.aspnetusers(id);


--
-- Name: shift shift_physicianid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shift
    ADD CONSTRAINT shift_physicianid_fkey FOREIGN KEY (physicianid) REFERENCES public.physician(physicianid);


--
-- Name: shiftdetail shiftdetail_modifiedby_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shiftdetail
    ADD CONSTRAINT shiftdetail_modifiedby_fkey FOREIGN KEY (modifiedby) REFERENCES public.aspnetusers(id);


--
-- Name: shiftdetail shiftdetail_shiftid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shiftdetail
    ADD CONSTRAINT shiftdetail_shiftid_fkey FOREIGN KEY (shiftid) REFERENCES public.shift(shiftid);


--
-- Name: shiftdetailregion shiftdetailregion_regionid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shiftdetailregion
    ADD CONSTRAINT shiftdetailregion_regionid_fkey FOREIGN KEY (regionid) REFERENCES public.region(regionid);


--
-- Name: shiftdetailregion shiftdetailregion_shiftdetailid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shiftdetailregion
    ADD CONSTRAINT shiftdetailregion_shiftdetailid_fkey FOREIGN KEY (shiftdetailid) REFERENCES public.shiftdetail(shiftdetailid);


--
-- PostgreSQL database dump complete
--

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      