alter table fileInfo
   drop foreign key F_Reference_1;

drop table download;

drop table fileInfo;

drop table userInfo;

--==============================================================
-- Table: download
--==============================================================
create table download
(
   downloadId         BIGINT                 not null,
   name               VARCHAR(50),
   fileName           VARCHAR(50),
   filePath           VARCHAR(200),
   priority           SMALLINT,
   constraint P_Key_1 primary key (downloadId)
);

--==============================================================
-- Table: fileInfo
--==============================================================
create table fileInfo
(
   fileId             BIGINT                 not null,
   userId             VARCHAR(20),
   fileName           VARCHAR(50),
   newFileName        VARCHAR(50),
   filePath           VARCHAR(200),
   uploadDate         DATE,
   transDate          DATE,
   status             SMALLINT,
   allNum             INT,
   successNum         INT,
   errorNum           INT,
   logPath            VARCHAR(200),
   logFileName        VARCHAR(50),
   constraint P_Key_1 primary key (fileId)
);

--==============================================================
-- Table: userInfo
--==============================================================
create table userInfo
(
   userId             VARCHAR(20)            not null,
   password           VARCHAR(50),
   userName           VARCHAR(20),
   uid                VARCHAR(20),
   dept               VARCHAR(50),
   email              VARCHAR(50),
   tel                VARCHAR(20),
   note               VARCHAR(200),
   role               SMALLINT,
   priority           SMALLINT,
   status             SMALLINT,
   pwdExpireDate      DATE,
   constraint P_Key_1 primary key (userId)
);

alter table fileInfo
   add constraint F_Reference_1 foreign key (userId)
      references userInfo (userId)
      on delete restrict on update restrict;
