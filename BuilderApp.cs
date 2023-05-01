

public static class BuilderApp{

    public static WebApplication BuildApp(WebApplication app){

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();


        return app;
    }

}