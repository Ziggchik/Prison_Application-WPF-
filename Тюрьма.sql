--drop database [Prison]

set ansi_padding on
go
set quoted_identifier on
go
set ansi_nulls on
go

create database [Prison]
go

use [Prison]
go

create table [dbo].[Type_of_Weapon]
 (
 [ID_Type_of_Weapon] [int] not null identity(1,1),
 [Name_of_Type_of_Weapon] [varchar] (30) not null default ('Отсутствует поставка'),
 constraint [PK_Type_of_Weapon] primary key clustered 
 ([ID_Type_of_Weapon] ASC) on [PRIMARY], )
 go 

create table [dbo].[Weapon]
(
	[ID_Weapon] [int] not null identity(1,1),
	[Name_Weapon] [varchar] (30) not null default ('Отсутствует поставка'),
	[Type_of_Weapon_ID] [int],
	[Guardians_ID][int],
	[Nakladnaya_ID][int],
	constraint [PK_Weapon] primary key clustered
	([ID_Weapon] ASC) on [PRIMARY],
	constraint [UQ_Name_of_Weapon] unique ([Name_Weapon]),
	constraint [FK_Type_of_Weapon_Weapon] foreign key ([Type_of_Weapon_ID])
	references [dbo].[Type_of_Weapon] ([ID_Type_of_Weapon]),
	constraint [FK_Guardians_Weapon] foreign key ([Guardians_ID])
    references [dbo].[Guardians] ([ID_Guardian]),
	constraint [FK_Nakladnaya_Weapon] foreign key ([Nakladnaya_ID])
	references [dbo].[Nakladnaya] ([ID_Nakladnaya])
)
go

select [Surname_Guardian]+' '+[Name_Guardian]+' '+[MiddleName_Guardian] from [dbo].[Guardians]

create table [dbo].[Guardians]
(
 [ID_Guardian] [int] not null identity(1,1),
 [Surname_Guardian] [varchar] (30) not null,
 [Name_Guardian] [varchar] (30) not null,
 [MiddleName_Guardian] [varchar] (30) not null,
 constraint [PK_Guardians] primary key clustered
([ID_Guardian] ASC) on [PRIMARY],
)
go

create table [dbo].[Prison_Block]
(
 [ID_Prison_Block] [int] not null identity(1,1),
 [Name_of_block] [varchar] (30) not null,
 constraint [PK_Block] primary key clustered
([ID_Prison_Block] ASC) on [PRIMARY],
)
 go

 alter table [dbo].[Prisoners] drop column [Prison_Block_ID]

create table [dbo].[Prisoners]
(
 [ID_Prisoner] [int] not null identity(1,1),
 [Surname_Prisoner] [varchar] (30) not null,
 [Name_Prisoner] [varchar] (30) not null,
 [MiddleName_Prisoner] [varchar] (30) not null,
 [Guardians_ID] [int],
 [Prison_Block_ID] [int],
 constraint [PK_Prisoners] primary key clustered
([ID_Prisoner] ASC) on [PRIMARY],
 constraint [FK_Guardian_Prisoners] foreign key ([Guardians_ID])
 references [dbo].[Guardians] ([ID_Guardian]),
 constraint [FK_Prison_Block_Prisoners] foreign key ([Prison_Block_ID])
 references [dbo].[Prison_Block] ([ID_Prison_Block])
 )
 go

create table [dbo].[Nakladnaya]
(
[ID_Nakladnaya] [int] not null identity(1,1),
[Nomer_Nakladnaya] [varchar](30) not null default ('Отсутствует поставка'),
constraint [PK_Nakladnaya] primary key clustered
([ID_Nakladnaya] ASC) on [PRIMARY],
) 
go

create procedure [dbo].[Prisoners_insert]
 @Surname_Prisoner [varchar] (30), @Name_Prisoner [varchar] (30),@MiddleName_Prisoner [varchar] (30),@Guardians_ID [int], @Prison_Block_ID [int]
as
   insert into [dbo].[Prisoners] ([Surname_Prisoner], [Name_Prisoner],[MiddleName_Prisoner],[Guardians_ID],[Prison_Block_ID])
   values (@Surname_Prisoner, @Name_Prisoner,@MiddleName_Prisoner,@Guardians_ID,@Prison_Block_ID)
go

create procedure [dbo].[Prisoners_update]
@ID_Prisoner [int], @Surname_Prisoner [varchar] (30), @Name_Prisoner [varchar] (30), @MiddleName_Prisoner [varchar] (30),@Guardians_ID [int], @Prison_Block_ID [int]
as
   update [dbo].[Prisoners] set
   [Surname_Prisoner] = @Surname_Prisoner,
   [Name_Prisoner] = @Name_Prisoner,
   [MiddleName_Prisoner] = @MiddleName_Prisoner,
   [Guardians_ID]=@Guardians_ID,
   [Prison_Block_ID]= @Prison_Block_ID
   where
        [ID_Prisoner] = @ID_Prisoner
go

create procedure [dbo].[Prisoners_delete]
@ID_Prisoner [int]
as
   delete from [dbo].[Prisoners]
   where
        [ID_Prisoner] = @ID_Prisoner
go

create procedure [dbo].[Guardians_insert]
 @Surname_Guardian [varchar] (30), @Name_Guardian [varchar] (30),@MiddleName_Guardian [varchar] (30)
as
   insert into [dbo].[Guardians] ([Surname_Guardian], [Name_Guardian],[MiddleName_Guardian])
   values (@Surname_Guardian, @Name_Guardian,@MiddleName_Guardian)
go

create procedure [dbo].[Guardians_update]
@ID_Guardian [int], @Surname_Guardian [varchar] (30), @Name_Guardian [varchar] (30), @MiddleName_Guardian [varchar] (30)
as
   update [dbo].[Guardians] set
   [Surname_Guardian] = @Surname_Guardian,
   [Name_Guardian] = @Name_Guardian,
   [MiddleName_Guardian] = @MiddleName_Guardian
   where
        [ID_Guardian] = @ID_Guardian
go

create procedure [dbo].[Guardians_delete]
@ID_Guardian [int]
as
   delete from [dbo].[Guardians]
   where
        [ID_Guardian] = @ID_Guardian
go

create procedure [dbo].[Weapon_insert]
 @Name_Weapon[varchar](30),@Type_of_Weapon_ID [int],@Guardians_ID[int],@Nakladnaya_ID[int]
as
   insert into [dbo].[Weapon] ([Name_Weapon],[Type_of_Weapon_ID],[Guardians_ID],[Nakladnaya_ID])
   values (@Name_Weapon,@Type_of_Weapon_ID,@Guardians_ID,@Nakladnaya_ID)
go

create procedure [dbo].[Weapon_update]
@ID_Weapon [int], @Name_Weapon [varchar] (30),@Type_of_Weapon_ID [int],@Guardians_ID[int],@Nakladnaya_ID[int]
as
   update [dbo].[Weapon] set
   [Name_Weapon] = @Name_Weapon,
   [Type_of_Weapon_ID]= @Type_of_Weapon_ID,
   [Guardians_ID]=@Guardians_ID,
   [Nakladnaya_ID]=@Nakladnaya_ID
   where
        [ID_Weapon] = @ID_Weapon
go

create procedure [dbo].[Weapon_delete]
@ID_Weapon [int]
as
   delete from [dbo].[Weapon]
   where
        [ID_Weapon] = @ID_Weapon
go

create procedure [dbo].[Type_of_Weapon_insert]
 @Name_of_Type_of_Weapon[varchar](30)
as
   insert into [dbo].[Type_of_Weapon] ([Name_of_Type_of_Weapon])
   values (@Name_of_Type_of_Weapon)
go

create procedure [dbo].[Type_of_Weapon_update]
@ID_Type_of_Weapon [int], @Name_of_Type_of_Weapon [varchar] (30)
as
   update [dbo].[Type_of_Weapon] set
   [Name_of_Type_of_Weapon] = @Name_of_Type_of_Weapon
   where
        [ID_Type_of_Weapon] = @ID_Type_of_Weapon
go

create procedure [dbo].[Type_of_Weapon_delete]
@ID_Type_of_Weapon [int]
as
   delete from [dbo].[Type_of_Weapon]
   where
        [ID_Type_of_Weapon] = @ID_Type_of_Weapon
go

create procedure [dbo].[Nakladnaya_insert]
 @Nomer_Nakladnaya[varchar](30)
as
   insert into [dbo].[Nakladnaya] ([Nomer_Nakladnaya])
   values (@Nomer_Nakladnaya)
go

create procedure [dbo].[Nakladnaya_update]
@ID_Nakladnaya [int], @Nomer_Nakladnaya [varchar] (30)
as
   update [dbo].[Nakladnaya] set
   [Nomer_Nakladnaya] = @Nomer_Nakladnaya
   where
        [ID_Nakladnaya] = @ID_Nakladnaya
go

create procedure [dbo].[Nakladnaya_delete]
@ID_Nakladnaya [int]
as
   delete from [dbo].[Nakladnaya]
   where
        [ID_Nakladnaya] = @ID_Nakladnaya
go

create procedure [dbo].[Prison_Block_insert]
 @Name_of_block[varchar](30)
as
   insert into [dbo].[Prison_Block] ([Name_of_block])
   values (@Name_of_block)
go

create procedure [dbo].[Prison_Block_update]
@ID_Prison_Block [int], @Name_of_block[varchar] (30)
as
   update [dbo].[Prison_Block] set
   [Name_of_block] = @Name_of_block
   where
        [ID_Prison_Block] = @ID_Prison_Block
go

create procedure [dbo].[Prison_Block_delete]
@ID_Prison_Block [int]
as
   delete from [dbo].[Prison_Block]
   where
        [ID_Prison_Block] = @ID_Prison_Block
go
