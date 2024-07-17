CREATE procedure dbo.Team_Delete
@idteam int
as
begin
	
	DECLARE @leagueid int, @id int, @teamno int
	select @leagueid=leagueid from team where id=@idteam

	if @leagueid is null
	  return 0  
    
	begin try
		begin transaction
		delete from team where id = @idteam

		select * into ##temp
		from 
		(select row_number() over
		(order by id)
			row_num,
				id
			FROM [Tournament].[dbo].[Team]
			WHERE LEAGUEID=@leagueid) AS X

		update team 
		set teamno = row_num
		from team 
		inner join ##temp on ##temp.id = team.id

	end try
	begin catch
		if OBJECT_ID('##temp') is not null
			drop table ##temp
		IF @@TRANCOUNT > 0  
			ROLLBACK transaction  
		return 0
	end catch
	IF @@TRANCOUNT > 0  
		commit transaction
	if OBJECT_ID('##temp') is not null
		drop table ##temp
	return 1
end