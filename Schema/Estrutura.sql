use Teste
go

 CREATE TABLE PLANO(
  CD_PLANO INT IDENTITY(1,1),
  NR_MINUTO DECIMAL(18,04) NOT NULL,
  FL_FRANQUIA_INTERNET BIT NOT NULL,
  VL_PLANO DECIMAL(18,04) NOT NULL,
  NR_TIPO_PLANO INT NOT NULL,
  NR_OPERADORA INT NOT NULL,
  NR_DDD INT NOT NULL,
  DT_INSERCAO DATETIME DEFAULT GETDATE() NOT NULL
  CONSTRAINT PK_PLANO PRIMARY KEY CLUSTERED(CD_PLANO)
)

CREATE TABLE DDD(
  NR_DDD INT IDENTITY(1,1),
  UF VARCHAR(100) NOT NULL,
  DT_INSERCAO DATETIME DEFAULT GETDATE() NOT NULL
  CONSTRAINT PK_DDD PRIMARY KEY CLUSTERED(NR_DDD)
)

CREATE TABLE OPERADORA(
  NR_OPERADORA INT IDENTITY(1,1),
  NM_OPERADORA VARCHAR(100) NOT NULL,
  DT_INSERCAO DATETIME DEFAULT GETDATE() NOT NULL
  CONSTRAINT PK_OPERADORA PRIMARY KEY CLUSTERED(NR_OPERADORA)
)

 ALTER TABLE PLANO
	ADD CONSTRAINT FK_PLANO_DDD FOREIGN KEY (NR_DDD)
	REFERENCES DDD (NR_DDD)

ALTER TABLE PLANO
	ADD CONSTRAINT FK_PLANO_OPERADORA FOREIGN KEY (NR_OPERADORA)
	REFERENCES OPERADORA (NR_OPERADORA)

INSERT INTO DDD (UF)
VALUES
('SP')
,('RJ')
,('GO')
,('RN')
,('PR')

INSERT INTO OPERADORA (NM_OPERADORA)
VALUES
('TIM')
,('VIVO')
,('OI')
,('NEXTEL')
,('CLARO')
,('CORREIOS')


INSERT INTO PLANO (NR_MINUTO, FL_FRANQUIA_INTERNET, VL_PLANO, NR_TIPO_PLANO, NR_OPERADORA, NR_DDD) 
VALUES 
(150,	1, 50,	1,	1, 1)
,(120,	1, 70,	1,	2, 5)
,(90,	1, 40,	1,	3, 4)
,(50,	1, 30,	1,	2, 5)
,(60,	1, 45,	1,	2, 4)
,(115,	1, 75,	1,	4, 3)
,(130,	1, 80,	1,	5, 3)
,(200,  1, 119, 1,	6, 2)
,(299,  1, 129, 1,	1, 1)
,(599,  1, 180, 1,	1, 1)
,(1000, 1, 399, 1,	1, 1)
