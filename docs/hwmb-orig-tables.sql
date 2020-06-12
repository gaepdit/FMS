create schema hwmb
go

create table allsites
(
	ID varchar(255) default N'' not null
		constraint PK_allsites_ID
			primary key,
	NAME varchar(255) default NULL,
	STREET varchar(255) default NULL,
	CITY varchar(255) default NULL,
	COUNTY varchar(255) default NULL,
	STATE varchar(255) default NULL,
	ZIP varchar(255) default NULL,
	EXTRACT_FLAG varchar(255) default NULL,
	ACTIVE_SITE varchar(255) default NULL,
	IN_A_UNIVERSE varchar(255) default NULL,
	LAT float default NULL,
	LNG float default NULL
)
go

create index Name
	on allsites (NAME)
go

create table appendix1
(
	cas bigint default NULL,
	source varchar(4) default NULL,
	substance varchar(300) default NULL,
	nc varchar(50) default NULL
)
go

create table benefits
(
	budgetcode varchar(10) default N'' not null,
	payperiod datetime2(0) default getdate() not null,
	employee_id varchar(10) default N'' not null,
	hours float default NULL,
	cost float default NULL,
	constraint PK_benefits_budgetcode
		primary key (budgetcode, payperiod, employee_id)
)
go

create table cdo
(
	id varchar(11) default N'' not null,
	lname varchar(20) default NULL,
	fname varchar(20) default NULL,
	startDate date default NULL,
	termDate date default NULL,
	status varchar(10) default N'0'
)
go

create table cdo_new
(
	id varchar(11) default N'' not null
		constraint PK_cdo_new_id
			primary key,
	lname varchar(20) default NULL,
	fname varchar(20) default NULL,
	startDate date default NULL,
	termDate date default NULL,
	status varchar(10) default N'0'
)
go

create index lname
	on cdo_new (lname)
go

create table cdocalendar
(
	calDate date default NULL,
	lastAssigned varchar(10) default NULL,
	createdBy varchar(10) default NULL
)
go

create table cdoroster
(
	id varchar(11) default N'' not null,
	date date default getdate() not null
)
go

create table cdoswaps
(
	reqDate date default getdate() not null,
	cdoID varchar(11) default N'' not null,
	cdoDate date default NULL,
	acceptID varchar(11) default NULL,
	acceptDate date default NULL,
	swapDate date default NULL,
	comment varchar(150) default NULL,
	status bigint default NULL,
	swapID int identity(1534, 1)
		constraint PK_cdoswaps_swapID
			primary key
)
go

create table chemicals
(
	ID int not null
		constraint PK_chemicals_ID
			primary key,
	SUBSTANCE varchar(255) not null,
	CODE varchar(50) default NULL,
	FAMILY varchar(50) default NULL,
	RFD_O float default NULL,
	RFD_I float default NULL,
	SF_O float default NULL,
	SF_I float default NULL,
	DI float default NULL,
	KOC float default NULL,
	H float default NULL,
	REF_RFD_O varchar(30) default NULL,
	DATE_RFD_O varchar(50) default NULL,
	REF_RFD_I varchar(30) default NULL,
	DATE_RFD_I varchar(50) default NULL,
	REF_SF_O varchar(30) default NULL,
	DATE_SF_O varchar(50) default NULL,
	REF_SF_I varchar(30) default NULL,
	DATE_SF_I varchar(50) default NULL,
	REF_DI varchar(30) default NULL,
	DATE_DI varchar(50) default NULL,
	REF_KOC varchar(30) default NULL,
	DATE_KOC varchar(50) default NULL,
	REF_H varchar(30) default NULL,
	DATE_H varchar(50) default NULL,
	CCLASS varchar(2) default NULL,
	NC float default NULL,
	GWATER float default NULL,
	TYPE1 float default NULL,
	SPECIAL varchar(5) default NULL,
	MODIFYDATE date default '1970-01-01' not null,
	MODIFYBY varchar(40) not null
)
go

create table coatype
(
	idcoatype int identity(5, 1)
		constraint PK_coatype_idcoatype
			primary key,
	coatype varchar(45) default NULL,
	code varchar(20) default NULL,
	description varchar(100) default NULL
)
go

create table coa
(
	idcoa bigint identity(31, 1)
		constraint PK_coa_idcoa
			primary key,
	coatype_idcoatype int not null
		constraint coa$fk_coa_coatype1
			references coatype,
	code varchar(45) not null,
	begin_balance decimal(20,2) default 0.00 not null,
	begin_bal_date date default NULL,
	checkNum varchar(50) default N'NA',
	isheader int default NULL,
	idparent int default NULL,
	link varchar(45) default NULL
)
go

create index fk_coa_coatype1
	on coa (coatype_idcoatype)
go

create table constraints
(
	empID varchar(10) default NULL,
	code varchar(12) default NULL,
	target smallint default NULL,
	limit smallint default NULL
)
go

create table county
(
	id int not null,
	code varchar(2) not null,
	name varchar(100) default N'' not null
)
go

create table docket
(
	docketID bigint default 0 not null,
	facilityID varchar(100) default N'' not null,
	date date default NULL,
	item varchar(50) default NULL,
	description varchar(255) default NULL,
	physLocation bigint default 0,
	linkLocation varchar(300) default N'none',
	category bigint default NULL,
	status bigint default 0,
	enteredBy varchar(10) not null,
	enteredDate datetime2(0) not null,
	locOther varchar(150) default NULL,
	keywords varchar(max),
	constraint PK_docket_docketID
		primary key (docketID, facilityID)
)
go

create table docketcategory
(
	id bigint default 0 not null,
	type varchar(50) default N'' not null,
	descrip varchar(100) default NULL,
	constraint PK_docketcategory_id
		primary key (id, type)
)
go

create table docketitems
(
	code varchar(255) default NULL,
	description varchar(255) default NULL,
	type varchar(20) default NULL
)
go

create table docketlocation
(
	id bigint default 0 not null
		constraint PK_docketlocation_id
			primary key,
	description varchar(100) default NULL
)
go

create table docketstatus
(
	id int default 0 not null
		constraint PK_docketstatus_id
			primary key,
	description varchar(100) default NULL
)
go

create table facilitycontacts
(
	facilityID varchar(50) default N'' not null,
	cFname varchar(50) default NULL,
	cLname varchar(50) default NULL,
	cTitle varchar(100) default NULL,
	cAddress1 varchar(150) default NULL,
	cAddress2 varchar(50) default NULL,
	cCity varchar(50) default NULL,
	cState varchar(2) default NULL,
	cEmail varchar(150) default NULL,
	cPhone varchar(50) default NULL,
	isPrimary tinyint default 0
)
go

create table facilityfavs
(
	owner varchar(10) default NULL,
	id varchar(50) default NULL,
	name varchar(200) default NULL
)
go

create table facilityfile
(
	type varchar(20) default NULL,
	program varchar(20) default NULL,
	ID varchar(255) not null,
	co varchar(8) default NULL,
	assignment int default 0 not null,
	name varchar(255) default NULL,
	ADDRESS varchar(255) default NULL,
	CITY varchar(255) default NULL,
	zipcode int default NULL,
	COUNTY varchar(255) default NULL,
	LAT float default NULL,
	lng float default NULL,
	acreage float default NULL,
	REGSTATUS varchar(255) default NULL,
	budget varchar(50) default NULL,
	complaint smallint default NULL,
	riverbasin int default NULL,
	createBy varchar(8) default NULL,
	createDate datetime2(0) default NULL,
	modBy varchar(8) default NULL,
	modDate datetime2(0) default NULL,
	FILE_LBL varchar(12) default NULL,
	COUNTYCODE int default NULL,
	retained smallint default 0 not null
)
go

create index ID
	on facilityfile (ID)
go

create table facilityfile_ref_id
(
	pid int identity
		constraint PK_facilityfile_ref_id_pid
			primary key,
	facility_id varchar(50) default N'0' not null,
	file_id varchar(10) default N'0' not null,
	ref_id varchar(10) default N'0' not null,
	comment varchar(255) default N'0' not null
)
go

create table facilitytithistory
(
	facilityID varchar(50) default NULL,
	name varchar(250) default NULL,
	lastDate datetime2(0) default NULL,
	enteredBy varchar(12) default NULL,
	enteredDate datetime2(0) default NULL
)
go

create table facilitytype
(
	id int identity(11, 1)
		constraint PK_facilitytype_id
			primary key,
	type varchar(20) not null
)
go

create table ff_corr_action
(
	name varchar(200) default NULL,
	id varchar(100) default NULL,
	address varchar(200) default NULL,
	city varchar(50) default NULL,
	zipcode varchar(50) default NULL,
	county varchar(50) default NULL,
	file_lbl varchar(10) default NULL,
	CO varchar(8) default NULL,
	latitude float default NULL,
	longitude float default NULL,
	retained smallint default NULL
)
go

create table filecabinet
(
	cabinetID int not null,
	cabinet varchar(4) not null,
	startCounty int not null,
	endCounty int not null,
	startSeq bigint default NULL,
	endSeq bigint default NULL
)
go

create table fin_ass
(
	ID varchar(20) not null
		constraint PK_fin_ass_ID
			primary key,
	fiscalDate date default NULL,
	ClosureDate date default NULL,
	PClosureDate date default NULL,
	insSPer_Due date default NULL,
	insSPer_Recd date default NULL,
	insSPer_Amt float default 0,
	insSPer_Type varchar(20) default NULL,
	insSPer_Issuer varchar(40) default NULL,
	insSAgg_Due date default NULL,
	insSAgg_Recd date default NULL,
	insSAgg_Amt float default 0,
	insSAgg_Type varchar(20) default NULL,
	insSAgg_Issuer varchar(40) default NULL,
	insNSPer_Due date default NULL,
	insNSPer_Recd date default NULL,
	insNSPer_Amt float default 0,
	insNSPer_Type varchar(20) default NULL,
	insNSPer_Issuer varchar(40) default NULL,
	insNSAgg_Due date default NULL,
	insNSAgg_Recd date default NULL,
	insNSAgg_Amt float default 0,
	insNSAgg_Type varchar(20) default NULL,
	insNSAgg_Issuer varchar(40) default NULL,
	closure_Due date default NULL,
	closure_Recd date default NULL,
	closure_Amt float default 0,
	closure_Type varchar(20) default NULL,
	closure_Issuer varchar(40) default NULL,
	CACost_Due date default NULL,
	CACost_Recd date default NULL,
	CACost_Amt float default 0,
	CACost_Type varchar(20) default NULL,
	CACost_Issuer varchar(40) default NULL,
	enteredBy varchar(10) not null,
	enteredDate datetime2(0) not null
)
go

create table fininstruments
(
	code varchar(15) not null
		constraint PK_fininstruments_code
			primary key,
	description varchar(45) not null,
	category varchar(1) default NULL,
	fullName varchar(100) default NULL
)
go

create table gen_cnty_assign
(
	county varchar(50) default NULL,
	fname varchar(50) default NULL,
	lname varchar(50) default NULL,
	empID varchar(10) default NULL
)
go

create table geocal
(
	geoWeek date default getdate() not null
		constraint PK_geocal_geoWeek
			primary key,
	id datetime default getdate() not null
)
go

create table georecord
(
	geoWeek date default NULL,
	employeeID varchar(10) default N'0',
	entryDate datetime default getdate() not null
		constraint PK_georecord_entryDate
			primary key,
	status smallint default 0,
	bcode varchar(20) default N'0',
	city varchar(20) default N'0',
	county varchar(20) default N'0',
	description varchar(200) default N'0',
	proxyID varchar(10) default N'0',
	supportTruck tinyint default 0,
	projectName varchar(40) default N'0'
)
go

create table hsi_site_info
(
	hsi_id varchar(10) default NULL,
	hsi_co varchar(15) default NULL,
	epa_id varchar(15) default NULL,
	status varchar(10) default NULL,
	site_name varchar(255) default NULL,
	site_addr varchar(150) default NULL,
	site_city varchar(30) default NULL,
	site_cnty varchar(30) default NULL,
	site_zip varchar(10) default NULL,
	rcra_co varchar(255) default NULL,
	acres float default NULL,
	filedBy varchar(15) default NULL,
	filedDate datetime2(0) default NULL,
	fundcode char(2) default NULL,
	class smallint default NULL,
	reclass smallint default 0 not null
)
go

create table journal
(
	journal_ID varchar(50) default N'' not null
		constraint PK_journal_journal_ID
			primary key,
	empID varchar(10) default NULL,
	handlerID varchar(50) default NULL,
	budgetcode varchar(50) default NULL,
	jdate datetime2(0) default NULL,
	entrydate datetime2(0) default NULL,
	docket_ID varchar(50) default NULL,
	category varchar(50) default NULL,
	hours float default NULL,
	description varchar(200) default NULL,
	cost float default NULL,
	status char default NULL,
	approvedBy varchar(10) default NULL,
	approvedDate datetime2(0) default NULL,
	telecommute smallint default 0 not null
)
go

create index budgetcode_index
	on journal (budgetcode)
go

create index id_by_date
	on journal (empID, jdate)
go

create index main
	on journal (empID, budgetcode)
go

create table leave_element
(
	type varchar(50) default NULL
)
go

create table leaverequests
(
	leaveID varchar(40) default N'' not null
		constraint PK_leaverequests_leaveID
			primary key,
	empID varchar(8) default N'' not null,
	type varchar(20) default NULL,
	totalHours float default NULL,
	submitDate datetime2(0) default NULL,
	startDate datetime2(0) default NULL,
	endDate datetime2(0) default NULL,
	status varchar(30) default NULL,
	startTime float default NULL,
	endTime float default NULL,
	hoursPerDay float default NULL,
	hoursFirstDay float default NULL,
	hoursLastDay float default NULL,
	approvedBy varchar(max),
	approvalDate datetime2(0) default NULL,
	reason varchar(200) default NULL,
	documentation varchar(100) default NULL,
	docRequired smallint default 0 not null,
	supervisor varchar(20) default NULL,
	comment varchar(100) default NULL
)
go

create index super_status
	on leaverequests (supervisor, status)
go

create table mailpreferences
(
	employee_id varchar(10) default NULL,
	pref_email varchar(100) default NULL,
	notify_time smallint default 0 not null,
	notify_leave smallint default 0 not null,
	notify_month smallint default 0 not null
)
go

create table nc_personnel
(
	lastname varchar(50) default NULL,
	firstname varchar(50) default NULL,
	employee_ID varchar(10) default N'' not null
		constraint PK_nc_personnel_employee_ID
			primary key,
	phone varchar(50) default NULL,
	password varchar(16) default NULL,
	rights varchar(10) default NULL,
	workplace smallint default NULL,
	supervisor_cat int default NULL,
	title varchar(50) default NULL,
	unit varchar(50) default NULL,
	program varchar(50) default NULL,
	supervisor varchar(50) default NULL,
	paygroup varchar(10) default NULL,
	email varchar(50) default NULL,
	ann_salary float default NULL,
	compOff smallint default 0 not null,
	workPref varchar(10) default NULL,
	flexDay varchar(4) default NULL,
	orggroup smallint default NULL,
	hireDate date default NULL
)
go

create table new_site_activity
(
	active_site varchar(13) default NULL
)
go

create table orgtable
(
	id int default NULL,
	name varchar(50) default NULL,
	manager varchar(10) default NULL,
	assignComplaint smallint default 0 not null,
	assignPASI smallint default 0 not null,
	assignNPL smallint default 0 not null,
	assignHSI smallint default 0 not null
)
go

create table pasi
(
	ID varchar(10) default N'' not null
		constraint PK_pasi_ID
			primary key,
	active smallint default 0 not null,
	co varchar(255) default NULL,
	site_name varchar(255) default NULL,
	qFUD smallint default 0 not null,
	refID varchar(255) default NULL,
	City varchar(255) default NULL,
	County varchar(255) default NULL,
	Type varchar(255) default NULL,
	SAP_due_epa datetime2(0) default NULL,
	lab_req_due datetime2(0) default NULL,
	lab_week datetime2(0) default NULL,
	rep_due_date datetime2(0) default NULL,
	epa_sam varchar(255) default NULL,
	createDate datetime2(0) default NULL,
	createBy varchar(8) default NULL
)
go

create table pasi_cost
(
	siteID varchar(40) default N'' not null,
	payperiod datetime2(0) default getdate() not null,
	employee_ID varchar(10) default N'' not null,
	hours float default NULL,
	cost float default NULL,
	constraint PK_pasi_cost_siteID
		primary key (siteID, payperiod, employee_ID)
)
go

create table pasi_old
(
	ID varchar(10) default NULL,
	active smallint default 0 not null,
	co varchar(255) default NULL,
	site_name varchar(255) default NULL,
	qFUD smallint default 0 not null,
	refID varchar(255) default NULL,
	City varchar(255) default NULL,
	County varchar(255) default NULL,
	Type varchar(255) default NULL,
	SAP_due_epa datetime2(0) default NULL,
	lab_req_due datetime2(0) default NULL,
	lab_week datetime2(0) default NULL,
	rep_due_date datetime2(0) default NULL,
	epa_sam varchar(255) default NULL,
	createDate datetime2(0) default NULL,
	createBy varchar(8) default NULL
)
go

create table pasi_type
(
	type varchar(5) default NULL,
	max_hours smallint default NULL
)
go

create table pending_invoices
(
	invoice_num int identity(1199, 1)
		constraint PK_pending_invoices_invoice_num
			primary key,
	site_id varchar(50) default N'0' not null,
	cuttoff date default NULL
)
go

create table personnel
(
	lastname varchar(50) default NULL,
	firstname varchar(50) default NULL,
	employee_ID varchar(8) default N'' not null
		constraint PK_personnel_employee_ID
			primary key,
	phone varchar(50) default NULL,
	password varchar(16) default NULL,
	rights varchar(10) default NULL,
	workplace smallint default NULL,
	supervisor_cat int default NULL,
	title varchar(50) default NULL,
	unit varchar(50) default NULL,
	program varchar(50) default NULL,
	supervisor varchar(50) default NULL,
	paygroup varchar(10) default NULL,
	email varchar(50) default NULL,
	ann_salary float default NULL,
	compOff smallint default 0 not null,
	workPref varchar(10) default NULL,
	flexDay varchar(4) default NULL,
	orggroup smallint default NULL,
	hireDate date default NULL
)
go

create table program_element
(
	name varchar(50) default NULL,
	code varchar(10) default N'' not null
		constraint PK_program_element_code
			primary key,
	type char default NULL,
	parent varchar(10) default NULL,
	orgNo varchar(50) default NULL,
	projNo varchar(50) default NULL,
	originCode smallint default NULL,
	progCode varchar(50) default NULL,
	alt_ID varchar(50) default NULL,
	access varchar(50) default NULL,
	constrainable smallint default 0 not null,
	paygroup smallint default 0 not null,
	journalReq smallint default 0 not null,
	branch varchar(4) default NULL,
	intBud smallint default 0
)
go

create index name
	on program_element (name)
go

create table program_time
(
	Date int default NULL,
	Employee_ID varchar(50) default NULL,
	FirstName varchar(50) default NULL,
	LastName varchar(50) default NULL,
	HAZ_WASTE float default NULL,
	CERCLA_CORE float default NULL,
	CERCLA_VCP float default NULL,
	PA_SI float default NULL,
	TARGET_PA float default NULL,
	STATE_SUPERFUND float default NULL,
	CEDARTOWN float default NULL,
	DIAMOND_SHAMROCK float default NULL,
	FIRESTONE float default NULL,
	HERCULES_009 float default NULL,
	MARZONE float default NULL,
	MATHIS_BROTHERS float default NULL,
	TH_AGRI float default NULL,
	WOOLFOLK float default NULL,
	LCP float default NULL,
	TERRY_CREEK float default NULL,
	TIFTON_INIT float default NULL,
	ESCAMBIA_BRUN float default NULL,
	ESCAMBIA_CAM float default NULL,
	HW_STATE_MATCH float default NULL,
	submitted smallint default 0 not null,
	APPROVED smallint default 0 not null,
	ANNRATE float default NULL
)
go

create table program_time1
(
	Date int default NULL,
	Employee_ID varchar(50) default NULL,
	FirstName varchar(50) default NULL,
	LastName varchar(50) default NULL,
	HAZ_WASTE float default NULL,
	CERCLA_CORE float default NULL,
	CERCLA_VCP float default NULL,
	PA_SI float default NULL,
	TARGET_PA float default NULL,
	STATE_SUPERFUND float default NULL,
	CEDARTOWN float default NULL,
	DIAMOND_SHAMROCK float default NULL,
	FIRESTONE float default NULL,
	HERCULES_009 float default NULL,
	MARZONE float default NULL,
	MATHIS_BROTHERS float default NULL,
	TH_AGRI float default NULL,
	WOOLFOLK float default NULL,
	LCP float default NULL,
	TERRY_CREEK float default NULL,
	TIFTON_INIT float default NULL,
	ESCAMBIA_BRUN float default NULL,
	ESCAMBIA_CAM float default NULL,
	HW_STATE_MATCH float default NULL,
	submitted smallint default 0 not null,
	APPROVED smallint default 0 not null,
	ANNRATE float default NULL
)
go

create table programaccess
(
	empID varchar(10) not null,
	bcode varchar(10) not null
)
go

create table proxies
(
	owner_ID varchar(10) default NULL,
	proxy_ID varchar(10) default NULL,
	proxy_date datetime2(0) default NULL
)
go

create table prp_orig
(
	hsi_id float default NULL,
	prp_id float default NULL,
	prp_area varchar(255) default NULL,
	prp_name varchar(255) default NULL,
	prp_company varchar(255) default NULL,
	prp_address varchar(255) default NULL,
	prp_city varchar(255) default NULL,
	prp_state varchar(255) default NULL,
	prp_zip varchar(255) default NULL,
	prp_phone varchar(255) default NULL
)
go

create table rcra_handler
(
	Handler_ID varchar(50) default N'' not null,
	RCRA_CO varchar(8) default NULL,
	Name varchar(255) default N'' not null,
	Address varchar(100) default NULL,
	City varchar(50) default NULL,
	Zipcode varchar(10) default NULL,
	County varchar(40) default NULL,
	JTrack smallint default 0 not null,
	class char default NULL,
	defaultBudget varchar(50) default N'' not null,
	constraint PK_rcra_handler_Handler_ID
		primary key (Handler_ID, defaultBudget)
)
go

create table rcra_handler2
(
	Handler_ID varchar(50) default N'' not null,
	RCRA_CO varchar(8) default NULL,
	Name varchar(255) default N'' not null,
	Address varchar(100) default NULL,
	City varchar(50) default NULL,
	Zipcode varchar(10) default NULL,
	County varchar(40) default NULL,
	JTrack smallint default 0 not null,
	class char default NULL,
	defaultBudget varchar(50) default N'' not null,
	constraint PK_rcra_handler2_Handler_ID
		primary key (Handler_ID, defaultBudget)
)
go

create table rcra_handler_npl
(
	Handler_ID varchar(50) default N'' not null,
	RCRA_CO varchar(8) default NULL,
	Name varchar(255) default N'' not null,
	Address varchar(100) default NULL,
	City varchar(50) default NULL,
	Zipcode varchar(10) default NULL,
	County varchar(40) default NULL,
	JTrack smallint default 0 not null,
	class char default NULL,
	defaultBudget varchar(50) default N'' not null,
	constraint PK_rcra_handler_npl_Handler_ID
		primary key (Handler_ID, defaultBudget)
)
go

create table rcra_handlers
(
	id varchar(50) default NULL
)
go

create table records_center
(
	Cabinet varchar(10) default NULL,
	File_ID varchar(20) default NULL,
	ID varchar(50) default NULL,
	Name varchar(150) default NULL,
	Start_yr int default NULL,
	End_yr int default NULL,
	Box_Num varchar(50) default NULL,
	CA_RCRA_Retained smallint default NULL,
	Permit_Retained smallint default NULL
)
go

create table [records_center-april_20_2018]
(
	Cabinet varchar(10) default NULL,
	File_ID varchar(20) default NULL,
	ID varchar(50) default NULL,
	Name varchar(150) default NULL,
	Start_yr int default NULL,
	End_yr int default NULL,
	Box_Num varchar(50) default NULL,
	CA_RCRA_Retained smallint default NULL,
	Permit_Retained smallint default NULL
)
go

create table [records_center-april_28_2018]
(
	Cabinet varchar(10) default NULL,
	File_ID varchar(20) default NULL,
	ID varchar(50) default NULL,
	Name varchar(150) default NULL,
	Start_yr int default NULL,
	End_yr int default NULL,
	Box_Num varchar(50) default NULL,
	CA_RCRA_Retained smallint default NULL,
	Permit_Retained smallint default NULL
)
go

create table riverbasin
(
	id bigint identity(17, 1)
		constraint PK_riverbasin_id
			primary key,
	name varchar(25) not null
)
go

create table [rollback]
(
	postedBy varchar(10) default NULL,
	postedDate datetime2(0) default NULL,
	employeeID varchar(10) default NULL,
	payperiod datetime2(0) default NULL,
	reason varchar(100) default NULL
)
go

create table scrap_tire_abatement_type
(
	id int identity(5, 1)
		constraint PK_scrap_tire_abatement_type_id
			primary key,
	atype varchar(50) default NULL
)
go

create table scrap_tire_loc
(
	pid int not null
		constraint PK_scrap_tire_loc_pid
			primary key,
	project_id varchar(255) default NULL,
	site_id float default NULL,
	county varchar(255) default NULL,
	est_tire_count int default 0 not null,
	inspection_date varchar(255) default NULL,
	photo_entrance varchar(255) default N'N/A' not null,
	photo_pile varchar(255) default N'N/A' not null,
	lat float default NULL,
	lng float default NULL,
	[cleaned_ up] binary(1) default NULL,
	atype int default 1 not null,
	lg_storage binary(1) default NULL
)
go

create table site_contacts
(
	ID varchar(255) not null,
	Company_Name varchar(255) not null,
	FName varchar(100) not null,
	LName varchar(100) not null,
	Title varchar(100) default NULL,
	Address varchar(255) not null,
	City varchar(255) not null,
	State varchar(3) not null,
	Country varchar(3) not null,
	Zip varchar(11) not null,
	Phone varchar(255) default N'NA',
	Fax varchar(255) default N'NA',
	Email varchar(255) default N'NA',
	isPrimary smallint default 0 not null
)
go

create table smiles
(
	substance varchar(300) default NULL,
	smiles varchar(200) default NULL,
	family varchar(100) default NULL
)
go

create table time_records
(
	employee_id varchar(10) default N'' not null,
	date datetime2(0) default getdate() not null,
	item varchar(10) default N'' not null,
	hours float default NULL,
	type char(2) default NULL,
	isCommute smallint default 0 not null,
	approved smallint default 0 not null,
	approvedBy varchar(10) default NULL,
	leaveStatus char default NULL,
	leaveReq varchar(50) default N'' not null,
	cost float default NULL,
	postDate datetime2(0) default NULL,
	constraint PK_time_records_employee_id
		primary key (employee_id, date, item, leaveReq)
)
go

create table time_tot
(
	employee_id varchar(10) default NULL,
	budget_code varchar(50) default NULL,
	actual_hours float default NULL,
	target_percent smallint default NULL,
	constrained smallint default 0 not null
)
go

create table timesheets
(
	empID varchar(12) default N'' not null,
	date datetime2(0) default getdate() not null,
	period smallint default NULL,
	year smallint default NULL,
	status varchar(50) default NULL,
	supervisor varchar(50) default NULL,
	readOnly smallint default 0 not null,
	comptime float default NULL,
	approveDate datetime2(0) default NULL,
	submitDate datetime2(0) default NULL,
	approveBy varchar(10) default NULL,
	submitBy varchar(10) default NULL,
	constraint PK_timesheets_empID
		primary key (empID, date)
)
go

create table titles
(
	title varchar(50) default N'' not null
)
go

create index title
	on titles (title)
go

create table tmpvrp
(
	ID varchar(100) not null
)
go

create table units
(
	id int identity(100, 1)
		constraint PK_units_id
			primary key,
	uName varchar(50) default NULL,
	active smallint default 0
)
go

create table vehactions
(
	id bigint default 0 not null
		constraint PK_vehactions_id
			primary key,
	item varchar(100) default N'0' not null
)
go

create table vehicle_stats
(
	ID varchar(20) default N'' not null
		constraint PK_vehicle_stats_ID
			primary key,
	status smallint default NULL,
	curr_mileage int default NULL,
	service_date date default NULL,
	last_serv_mile int default NULL,
	next_ser_mileage int default NULL,
	exam_date date default NULL
)
go

create table vehicles
(
	ID varchar(20) default N'' not null
		constraint PK_vehicles_ID
			primary key,
	year int default NULL,
	make varchar(20) default NULL,
	model varchar(20) default NULL,
	body_type varchar(20) default NULL,
	vin varchar(20) default NULL,
	parking_card varchar(12) default NULL,
	gas_card varchar(12) default NULL,
	tag_no varchar(10) default NULL,
	location varchar(20) default NULL,
	status bigint default 1,
	is4x4 tinyint default 0,
	pUse char default NULL,
	useCode bigint default 0
)
go

create table vehres
(
	res_ID int identity(1040, 1)
		constraint PK_vehres_res_ID
			primary key,
	emp_ID varchar(20) default NULL,
	veh_ID varchar(10) default N'' not null,
	res_start datetime2(0) default getdate() not null,
	res_end datetime2(0) default getdate() not null,
	budget varchar(12) default NULL,
	action int default 0 not null,
	passengers int default NULL,
	depcity varchar(30) default N'' not null,
	depcnty varchar(30) default N'' not null,
	depstate char(3) default N'' not null,
	destcity varchar(30) default N'' not null,
	destcnty varchar(30) default N'' not null,
	deststate char(3) default N'' not null,
	comment varchar(150) default N'' not null,
	entryDate datetime2(0) default NULL,
	mileage_end bigint default 0,
	constraint vehres$veh_ID
		unique (veh_ID, res_start, res_end)
)
go

create table vehrespassengers
(
	res_ID bigint default 0,
	passengerID varchar(10) default N'0'
)
go

create table voidleave
(
	leaveID varchar(40) default N'' not null,
	empID varchar(8) default N'' not null,
	type varchar(20) default NULL,
	totalHours float default NULL,
	submitDate datetime2(0) default NULL,
	startDate datetime2(0) default NULL,
	endDate datetime2(0) default NULL,
	status varchar(30) default NULL,
	startTime float default NULL,
	endTime float default NULL,
	hoursPerDay float default NULL,
	hoursFirstDay float default NULL,
	hoursLastDay float default NULL,
	approvedBy varchar(max),
	approvalDate datetime2(0) default NULL,
	reason varchar(100) default NULL,
	documentation varchar(100) default NULL,
	docRequired smallint default 0 not null,
	supervisor varchar(20) default NULL,
	comment varchar(100) default NULL,
	voidBy varchar(10) default N'' not null,
	time_stamp datetime default getdate() not null
)
go

create table vrp_inv_no_contact
(
	ID varchar(255) not null,
	name varchar(255) default NULL
)
go

